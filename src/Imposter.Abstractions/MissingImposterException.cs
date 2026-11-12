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
    /// <summary>Gets a snapshot of the configured setup descriptions.</summary>
    public IReadOnlyList<string> ConfiguredSetups { get; } = Array.Empty<string>();

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

    /// <summary>Creates a failure with the actual invocation and configured setups.</summary>
    /// <param name="methodName">The member name.</param>
    /// <param name="actualInvocation">The member and actual argument description.</param>
    /// <param name="configuredSetups">The configured setup descriptions.</param>
    /// <param name="methodDisplayName">The full member signature retained by MethodName, when available.</param>
    public MissingImposterException(
        string methodName,
        string actualInvocation,
        IReadOnlyList<string> configuredSetups,
        string? methodDisplayName = null
    )
        : base(
            $"No setup matched {actualInvocation}."
                + Environment.NewLine
                + $"Configured setups for {methodName}:"
                + (
                    configuredSetups.Count == 0
                        ? " none."
                        : Environment.NewLine
                            + "  "
                            + string.Join(Environment.NewLine + "  ", configuredSetups)
                )
        )
    {
        MethodName = methodDisplayName ?? methodName;
        ConfiguredSetups = Array.AsReadOnly(configuredSetups.ToArray());
    }
}
