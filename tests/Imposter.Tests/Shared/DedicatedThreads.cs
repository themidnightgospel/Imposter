using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace Imposter.Tests.Shared
{
    /// <summary>
    /// Runs workers that block on dedicated threads instead of the thread pool, which the rest of the suite shares
    /// and which adds threads only slowly.
    /// </summary>
    internal static class DedicatedThreads
    {
        /// <summary>
        /// Starts <paramref name="count"/> threads, releases them together once all are running, waits for them, and
        /// rethrows what any of them threw.
        /// </summary>
        internal static void RunTogether(int count, Action<int> work)
        {
            using var ready = new CountdownEvent(count);
            using var start = new ManualResetEventSlim();
            var failures = new ConcurrentQueue<Exception>();
            var threads = Enumerable
                .Range(0, count)
                .Select(worker => new Thread(() =>
                {
                    ready.Signal();
                    start.Wait();
                    try
                    {
                        work(worker);
                    }
                    catch (Exception exception)
                    {
                        failures.Enqueue(exception);
                    }
                }))
                .ToArray();

            foreach (var thread in threads)
            {
                thread.Start();
            }

            ready.Wait();
            start.Set();
            foreach (var thread in threads)
            {
                thread.Join();
            }

            if (!failures.IsEmpty)
            {
                throw new AggregateException(failures);
            }
        }
    }
}
