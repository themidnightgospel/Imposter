namespace Imposter.Abstractions;

/// <summary>Provides access to the recorded interactions of a generated imposter.</summary>
public interface IImposter
{
    /// <summary>Returns a snapshot of recorded interactions in entry order.</summary>
    IReadOnlyList<IImposterInvocation> Invocations();

    /// <summary>Clears recorded interactions while preserving setups, their usage and sequence positions, and stored values.</summary>
    void ClearInvocations();

    /// <summary>Clears the imposter-owned state while preserving its mode and underlying instance identity.</summary>
    /// <remarks>Previously obtained setup builders are detached. Do not race this operation with instance calls.</remarks>
    void Reset();

    /// <summary>Verifies that every recorded interaction was covered by a successful check.</summary>
    void VerifyNoOtherCalls();

    /// <summary>Verifies that no interactions have been recorded.</summary>
    void VerifyNoInteractions();

    /// <summary>Verifies that every non-lenient configured setup has answered a call.</summary>
    void VerifyAllSetupsUsed();
}
