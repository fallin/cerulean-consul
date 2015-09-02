using System;

namespace Cerulean.Consul
{
    public sealed class KeyValuePutOptions : KeyValueOptions
    {
        public long? Flags { get; set; }
        public long? CheckAndSet { get; set; }
        public LockOperation LockOperation { get; set; }

        public void AcquireLock(Guid session)
        {
            LockOperation = new AcquireLock(session);
        }

        public void ReleaseLock(Guid session)
        {
            LockOperation = new ReleaseLock(session);
        }

        public override void BuildQuery(Query query)
        {
            base.BuildQuery(query);

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
}