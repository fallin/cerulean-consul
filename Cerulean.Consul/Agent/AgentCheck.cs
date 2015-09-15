using System;
using Cerulean.Consul.Catalog;

namespace Cerulean.Consul.Agent
{
    public class AgentCheck
    {
        public string Node { get; set; }
        public string CheckID { get; set; }
        public string Name { get; set; }
        public CheckStatus Status { get; set; }
        public string Notes { get; set; }
        public string Output { get; set; }
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
    }
}