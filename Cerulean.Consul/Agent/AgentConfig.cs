using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Cerulean.Consul.Agent
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AgentConfig
    {
        public bool Bootstrap { get; set; }
        public int BootstrapExpect { get; set; }
        public bool Server { get; set; }
        public string Datacenter { get; set; }
        public string DataDir { get; set; }

        public string DNSRecursor { get; set; }
        public string[] DNSRecursors { get; set; }
        public AgentDnsConfig DNSConfig { get; set; }
        public string Domain { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AgentLogLevel LogLevel { get; set; }
        public string NodeName { get; set; }
        public string ClientAddr { get; set; }
        public string BindAddr { get; set; }
        public string AdvertiseAddr { get; set; }
        public AgentPortConfig Ports { get; set; }
        public AgentAddresses Addresses { get; set; }
        public bool LeaveOnTerm { get; set; }
        public bool SkipLeaveOnInt { get; set; }
        public string StatsiteAddr { get; set; }
        public string StatsitePrefix { get; set; }
        public string StatsdAddr { get; set; }
        public int Protocol { get; set; }
        public bool EnableDebug { get; set; }
        public bool VerifyIncoming { get; set; }
        public bool VerifyOutgoing { get; set; }
        public bool VerifyServerHostname { get; set; }
        public string CAFile { get; set; }
        public string CertFile { get; set; }
        public string KeyFile { get; set; }
        public string ServerName { get; set; }
        public string[] StartJoin { get; set; }
        public string[] StartJoinWan { get; set; }
        public string[] RetryJoin { get; set; }
        public int RetryMaxAttempts { get; set; }
        public string RetryIntervalRaw { get; set; }
        public string[] RetryJoinWan { get; set; }
        public int RetryMaxAttemptsWan { get; set; }
        public string RetryIntervalWanRaw { get; set; }
        public string UiDir { get; set; }
        public string PidFile { get; set; }
        public bool EnableSyslog { get; set; }
        public string SyslogFacility { get; set; }
        public bool RejoinAfterLeave { get; set; }
        public long CheckUpdateInterval { get; set; }
        public string ACLDatacenter { get; set; }
        public long ACLTTL { get; set; }
        public string ACLTTLRaw { get; set; }
        public string ACLDefaultPolicy { get; set; }
        public string ACLDownPolicy { get; set; }
        public JArray Watches { get; set; }
        public bool DisableRemoteExec { get; set; }
        public bool DisableUpdateCheck { get; set; }
        public bool DisableAnonymousSignature { get; set; }
        public IDictionary<string, string> HTTPAPIResponseHeaders { get; set; }
        public string AtlasInfrastructure { get; set; }
        public bool AtlasJoin { get; set; }
        public string Revision { get; set; }
        public string Version { get; set; }
        public string VersionPrerelease { get; set; }
        public IDictionary<string, string> UnixSockets { get; set; }
        public long SessionTTLMin { get; set; }
        public string SessionTTLMinRaw { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalProperties { get; set; } 
    }
}