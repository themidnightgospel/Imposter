namespace Imposter.Abstractions;

// The element checks behind SpanArg<T> and ReadOnlySpanArg<T>, which match an imposter's copy of a span argument.
internal static class SpanElementMatchers
{
    internal static Func<T[], bool> SequenceEqualTo<T>(T[] expected, IEqualityComparer<T> comparer)
    {
        if (expected is null)
            throw new ArgumentNullException(nameof(expected));
        if (comparer is null)
            throw new ArgumentNullException(nameof(comparer));

        var elements = (T[])expected.Clone();
        return actual => actual.SequenceEqual(elements, comparer);
    }

    internal static Func<T[], bool> Satisfying<T>(Func<T[], bool> predicate) =>
        predicate ?? throw new ArgumentNullException(nameof(predicate));
}
