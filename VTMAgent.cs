using System;
using System.Collections.Generic;
using NewRelic.Platform.Sdk;
using NewRelic.Platform.Sdk.Utils;
using NewRelic.Platform.Sdk.Processors;

namespace Org.BeyondComputing.NewRelic.Brocade.VTM
{
    class VTMAgent : Agent
    {
        public override string Guid
        {
            get
            {
                return "org.beyondcomputing.newrelic.brocade.vtm";
            }
        }

        public override string Version
        {
            get
            {
                return "1.4.0";
            }
        }
    
        private string name;
        private VTM VTM;
        private string APIVersion;

        // Create Dictionary of EpochProcessors to track rate over time for unknown number of items
        private Dictionary<string,IProcessor> processors = new Dictionary<string,IProcessor>();

        private Logger log = Logger.GetLogger(typeof(VTMAgent).Name);

        public VTMAgent(string name, string host, int port, string username, string password, string APIVersion)
        {
            this.name = name;
            this.VTM = new VTM(host, port, username, password);
            this.APIVersion = APIVersion;
        }

        /// <summary>
        /// Returns a human-readable string to differentiate different hosts/entities in the New Relic UI
        /// </summary>
        public override string GetAgentName()
        {
            return this.name;
        }

        /// <summary>
        // This is where logic for fetching and reporting metrics should exist.  
        // Call off to a REST head, SQL DB, virtually anything you can programmatically 
        // get metrics from and then call ReportMetric.
        /// </summary>
        public override void PollCycle()
        {
            PollGlobal();
            PollVirtualServer();
            PollNodes();
            PollPool();
        }

        private void PollGlobal()
        {
            try
            {
                // Setup new EpochProcessors for Global Stats
                // Check for existance of first Dictionary Item for the Global Stats
                if (!processors.ContainsKey("global_bytes_in"))
                {
                    processors.Add("global_bytes_in", new EpochProcessor());
                    processors.Add("global_bytes_out", new EpochProcessor());
                }

                // Get Global Statistics and report to New Relic
                GlobalStatistics globalStats = VTM.fetchVTMObject<GlobalStatObject>("/api/tm/3.3/status/local_tm/statistics/globals").statistics;

                // Calculate System Memory Percent Used
                float systemMemoryPercentUsed = (((float)globalStats.sys_mem_in_use) / (globalStats.sys_mem_total))*100;

                ReportMetric("global/throughput/Received", "mebibits/second", processors["global_bytes_in"].Process(globalStats.total_bytes_in) / 131072);
                ReportMetric("global/throughput/Transmitted", "mebibits/second", processors["global_bytes_out"].Process(globalStats.total_bytes_out) / 131072);
                ReportMetric("global/current_conn", "connections", globalStats.total_current_conn);
                ReportMetric("global/sys_mem_used", "percent",systemMemoryPercentUsed);
                ReportMetric("global/sys_cpu_busy_percent", "percent", globalStats.sys_cpu_busy_percent);
            }
            catch
            {
                log.Error("Unable to fetch Global information from the Virtual Traffic Manager '{0}'", this.name);
            }
        }

