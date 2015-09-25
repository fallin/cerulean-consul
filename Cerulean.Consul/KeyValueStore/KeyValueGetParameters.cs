using System;

namespace Cerulean.Consul.KeyValueStore
{
    public sealed class KeyValueGetParameters : KeyValueParameters
    {
        public void Recurse()
        {
            Add("recurse");
        }

        public void Index(long index)
        {
            Add("index", index);
        }
    }
}