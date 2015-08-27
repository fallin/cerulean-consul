using System;
using System.Collections.Generic;

namespace Wildflower.Consul
{
    public static class StorageOperationsExtensions
    {
        public static ConsulResponse<IList<StorageValue>> GetAll(this StorageOperations ops)
        {
            return Synchronous.Invoke(ops.GetAllAsync);
        }
    }
}