using System;

namespace Cerulean.Consul.Catalog
{
    public class GetNodesProvidingServiceParameters : CatalogParameters
    {
        public string Tag { get; set; }

        public override void BuildQuery(Query query)
        {
            base.BuildQuery(query);

            if (!string.IsNullOrEmpty(Tag))
            {
                query.Add("tag", Tag);
            }
        }
    }
}