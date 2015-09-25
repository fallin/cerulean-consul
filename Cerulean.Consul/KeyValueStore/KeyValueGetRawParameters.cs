using System;

namespace Cerulean.Consul.KeyValueStore
{
    public sealed class KeyValueGetRawParameters : KeyValueParameters
    {
        public KeyValueGetRawParameters()
        {
            Add("raw");
        }

        public void Index(long index)
        {
            Add("index", index);
        }
    }
}