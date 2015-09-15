using System;
using Newtonsoft.Json;

namespace Cerulean.Consul.Catalog
{
    public class CheckDescriptor
    {
        public string Node { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CheckID { get; set; }

        public string Name { get; set; }
        public string Notes { get; set; }
        public CheckStatus Status { get; set; }
        public string ServiceID { get; set; }
    }
}