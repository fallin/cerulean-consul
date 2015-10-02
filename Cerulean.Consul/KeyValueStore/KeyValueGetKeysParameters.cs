using System;

namespace Cerulean.Consul.KeyValueStore
{
    public class KeyValueGetKeysParameters : KeyValueGetParameters
    {
        public KeyValueGetKeysParameters()
        {
            Add("keys");
        }

        [InitializeFromGlobal("separator")]
        public void Separator(char separator)
        {
            Add("separator", separator);
        }
    }
}