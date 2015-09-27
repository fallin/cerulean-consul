using System;

namespace Cerulean.Consul
{
    public class GlobalParameters : Parameters
    {
        public void AclToken(Guid token)
        {
            Add("token", token.ToString("D"));
        }

        public void Datacenter(string dc)
        {
            if (!string.IsNullOrWhiteSpace(dc))
            {
                Add("dc", dc);
            }
        }
    }
}