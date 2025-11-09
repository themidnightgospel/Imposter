namespace Imposter.Abstractions;

/// <summary>
/// Specifies that a mock implementation should be generated for the specified type.
/// When applied to a method, class, assembly, interface, or struct, this attribute triggers the generation of an imposter (mock) implementation.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class GenerateImposterAttribute : Attribute
{
    /// <summary>
    /// Gets the type for which a mock implementation should be generated.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// Gets a value indicating whether the generated mock should be placed in the same namespace as the original type.
    /// </summary>
    public bool PutInTheSameNamespace { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GenerateImposterAttribute"/> class.
    /// </summary>
    /// <param name="type"><see cref="Type"/></param>
    /// <param name="putInTheSameNamespace"><see cref="PutInTheSameNamespace"/></param>
    public GenerateImposterAttribute(Type type, bool putInTheSameNamespace = true)
    {
        Type = type;
        PutInTheSameNamespace = putInTheSameNamespace;
    }
}