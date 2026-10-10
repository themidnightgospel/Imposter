using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A method's return type. <see cref="AwaitableResultType"/> is the T of an awaited Task&lt;T&gt; or
/// ValueTask&lt;T&gt;. <see cref="Span"/> is set when the method returns a <c>Span&lt;T&gt;</c> or
/// <c>ReadOnlySpan&lt;T&gt;</c> by value. <see cref="IsPassedThrough"/> is true when it returns another
/// <c>ref struct</c>, or a value of its type parameter that allows ref structs, by value, which an imposter can't keep:
/// only a <c>Returns</c> delegate or the base implementation produces it, and the history doesn't record it.
/// </summary>
internal sealed record ReturnTypeModel(
    TypeModel Type,
    bool IsVoid,
    bool IsAwaitable,
    TypeModel? AwaitableResultType,
    bool ReferencesMethodTypeParameter,
    SpanModel? Span,
    bool IsPassedThrough
)
{
    internal static ReturnTypeModel From(IMethodSymbol method)
    {
        var returnType = method.ReturnType;
        var taskLike = returnType.GetTaskLikeMetadata();

        return new ReturnTypeModel(
            TypeModel.From(returnType),
            method.ReturnsVoid,
            taskLike.IsAwaitable,
            taskLike.GenericAwaitableResultType is { } resultType
                ? TypeModel.From(resultType)
                : null,
            returnType.ReferencesTypeParameterOf(method),
            SpanModel.FromReturnType(method),
            ReturnsPassedThrough(method)
        );
    }

    internal static bool ReturnsPassedThrough(IMethodSymbol method) =>
        ReturnsCustomRefStruct(method)
        || (
            method.RefKind == RefKind.None
            && method.ReturnType.IsMethodTypeParameterAllowingRefStructs()
        );

    internal static bool ReturnsCustomRefStruct(IMethodSymbol method) =>
        method.RefKind == RefKind.None
        && method.ReturnType.IsRefLikeType
        && SpanModel.FromReturnType(method) is null;
}
