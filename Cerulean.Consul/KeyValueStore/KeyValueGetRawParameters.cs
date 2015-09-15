using System;

namespace Cerulean.Consul.KeyValueStore
{
    public sealed class KeyValueGetRawParameters : KeyValueParameters
    {
        public long? Index { get; set; }

        public override void BuildQuery(Query query)
        {
            base.BuildQuery(query);

            query.Add("raw");

            if (Index.HasValue)
            {
                query.Add("index", Index);
            }
        }
    }
}