using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class TypeSymbolExtensions
{
    internal static bool IsWellKnownType(
        this ITypeSymbol? symbol,
        TypeSyntax typeSyntax,
        params string[] assemblyNames
    )
    {
        if (symbol is not INamedTypeSymbol named)
        {
            return false;
        }

        var definition = named.IsGenericType ? named.ConstructedFrom : named;
        var assemblyName = definition.ContainingAssembly?.Identity.Name;

        if (
            assemblyName is null
            || assemblyNames.Length == 0
            || Array.IndexOf(assemblyNames, assemblyName) == -1
        )
        {
            return false;
        }

        var wellKnownTypeName = typeSyntax.NormalizeWhitespace().ToFullString();
        var candidateName = definition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        return string.Equals(candidateName, wellKnownTypeName, StringComparison.Ordinal);
    }

    internal static bool IsAwaitable(this ITypeSymbol? symbol) =>
        symbol.GetTaskLikeMetadata().IsAwaitable;

    internal static TaskLikeMetadata GetTaskLikeMetadata(this ITypeSymbol? symbol)
    {
        if (symbol is not INamedTypeSymbol named)
        {
            return TaskLikeMetadata.Empty;
        }

        var definition = named.IsGenericType ? named.ConstructedFrom : named;
        var isTask = definition.IsWellKnownType(
            WellKnownTypes.System.Threading.Tasks.Task,
            WellKnownAssemblyNames.SystemAssemblies
        );
        var isValueTask = definition.IsWellKnownType(
            WellKnownTypes.System.Threading.Tasks.ValueTask,
            WellKnownAssemblyNames.SystemAssemblies
        );

        var genericParameter = IdentifierName("TResult");
        var genericTask = WellKnownTypes.System.Threading.Tasks.TaskOfT(genericParameter);
        var genericValueTask = WellKnownTypes.System.Threading.Tasks.ValueTaskOfT(genericParameter);
        var genericAsyncEnumerable = WellKnownTypes.System.Collections.Generic.IAsyncEnumerable(
            genericParameter
        );
        var genericAsyncEnumerator = WellKnownTypes.System.Collections.Generic.IAsyncEnumerator(
            genericParameter
        );

        var isGenericTask =
            named.IsGenericType
            && definition.IsWellKnownType(genericTask, WellKnownAssemblyNames.SystemAssemblies);
        var isGenericValueTask =
            named.IsGenericType
            && definition.IsWellKnownType(
                genericValueTask,
                WellKnownAssemblyNames.SystemAssemblies
            );
        var isAsyncEnumerable =
            named.IsGenericType
            && definition.IsWellKnownType(
                genericAsyncEnumerable,
                WellKnownAssemblyNames.SystemAssemblies
            );
        var isAsyncEnumerator =
            named.IsGenericType
            && definition.IsWellKnownType(
                genericAsyncEnumerator,
                WellKnownAssemblyNames.SystemAssemblies
            );
        var isAwaitable =
            isTask
            || isGenericTask
            || isValueTask
            || isGenericValueTask
            || isAsyncEnumerable
            || isAsyncEnumerator;

        var genericAwaitableResultType =
            isAwaitable && named.TypeArguments.Length > 0 ? named.TypeArguments[0] : null;

        return new TaskLikeMetadata(
            isTask: isTask || isGenericTask,
            isGenericTask: isGenericTask,
            isValueTask: isValueTask || isGenericValueTask,
            isGenericValueTask: isGenericValueTask,
            isAsyncEnumerable: isAsyncEnumerable,
            isAsyncEnumerator: isAsyncEnumerator,
            isAwaitable: isAwaitable,
            genericAwaitableResultType: genericAwaitableResultType
        );
    }

    internal static TypeSymbolMetadata GetTypeSymbolMetadata(
        this ITypeSymbol? symbol,
        bool isAwaitable,
        bool supportsNullableGenericType
    )
    {
        if (symbol is null)
        {
            return TypeSymbolMetadata.Empty;
        }

        var typeSyntax = SyntaxFactoryHelper.TypeSyntax(symbol);
        var isGenericType = symbol.TypeKind == TypeKind.TypeParameter;
        var isNullableType = typeSyntax is NullableTypeSyntax;
        var isConstructedGenericType = typeSyntax is GenericNameSyntax;
        var shouldConvertToNullable =
            !isNullableType
            && symbol.SpecialType != SpecialType.System_Void
            && !isAwaitable
            && !((isGenericType || isConstructedGenericType) && !supportsNullableGenericType);

        var nullableTypeSyntax = shouldConvertToNullable ? typeSyntax.ToNullableType() : typeSyntax;

        return new TypeSymbolMetadata(typeSyntax, isGenericType, nullableTypeSyntax);
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
        symbol.IsWellKnownType(
            WellKnownTypes.System.Runtime.CompilerServices.IAsyncStateMachine,
            WellKnownAssemblyNames.SystemAssemblies
        );
}

internal readonly struct TaskLikeMetadata
{
    internal static TaskLikeMetadata Empty => default;

    internal TaskLikeMetadata(
        bool isTask,
        bool isGenericTask,
        bool isValueTask,
        bool isGenericValueTask,
        bool isAsyncEnumerable,
        bool isAsyncEnumerator,
        bool isAwaitable,
        ITypeSymbol? genericAwaitableResultType
    )
    {
        IsTask = isTask;
        IsGenericTask = isGenericTask;
        IsValueTask = isValueTask;
        IsGenericValueTask = isGenericValueTask;
        IsAsyncEnumerable = isAsyncEnumerable;
        IsAsyncEnumerator = isAsyncEnumerator;
        GenericAwaitableResultType = genericAwaitableResultType;
        IsAwaitable = isAwaitable;
    }

    internal bool IsTask { get; }

    internal bool IsGenericTask { get; }

    internal bool IsValueTask { get; }

    internal bool IsGenericValueTask { get; }

    internal bool IsAsyncEnumerable { get; }

    internal bool IsAsyncEnumerator { get; }

    internal bool IsAwaitable { get; }

    internal ITypeSymbol? GenericAwaitableResultType { get; }
}

internal readonly struct TypeSymbolMetadata
{
    internal static TypeSymbolMetadata Empty => default;

    internal TypeSymbolMetadata(
        TypeSyntax typeSyntax,
        bool isGenericType,
        TypeSyntax nullableTypeSyntax
    )
    {
        TypeSyntax = typeSyntax;
        IsGenericType = isGenericType;
        NullableTypeSyntax = nullableTypeSyntax;
    }

    internal TypeSyntax TypeSyntax { get; }

    internal bool IsGenericType { get; }

    internal TypeSyntax NullableTypeSyntax { get; }
}
