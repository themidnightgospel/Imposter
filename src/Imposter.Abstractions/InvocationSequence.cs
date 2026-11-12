using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Assigns globally ordered invocation stamps for generated imposters.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class InvocationSequence
{
    private static long _sequence;

    /// <summary>Returns the next invocation entry stamp.</summary>
    public static long Next() => Interlocked.Increment(ref _sequence);
}
