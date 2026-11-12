namespace Imposter.Abstractions;

/// <summary>Provides access to the recorded interactions of a generated imposter.</summary>
public interface IImposter
{
    /// <summary>Returns a snapshot of recorded interactions in entry order.</summary>
    IReadOnlyList<IImposterInvocation> Invocations();

    /// <summary>Clears recorded interactions while preserving setups, their sequence positions, and stored values.</summary>
    void ClearInvocations();

    /// <summary>Clears imposter-owned state while preserving mode and underlying instance identity.</summary>
    /// <remarks>Previously obtained builders are detached. Do not race this operation with instance calls.</remarks>
    void Reset();
}
