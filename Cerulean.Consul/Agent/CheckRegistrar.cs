using System;
using Newtonsoft.Json;

namespace Cerulean.Consul.Agent
{
    public abstract class CheckRegistrar
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceID { get; set; }
    }
}