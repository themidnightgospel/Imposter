using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// Everything the generator reads from a target to build its imposter. Members are in the order the imposter emits
/// them, and only the ones the imposter's assembly can override or implement are included.
/// </summary>
internal sealed record ImposterTargetModel(
    string Name,
    string DisplayName,
    TypeModel Type,
    NamespaceModel ContainingNamespace,
    bool IsClass,
    Accessibility DeclaredAccessibility,
    EquatableArray<TypeParameterModel> TypeParameters,
    EquatableArray<string> MemberNames,
    EquatableArray<TargetMemberModel<MethodModel>> Methods,
    EquatableArray<TargetMemberModel<PropertyModel>> Properties,
    EquatableArray<TargetMemberModel<PropertyModel>> Indexers,
    EquatableArray<TargetMemberModel<EventModel>> Events,
    EquatableArray<ConstructorModel> AccessibleConstructors,
    InterfaceSetupTargetModel? InterfaceSetup
)
{
    internal static ImposterTargetModel From(INamedTypeSymbol target, MemberAccess memberAccess)
    {
        var isInterface = target.TypeKind is TypeKind.Interface;
        var methods = GetMethods(target, memberAccess);
        var properties = GetProperties(target, memberAccess);
        var indexers = properties.Where(property => property.IsIndexer).ToArray();
        var nonIndexers = properties.Where(property => !property.IsIndexer).ToArray();
        var events = GetEvents(target, memberAccess);

        return new ImposterTargetModel(
            target.Name,
            target.ToDisplayString(),
            TypeModel.From(target),
            NamespaceModel.From(target.ContainingNamespace),
            target.TypeKind is TypeKind.Class,
            target.DeclaredAccessibility,
            target.TypeParameters.Select(TypeParameterModel.From).ToEquatableArray(),
            target
                .GetMembers()
                .Select(member => member.Name)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToEquatableArray(),
            ToTargetMembers(
                methods,
                isInterface ? DetectCollisions(methods, MethodKey) : null,
                method => MethodModel.From(method, memberAccess),
                isInterface
            ),
            ToTargetMembers(
                InEmitOrder(nonIndexers),
                isInterface ? DetectCollisions(nonIndexers, property => property.Name) : null,
                property => PropertyModel.From(property, memberAccess),
                isInterface
            ),
            ToTargetMembers(
                InEmitOrder(indexers),
                isInterface ? DetectCollisions(indexers, IndexerKey) : null,
                indexer => PropertyModel.From(indexer, memberAccess),
                isInterface
            ),
            ToTargetMembers(
                InEmitOrder(events),
                isInterface ? DetectCollisions(events, @event => @event.Name) : null,
                @event => EventModel.From(@event, memberAccess),
                isInterface
            ),
            GetAccessibleConstructors(target, memberAccess),
            isInterface ? InterfaceSetupTargetModel.From(target) : null
        );
    }

    private static EquatableArray<TargetMemberModel<TModel>> ToTargetMembers<TSymbol, TModel>(
        IEnumerable<TSymbol> members,
        HashSet<TSymbol>? collidingMembers,
        Func<TSymbol, TModel> createModel,
        bool isInterface
    )
        where TSymbol : class, ISymbol
        where TModel : IEquatable<TModel> =>
        members
            .Select(member => new TargetMemberModel<TModel>(
                createModel(member),
                collidingMembers?.Contains(member) == true,
                isInterface ? InterfaceSetupMemberModel.From(member) : null
            ))
            .ToEquatableArray();

    internal static IReadOnlyCollection<IMethodSymbol> GetMethods(
        INamedTypeSymbol target,
        MemberAccess memberAccess
    ) =>
        target.TypeKind switch
        {
            TypeKind.Interface => target.GetAllInterfaceMethods(),
            TypeKind.Class => target
                .GetAllOverridableMethods()
                .Where(memberAccess.IsAccessible)
                .ToArray(),
            _ => [],
        };

    internal static IReadOnlyCollection<IPropertySymbol> GetProperties(
        INamedTypeSymbol target,
        MemberAccess memberAccess
    ) =>
        target.TypeKind switch
        {
            TypeKind.Interface => target.GetAllInterfaceProperties(),
            TypeKind.Class => target
                .GetAllOverridableProperties()
                .Where(memberAccess.IsAccessible)
                .ToArray(),
            _ => [],
        };

    internal static IReadOnlyCollection<IEventSymbol> GetEvents(
        INamedTypeSymbol target,
        MemberAccess memberAccess
    ) =>
        target.TypeKind switch
        {
            TypeKind.Interface => target.GetAllInterfaceEvents(),
            TypeKind.Class => target
                .GetAllOverridableEvents()
                .Where(memberAccess.IsAccessible)
                .ToArray(),
            _ => [],
        };

    private static IEnumerable<TSymbol> InEmitOrder<TSymbol>(IEnumerable<TSymbol> members)
        where TSymbol : ISymbol =>
        members
            .OrderBy(member => member.MetadataName, StringComparer.Ordinal)
            .ThenBy(
                member => member.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                StringComparer.Ordinal
            );

    // Members of different interfaces that collide on the instance are implemented explicitly.
    private static HashSet<TSymbol> DetectCollisions<TSymbol>(
        IEnumerable<TSymbol> members,
        Func<TSymbol, string> key
    )
        where TSymbol : class, ISymbol =>
        new(
            members.GroupBy(key).Where(group => group.Count() > 1).SelectMany(group => group),
            SymbolEqualityComparer.Default
        );

    private static string MethodKey(IMethodSymbol method) =>
        $"{method.Name}({ParameterTypesKey(method.Parameters)})";

    // Indexers with the same parameter types collide on the instance, and their setup indexers, which take Arg<T>
    // whatever the ref kind, would collide on the imposter.
    private static string IndexerKey(IPropertySymbol indexer) =>
        ParameterTypesKey(indexer.Parameters);

    private static string ParameterTypesKey(IEnumerable<IParameterSymbol> parameters) =>
        string.Join(
            ",",
            parameters.Select(parameter =>
                parameter.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            )
        );

    private static EquatableArray<ConstructorModel> GetAccessibleConstructors(
        INamedTypeSymbol target,
        MemberAccess memberAccess
    )
    {
        if (target.TypeKind is not TypeKind.Class)
        {
            return default;
        }

        var declaredConstructors = target
            .InstanceConstructors.Where(constructor =>
                !constructor.IsImplicitlyDeclared && memberAccess.IsAccessible(constructor)
            )
            .Select(constructor => new ConstructorModel(
                constructor.Parameters.Select(ParameterModel.From).ToEquatableArray()
            ))
            .ToArray();

        if (declaredConstructors.Length > 0)
        {
            return declaredConstructors.ToEquatableArray();
        }

        return target.InstanceConstructors.Any(constructor => !constructor.IsImplicitlyDeclared)
            ? default
            : new[] { new ConstructorModel(default) }.ToEquatableArray();
    }
}
