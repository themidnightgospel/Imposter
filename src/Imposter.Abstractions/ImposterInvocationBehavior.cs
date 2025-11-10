namespace Imposter.Abstractions;

/// <summary>
/// Specifies how an imposter should behave when a member is invoked without an explicit setup.
/// </summary>
/// <remarks>
/// Use <see cref="Implicit"/> to have missing setups return default values (loose mode).
/// Use <see cref="Explicit"/> to throw when a missing setup is encountered (strict mode).
/// </remarks>
public enum ImposterInvocationBehavior
{
    /// <summary>
    /// Implicit (loose) mode — missing setups fall back to default return values.
    /// </summary>
    Implicit = 0,

    /// <summary>
    /// Explicit (strict) mode — missing setups throw <see cref="MissingImposterException"/>.
    /// </summary>
    Explicit = 1
}
