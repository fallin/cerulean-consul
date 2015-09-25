using System;

namespace Cerulean.Consul.KeyValueStore
{
    public sealed class KeyValueGetKeysParameters : KeyValueParameters
    {
        public KeyValueGetKeysParameters()
        {
            Add("keys");
        }

        public void Index(long index)
        {
            Add("index", index);
        }

        public void Separator(char separator)
        {
            Add("separator", separator);
        }
    }
}