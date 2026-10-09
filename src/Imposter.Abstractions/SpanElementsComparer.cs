namespace Imposter.Abstractions;

/// <summary>
/// Compares the arrays an imposter keeps span arguments in by their elements, so two span keys of an indexer that
/// hold the same elements address the same value. Generated imposters use it.
/// </summary>
/// <typeparam name="T">The span's element type.</typeparam>
public sealed class SpanElementsComparer<T> : IEqualityComparer<T[]>
{
    private SpanElementsComparer() { }

    /// <summary>
    /// The comparer, which compares the elements with <see cref="EqualityComparer{T}.Default"/>.
    /// </summary>
    public static SpanElementsComparer<T> Default { get; } = new();

    /// <inheritdoc/>
    public bool Equals(T[]? x, T[]? y) =>
        ReferenceEquals(x, y)
        || (x is not null && y is not null && x.SequenceEqual(y, EqualityComparer<T>.Default));

    /// <inheritdoc/>
    /// <remarks>
    /// A null array hashes to 0, as it does with <see cref="EqualityComparer{T}.Default"/>.
    /// </remarks>
    public int GetHashCode(T[] obj)
    {
        if (obj is null)
        {
            return 0;
        }
        unchecked
        {
            var hash = 17;
            foreach (var element in obj)
            {
                hash =
                    hash * 31
                    + (element is null ? 0 : EqualityComparer<T>.Default.GetHashCode(element));
            }

            return hash;
        }
    }
}
