using System;
using System.Text;

namespace Wildflower.Consul
{
    public class KeyValueResource
    {
        public int CreateIndex { get; set; }
        public int ModifyIndex { get; set; }
        public int LockIndex { get; set; }
        public string Key { get; set; }
        public int Flags { get; set; }
        public string Value { get; set; }

        public string DecodeValue()
        {
            byte[] bytes = Convert.FromBase64String(Value);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}