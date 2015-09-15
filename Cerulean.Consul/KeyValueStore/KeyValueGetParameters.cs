using System;

namespace Cerulean.Consul.KeyValueStore
{
    public sealed class KeyValueGetParameters : KeyValueParameters
    {
        public bool Recurse { get; set; }
        public long? Index { get; set; }

        public override void BuildQuery(Query query)
        {
            base.BuildQuery(query);

            if (Recurse)
            {
                query.Add("recurse");
            }

            if (Index.HasValue)
            {
                query.Add("index", Index);
            }
        }
    }
}