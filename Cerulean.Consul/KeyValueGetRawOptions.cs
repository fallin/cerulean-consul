using System;

namespace Cerulean.Consul
{
    public sealed class KeyValueGetRawOptions : KeyValueOptions
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