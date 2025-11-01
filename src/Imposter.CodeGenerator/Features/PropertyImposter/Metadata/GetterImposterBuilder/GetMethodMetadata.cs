using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilder;

internal readonly struct GetMethodMetadata
{
    internal readonly string Name = "Get";

    internal readonly TypeSyntax ReturnType;

    internal GetMethodMetadata(in ImposterPropertyCoreMetadata property)
    {
        ReturnType = property.TypeSyntax;
    }
}