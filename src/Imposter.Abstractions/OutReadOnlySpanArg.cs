namespace Imposter.Abstractions;

/// <summary>
/// Matches an <c>out ReadOnlySpan&lt;T&gt;</c> parameter. An out argument is an output, so it always matches.
/// </summary>
/// <typeparam name="T">The span's element type.</typeparam>
/// <remarks>
/// Example:
/// <code>
/// // bool TryRead(out ReadOnlySpan&lt;byte&gt; data);
/// imposter.TryRead(OutReadOnlySpanArg&lt;byte&gt;.Any())
///     .Returns((out ReadOnlySpan&lt;byte&gt; data) =&gt; { data = new byte[] { 1 }; return true; });
/// </code>
/// </remarks>
public sealed class OutReadOnlySpanArg<T>
{
    private static readonly OutReadOnlySpanArg<T> Instance = new();

    private OutReadOnlySpanArg() { }

    /// <summary>
    /// Matches any <c>out ReadOnlySpan&lt;T&gt;</c> argument.
    /// </summary>
    public static OutReadOnlySpanArg<T> Any() => Instance;
}
