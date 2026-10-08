using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Imposter.Tests.Shared
{
    /// <summary>
    /// Makes the first calls on a freshly configured instance from several threads at once, for many fresh
    /// instances, and counts the outcomes: the returned value, or the name of the thrown exception.
    /// </summary>
    internal static class ConcurrentFirstCalls
    {
        private const int WorkerCount = 8;

        private const int Rounds = 20_000;

        internal static IReadOnlyDictionary<string, int> EveryCall(string outcome) =>
            new Dictionary<string, int> { [outcome] = WorkerCount * Rounds };

        internal static IReadOnlyDictionary<string, int> FirstCallThenEveryOtherCall(
            string firstOutcome,
            string otherOutcome
        ) =>
            new Dictionary<string, int>
            {
                [firstOutcome] = Rounds,
                [otherOutcome] = (WorkerCount - 1) * Rounds,
            };

        internal static IReadOnlyDictionary<string, int> CountOutcomes<TInstance>(
            Func<TInstance> createConfiguredInstance,
            Func<TInstance, int> call
        )
        {
            var outcomes = new ConcurrentDictionary<string, int>();
            using var barrier = new Barrier(WorkerCount);
            var current = default(TInstance)!;

            var workers = Enumerable
                .Range(0, WorkerCount)
                .Select(worker => new Thread(() =>
                {
                    for (var round = 0; round < Rounds; round++)
                    {
                        if (worker == 0)
                        {
                            current = createConfiguredInstance();
                        }

                        // The setup is complete before any caller starts.
                        barrier.SignalAndWait();
                        outcomes.AddOrUpdate(
                            Outcome(() => call(current)),
                            1,
                            (_, count) => count + 1
                        );

                        // Every caller is done before the next instance replaces this one.
                        barrier.SignalAndWait();
                    }
                }))
                .ToArray();

            foreach (var worker in workers)
            {
                worker.Start();
            }

            foreach (var worker in workers)
            {
                worker.Join();
            }

            return outcomes;
        }

        private static string Outcome(Func<int> call)
        {
            try
            {
                return call().ToString();
            }
            catch (Exception exception)
            {
                return exception.GetType().Name;
            }
        }
    }
}
