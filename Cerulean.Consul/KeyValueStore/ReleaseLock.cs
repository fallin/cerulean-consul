using System;

namespace Cerulean.Consul.KeyValueStore
{
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