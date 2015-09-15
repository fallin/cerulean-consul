using System;
using System.Diagnostics.CodeAnalysis;

namespace Cerulean.Consul.Agent
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AgentAddresses
    {
        public string DNS { get; set; }
        public string HTTP { get; set; }
        public string HTTPS { get; set; }
        public string RPC { get; set; }
    }
}