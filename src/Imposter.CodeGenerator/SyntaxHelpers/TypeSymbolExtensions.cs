using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class TypeSymbolExtensions
{
    internal static bool IsWellKnownType(this ITypeSymbol? symbol, TypeSyntax typeSyntax, params string[] assemblyNames)
    {
        if (symbol is not INamedTypeSymbol named)
        {
            return false;
        }

        var definition = named.IsGenericType ? named.ConstructedFrom : named;
        var assemblyName = definition.ContainingAssembly?.Identity.Name;

        if (assemblyName is null ||
            assemblyNames.Length == 0 ||
            Array.IndexOf(assemblyNames, assemblyName) == -1)
        {
            return false;
        }

        var wellKnownTypeName = typeSyntax.NormalizeWhitespace().ToFullString();
        var candidateName = definition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        return string.Equals(candidateName, wellKnownTypeName, StringComparison.Ordinal);
    }
    
    internal static bool IsTaskLike(this ITypeSymbol? symbol) => symbol.GetTaskLikeMetadata().IsTaskLike;

    internal static TaskLikeMetadata GetTaskLikeMetadata(this ITypeSymbol? symbol)
    {
        if (symbol is not INamedTypeSymbol named)
        {
            return TaskLikeMetadata.Empty;
        }

        var definition = named.IsGenericType ? named.ConstructedFrom : named;
        var isTask = definition.IsWellKnownType(WellKnownTypes.System.Threading.Tasks.Task, WellKnownAssemblyNames.SystemAssemblies);
        var isValueTask = definition.IsWellKnownType(WellKnownTypes.System.Threading.Tasks.ValueTask, WellKnownAssemblyNames.SystemAssemblies);

        var genericParameter = IdentifierName("TResult");
        var genericTask = WellKnownTypes.System.Threading.Tasks.TaskOfT(genericParameter);
        var genericValueTask = WellKnownTypes.System.Threading.Tasks.ValueTaskOfT(genericParameter);

        var isGenericTask = named.IsGenericType && definition.IsWellKnownType(genericTask, WellKnownAssemblyNames.SystemAssemblies);
        var isGenericValueTask = named.IsGenericType && definition.IsWellKnownType(genericValueTask, WellKnownAssemblyNames.SystemAssemblies);

        ITypeSymbol? asyncValueTypeSymbol = (isGenericTask || isGenericValueTask) && named.TypeArguments.Length > 0
            ? named.TypeArguments[0]
            : null;

        return new TaskLikeMetadata(
            isTask: isTask || isGenericTask,
            isGenericTask: isGenericTask,
            isValueTask: isValueTask || isGenericValueTask,
            isGenericValueTask: isGenericValueTask,
            asyncValueTypeSymbol: asyncValueTypeSymbol);
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
        symbol.IsWellKnownType(WellKnownTypes.System.Runtime.CompilerServices.IAsyncStateMachine, WellKnownAssemblyNames.SystemAssemblies);
}

internal readonly struct TaskLikeMetadata
{
    internal static TaskLikeMetadata Empty => default;

    internal TaskLikeMetadata(
        bool isTask,
        bool isGenericTask,
        bool isValueTask,
        bool isGenericValueTask,
        ITypeSymbol? asyncValueTypeSymbol)
    {
        IsTask = isTask;
        IsGenericTask = isGenericTask;
        IsValueTask = isValueTask;
        IsGenericValueTask = isGenericValueTask;
        AsyncValueTypeSymbol = asyncValueTypeSymbol;
    }

    internal bool IsTask { get; }

    internal bool IsGenericTask { get; }

    internal bool IsValueTask { get; }

    internal bool IsGenericValueTask { get; }

    internal bool SupportsAsyncValueResult => AsyncValueTypeSymbol is not null;

    internal bool IsTaskLike => IsTask || IsValueTask;

    internal ITypeSymbol? AsyncValueTypeSymbol { get; }
}
