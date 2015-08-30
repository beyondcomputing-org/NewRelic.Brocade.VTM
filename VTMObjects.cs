using System;
using System.Collections.Generic;

namespace Org.BeyondComputing.NewRelic.Brocade.VTM
{
    public class Child
    {
        public string name { get; set; }
        public string href { get; set; }
    }

    public class Children
    {
        public List<Child> children { get; set; }
    }

    public class VirtualServerStatistics
    {
        public UInt64 bytes_in { get; set; }
        public uint bytes_in_hi { get; set; }
        public uint bytes_in_lo { get; set; }
        public UInt64 bytes_out { get; set; }
        public uint bytes_out_hi { get; set; }
        public uint bytes_out_lo { get; set; }
        public uint cert_status_requests { get; set; }
        public uint cert_status_responses { get; set; }
        public uint connect_timed_out { get; set; }
        public uint connection_errors { get; set; }
        public uint connection_failures { get; set; }
        public uint current_conn { get; set; }
        public uint data_timed_out { get; set; }
        public uint direct_replies { get; set; }
        public uint discard { get; set; }
        public uint gzip { get; set; }
        public UInt64 gzip_bytes_saved { get; set; }
        public uint gzip_bytes_saved_hi { get; set; }
        public uint gzip_bytes_saved_lo { get; set; }
        public uint http_cache_hit_rate { get; set; }
        public uint http_cache_hits { get; set; }
        public uint http_cache_lookups { get; set; }
        public uint http_rewrite_cookie { get; set; }
        public uint http_rewrite_location { get; set; }
        public uint keepalive_timed_out { get; set; }
        public uint max_conn { get; set; }
        public uint max_duration_timed_out { get; set; }
        public uint port { get; set; }
        public uint processing_timed_out { get; set; }
        public string protocol { get; set; }
        public uint sip_rejected_requests { get; set; }
        public uint sip_total_calls { get; set; }
        public uint total_conn { get; set; }
        public uint total_dgram { get; set; }
        public uint udp_timed_out { get; set; }
    }

    public class VirtualServerStatObject
    {
        public VirtualServerStatistics statistics { get; set; }
    }

    public class NodeStatistics
    {
        public uint bytes_from_node_hi { get; set; }
        public uint bytes_from_node_lo { get; set; }
        public uint bytes_to_node_hi { get; set; }
        public uint bytes_to_node_lo { get; set; }
        public uint current_conn { get; set; }
        public uint current_requests { get; set; }
        public uint errors { get; set; }
        public uint failures { get; set; }
        public uint new_conn { get; set; }
        public uint pooled_conn { get; set; }
        public uint port { get; set; }
        public uint response_max { get; set; }
        public uint response_mean { get; set; }
        public uint response_min { get; set; }
        public string state { get; set; }
        public uint total_conn { get; set; }
    }

    public class NodeStatObject
    {
        public NodeStatistics statistics { get; set; }
    }

    public class PoolStatistics
    {
        public string algorithm { get; set; }
        public UInt64 bytes_in { get; set; }
        public uint bytes_in_hi { get; set; }
        public uint bytes_in_lo { get; set; }
        public UInt64 bytes_out { get; set; }
        public uint bytes_out_hi { get; set; }
        public uint bytes_out_lo { get; set; }
        public uint conns_queued { get; set; }
        public uint disabled { get; set; }
        public uint draining { get; set; }
        public uint max_queue_time { get; set; }
        public uint mean_queue_time { get; set; }
        public uint min_queue_time { get; set; }
        public uint nodes { get; set; }
        public string persistence { get; set; }
        public uint queue_timeouts { get; set; }
        public uint session_migrated { get; set; }
        public string state { get; set; }
        public uint total_conn { get; set; }
    }

    public class PoolStatObject
    {
        public PoolStatistics statistics { get; set; }
    }
}
