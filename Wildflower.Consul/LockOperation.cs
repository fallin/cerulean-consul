using System;

namespace Wildflower.Consul
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