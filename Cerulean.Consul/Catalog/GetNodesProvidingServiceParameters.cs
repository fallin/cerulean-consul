using System;

namespace Cerulean.Consul.Catalog
{
    public class GetNodesProvidingServiceParameters : CatalogParameters
    {
        public void Tag(string tag)
        {
            if (!string.IsNullOrEmpty(tag))
            {
                Add("tag", tag);
            }
        }
    }
}