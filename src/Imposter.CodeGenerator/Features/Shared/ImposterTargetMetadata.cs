using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterTargetMetadata
{
    internal readonly string Name;

    // TODO this will make ImposterTargetMethodMetadata allocate on heap
    internal readonly List<ImposterTargetMethodMetadata> Methods;

    internal readonly IReadOnlyCollection<IPropertySymbol> PropertySymbols;

    private readonly NameSet _symbolNameNamespace = new([]);

    internal ImposterTargetMetadata(INamedTypeSymbol targetSymbol)
    {
        Name = targetSymbol.Name + "Imposter";
        Methods = GetMethods(targetSymbol, _symbolNameNamespace);
        PropertySymbols = GetPropertySymbols(targetSymbol, _symbolNameNamespace);
    }

    private static List<ImposterTargetMethodMetadata> GetMethods(INamedTypeSymbol typeSymbol, NameSet nameSet)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceMethods()
                .Select(methodSymbol => new ImposterTargetMethodMetadata(methodSymbol, nameSet.Use(methodSymbol.Name)))
                .ToList();
        }

        // TODO Add class support.
        throw new InvalidOperationException("Only interfaces are supported");
    }

    private static IReadOnlyCollection<IPropertySymbol> GetPropertySymbols(INamedTypeSymbol typeSymbol, NameSet nameSet)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol.GetAllInterfaceProperties();
        }

        // TODO Add class support.
        throw new InvalidOperationException("Only interfaces are supported");
    }

    internal ImposterTargetMethodMetadata CreateMethodMetadata(IMethodSymbol methodSymbol)
        => new ImposterTargetMethodMetadata(methodSymbol, _symbolNameNamespace.Use(methodSymbol.Name));

    internal ImposterPropertyMetadata CreatePropertyMetadata(IPropertySymbol propertySymbol)
        => new(propertySymbol, _symbolNameNamespace.Use(propertySymbol.Name));
}