using System;

namespace Cerulean.Consul.Agent
{
    public class LocalAgent
    {
        public AgentConfig Config { get; set; }
        public AgentMember Member { get; set; }
    }
}