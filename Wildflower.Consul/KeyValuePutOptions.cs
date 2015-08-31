using System;

namespace Wildflower.Consul
{
    public class KeyValuePutOptions : Options
    {
        public long? Flags { get; set; }
        public long? CheckAndSet { get; set; }
        public LockOperation LockOperation { get; set; }

        public override void BuildQuery(Query query)
        {
            if (Flags.HasValue)
            {
                query.Add("flags", Flags);
            }

            if (CheckAndSet.HasValue)
            {
                query.Add("cas", CheckAndSet);
            }

            if (LockOperation != null)
            {
                LockOperation.BuildQuery(query);
            }
        }
    }

    public abstract class LockOperation : IQueryProvider
    {
        public Guid Session { get; private set; }

        protected LockOperation(Guid session)
        {
            Session = session;
        }

        public abstract void BuildQuery(Query query);
    }

    public class AcquireLock : LockOperation
    {
        public AcquireLock(Guid session)
            : base(session)
        {
        }

        public override void BuildQuery(Query query)
        {
            query.Add("acquire", Session);
        }
    }

    public class ReleaseLock : LockOperation
    {
        public ReleaseLock(Guid session)
            : base(session)
        {
        }

        public override void BuildQuery(Query query)
        {
            query.Add("release", Session);
        }
    }
}