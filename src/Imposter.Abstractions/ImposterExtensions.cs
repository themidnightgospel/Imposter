namespace Imposter.Abstractions;

/// <summary>
/// Provides extension methods for working with imposters.
/// </summary>
public static class ImposterExtensions
{
    /// <summary>Verifies that every recorded interaction was covered by a successful check.</summary>
    /// <param name="imposter">The imposter to verify.</param>
    public static void VerifyNoOtherCalls(this IImposter imposter) => imposter.VerifyNoOtherCalls();

    /// <summary>Verifies that no interactions have been recorded.</summary>
    /// <param name="imposter">The imposter to verify.</param>
    public static void VerifyNoInteractions(this IImposter imposter) =>
        imposter.VerifyNoInteractions();

    /// <summary>Verifies that all non-lenient configured setups have answered a call.</summary>
    /// <param name="imposter">The imposter to verify.</param>
    public static void VerifyAllSetupsUsed(this IImposter imposter) =>
        imposter.VerifyAllSetupsUsed();

    /// <summary>Clears recorded interactions without changing the imposter's behavior.</summary>
    /// <param name="imposter">The imposter whose history is cleared.</param>
    public static void ClearInvocations(this IImposter imposter) => imposter.ClearInvocations();

    /// <summary>Clears imposter-owned state, preserving the mode and instance identity.</summary>
    /// <param name="imposter">The imposter to reset.</param>
    public static void Reset(this IImposter imposter) => imposter.Reset();

    /// <summary>Returns a snapshot of the imposter's recorded interactions in entry order.</summary>
    /// <param name="imposter">The imposter whose interactions are returned.</param>
    public static IReadOnlyList<IImposterInvocation> Invocations(this IImposter imposter) =>
        imposter.Invocations();

    /// <summary>
    /// Retrieves the underlying instance from an object that exposes an imposter instance.
    /// </summary>
    /// <typeparam name="TInstance">The type of the imposter instance.</typeparam>
    /// <param name="imposter">The object that contains the imposter instance.</param>
    /// <returns>The underlying imposter instance.</returns>
    /// <remarks>
    /// This is a convenience wrapper over <see cref="IHaveImposterInstance{TInstance}.Instance"/> that improves readability
    /// in tests: <c>sut.Instance().Method()</c>.
    /// </remarks>
    public static TInstance Instance<TInstance>(this IHaveImposterInstance<TInstance> imposter) =>
        imposter.Instance();
}
