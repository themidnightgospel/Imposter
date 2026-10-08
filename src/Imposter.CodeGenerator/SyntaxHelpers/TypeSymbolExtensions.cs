using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class TypeSymbolExtensions
{
    internal static bool IsAwaitable(this ITypeSymbol? symbol) =>
        symbol.GetTaskLikeMetadata().IsAwaitable;

    internal static TaskLikeMetadata GetTaskLikeMetadata(this ITypeSymbol? symbol)
    {
        if (
            symbol is not INamedTypeSymbol named
            || !named.IsInNamespace("System", "Threading", "Tasks")
        )
        {
            return TaskLikeMetadata.Empty;
        }

        return named.MetadataName switch
        {
            "Task" or "ValueTask" => new TaskLikeMetadata(isAwaitable: true, null),
            "Task`1" or "ValueTask`1" => new TaskLikeMetadata(
                isAwaitable: true,
                named.TypeArguments[0]
            ),
            _ => TaskLikeMetadata.Empty,
        };
    }

    internal static bool IsNonGenericValueTask(this ITypeSymbol? symbol) =>
        symbol is INamedTypeSymbol { MetadataName: "ValueTask" } named
        && named.IsInNamespace("System", "Threading", "Tasks");

    // Matches by metadata name and namespace only: depending on the target framework these types live in
    // System.Private.CoreLib, System.Runtime, mscorlib, netstandard or System.Threading.Tasks.Extensions.
    private static bool IsInNamespace(
        this ISymbol symbol,
        string outerNamespace,
        string middleNamespace,
        string innerNamespace
    ) =>
        symbol.ContainingNamespace is { } inner
        && inner.Name == innerNamespace
        && inner.ContainingNamespace is { } middle
        && middle.Name == middleNamespace
        && middle.ContainingNamespace is { } outer
        && outer.Name == outerNamespace
        && outer.ContainingNamespace is { IsGlobalNamespace: true };

    internal static TypeSymbolMetadata GetTypeSymbolMetadata(
        this ITypeSymbol? symbol,
        TypeSyntax typeSyntax,
        bool isAwaitable,
        bool supportsNullableGenericType
    )
    {
        if (symbol is null)
        {
            return TypeSymbolMetadata.Empty;
        }

        var isGenericType = symbol.TypeKind == TypeKind.TypeParameter;
        var isNullableType = typeSyntax is NullableTypeSyntax;
        var isConstructedGenericType = typeSyntax is GenericNameSyntax;
        var shouldConvertToNullable =
            !isNullableType
            && symbol.SpecialType != SpecialType.System_Void
            && !isAwaitable
            && !((isGenericType || isConstructedGenericType) && !supportsNullableGenericType);

        var nullableTypeSyntax = shouldConvertToNullable ? typeSyntax.ToNullableType() : typeSyntax;

        return new TypeSymbolMetadata(typeSyntax, nullableTypeSyntax);
    }

    internal static bool IsMethodAsync(this IMethodSymbol methodSymbol)
    {
        if (
            !methodSymbol.ReturnsVoid
            && methodSymbol.ReturnType.ImplementsAsyncStateMachineInterface()
        )
        {
            return true;
        }

        return methodSymbol.ReturnType.IsAwaitable();
    }

    internal static bool ImplementsAsyncStateMachineInterface(this ITypeSymbol? symbol)
    {
        if (symbol is null)
        {
            return false;
        }

        foreach (var interfaceSymbol in symbol.AllInterfaces)
        {
            if (interfaceSymbol.IsAsyncStateMachineInterface())
            {
                return true;
            }
        }

        return false;
    }

    internal static bool IsAsyncStateMachineInterface(this ITypeSymbol? symbol) =>
        symbol is { MetadataName: "IAsyncStateMachine" }
        && symbol.IsInNamespace("System", "Runtime", "CompilerServices");
}

internal readonly struct TaskLikeMetadata
{
    internal static TaskLikeMetadata Empty => default;

    internal TaskLikeMetadata(bool isAwaitable, ITypeSymbol? genericAwaitableResultType)
    {
        IsAwaitable = isAwaitable;
        GenericAwaitableResultType = genericAwaitableResultType;
    }

    internal bool IsAwaitable { get; }

    internal ITypeSymbol? GenericAwaitableResultType { get; }
}

internal readonly struct TypeSymbolMetadata
{
    internal static TypeSymbolMetadata Empty => default;

    internal TypeSymbolMetadata(TypeSyntax typeSyntax, TypeSyntax nullableTypeSyntax)
    {
        TypeSyntax = typeSyntax;
        NullableTypeSyntax = nullableTypeSyntax;
    }

    internal TypeSyntax TypeSyntax { get; }

    internal TypeSyntax NullableTypeSyntax { get; }
}
