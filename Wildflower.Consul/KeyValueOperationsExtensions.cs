using System;

namespace Wildflower.Consul
{
    public static class KeyValueOperationsExtensions
    {
        public static KeyValueResponse<KeyValueResource[]> GetAll(this KeyValueOperations ops)
        {
            return Synchronous.Invoke(ops.GetAllAsync);
        }

        public static KeyValueResponse<KeyValueResource> Get(this KeyValueOperations ops, string key)
        {
            return Synchronous.Invoke(() => ops.GetAsync(key));
        }
    }
}