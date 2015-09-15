using System;
using System.Diagnostics.CodeAnalysis;

namespace Cerulean.Consul.Agent
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AgentPortConfig
    {
        public int DNS { get; set; }
        public int HTTP { get; set; }
        public int HTTPS { get; set; }
        public int RPC { get; set; }
        public int SerfLan { get; set; }
        public int SerfWan { get; set; }
        public int Server { get; set; }
    }
}