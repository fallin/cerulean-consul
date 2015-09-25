using System;

namespace Cerulean.Consul.KeyValueStore
{
    public class KeyValuePutParameters : KeyValueParameters
    {
        public void Flags(long flags)
        {
            Add("flags", flags);
        }

        public void CheckAndSet(long index)
        {
            Add("cas", index);
        }

        public void Acquire(Guid session)
        {
            Add("acquire", session);
        }

        public void Release(Guid session)
        {
            Add("release", session);
        }
    }
}