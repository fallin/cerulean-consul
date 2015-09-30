using System;

namespace Cerulean.Consul.Agent
{
    public class AgentJoinParameters : Parameters
    {
        /// <summary>
        /// For agents running in server mode, cause the agent to join using the WAN pool.
        /// </summary>
        public void Wan()
        {
            Add("wan", 1);
        }
    }
}