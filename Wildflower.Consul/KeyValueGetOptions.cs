using System;

namespace Wildflower.Consul
{
    public class KeyValueGetOptions : Options
    {
        public bool Recurse { get; set; }
        public string DataCenter { get; set; }
        public long? Index { get; set; }

        public override void BuildQuery(Query query)
        {
            if (Recurse)
            {
                query.Add("recurse");
            }

            if (!string.IsNullOrEmpty(DataCenter))
            {
                query.Add("dc", DataCenter);
            }

            if (Index.HasValue)
            {
                query.Add("index", Index);
            }
        }
    }
}