using System;

namespace Cerulean.Consul
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