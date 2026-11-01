namespace Imposter.Abstractions;

/// <summary>
/// Exception thrown when an imposter running in <see cref="ImposterMode.Explicit"/> mode
/// receives an invocation that has not been configured.
/// </summary>
/// <remarks>
/// The <see cref="MethodName"/> (when provided) can be used to inspect which member
/// was invoked without a matching setup.
/// </remarks>
public class MissingImposterException : Exception
{
    /// <summary>
    /// Gets the fully-qualified method or member name associated with the missing setup.
    /// </summary>
    public string? MethodName { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public MissingImposterException() { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="methodName">The fully-qualified member name that was invoked without a matching setup.</param>
    public MissingImposterException(string methodName)
        : base(MessageFor(methodName))
    {
        MethodName = methodName;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="methodName">The fully-qualified member name that was invoked without a matching setup.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public MissingImposterException(string methodName, Exception innerException)
        : base(MessageFor(methodName), innerException)
    {
        MethodName = methodName;
    }

    private static string MessageFor(string methodName) =>
        $"No imposter setup was configured for '{methodName}'.";
}
