using System;

namespace Cerulean.Consul.Agent
{
    public class MaintenanceParameters : Parameters
    {
        public void Reason(string reason)
        {
            Add("reason", reason);
        }
    }
}