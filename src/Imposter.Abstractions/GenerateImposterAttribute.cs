namespace Imposter.Abstractions;

/// <summary>
/// TODO Add docsstring
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly | AttributeTargets.Interface | AttributeTargets.Struct, AllowMultiple = true)]
public class GenerateImposterAttribute : Attribute
{
    public Type Type { get; }

    public bool PutInTheSameNamespace { get; }

    public GenerateImposterAttribute(Type type, bool putInTheSameNamespace = true)
    {
        Type = type;
        PutInTheSameNamespace = putInTheSameNamespace;
    }
}