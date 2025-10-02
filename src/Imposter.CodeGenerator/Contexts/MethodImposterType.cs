namespace Imposter.CodeGenerator.Contexts;

// TODO delete
internal class MethodImposterType2
{
    public string DeclaredAsParameterName { get; }

    public string DeclaredAsFieldName { get; }

    public string Name { get; }

    public string BuilderInterfaceName => $"I{Name}Builder";

    public MethodImposterType2(ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodImposter";
        DeclaredAsParameterName = char.ToLower(Name[0]) + Name.Substring(1);
        DeclaredAsFieldName = "_" + DeclaredAsParameterName;
    }
}