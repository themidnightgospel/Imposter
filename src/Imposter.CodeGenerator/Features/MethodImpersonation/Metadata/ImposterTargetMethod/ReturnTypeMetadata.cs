using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;

internal readonly struct ReturnTypeMetadata
{
    internal readonly TypeSyntax? GenericAwaitableResultType;

    internal readonly TypeSymbolMetadata TypeSymbolMetadata;

    internal readonly bool IsAwaitable;

    internal readonly bool IsSpan;

    private readonly ReturnTypeModel _returnType;

    // The type Returns takes the result as: the return type, or the array a returned span covers.
    internal readonly TypeSyntax ValueTypeSyntax;

    // The type the invocation history keeps the result as: the nullable return type, or a copy of a span's elements.
    internal readonly TypeSyntax StoredTypeSyntax;

    internal ReturnTypeMetadata(ReturnTypeModel returnType, TypeSyntax returnTypeSyntax)
    {
        IsAwaitable = returnType.IsAwaitable;

        GenericAwaitableResultType = returnType.AwaitableResultType is { } resultType
            ? SyntaxFactoryHelper.TypeSyntaxIncludingNullable(resultType)
            : null;

        TypeSymbolMetadata = new TypeSymbolMetadata(
            returnTypeSyntax,
            NullableReturnTypeSyntax(returnType, returnTypeSyntax)
        );

        IsSpan = returnType.Span is not null;
        _returnType = returnType;
        ValueTypeSyntax = returnType.Span is { } span
            ? SyntaxFactoryHelper.SpanElementsArrayType(span)
            : returnTypeSyntax;
        StoredTypeSyntax = IsSpan
            ? ValueTypeSyntax.ToNullableType()
            : TypeSymbolMetadata.NullableTypeSyntax;
    }

    internal ExpressionSyntax StoredValue(ExpressionSyntax result) =>
        SyntaxFactoryHelper.StoredValue(result, _returnType.Span, _returnType.Type);

    private static TypeSyntax NullableReturnTypeSyntax(
        ReturnTypeModel returnType,
        TypeSyntax typeSyntax
    ) =>
        typeSyntax is not NullableTypeSyntax && !returnType.IsVoid && !returnType.IsAwaitable
            ? typeSyntax.ToNullableType()
            : typeSyntax;
}
