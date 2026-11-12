namespace Imposter.Abstractions;

/// <summary>
/// Provides extension methods for working with imposters.
/// </summary>
public static class ImposterExtensions
{
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
