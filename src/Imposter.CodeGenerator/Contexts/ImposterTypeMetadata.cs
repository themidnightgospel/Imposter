using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Contexts;


internal readonly struct ImposterTypeMetadata
{
    internal readonly string Name;

    internal readonly INamedTypeSymbol TargetSymbol;

    internal readonly IReadOnlyList<ImposterTargetMethodMetadata> Methods;

    internal readonly SymbolNameNamespace SymbolNameNamespace;

    internal ImposterTypeMetadata(INamedTypeSymbol targetSymbol)
    {
        SymbolNameNamespace = new SymbolNameNamespace([]);
        TargetSymbol = targetSymbol;
        Name = targetSymbol.Name + "Imposter";
        Methods = GetMethods(targetSymbol, SymbolNameNamespace);
    }

    private static IReadOnlyList<ImposterTargetMethodMetadata> GetMethods(INamedTypeSymbol typeSymbol, SymbolNameNamespace symbolNameNamespace)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceMethods()
                .Select(method => new ImposterTargetMethodMetadata(method, symbolNameNamespace.Use(method.Name)))
                .ToList();
        }

        // TODO Add class support.
        throw new InvalidOperationException("Only interfaces are supported");
    }
}