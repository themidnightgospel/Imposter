namespace Imposter.Abstractions;

/// <summary>
/// Instructs the Imposter source generator to emit an imposter (test double) for the specified type.
/// </summary>
/// <remarks>
/// Apply this attribute at the assembly level to request imposters for one or more types.
/// The generator will produce a concrete type that can be used in tests to configure
/// and verify calls for the target interface or class.
///
/// Example:
/// <code>
/// [assembly: Imposter.Abstractions.GenerateImposter(typeof(IMyService))]
/// </code>
///
/// The generated type name and namespace are determined by the generator. When
/// <see cref="PutInTheSameNamespace"/> is <see langword="true"/>, the imposter is generated
/// into the same namespace as the original type (when possible).
/// </remarks>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class GenerateImposterAttribute : Attribute
{
    /// <summary>
    /// Gets the type for which an imposter should be generated.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// Gets a value indicating whether the generated imposter should be placed in the same namespace
    /// as the original type (when feasible).
    /// </summary>
    public bool PutInTheSameNamespace { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GenerateImposterAttribute"/> class.
    /// </summary>
    /// <param name="type">The target interface or class for which to generate an imposter.</param>
    /// <param name="putInTheSameNamespace">Whether to place the generated imposter in the same namespace as <paramref name="type"/>.</param>
    public GenerateImposterAttribute(Type type, bool putInTheSameNamespace = true)
    {
        Type = type;
        PutInTheSameNamespace = putInTheSameNamespace;
    }
}
