using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterTargetMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax ImposterTypeSyntax;

    internal readonly TypeSyntax TargetTypeSyntax;

    internal readonly bool IsClass;

    internal readonly Accessibility DeclaredAccessibility;

    internal readonly ImposterTargetConstructorMetadata[] AccessibleConstructors;

    internal readonly List<ImposterTargetMethodMetadata> Methods;

    internal readonly IReadOnlyCollection<IPropertySymbol> PropertySymbols;

    internal readonly IReadOnlyCollection<IPropertySymbol> IndexerSymbols;

    internal readonly IReadOnlyCollection<IEventSymbol> EventSymbols;

    private readonly HashSet<IPropertySymbol> _explicitProperties;

    private readonly HashSet<IEventSymbol> _explicitEvents;

    internal readonly ImposterTargetTypeParametersMetadata TypeParameters;

    private readonly NameSet _symbolNameNamespace = new([]);

    internal ImposterTargetMetadata(
        INamedTypeSymbol targetSymbol,
        in SupportedCSharpFeatures supportedCSharpFeatures
    )
    {
        Name = targetSymbol.Name + "Imposter";
        TypeParameters = new ImposterTargetTypeParametersMetadata(targetSymbol);
        ImposterTypeSyntax = SyntaxFactoryHelper.WithMethodGenericArguments(
            TypeParameters.TypeArguments,
            Name
        );
        TargetTypeSyntax = SyntaxFactoryHelper.TypeSyntax(targetSymbol);
        Methods = GetMethods(targetSymbol, _symbolNameNamespace, supportedCSharpFeatures);
        IsClass = targetSymbol.TypeKind is TypeKind.Class;
        DeclaredAccessibility = targetSymbol.DeclaredAccessibility;
        AccessibleConstructors = GetAccessibleConstructors(targetSymbol);

        var propertySymbols = GetPropertySymbols(targetSymbol);
        PropertySymbols = propertySymbols.Where(property => !property.IsIndexer).ToArray();
        IndexerSymbols = propertySymbols.Where(property => property.IsIndexer).ToArray();
        EventSymbols = GetEventSymbols(targetSymbol);

        _explicitProperties =
            targetSymbol.TypeKind is TypeKind.Interface
                ? DetectExplicitInterfaceProperties(PropertySymbols)
                : new HashSet<IPropertySymbol>(SymbolEqualityComparer.Default);
        _explicitEvents =
            targetSymbol.TypeKind is TypeKind.Interface
                ? DetectExplicitInterfaceEvents(EventSymbols)
                : new HashSet<IEventSymbol>(SymbolEqualityComparer.Default);
    }

    private static List<ImposterTargetMethodMetadata> GetMethods(
        INamedTypeSymbol typeSymbol,
        NameSet nameSet,
        in SupportedCSharpFeatures supportedCSharpFeatures
    )
    {
        var supportsNullableGenericType = supportedCSharpFeatures.SupportsNullableGenericType;

        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            var allMethods = typeSymbol.GetAllInterfaceMethods();
            var explicitMethods = DetectExplicitInterfaceMethods(allMethods);

            return allMethods
                .Select(methodSymbol => new ImposterTargetMethodMetadata(
                    methodSymbol,
                    nameSet.Use(methodSymbol.Name),
                    supportsNullableGenericType,
                    explicitMethods.Contains(methodSymbol)
                ))
                .ToList();
        }

        if (typeSymbol.TypeKind is TypeKind.Class)
        {
            return typeSymbol
                .GetAllOverridableMethods()
                .Select(methodSymbol => new ImposterTargetMethodMetadata(
                    methodSymbol,
                    nameSet.Use(methodSymbol.Name),
                    supportsNullableGenericType
                ))
                .ToList();
        }

        return [];
    }

    private static ImposterTargetConstructorMetadata[] GetAccessibleConstructors(
        INamedTypeSymbol typeSymbol
    )
    {
        if (typeSymbol.TypeKind is not TypeKind.Class)
        {
            return [];
        }

        var declaredConstructors = typeSymbol
            .InstanceConstructors.Where(constructor =>
                !constructor.IsImplicitlyDeclared
                && constructor.DeclaredAccessibility != Accessibility.Private
            )
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
                ImposterTargetConstructorMetadata.CreateImplicitParameterless(
                    typeSymbol.DeclaredAccessibility
                ),
            };
        }

        return [];
    }

    private static IReadOnlyCollection<IPropertySymbol> GetPropertySymbols(
        INamedTypeSymbol typeSymbol
    )
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

    internal ImposterPropertyMetadata CreatePropertyMetadata(
        IPropertySymbol propertySymbol,
        NameSet memberNameSet
    ) =>
        new(
            propertySymbol,
            _symbolNameNamespace.Use(propertySymbol.Name),
            memberNameSet,
            _explicitProperties.Contains(propertySymbol)
        );

    internal ImposterIndexerMetadata CreateIndexerMetadata(IPropertySymbol propertySymbol) =>
        new(
            propertySymbol,
            _symbolNameNamespace.Use(propertySymbol.IsIndexer ? "Indexer" : propertySymbol.Name)
        );

    internal ImposterEventMetadata CreateEventMetadata(IEventSymbol eventSymbol) =>
        new(
            eventSymbol,
            _symbolNameNamespace.Use(eventSymbol.Name),
            _explicitEvents.Contains(eventSymbol)
        );

    private static HashSet<IPropertySymbol> DetectExplicitInterfaceProperties(
        IReadOnlyCollection<IPropertySymbol> properties
    )
    {
        var result = new HashSet<IPropertySymbol>(SymbolEqualityComparer.Default);

        var groups = properties.GroupBy(p => p.Name);
        foreach (var group in groups)
        {
            var members = group.ToList();
            if (members.Count > 1)
            {
                foreach (var member in members)
                {
                    result.Add(member);
                }
            }
        }

        return result;
    }

    private static HashSet<IEventSymbol> DetectExplicitInterfaceEvents(
        IReadOnlyCollection<IEventSymbol> events
    )
    {
        var result = new HashSet<IEventSymbol>(SymbolEqualityComparer.Default);

        var groups = events.GroupBy(e => e.Name);
        foreach (var group in groups)
        {
            var members = group.ToList();
            if (members.Count > 1)
            {
                foreach (var member in members)
                {
                    result.Add(member);
                }
            }
        }

        return result;
    }

    private static HashSet<IMethodSymbol> DetectExplicitInterfaceMethods(
        IReadOnlyCollection<IMethodSymbol> methods
    )
    {
        var result = new HashSet<IMethodSymbol>(SymbolEqualityComparer.Default);

        var groups = methods.GroupBy(m => GetMethodSignatureKey(m));
        foreach (var group in groups)
        {
            var members = group.ToList();
            if (members.Count > 1)
            {
                foreach (var member in members)
                {
                    result.Add(member);
                }
            }
        }

        return result;
    }

    private static string GetMethodSignatureKey(IMethodSymbol method)
    {
        var paramTypes = string.Join(
            ",",
            method.Parameters.Select(p =>
                p.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            )
        );
        return $"{method.Name}({paramTypes})";
    }

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
