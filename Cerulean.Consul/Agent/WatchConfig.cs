using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cerulean.Consul.Agent
{
    public class WatchConfig
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public WatchType Type { get; set; }
        public string Handler { get; set; }

        public string HttpAddr { get; set; }
        public string Datacenter { get; set; }
        public string Token { get; set; }
        public string Key { get; set; }
    }
}