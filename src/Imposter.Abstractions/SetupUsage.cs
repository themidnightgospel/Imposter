using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Tracks execution of append-only callback registrations.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class SetupUsage
{
    /// <summary>Records that a registration and the preceding registrations were executed.</summary>
    public static void MarkUsed(ref int usedCount, int index)
    {
        var observed = Volatile.Read(ref usedCount);
        while (index > observed)
        {
            var previous = Interlocked.CompareExchange(ref usedCount, index, observed);
            if (previous == observed)
                return;
            observed = previous;
        }
    }
}
