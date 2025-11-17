using System;
using System.Linq;
using Imposter.Abstractions;
using Shouldly;

namespace Imposter.Tests.Features.Verification
{
    internal static class PerformedInvocationTestHelper
    {
        private static readonly string[] EntrySeparators = { "\r\n", "\n" };

        internal static string[] ReadEntries(this VerificationFailedException exception)
        {
            if (string.IsNullOrWhiteSpace(exception.PerformedInvocations))
            {
                return Array.Empty<string>();
            }

            var normalized = exception.PerformedInvocations!.Replace(
                "\r",
                string.Empty,
                StringComparison.Ordinal
            );

            var entries = normalized
                .Split(EntrySeparators, StringSplitOptions.RemoveEmptyEntries)
                .Select(entry => entry.Trim())
                .ToArray();

            return entries;
        }

        internal static void MessageShouldDescribeCounts(
            this VerificationFailedException exception,
            Count expected,
            int actual
        )
        {
            exception.Message.ShouldContain($"expected to be performed {expected}");
            exception.Message.ShouldContain($"performed {actual} times");
        }

        internal static string FormatValue(object? value) => $"<{value?.ToString() ?? "null"}>";
    }
}
