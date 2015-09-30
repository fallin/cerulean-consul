using System;
using Newtonsoft.Json;

namespace Cerulean.Consul.Agent
{
    public sealed class TtlCheckRegistrar : CheckRegistrar
    {
        [JsonProperty("TTL", NullValueHandling = NullValueHandling.Ignore)]
        public string Ttl { get; set; }
    }
}