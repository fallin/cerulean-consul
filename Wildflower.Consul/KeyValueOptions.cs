using System;

namespace Wildflower.Consul
{
    public abstract class KeyValueOptions : IQueryProvider
    {
        public string DataCenter { get; set; }

        public virtual void BuildQuery(Query query)
        {
            if (!string.IsNullOrEmpty(DataCenter))
            {
                query.Add("dc", DataCenter);
            }
        }
    }
}