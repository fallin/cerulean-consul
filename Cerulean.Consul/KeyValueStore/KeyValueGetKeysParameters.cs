using System;

namespace Cerulean.Consul.KeyValueStore
{
    public class KeyValueGetKeysParameters : KeyValueGetParameters
    {
        public KeyValueGetKeysParameters()
        {
            Add("keys");
        }

        [InitializeFromDefault("separator")]
        public void Separator(char separator)
        {
            Add("separator", separator);
        }
    }
}