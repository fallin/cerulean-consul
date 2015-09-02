using System;

namespace Cerulean.Consul
{
    public sealed class KeyValueGetOptions : KeyValueOptions
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