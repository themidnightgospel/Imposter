using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Formats values for diagnostics without allowing a failing ToString to hide the original failure.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class DiagnosticValue
{
    /// <summary>Gets a cached label for a configured exception type.</summary>
    /// <typeparam name="TException">The configured exception type.</typeparam>
    public static string Throws<TException>() => ExceptionOutcome<TException>.Description;

    private static class ExceptionOutcome<TException>
    {
        internal static readonly string Description = "Throws<" + typeof(TException).Name + ">";
    }

    /// <summary>Describes other recorded interactions when a verified member has no history.</summary>
    /// <param name="imposter">The owner of the recorded history.</param>
    public static string OtherInteractions(IImposter imposter)
    {
        var invocations = imposter.Invocations();
        if (invocations.Count == 0)
            return string.Empty;
        var entries = new List<string>();
        foreach (var invocation in invocations)
            entries.Add("  " + invocation);
        var ownerName = imposter.GetType().Name;
        var arity = ownerName.IndexOf('`');
        if (arity >= 0)
            ownerName = ownerName.Substring(0, arity);
        return "Other interactions on "
            + ownerName
            + ":"
            + Environment.NewLine
            + string.Join(Environment.NewLine, entries);
    }

    /// <summary>Formats stored outcome kinds without evaluating configured behavior.</summary>
    /// <param name="descriptions">The descriptions retained when the setup was configured.</param>
    public static string Outcomes(object? descriptions)
    {
        if (descriptions is null)
            return string.Empty;
        return descriptions is string single ? single : string.Join(", ", (string[])descriptions);
    }

    /// <summary>Retains outcome labels without allocating storage for a single outcome.</summary>
    /// <param name="descriptions">The immutable label or label array.</param>
    /// <param name="description">The outcome label.</param>
    public static void AddOutcome(ref object? descriptions, string description)
    {
        object? snapshot;
        object updated;
        do
        {
            snapshot = Volatile.Read(ref descriptions);
            if (snapshot is null)
                updated = description;
            else if (snapshot is string single)
                updated = new[] { single, description };
            else
            {
                var previous = (string[])snapshot;
                var entries = new string[previous.Length + 1];
                Array.Copy(previous, entries, previous.Length);
                entries[previous.Length] = description;
                updated = entries;
            }
        } while (
            !ReferenceEquals(
                Interlocked.CompareExchange(ref descriptions, updated, snapshot),
                snapshot
            )
        );
    }

    /// <summary>Formats a value, including null, inside angle brackets.</summary>
    /// <param name="value">The value to describe.</param>
    /// <remarks>
    /// User-defined <see cref="object.ToString"/> implementations may throw. Only that call is
    /// guarded so a diagnostic can retain an unprintable-value label without hiding the
    /// verification failure it describes. Other failures in this formatter are not swallowed.
    /// </remarks>
    public static string Format(object? value)
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

    /// <summary>Formats matcher descriptions verbatim without adding value delimiters.</summary>
    /// <remarks>Only a user-defined ToString call is guarded so a formatting failure cannot hide verification findings.</remarks>
    internal static string FormatMatcher(object? matcher)
    {
        if (matcher is null)
            return "null";
        string? description;
        try
        {
            description = matcher.ToString();
        }
        catch (Exception)
        {
            return "<unprintable " + matcher.GetType().Name + ">";
        }
        return description ?? "null";
    }

    internal static string Sequence<T>(IEnumerable<T> values) =>
        "[" + string.Join(", ", values.Select(value => Format(value))) + "]";
}
