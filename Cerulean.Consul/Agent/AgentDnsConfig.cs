using System;
using System.Diagnostics.CodeAnalysis;

namespace Cerulean.Consul.Agent
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AgentDnsConfig
    {
        public int NodeTTL { get; set; }
        public int? ServiceTTL { get; set; }
        public bool AllowStale { get; set; }
        public bool EnableTruncate { get; set; }
        public long MaxStale { get; set; }
        public bool OnlyPassing { get; set; }
    }
}