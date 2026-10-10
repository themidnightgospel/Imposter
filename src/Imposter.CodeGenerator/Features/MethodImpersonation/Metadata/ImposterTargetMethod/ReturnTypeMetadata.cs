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

    private readonly TypeModel? _dynamicType;

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
        _dynamicType = returnType.Type.IsDynamic ? returnType.Type : null;
        ValueTypeSyntax = returnType.Span is { } span
            ? SyntaxFactoryHelper.SpanElementsArrayType(span)
            : returnTypeSyntax;
        StoredTypeSyntax = IsSpan
            ? ValueTypeSyntax.ToNullableType()
            : TypeSymbolMetadata.NullableTypeSyntax;
    }

    // The result as the stored type: a copy of a span's elements, the object a dynamic result is (see
    // SyntaxFactoryHelper.AsObject), or the result itself.
    internal ExpressionSyntax StoredValue(ExpressionSyntax result) =>
        IsSpan ? SyntaxFactoryHelper.SpanElementsCopy(result)
        : _dynamicType is { } dynamicType ? SyntaxFactoryHelper.AsObject(result, dynamicType)
        : result;

    private static TypeSyntax NullableReturnTypeSyntax(
        ReturnTypeModel returnType,
        TypeSyntax typeSyntax
    ) =>
        typeSyntax is not NullableTypeSyntax && !returnType.IsVoid && !returnType.IsAwaitable
            ? typeSyntax.ToNullableType()
            : typeSyntax;
}
