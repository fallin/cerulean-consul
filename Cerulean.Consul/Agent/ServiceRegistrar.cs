using System;
using Cerulean.Consul.Catalog;
using Newtonsoft.Json;

namespace Cerulean.Consul.Agent
{
    public class ServiceRegistrar
    {
        public ServiceRegistrar()
        {
            Tags = new ServiceTags();
        }

        public string ID { get; set; }
        public string Name { get; set; }

        public ServiceTags Tags { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CheckRegistrar Check { get; set; }
    }
}