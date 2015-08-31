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
                return "1.1.0";
            }
        }
    
        private string name;
        private VTM VTM;

        // Create Dictionary of EpochProcessors to track rate over time for unknown number of items
        private Dictionary<string,IProcessor> processors = new Dictionary<string,IProcessor>();

        private Logger log = Logger.GetLogger(typeof(VTMAgent).Name);

        public VTMAgent(string name, string host, int port, string username, string password)
        {
            this.name = name;
            this.VTM = new VTM(host, port, username, password);
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
                GlobalStatistics globalStats = VTM.fetchGlobalStats();

                // Calculate System Memory Percent Used
                float systemMemoryPercentUsed = (((float)globalStats.sys_mem_in_use) / (globalStats.sys_mem_total))*100;

                ReportMetric("global/bytes_in", "bytes/sec", processors["global_bytes_in"].Process(globalStats.total_bytes_in));
                ReportMetric("global/bytes_out", "bytes/sec", processors["global_bytes_out"].Process(globalStats.total_bytes_out));
                ReportMetric("global/current_conn", "connections", globalStats.total_current_conn);
                ReportMetric("global/sys_mem_used", "percent",systemMemoryPercentUsed);
                ReportMetric("global/sys_cpu_busy_percent", "percent", globalStats.sys_cpu_busy_percent);
            }
            catch
            {
                log.Error("Unable to fetch Virtual Server information from the Virtual Traffic Manager '{0}'", this.name);
            }
        }

        private void PollPool()
        {
            try
            {
                Children pools = VTM.fetchPools();

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
                    PoolStatistics poolStats = VTM.fetchPoolStats(pool.href);
                    ReportMetric("pools/" + pool.name + "/bytes_in", "bytes/sec", processors["pool_" + pool.name + "_bytes_in"].Process(poolStats.bytes_in));
                    ReportMetric("pools/" + pool.name + "/bytes_out", "bytes/sec", processors["pool_" + pool.name + "_bytes_out"].Process(poolStats.bytes_out));
                    ReportMetric("pools/" + pool.name + "/nodes", "nodes", poolStats.nodes);
                }
            }
            catch
            {
                log.Error("Unable to fetch Virtual Server information from the Virtual Traffic Manager '{0}'", this.name);
            }
        }

        private void PollNodes()
        {
            try
            {
                Children nodes = VTM.fetchNodes();

                foreach (var node in nodes.children)
                {
                    // Setup new EpochProcessors for each Node
                    // Check for existance of first Dictionary Item for the Node
                    if (!processors.ContainsKey("node_" + node.name + "_errors"))
                    {
                        processors.Add("node_" + node.name + "_errors", new EpochProcessor());
                        processors.Add("node_" + node.name + "_failures", new EpochProcessor());
                    }

                    // Get Node Statistics and report to New Relic
                    NodeStatistics nodeStats = VTM.fetchNodeStats(node.href);
                    ReportMetric("nodes/" + node.name + "/errors", "sec", processors["node_" + node.name + "_errors"].Process(nodeStats.errors));
                    ReportMetric("nodes/" + node.name + "/failures", "sec", processors["node_" + node.name + "_failures"].Process(nodeStats.failures));
                    ReportMetric("nodes/" + node.name + "/current_conn", "connections", nodeStats.current_conn);
                    ReportMetric("nodes/" + node.name + "/current_requests", "requests", nodeStats.current_requests);
                }
            }
            catch
            {
                log.Error("Unable to fetch Node information from the Virtual Traffic Manager '{0}'", this.name);
            }
        }

        private void PollVirtualServer()
        {
            try
            {
                Children virtualServers = VTM.fetchVirtualServers();

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
                    VirtualServerStatistics virtualServerStats = VTM.fetchVirtualServerStats(virtualServer.href);
                    ReportMetric("virtual_servers/" + virtualServer.name + "/bytes_in", "bytes/sec", processors["vs_" + virtualServer.name + "_bytes_in"].Process(virtualServerStats.bytes_in));
                    ReportMetric("virtual_servers/" + virtualServer.name + "/bytes_out", "bytes/sec", processors["vs_" + virtualServer.name + "_bytes_out"].Process(virtualServerStats.bytes_out));
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
