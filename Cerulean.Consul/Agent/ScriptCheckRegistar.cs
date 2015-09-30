using System;
using Newtonsoft.Json;

namespace Cerulean.Consul.Agent
{
    public sealed class ScriptCheckRegistar : CheckRegistrar
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Script { get; set; }

        public string Interval { get; set; }
    }
}