using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cerulean.Consul.Agent
{
    public class AgentMember
    {
        public AgentMember()
        {
            Tags = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public string Name { get; set; }

        [JsonProperty("Addr")]
        public string Address { get; set; }

        public int Port { get; set; }
        public IDictionary<string,string> Tags { get; private set; }

        public int Status { get; set; }
        public int ProtocolMin { get; set; }
        public int ProtocolMax { get; set; }
        public int ProtocolCur { get; set; }
        public int DelegateMin { get; set; }
        public int DelegateMax { get; set; }
        public int DelegateCur { get; set; }
    }
}