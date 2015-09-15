using System;
using System.Collections.Generic;

namespace Cerulean.Consul.Catalog
{
    public class NodeServices
    {
        public NodeServices()
        {
            Services = new Dictionary<string, ServiceDescriptor>(StringComparer.OrdinalIgnoreCase);
        }

        public NodeDescriptor Node { get; set; }
        public IDictionary<string, ServiceDescriptor> Services { get; private set; } 
    }
}