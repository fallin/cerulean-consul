using System;

namespace Cerulean.Consul.KeyValueStore
{
    public abstract class KeyValueParameters : Parameters
    {
        [InitializeFromGlobal("dc")]
        public void Datacenter(string dc)
        {
            if (!string.IsNullOrEmpty(dc))
            {
                Add("dc", dc);
            }
        }

        [InitializeFromGlobal("token")]
        public void AclToken(Guid token)
        {
            Add("token", token.ToString("D"));
        }
    }
}