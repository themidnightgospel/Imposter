namespace Imposter.Abstractions;

/// <summary>
/// Defines a contract for classes that provide access to an imposter instance.
/// </summary>
/// <typeparam name="TInstance">The type of the imposter instance being provided.</typeparam>
public interface IHaveImposterInstance<out TInstance>
{
    /// <summary>
    /// Gets the imposter instance.
    /// </summary>
    TInstance Instance();
}