using System;

namespace Cerulean.Consul.KeyValueStore
{
    public abstract class KeyValueParameters : Parameters
    {
        [InitializeFromDefault("dc")]
        public void Datacenter(string dc)
        {
            if (!string.IsNullOrEmpty(dc))
            {
                Add("dc", dc);
            }
        }

        [InitializeFromDefault("token")]
        public void AclToken(Guid token)
        {
            Add("token", token.ToString("D"));
        }
    }
}