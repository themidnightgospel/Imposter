namespace Imposter.Abstractions;

/// <summary>
/// TODO Add docsstring
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
public class GenerateImposterAttribute : Attribute
{
    public Type Type { get; }

    public GenerateImposterAttribute(Type type)
    {
        Type = type;
    }
}