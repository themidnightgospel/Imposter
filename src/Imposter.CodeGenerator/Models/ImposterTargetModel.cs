using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// Everything the generator reads from a target to build its imposter. Properties, indexers and events are in the
/// order the imposter emits them; ImposterGenerator orders the methods when it emits them. Only the members the
/// imposter's assembly can override or implement are included.
/// <see cref="HasRequiredMembers"/> is true for a class with C# 11 required members, which the imposter's
/// <c>new</c> of its instance can skip only through <c>[SetsRequiredMembers]</c>.
/// </summary>
internal sealed record ImposterTargetModel(
    string Name,
    string DisplayName,
    TypeModel Type,
    NamespaceModel ContainingNamespace,
    bool IsClass,
    bool HasRequiredMembers,
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
        // A class's imposter sets all its methods up side by side. An interface's setup view lists only the methods
        // its interface declares, and DetectCollisions already names them apart on the imposter, so there they
        // collide only within one interface.
        var overloadsWithTheSameSetup = FindOverloadsWithTheSameSetup(
            isInterface
                ? methods.GroupBy(method => method.ContainingType, SymbolEqualityComparer.Default)
                : [methods]
        );
        var properties = GetProperties(target, memberAccess);
        var indexers = properties.Where(property => property.IsIndexer).ToArray();
        var indexersWithTheSameSetup = FindIndexersWithTheSameSetup(indexers);
        var nonIndexers = properties.Where(property => !property.IsIndexer).ToArray();
        var events = GetEvents(target, memberAccess);

        return new ImposterTargetModel(
            target.Name,
            target.ToDisplayString(),
            TypeModel.From(target),
            NamespaceModel.From(target.ContainingNamespace),
            target.TypeKind is TypeKind.Class,
            target.TypeKind is TypeKind.Class && target.HasRequiredMembers(),
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
                method =>
                    MethodModel.From(
                        method,
                        memberAccess,
                        overloadsWithTheSameSetup.Contains(method)
                    ),
                isInterface
            ),
            ToTargetMembers(
                InEmitOrder(nonIndexers),
                isInterface ? DetectCollisions(nonIndexers, property => property.Name) : null,
                property => PropertyModel.From(property, memberAccess, false),
                isInterface
            ),
            ToTargetMembers(
                InEmitOrder(indexers),
                isInterface ? DetectCollisions(indexers, IndexerKey) : null,
                indexer =>
                    PropertyModel.From(
                        indexer,
                        memberAccess,
                        indexersWithTheSameSetup.Contains(indexer)
                    ),
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

    // A setup matches a parameter passed by value, in, ref or ref readonly with the same Arg<T>, and leaves out the ref
    // structs it only passes through, so overloads that differ only in those would get setups with the same signature.
    private static HashSet<IMethodSymbol> FindOverloadsWithTheSameSetup(
        IEnumerable<IEnumerable<IMethodSymbol>> setupScopes
    ) =>
        new(
            setupScopes
                .SelectMany(scope => scope.GroupBy(method => method.Name))
                .SelectMany(overloads =>
                    overloads.Where(method =>
                        overloads.Any(other => HaveTheSameSetupButDiffer(method, other))
                    )
                ),
            SymbolEqualityComparer.Default
        );

    private static bool HaveTheSameSetupButDiffer(IMethodSymbol method, IMethodSymbol other)
    {
        if (method.TypeParameters.Length != other.TypeParameters.Length)
        {
            return false;
        }

        // A generic overload compares with the other one written in terms of its own type parameters. Which of the
        // other's parameters its setup matches comes from its own declaration: written in this method's type
        // parameters, a value of one that allows ref structs could count as matched.
        var otherParameters = other.TypeParameters.IsEmpty
            ? other.Parameters
            : other.Construct([.. method.TypeParameters]).Parameters;
        var otherMatchedParameters = otherParameters
            .Where((_, index) => !ParameterModel.PassesThrough(other.Parameters[index]))
            .ToImmutableArray();

        return HaveTheSameMatchers(
                ParameterModel.MatchedParameters(method.Parameters),
                otherMatchedParameters
            ) && !HaveTheSameSignature(method.Parameters, otherParameters);
    }

    // Indexers whose setups match the same keys, leaving out the ref structs they only pass through, would share a
    // setup indexer. An imposter sets all of a target's indexers up side by side, and an interface's setup view
    // declares an inherited interface's indexers too. Indexers of the same key types already get set up apart (see
    // IndexerKey).
    private static HashSet<IPropertySymbol> FindIndexersWithTheSameSetup(
        IReadOnlyCollection<IPropertySymbol> indexers
    ) =>
        new(
            indexers.Where(indexer =>
                indexers.Any(other =>
                    HaveTheSameMatchers(
                        ParameterModel.MatchedParameters(indexer.Parameters),
                        ParameterModel.MatchedParameters(other.Parameters)
                    )
                    && IndexerKey(indexer) != IndexerKey(other)
                )
            ),
            SymbolEqualityComparer.Default
        );

    private static bool HaveTheSameMatchers(
        ImmutableArray<IParameterSymbol> parameters,
        ImmutableArray<IParameterSymbol> others
    ) =>
        parameters.Length == others.Length
        && parameters.Zip(others, HaveTheSameMatcher).All(same => same);

    private static bool HaveTheSameSignature(
        ImmutableArray<IParameterSymbol> parameters,
        ImmutableArray<IParameterSymbol> others
    ) =>
        parameters.Length == others.Length
        && parameters
            .Zip(
                others,
                (parameter, other) =>
                    parameter.RefKind == other.RefKind
                    && SymbolEqualityComparer.Default.Equals(parameter.Type, other.Type)
            )
            .All(same => same);

    // Only out gets a matcher of its own (OutArg<T>).
    private static bool HaveTheSameMatcher(IParameterSymbol parameter, IParameterSymbol other) =>
        (parameter.RefKind == RefKind.Out) == (other.RefKind == RefKind.Out)
        && SymbolEqualityComparer.Default.Equals(parameter.Type, other.Type);

    // Methods collide on the instance when their parameter types match, and their setups, which leave out the ref
    // structs they only pass through, collide on the imposter.
    private static string MethodKey(IMethodSymbol method) =>
        $"{method.Name}({ParameterTypesKey(ParameterModel.MatchedParameters(method.Parameters))})";

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
