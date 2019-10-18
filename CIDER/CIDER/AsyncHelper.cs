using System;
using System.Threading;
using System.Threading.Tasks;

namespace CIDER
{
    public static class AsyncHelper
    /*/
     * This class provides the facility to start async code from non-async functions
     * Source: https://cpratt.co/async-tips-tricks/
    /*/
    {
        private static readonly TaskFactory _taskFactory = new
            TaskFactory(CancellationToken.None,
                        TaskCreationOptions.None,
                        TaskContinuationOptions.None,
                        TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
            => _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        public static void RunSync(Func<Task> func)
            => _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
    }
}