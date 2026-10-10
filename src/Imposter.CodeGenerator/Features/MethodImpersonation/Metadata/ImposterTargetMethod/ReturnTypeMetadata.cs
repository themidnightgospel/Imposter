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

    // The type the invocation history keeps the result as: the nullable return type, a copy of a span's elements, or
    // the object a dynamic result is.
    internal readonly TypeSyntax KeptTypeSyntax;

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
        KeptTypeSyntax =
            IsSpan ? ValueTypeSyntax.ToNullableType()
            : returnType.Type.IsDynamic
                ? NullableReturnTypeSyntax(
                    returnType,
                    SyntaxFactoryHelper.RuntimeTypeSyntax(returnType.Type)
                )
            : TypeSymbolMetadata.NullableTypeSyntax;
    }

    internal ExpressionSyntax KeptValue(ExpressionSyntax result) =>
        SyntaxFactoryHelper.KeptValue(result, _returnType.Span, _returnType.Type);

    private static TypeSyntax NullableReturnTypeSyntax(
        ReturnTypeModel returnType,
        TypeSyntax typeSyntax
    ) =>
        typeSyntax is not NullableTypeSyntax && !returnType.IsVoid && !returnType.IsAwaitable
            ? typeSyntax.ToNullableType()
            : typeSyntax;
}
