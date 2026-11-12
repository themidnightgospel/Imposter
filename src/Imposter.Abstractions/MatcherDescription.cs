using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Describes matchers for generated setup and step descriptions without evaluating predicates.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class MatcherDescription
{
    /// <summary>Describes an argument matcher.</summary>
    public static string Format<T>(Arg<T> matcher) => matcher.DescribeMatcher();

    /// <summary>Describes a span matcher.</summary>
    public static string Format<T>(SpanArg<T> matcher) => matcher.DescribeMatcher();

    /// <summary>Describes a read-only span matcher.</summary>
    public static string Format<T>(ReadOnlySpanArg<T> matcher) => matcher.DescribeMatcher();

    /// <summary>Formats a matcher value without allowing user formatting to hide verification failures.</summary>
    /// <param name="value">The value to describe.</param>
    /// <remarks>
    /// User-defined <see cref="object.ToString"/> implementations may throw. Only that call is
    /// guarded so a diagnostic can retain an unprintable-value label without hiding the
    /// verification failure it describes. Other failures in this formatter are not swallowed.
    /// </remarks>
    internal static string Value(object? value)
    {
        if (value is null)
            return "<null>";
        string? formatted;
        try
        {
            formatted = value.ToString();
        }
        catch (Exception)
        {
            return "<unprintable " + value.GetType().Name + ">";
        }
        return "<" + (formatted ?? "null") + ">";
    }
}
