using System;

namespace Cerulean.Consul.KeyValueStore
{
    public class KeyValueDelParameters : KeyValueParameters
    {
        public void Recurse()
        {
            Add("recurse");
        }

        public void CheckAndSet(long index)
        {
            Add("cas", index);
        }
    }
}