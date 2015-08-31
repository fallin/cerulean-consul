using System;

namespace Wildflower.Consul
{
    public class KeyValueDelOptions : Options
    {
        public bool Recurse { get; set; }

        public override void BuildQuery(Query query)
        {
            if (Recurse)
            {
                query.Add("recurse");
            }
        }
    }
}