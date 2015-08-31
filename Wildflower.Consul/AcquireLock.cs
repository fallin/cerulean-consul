﻿using System;

namespace Wildflower.Consul
{
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
}