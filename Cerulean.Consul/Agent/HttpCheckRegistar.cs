using System;
using Newtonsoft.Json;

namespace Cerulean.Consul.Agent
{
    public sealed class HttpCheckRegistar : CheckRegistrar
    {
        [JsonProperty("HTTP", NullValueHandling = NullValueHandling.Ignore)]
        public string Http { get; set; }

        public string Interval { get; set; }
        public string Timeout { get; set; }
    }
}