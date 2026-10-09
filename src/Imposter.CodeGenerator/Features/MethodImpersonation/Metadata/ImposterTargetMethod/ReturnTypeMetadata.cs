using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;

internal readonly struct ReturnTypeMetadata
{
    internal readonly TypeSyntax? GenericAwaitableResultType;

    internal readonly TypeSymbolMetadata TypeSymbolMetadata;

    internal readonly bool IsAwaitable;

    internal ReturnTypeMetadata(
        ReturnTypeModel returnType,
        TypeSyntax returnTypeSyntax,
        bool supportsNullableGenericType
    )
    {
        IsAwaitable = returnType.IsAwaitable;

        GenericAwaitableResultType = returnType.AwaitableResultType is { } resultType
            ? SyntaxFactoryHelper.TypeSyntaxIncludingNullable(resultType)
            : null;

        TypeSymbolMetadata = new TypeSymbolMetadata(
            returnTypeSyntax,
            NullableReturnTypeSyntax(returnType, returnTypeSyntax, supportsNullableGenericType)
        );
    }

    private static TypeSyntax NullableReturnTypeSyntax(
        ReturnTypeModel returnType,
        TypeSyntax typeSyntax,
        bool supportsNullableGenericType
    )
    {
        var isConstructedGenericType = typeSyntax is GenericNameSyntax;
        var shouldConvertToNullable =
            typeSyntax is not NullableTypeSyntax
            && !returnType.IsVoid
            && !returnType.IsAwaitable
            && !(
                (returnType.IsTypeParameter || isConstructedGenericType)
                && !supportsNullableGenericType
            );

        return shouldConvertToNullable ? typeSyntax.ToNullableType() : typeSyntax;
    }
}
