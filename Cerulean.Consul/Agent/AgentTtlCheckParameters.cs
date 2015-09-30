using System;

namespace Cerulean.Consul.Agent
{
    public class AgentTtlCheckParameters : Parameters
    {
        public void Note(string note)
        {
            Add("note", note);
        }
    }
}