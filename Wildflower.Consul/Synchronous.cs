using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wildflower.Consul
{
    static class Synchronous
    {
        public static T Invoke<T>(Func<Task<T>> action)
        {
            object state = action;

            Func<object, Task<T>> function = s => ((Func<Task<T>>)s)();
            return Task.Factory.StartNew(function, state, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)
                .Unwrap().GetAwaiter().GetResult();
        }
    }
}