using System;
using Newtonsoft.Json;

namespace Cerulean.Consul.Catalog
{
    public class CatalogDescriptor
    {
        [JsonProperty("Datacenter", NullValueHandling = NullValueHandling.Ignore)]
        public string DataCenter { get; set; }
        public string Node { get; set; }
        public string Address { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ServiceDescriptor Service { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CheckDescriptor Check { get; set; }
    }
}