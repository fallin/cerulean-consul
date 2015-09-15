using System;
using System.Collections.Generic;

namespace Cerulean.Consul.Catalog
{
    public class ServiceTags : SortedSet<string>
    {
        public ServiceTags() : base(StringComparer.OrdinalIgnoreCase)
        {
            
        }
    }
}