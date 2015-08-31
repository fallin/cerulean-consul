using System;

namespace Cerulean.Consul
{
    public abstract class LockOperation : IQueryProvider
    {
        public Guid Session { get; private set; }

        protected LockOperation(Guid session)
        {
            Session = session;
        }

        public abstract void BuildQuery(Query query);
    }
}