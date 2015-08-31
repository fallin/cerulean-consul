using System;

namespace Wildflower.Consul
{
    public class KeyValueGetKeysOptions : Options
    {
        public string DataCenter { get; set; }
        public long? Index { get; set; }
        public char? Separator { get; set; }

        public override void BuildQuery(Query query)
        {
            query.Add("keys");

            if (!string.IsNullOrEmpty(DataCenter))
            {
                query.Add("dc", DataCenter);
            }

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