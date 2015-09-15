using System;

namespace Cerulean.Consul.KeyValueStore
{
    public abstract class LockOperation : IQueryBuilder
    {
        public Guid Session { get; private set; }

        protected LockOperation(Guid session)
        {
            Session = session;
        }

        public abstract void BuildQuery(Query query);
    }
}