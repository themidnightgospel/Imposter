using System.Globalization;
using Imposter.CodeGenerator.Helpers;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal readonly record struct FieldDeclarationMetadata
{
    public string Name { get; }

    // The method's setup member uses the field by its bare name, so the name avoids the method's parameter names.
    public FieldDeclarationMetadata(string typeName, NameSet fieldNames)
    {
        Name = fieldNames.Use(
            "_" + char.ToLower(typeName[0], CultureInfo.InvariantCulture) + typeName[1..]
        );
    }
}
