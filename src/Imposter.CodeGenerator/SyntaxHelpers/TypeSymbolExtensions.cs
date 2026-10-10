using System.Collections.Generic;
using System.Linq;
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

    internal static bool ReferencesTypeParameterOf(this ITypeSymbol type, IMethodSymbol method) =>
        method.TypeParameters.Any(typeParameter => type.Contains(typeParameter));

    private static bool Contains(this ITypeSymbol type, ITypeParameterSymbol typeParameter) =>
        SymbolEqualityComparer.Default.Equals(type, typeParameter)
        || type switch
        {
            INamedTypeSymbol namedType => namedType.TypeArguments.Any(typeArgument =>
                typeArgument.Contains(typeParameter)
            ),
            IArrayTypeSymbol arrayType => arrayType.ElementType.Contains(typeParameter),
            _ => false,
        };

    // A type's fully qualified name with a method's own type parameters written by position (!!0, as in IL), so the
    // parameter types of two methods compare the same whatever the methods name their type parameters.
    internal static string ToSignatureKey(this ITypeSymbol type) =>
        type switch
        {
            ITypeParameterSymbol { TypeParameterKind: TypeParameterKind.Method } typeParameter =>
                $"!!{typeParameter.Ordinal}",
            IArrayTypeSymbol arrayType =>
                $"{arrayType.ElementType.ToSignatureKey()}[{new string(',', arrayType.Rank - 1)}]",
            IPointerTypeSymbol pointerType => $"{pointerType.PointedAtType.ToSignatureKey()}*",
            INamedTypeSymbol namedType when AllTypeArguments(namedType).Any() => ConstructedTypeKey(
                namedType
            ),
            _ => type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
        };

    // A constructed type's definition, followed by its type arguments.
    private static string ConstructedTypeKey(INamedTypeSymbol type)
    {
        var definition = type.OriginalDefinition.ToDisplayString(
            SymbolDisplayFormat.FullyQualifiedFormat
        );
        var typeArguments = AllTypeArguments(type).Select(it => it.ToSignatureKey());

        return $"{definition}[{string.Join(",", typeArguments)}]";
    }

    // The type arguments of a type and of the types it's nested in, such as Outer<T>.Inner<U>.
    private static IEnumerable<ITypeSymbol> AllTypeArguments(INamedTypeSymbol type) =>
        type.ContainingType is { } containingType
            ? AllTypeArguments(containingType).Concat(type.TypeArguments)
            : type.TypeArguments;

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
    internal TypeSymbolMetadata(TypeSyntax typeSyntax, TypeSyntax nullableTypeSyntax)
    {
        TypeSyntax = typeSyntax;
        NullableTypeSyntax = nullableTypeSyntax;
    }

    internal TypeSyntax TypeSyntax { get; }

    internal TypeSyntax NullableTypeSyntax { get; }
}
