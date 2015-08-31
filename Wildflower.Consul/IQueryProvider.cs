using System;

namespace Wildflower.Consul
{
    public interface IQueryProvider
    {
        void BuildQuery(Query query);
    }
}