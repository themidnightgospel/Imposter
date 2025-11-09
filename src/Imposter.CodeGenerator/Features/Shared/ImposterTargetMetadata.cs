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

    internal readonly bool IsClass;

    internal readonly ImposterTargetConstructorMetadata[] AccessibleConstructors;

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
        IsClass = targetSymbol.TypeKind is TypeKind.Class;
        AccessibleConstructors = GetAccessibleConstructors(targetSymbol);

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

        if (typeSymbol.TypeKind is TypeKind.Class)
        {
            return typeSymbol
                .GetAllOverridableMethods()
                .Select(methodSymbol => new ImposterTargetMethodMetadata(methodSymbol, nameSet.Use(methodSymbol.Name)))
                .ToList();
        }

        return [];
    }

    private static ImposterTargetConstructorMetadata[] GetAccessibleConstructors(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.TypeKind is not TypeKind.Class)
        {
            return [];
        }

        var declaredConstructors = typeSymbol.InstanceConstructors
            .Where(constructor => !constructor.IsImplicitlyDeclared && constructor.DeclaredAccessibility != Accessibility.Private)
            .Select(ImposterTargetConstructorMetadata.FromSymbol)
            .ToArray();

        if (declaredConstructors.Length > 0)
        {
            return declaredConstructors;
        }

        if (!typeSymbol.InstanceConstructors.Any(constructor => !constructor.IsImplicitlyDeclared))
        {
            return new[]
            {
                ImposterTargetConstructorMetadata.CreateImplicitParameterless(typeSymbol.DeclaredAccessibility),
            };
        }

        return [];
    }

    private static IReadOnlyCollection<IPropertySymbol> GetPropertySymbols(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol.GetAllInterfaceProperties();
        }

        if (typeSymbol.TypeKind is TypeKind.Class)
        {
            return typeSymbol.GetAllOverridableProperties();
        }

        return [];
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

        if (typeSymbol.TypeKind is TypeKind.Class)
        {
            return typeSymbol.GetAllOverridableEvents();
        }

        return [];
    }
}
