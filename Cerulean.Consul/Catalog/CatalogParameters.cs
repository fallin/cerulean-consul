using System;

namespace Cerulean.Consul.Catalog
{
    public abstract class CatalogParameters : IQueryBuilder
    {
        public string DataCenter { get; set; }

        public virtual void BuildQuery(Query query)
        {
            if (!string.IsNullOrEmpty(DataCenter))
            {
                query.Add("dc", DataCenter);
            }
        }
    }
}