using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImposter.Metadata;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
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

    internal readonly IReadOnlyCollection<IPropertySymbol> IndexerSymbols;

    internal readonly IReadOnlyCollection<IEventSymbol> EventSymbols;

    private readonly NameSet _symbolNameNamespace = new([]);

    internal ImposterTargetMetadata(INamedTypeSymbol targetSymbol)
    {
        Name = targetSymbol.Name + "Imposter";
        Methods = GetMethods(targetSymbol, _symbolNameNamespace);

        var propertySymbols = GetPropertySymbols(targetSymbol);
        PropertySymbols = propertySymbols.Where(property => !property.IsIndexer).ToArray();
        IndexerSymbols = propertySymbols.Where(property => property.IsIndexer).ToArray();
        EventSymbols = GetEventSymbols(targetSymbol);
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

    private static IReadOnlyCollection<IPropertySymbol> GetPropertySymbols(INamedTypeSymbol typeSymbol)
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

    internal ImposterIndexerMetadata CreateIndexerMetadata(IPropertySymbol propertySymbol)
        => new(propertySymbol, _symbolNameNamespace.Use(propertySymbol.IsIndexer ? "Indexer" : propertySymbol.Name));

    internal ImposterEventMetadata CreateEventMetadata(IEventSymbol eventSymbol)
        => new(eventSymbol, _symbolNameNamespace.Use(eventSymbol.Name));

    private static IReadOnlyCollection<IEventSymbol> GetEventSymbols(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol.GetAllInterfaceEvents();
        }

        throw new InvalidOperationException("Only interfaces are supported");
    }
}
