using System;

namespace Cerulean.Consul.Agent
{
    public class RegisterServiceParameters : Parameters
    {
        public void AclToken(string token)
        {
            Add("token", token);
        }
    }
}