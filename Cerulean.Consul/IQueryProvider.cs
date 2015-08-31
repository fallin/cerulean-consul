using System;

namespace Cerulean.Consul
{
    public interface IQueryProvider
    {
        void BuildQuery(Query query);
    }
}