using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct DefaultResultGeneratorMethodMetadata
{
    internal readonly string Name = "DefaultResultGenerator";

    internal readonly TypeSyntax ReturnType;

    internal readonly bool ReturnsNullable;

    internal DefaultResultGeneratorMethodMetadata(
        TypeSyntax methodReturnTypeSyntax,
        bool hasReturnValue,
        bool supportsNullableGenericType,
        bool hasGenericReturnType,
        bool hasTaskLikeReturnType)
    {
        var shouldUseNullable = hasReturnValue &&
            supportsNullableGenericType &&
            !hasGenericReturnType &&
            !hasTaskLikeReturnType &&
            methodReturnTypeSyntax is not NullableTypeSyntax;

        ReturnsNullable = shouldUseNullable;

        ReturnType = shouldUseNullable
            ? methodReturnTypeSyntax.ToNullableType()
            : methodReturnTypeSyntax;
    }
}
