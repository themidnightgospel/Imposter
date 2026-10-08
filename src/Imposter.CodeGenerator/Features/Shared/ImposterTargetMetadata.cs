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
    internal const string IndexerMemberName = "Indexer";

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

    private readonly HashSet<IPropertySymbol> _explicitIndexers;

    private readonly MemberAccess _memberAccess;

    internal readonly ImposterTargetTypeParametersMetadata TypeParameters;

    private readonly NameSet _symbolNameNamespace = new([]);

    internal ImposterTargetMetadata(
        INamedTypeSymbol targetSymbol,
        in SupportedCSharpFeatures supportedCSharpFeatures,
        MemberAccess memberAccess
    )
    {
        _memberAccess = memberAccess;
        Name = targetSymbol.Name + "Imposter";
        TypeParameters = new ImposterTargetTypeParametersMetadata(targetSymbol);
        ImposterTypeSyntax = SyntaxFactoryHelper.WithMethodGenericArguments(
            TypeParameters.TypeArguments,
            Name
        );
        TargetTypeSyntax = SyntaxFactoryHelper.TypeSyntax(targetSymbol);
        Methods = GetMethods(
            targetSymbol,
            _symbolNameNamespace,
            supportedCSharpFeatures,
            memberAccess
        );
        IsClass = targetSymbol.TypeKind is TypeKind.Class;
        DeclaredAccessibility = targetSymbol.DeclaredAccessibility;
        AccessibleConstructors = GetAccessibleConstructors(targetSymbol, memberAccess);

        var propertySymbols = GetPropertySymbols(targetSymbol, memberAccess);
        PropertySymbols = propertySymbols.Where(property => !property.IsIndexer).ToArray();
        IndexerSymbols = propertySymbols.Where(property => property.IsIndexer).ToArray();
        EventSymbols = GetEventSymbols(targetSymbol, memberAccess);

        _explicitProperties =
            targetSymbol.TypeKind is TypeKind.Interface
                ? DetectExplicitInterfaceProperties(PropertySymbols)
                : new HashSet<IPropertySymbol>(SymbolEqualityComparer.Default);
        _explicitEvents =
            targetSymbol.TypeKind is TypeKind.Interface
                ? DetectExplicitInterfaceEvents(EventSymbols)
                : new HashSet<IEventSymbol>(SymbolEqualityComparer.Default);
        _explicitIndexers =
            targetSymbol.TypeKind is TypeKind.Interface
                ? DetectExplicitInterfaceIndexers(IndexerSymbols)
                : new HashSet<IPropertySymbol>(SymbolEqualityComparer.Default);
    }

    private static List<ImposterTargetMethodMetadata> GetMethods(
        INamedTypeSymbol typeSymbol,
        NameSet nameSet,
        in SupportedCSharpFeatures supportedCSharpFeatures,
        MemberAccess memberAccess
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
                    memberAccess,
                    explicitMethods.Contains(methodSymbol)
                ))
                .ToList();
        }

        if (typeSymbol.TypeKind is TypeKind.Class)
        {
            return typeSymbol
                .GetAllOverridableMethods()
                .Where(memberAccess.IsAccessible)
                .Select(methodSymbol => new ImposterTargetMethodMetadata(
                    methodSymbol,
                    nameSet.Use(methodSymbol.Name),
                    supportsNullableGenericType,
                    memberAccess
                ))
                .ToList();
        }

        return [];
    }

    private static ImposterTargetConstructorMetadata[] GetAccessibleConstructors(
        INamedTypeSymbol typeSymbol,
        MemberAccess memberAccess
    )
    {
        if (typeSymbol.TypeKind is not TypeKind.Class)
        {
            return [];
        }

        var declaredConstructors = typeSymbol
            .InstanceConstructors.Where(constructor =>
                !constructor.IsImplicitlyDeclared && memberAccess.IsAccessible(constructor)
            )
            .Select(ImposterTargetConstructorMetadata.FromSymbol)
            .ToArray();

        if (declaredConstructors.Length > 0)
        {
            return declaredConstructors;
        }

        if (!typeSymbol.InstanceConstructors.Any(constructor => !constructor.IsImplicitlyDeclared))
        {
            return new[] { ImposterTargetConstructorMetadata.CreateImplicitParameterless() };
        }

        return [];
    }

    private static IReadOnlyCollection<IPropertySymbol> GetPropertySymbols(
        INamedTypeSymbol typeSymbol,
        MemberAccess memberAccess
    )
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol.GetAllInterfaceProperties();
        }

        if (typeSymbol.TypeKind is TypeKind.Class)
        {
            return typeSymbol
                .GetAllOverridableProperties()
                .Where(memberAccess.IsAccessible)
                .ToArray();
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
            _memberAccess,
            _explicitProperties.Contains(propertySymbol)
        );

    internal ImposterIndexerMetadata CreateIndexerMetadata(IPropertySymbol propertySymbol) =>
        new(
            propertySymbol,
            _symbolNameNamespace.Use(IndexerMemberName),
            _memberAccess,
            _explicitIndexers.Contains(propertySymbol)
        );

    // Indexers of different interfaces with the same parameter types would collide on the instance, and their setup
    // indexers, which take Arg<T> whatever the ref kind, would collide on the imposter.
    private static HashSet<IPropertySymbol> DetectExplicitInterfaceIndexers(
        IReadOnlyCollection<IPropertySymbol> indexers
    ) =>
        new(
            indexers
                .GroupBy(indexer =>
                    string.Join(
                        ",",
                        indexer.Parameters.Select(parameter =>
                            parameter.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                        )
                    )
                )
                .Where(group => group.Count() > 1)
                .SelectMany(group => group),
            SymbolEqualityComparer.Default
        );

    internal ImposterEventMetadata CreateEventMetadata(IEventSymbol eventSymbol) =>
        new(
            eventSymbol,
            _symbolNameNamespace.Use(eventSymbol.Name),
            _memberAccess,
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

    private static IReadOnlyCollection<IEventSymbol> GetEventSymbols(
        INamedTypeSymbol typeSymbol,
        MemberAccess memberAccess
    )
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol.GetAllInterfaceEvents();
        }

        if (typeSymbol.TypeKind is TypeKind.Class)
        {
            return typeSymbol.GetAllOverridableEvents().Where(memberAccess.IsAccessible).ToArray();
        }

        return [];
    }
}
