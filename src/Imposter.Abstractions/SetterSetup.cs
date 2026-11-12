using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Tracks one setup chain for an accessor whose value cannot be retained.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class SetterSetup
{
    internal SetterSetupState State;

    /// <summary>Excludes this setup chain from unused setup verification.</summary>
    public void Lenient() => State.IsLenient = true;

    /// <summary>Marks this setup chain as executed.</summary>
    public void MarkUsed() => State.MarkUsed();
}

internal struct SetterSetupState
{
    internal int Registered;
    internal int Used;
    internal bool UsesBase;
    internal bool IsLenient;

    internal void MarkUsed()
    {
        if (Volatile.Read(ref Used) == 0)
            Interlocked.Exchange(ref Used, 1);
    }
}
