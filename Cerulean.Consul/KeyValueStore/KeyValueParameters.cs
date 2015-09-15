using System;

namespace Cerulean.Consul.KeyValueStore
{
    public abstract class KeyValueParameters : IQueryBuilder
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