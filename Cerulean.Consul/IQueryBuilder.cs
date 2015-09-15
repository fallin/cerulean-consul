using System;

namespace Cerulean.Consul
{
    public interface IQueryBuilder
    {
        void BuildQuery(Query query);
    }
}