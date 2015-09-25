using System;

namespace Cerulean.Consul.Catalog
{
    public abstract class CatalogParameters : Parameters
    {
        public string DataCenter { get; set; }

        public void Datacenter(string dc)
        {
            if (!string.IsNullOrEmpty(DataCenter))
            {
                Add("dc", DataCenter);
            }
        }
    }
}