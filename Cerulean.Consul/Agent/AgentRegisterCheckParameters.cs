using System;

namespace Cerulean.Consul.Agent
{
    public class AgentRegisterCheckParameters : Parameters
    {
        public void AclToken(string token)
        {
            Add("token", token);
        }
    }
}