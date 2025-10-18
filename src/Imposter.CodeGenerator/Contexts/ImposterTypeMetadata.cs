using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Contexts;

internal class ImposterTypeMetadata
{
    internal readonly string Name;

    internal readonly INamedTypeSymbol TargetSymbol;

    internal readonly IReadOnlyList<ImposterTargetMethodMetadata> Methods;

    internal readonly SymbolNameContext SymbolNameContext;

    internal ImposterTypeMetadata(INamedTypeSymbol targetSymbol)
    {
        SymbolNameContext = new SymbolNameContext([]);
        TargetSymbol = targetSymbol;
        Name = targetSymbol.Name + "Imposter";
        Methods = GetMethods(targetSymbol, SymbolNameContext);
    }

    private static IReadOnlyList<ImposterTargetMethodMetadata> GetMethods(INamedTypeSymbol typeSymbol, SymbolNameContext symbolNameContext)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceMethods()
                .Select(method => new ImposterTargetMethodMetadata(method, symbolNameContext.Use(method.Name)))
                .ToList();
        }

        // TODO Add class support.
        throw new InvalidOperationException("Only interfaces are supported");
    }
}