        private void PollPool()
        {
            try
            {
                Children pools = VTM.fetchVTMObject<Children>("/api/tm/3.3/status/local_tm/statistics/pools");

                foreach (var pool in pools.children)
                {
                    // Setup new EpochProcessors for each Pool
                    // Check for existance of first Dictionary Item for the Pool
                    if (!processors.ContainsKey("pool_" + pool.name + "_bytes_in"))
                    {
                        processors.Add("pool_" + pool.name + "_bytes_in", new EpochProcessor());
                        processors.Add("pool_" + pool.name + "_bytes_out", new EpochProcessor());
                    }

                    // Get Pool Statistics and report to New Relic
                    PoolStatistics poolStats = VTM.fetchVTMObject<PoolStatObject>(pool.href).statistics;
                    ReportMetric("pools/" + pool.name + "/throughput/Received", "mebibits/second", processors["pool_" + pool.name + "_bytes_in"].Process(poolStats.bytes_in) / 131072);
                    ReportMetric("pools/" + pool.name + "/throughput/Transmitted", "mebibits/second", processors["pool_" + pool.name + "_bytes_out"].Process(poolStats.bytes_out) / 131072);
                    ReportMetric("pools/" + pool.name + "/nodes", "nodes", poolStats.nodes);
                    ReportMetric("pools/" + pool.name + "/disabled", "nodes", poolStats.disabled);
                    ReportMetric("pools/" + pool.name + "/draining", "nodes", poolStats.draining);
                }
            }
            catch
            {
                log.Error("Unable to fetch Pool information from the Virtual Traffic Manager '{0}'", this.name);
            }
        }

        private void PollNodes()
        {
            Children nodes = VTM.fetchVTMObject<Children>("/api/tm/3.3/status/local_tm/statistics/nodes/node");

            foreach (var node in nodes.children)
            {
                try
                {                
                    // Setup new EpochProcessors for each Node
                    // Check for existance of first Dictionary Item for the Node
                    if (!processors.ContainsKey("node_" + node.name + "_errors"))
                    {
                        processors.Add("node_" + node.name + "_errors", new EpochProcessor());
                        processors.Add("node_" + node.name + "_failures", new EpochProcessor());
                    }

                    // Get Node Statistics and report to New Relic
                    NodeStatistics nodeStats = VTM.fetchVTMObject<NodeStatObject>(node.href).statistics;
                    ReportMetric("nodes/" + node.name + "/errors", "sec", processors["node_" + node.name + "_errors"].Process(nodeStats.errors));
                    ReportMetric("nodes/" + node.name + "/failures", "sec", processors["node_" + node.name + "_failures"].Process(nodeStats.failures));
                    ReportMetric("nodes/" + node.name + "/current_conn", "connections", nodeStats.current_conn);
                    ReportMetric("nodes/" + node.name + "/current_requests", "requests", nodeStats.current_requests);
                }
                catch
                {
                    log.Error("Unable to fetch Node information from the Virtual Traffic Manager '{0}' for Node: '{1}'", this.name, node.name);
                }
            }
        }

        private void PollVirtualServer()
        {
            try
            {
                Children virtualServers = VTM.fetchVTMObject<Children>("/api/tm/3.3/status/local_tm/statistics/virtual_servers");

                foreach (var virtualServer in virtualServers.children)
                {
                    // Setup new EpochProcessors for each Virtual Server
                    // Check for existance of first Dictionary Item for the Virtual Server
                    if (!processors.ContainsKey("vs_" + virtualServer.name + "_bytes_in"))
                    {
                        processors.Add("vs_" + virtualServer.name + "_bytes_in", new EpochProcessor());
                        processors.Add("vs_" + virtualServer.name + "_bytes_out", new EpochProcessor());
                    }

                    // Get Virtual Server Statistics and report to New Relic
                    VirtualServerStatistics virtualServerStats = VTM.fetchVTMObject<VirtualServerStatObject>(virtualServer.href).statistics;
                    ReportMetric("virtual_servers/" + virtualServer.name + "/throughput/Received", "mebibits/second", processors["vs_" + virtualServer.name + "_bytes_in"].Process(virtualServerStats.bytes_in) / 131072);
                    ReportMetric("virtual_servers/" + virtualServer.name + "/throughput/Transmitted", "mebibits/second", processors["vs_" + virtualServer.name + "_bytes_out"].Process(virtualServerStats.bytes_out) / 131072);
                    ReportMetric("virtual_servers/" + virtualServer.name + "/current_conn", "connections", virtualServerStats.current_conn);
                }
            }
            catch
            {
                log.Error("Unable to fetch Virtual Server information from the Virtual Traffic Manager '{0}'", this.name);
            }
        }

    }
}
