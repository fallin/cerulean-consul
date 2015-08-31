using System;
using System.Text;

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
        public Guid Session { get; set; }

        public string DecodeValue()
        {
            byte[] bytes = Convert.FromBase64String(Value);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}