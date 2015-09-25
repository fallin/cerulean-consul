using System;

namespace Cerulean.Consul.KeyValueStore
{
    public class KeyValueGetKeysParameters : KeyValueGetParameters
    {
        public KeyValueGetKeysParameters()
        {
            Add("keys");
        }

        public void Separator(char separator)
        {
            Add("separator", separator);
        }
    }
}