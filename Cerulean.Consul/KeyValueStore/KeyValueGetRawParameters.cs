using System;

namespace Cerulean.Consul.KeyValueStore
{
    public class KeyValueGetRawParameters : KeyValueGetParameters
    {
        public KeyValueGetRawParameters()
        {
            Add("raw");
        }
    }
}