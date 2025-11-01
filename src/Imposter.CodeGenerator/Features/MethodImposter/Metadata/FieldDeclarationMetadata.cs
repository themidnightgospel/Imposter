using System.Globalization;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata;

internal readonly record struct FieldDeclarationMetadata
{
    public string Name { get; }

    public FieldDeclarationMetadata(string typeName)
    {
        Name = "_" + char.ToLower(typeName[0], CultureInfo.InvariantCulture) + typeName[1..];
    }
}