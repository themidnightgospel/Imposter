namespace Imposter.Abstractions;

/// <summary>
/// Matches an <c>out Span&lt;T&gt;</c> parameter. An out argument is an output, so it always matches.
/// </summary>
/// <typeparam name="T">The span's element type.</typeparam>
/// <remarks>
/// Example:
/// <code>
/// // void Rent(out Span&lt;byte&gt; buffer);
/// imposter.Rent(OutSpanArg&lt;byte&gt;.Any()).Callback((out Span&lt;byte&gt; buffer) =&gt; buffer = new byte[16]);
/// </code>
/// </remarks>
public sealed class OutSpanArg<T>
{
    private static readonly OutSpanArg<T> Instance = new();

    private OutSpanArg() { }

    /// <summary>
    /// Matches any <c>out Span&lt;T&gt;</c> argument.
    /// </summary>
    public static OutSpanArg<T> Any() => Instance;
}
