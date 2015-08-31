using System;

namespace Wildflower.Consul
{
    public class KeyValueGetRawOptions : Options
    {
        public string DataCenter { get; set; }
        public long? Index { get; set; }

        public override void BuildQuery(Query query)
        {
            query.Add("raw");

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