using System;

namespace Wildflower.Consul
{
    public class KeyValueGetKeysOptions : KeyValueOptions
    {
        public long? Index { get; set; }
        public char? Separator { get; set; }

        public override void BuildQuery(Query query)
        {
            base.BuildQuery(query);

            query.Add("keys");

            if (Index.HasValue)
            {
                query.Add("index", Index);
            }

            if (Separator.HasValue)
            {
                query.Add("separator", Separator);
            }
        }
    }
}