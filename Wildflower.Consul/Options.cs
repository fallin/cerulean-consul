using System;

namespace Wildflower.Consul
{
    public abstract class Options : IQueryProvider
    {
        public abstract void BuildQuery(Query query);
    }
}