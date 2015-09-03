using System;
using System.Text;
using Newtonsoft.Json;

namespace Cerulean.Consul
{
    public class KeyValue
    {
        public int CreateIndex { get; set; }
        public int ModifyIndex { get; set; }
        public int LockIndex { get; set; }
        public string Key { get; set; }
        public uint Flags { get; set; }
        public string Value { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Session { get; set; }

        public string DecodeValue()
        {
            byte[] bytes = Convert.FromBase64String(Value);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}