using System;

namespace Cerulean.Consul
{
    public class KeyValueDelOptions : KeyValueOptions
    {
        public bool Recurse { get; set; }
        public long? CheckAndSet { get; set; }

        public override void BuildQuery(Query query)
        {
            base.BuildQuery(query);

            if (Recurse)
            {
                query.Add("recurse");
            }

            if (CheckAndSet.HasValue)
            {
                query.Add("cas", CheckAndSet);
            }
        }
    }
}