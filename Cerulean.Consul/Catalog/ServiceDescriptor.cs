using System;
using Newtonsoft.Json;

namespace Cerulean.Consul.Catalog
{
    public class ServiceDescriptor
    {
        public ServiceDescriptor()
        {
            Tags = new ServiceTags();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }

        public string Service { get; set; }

        public ServiceTags Tags { get; private set; }

        public string Address { get; set; }
        public int Port { get; set; }
    }
}