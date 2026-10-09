using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A method's return type. <see cref="AwaitableResultType"/> is the T of an awaited Task&lt;T&gt; or
/// ValueTask&lt;T&gt;. <see cref="Span"/> is set when the method returns a <c>Span&lt;T&gt;</c> or
/// <c>ReadOnlySpan&lt;T&gt;</c> by value.
/// </summary>
internal sealed record ReturnTypeModel(
    TypeModel Type,
    bool IsVoid,
    bool IsTypeParameter,
    bool IsAwaitable,
    TypeModel? AwaitableResultType,
    bool ReferencesMethodTypeParameter,
    SpanModel? Span
)
{
    internal static ReturnTypeModel From(IMethodSymbol method)
    {
        var returnType = method.ReturnType;
        var taskLike = returnType.GetTaskLikeMetadata();

        return new ReturnTypeModel(
            TypeModel.From(returnType),
            method.ReturnsVoid,
            returnType.TypeKind == TypeKind.TypeParameter,
            taskLike.IsAwaitable,
            taskLike.GenericAwaitableResultType is { } resultType
                ? TypeModel.From(resultType)
                : null,
            returnType.ReferencesTypeParameterOf(method),
            SpanModel.FromReturnType(method)
        );
    }
}
