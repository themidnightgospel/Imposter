namespace Imposter.Abstractions;

/// <summary>
/// Defines a contract for exposing the underlying, user-facing instance that an imposter implements.
/// </summary>
/// <typeparam name="TInstance">The type of the instance being exposed by the imposter.</typeparam>
/// <remarks>
/// Generated imposters implement this interface so test code can access the proxied instance easily.
/// Prefer using the <see cref="ImposterExtensions"/>.Instance extension for concise call sites.
/// </remarks>
public interface IHaveImposterInstance<out TInstance>
{
    /// <summary>
    /// Gets the underlying instance implemented by the imposter.
    /// </summary>
    TInstance Instance();
}
