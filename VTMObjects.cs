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

    public class GlobalStatistics
    {
        public uint data_entries { get; set; }
        public uint data_memory_usage { get; set; }
        public uint events_seen { get; set; }
        public uint hourly_peak_bytes_in_per_second { get; set; }
        public uint hourly_peak_bytes_out_per_second { get; set; }
        public uint hourly_peak_requests_per_second { get; set; }
        public uint hourly_peak_ssl_connections_per_second { get; set; }
        public uint num_idle_connections { get; set; }
        public uint number_child_processes { get; set; }
        public uint number_dnsa_cache_hits { get; set; }
        public uint number_dnsa_requests { get; set; }
        public uint number_dnsptr_cache_hits { get; set; }
        public uint number_dnsptr_requests { get; set; }
        public uint number_snmp_bad_requests { get; set; }
        public uint number_snmp_get_bulk_requests { get; set; }
        public uint number_snmp_get_next_requests { get; set; }
        public uint number_snmp_get_requests { get; set; }
        public uint number_snmp_unauthorised_requests { get; set; }
        public uint ssl_cipher_3des_decrypts { get; set; }
        public uint ssl_cipher_3des_encrypts { get; set; }
        public uint ssl_cipher_aes_decrypts { get; set; }
        public uint ssl_cipher_aes_encrypts { get; set; }
        public uint ssl_cipher_aes_gcm_decrypts { get; set; }
        public uint ssl_cipher_aes_gcm_encrypts { get; set; }
        public uint ssl_cipher_decrypts { get; set; }
        public uint ssl_cipher_des_decrypts { get; set; }
        public uint ssl_cipher_des_encrypts { get; set; }
        public uint ssl_cipher_dh_agreements { get; set; }
        public uint ssl_cipher_dh_generates { get; set; }
        public uint ssl_cipher_dsa_signs { get; set; }
        public uint ssl_cipher_dsa_verifies { get; set; }
        public uint ssl_cipher_encrypts { get; set; }
        public uint ssl_cipher_rc4_decrypts { get; set; }
        public uint ssl_cipher_rc4_encrypts { get; set; }
        public uint ssl_cipher_rsa_decrypts { get; set; }
        public uint ssl_cipher_rsa_decrypts_external { get; set; }
        public uint ssl_cipher_rsa_encrypts { get; set; }
        public uint ssl_cipher_rsa_encrypts_external { get; set; }
        public uint ssl_client_cert_expired { get; set; }
        public uint ssl_client_cert_invalid { get; set; }
        public uint ssl_client_cert_not_sent { get; set; }
        public uint ssl_client_cert_revoked { get; set; }
        public uint ssl_connections { get; set; }
        public uint ssl_handshake_sslv2 { get; set; }
        public uint ssl_handshake_sslv3 { get; set; }
        public uint ssl_handshake_t_l_sv1 { get; set; }
        public uint ssl_handshake_t_l_sv11 { get; set; }
        public uint ssl_handshake_t_l_sv12 { get; set; }
        public uint ssl_session_id_disk_cache_hit { get; set; }
        public uint ssl_session_id_disk_cache_miss { get; set; }
        public uint ssl_session_id_mem_cache_hit { get; set; }
        public uint ssl_session_id_mem_cache_miss { get; set; }
        public uint sys_cpu_busy_percent { get; set; }
        public uint sys_cpu_idle_percent { get; set; }
        public uint sys_cpu_system_busy_percent { get; set; }
        public uint sys_cpu_user_busy_percent { get; set; }
        public uint sys_fds_free { get; set; }
        public uint sys_mem_buffered { get; set; }
        public uint sys_mem_free { get; set; }
        public uint sys_mem_in_use { get; set; }
        public uint sys_mem_swap_total { get; set; }
        public uint sys_mem_swapped { get; set; }
        public uint sys_mem_total { get; set; }
        public uint time_last_config_update { get; set; }
        public uint total_backend_server_errors { get; set; }
        public uint total_bad_dns_packets { get; set; }
        public UInt64 total_bytes_in { get; set; }
        public uint total_bytes_in_hi { get; set; }
        public uint total_bytes_in_lo { get; set; }
        public UInt64 total_bytes_out { get; set; }
        public uint total_bytes_out_hi { get; set; }
        public uint total_bytes_out_lo { get; set; }
        public uint total_conn { get; set; }
        public uint total_current_conn { get; set; }
        public uint total_dns_responses { get; set; }
        public uint total_requests { get; set; }
        public uint total_transactions { get; set; }
        public uint up_time { get; set; }
    }

    public class GlobalStatObject
    {
        public GlobalStatistics statistics { get; set; }
    }
}
