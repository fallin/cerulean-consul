using System;

namespace Cerulean.Consul.Agent
{
    public class AgentMembersParameters : Parameters
    {
        /// <summary>
        /// For agents running in server mode, provide WAN members instead of the (default) LAN members.
        /// </summary>
        public void Wan()
        {
            Add("wan", 1);
        }
    }
}