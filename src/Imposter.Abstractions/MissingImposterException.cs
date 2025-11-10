using System.Runtime.Serialization;

namespace Imposter.Abstractions;

/// <summary>
/// Exception thrown when an imposter running in <see cref="ImposterInvocationBehavior.Explicit"/> mode
/// receives an invocation that has not been configured.
/// </summary>
/// <remarks>
/// The <see cref="MethodName"/> (when provided) can be used to inspect which member
/// was invoked without a matching setup.
/// </remarks>
[Serializable]
public class MissingImposterException : Exception
{
    /// <summary>
    /// Gets the fully-qualified method or member name associated with the missing setup.
    /// </summary>
    public string? MethodName { get; }

    /// <inheritdoc />
    public MissingImposterException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingImposterException"/> class for the specified method.
    /// </summary>
    /// <param name="methodName">The fully-qualified member name that was invoked without a matching setup.</param>
    public MissingImposterException(string methodName)
        : base($"No imposter setup was configured for '{methodName}'.")
    {
        MethodName = methodName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingImposterException"/> class with an inner exception.
    /// </summary>
    /// <param name="methodName">The fully-qualified member name that was invoked without a matching setup.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public MissingImposterException(string methodName, Exception innerException)
        : base($"No imposter setup was configured for '{methodName}'.", innerException)
    {
        MethodName = methodName;
    }

    /// <inheritdoc />
    protected MissingImposterException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        MethodName = info.GetString(nameof(MethodName));
    }

    /// <inheritdoc />
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(MethodName), MethodName);
    }
}
