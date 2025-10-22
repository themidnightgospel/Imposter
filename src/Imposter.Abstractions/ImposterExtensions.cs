namespace Imposter.Abstractions;

/// <summary>
/// Provides extension methods for working with imposters.
/// </summary>
public static class ImposterExtensions
{
    /// <summary>
    /// Retrieves the underlying instance from an object that exposes an imposter instance.
    /// </summary>
    /// <typeparam name="TInstance">The type of the imposter instance.</typeparam>
    /// <param name="imposter">The object that contains the imposter instance.</param>
    /// <returns>The underlying imposter instance.</returns>
    public static TInstance Instance<TInstance>(this IHaveImposterInstance<TInstance> imposter)
        => imposter.Instance();
}
