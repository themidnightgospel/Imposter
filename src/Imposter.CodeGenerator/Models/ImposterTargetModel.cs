using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// Everything the generator reads from a target to build its imposter. Properties, indexers and events are in the
/// order the imposter emits them; ImposterGenerator orders the methods when it emits them. Only the members the
/// imposter's assembly can override or implement are included.
/// <see cref="HasRequiredMembers"/> is true for a class with C# 11 required members, which the imposter's
/// <c>new</c> of its instance can skip only through <c>[SetsRequiredMembers]</c>.
/// <see cref="TypeParameters"/> includes the open parameters of containing types, from outer to inner, followed by
/// the target's own parameters.
/// <see cref="IdentityName"/> names the namespace and file independently of parameter renaming in generated code.
/// </summary>
internal sealed record ImposterTargetModel(
    string Name,
    string DisplayName,
    string IdentityName,
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
    internal static ImposterTargetModel From(
        INamedTypeSymbol target,
        TargetMembers members,
        MemberAccess memberAccess
    )
    {
        var isInterface = target.TypeKind is TypeKind.Interface;
        var methods = members.Methods;
        // A class's imposter sets all its methods up side by side. An interface's setup view lists only the methods
        // its interface declares, and DetectCollisions already names them apart on the imposter, so there they
        // collide only within one interface.
        var overloadsWithTheSameSetup = FindOverloadsWithTheSameSetup(
            isInterface
                ? methods.GroupBy(method => method.ContainingType, SymbolEqualityComparer.Default)
                : [methods]
        );
        var properties = members.Properties;
        var indexers = properties.Where(property => property.IsIndexer).ToArray();
        var indexersWithTheSameSetup = FindIndexersWithTheSameSetup(indexers);
        var nonIndexers = properties.Where(property => !property.IsIndexer).ToArray();
        var events = members.Events;
        // Two interfaces can declare a method, a property and an event of one name, which the imposter can't, so
        // those collide like members of one kind.
        var namesOfSeveralKinds = NamesOfSeveralKinds(methods, nonIndexers, events);

        return new ImposterTargetModel(
            target.Name,
            target.ToDisplayString(),
            target.ToImposterIdentityString(),
            TypeModel.From(target),
            NamespaceModel.From(target.ContainingNamespace),
            target.TypeKind is TypeKind.Class,
            target.TypeKind is TypeKind.Class && target.HasRequiredMembers(),
            target.DeclaredAccessibility,
            target.TypeParametersIn().Select(TypeParameterModel.From).ToEquatableArray(),
            target
                .GetMembers()
                .Select(member => member.Name)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToEquatableArray(),
            ToTargetMembers(
                methods,
                isInterface ? DetectMethodCollisions(methods, namesOfSeveralKinds) : null,
                method =>
                    MethodModel.From(
                        method,
                        memberAccess,
                        overloadsWithTheSameSetup.Contains(method)
                    ),
                isInterface ? InterfaceSetupMemberModel.From : null
            ),
            ToTargetMembers(
                InEmitOrder(nonIndexers),
                isInterface
                    ? DetectCollisions(nonIndexers, property => property.Name, namesOfSeveralKinds)
                    : null,
                property => PropertyModel.From(property, memberAccess, false),
                isInterface ? InterfaceSetupMemberModel.From : null
            ),
            ToTargetMembers(
                InEmitOrder(indexers),
                isInterface ? DetectCollisions(indexers, IndexerKey, namesOfSeveralKinds) : null,
                indexer =>
                    PropertyModel.From(
                        indexer,
                        memberAccess,
                        indexersWithTheSameSetup.Contains(indexer)
                    ),
                isInterface ? InterfaceSetupMemberModel.From : null
            ),
            ToTargetMembers(
                InEmitOrder(events),
                isInterface
                    ? DetectCollisions(events, @event => @event.Name, namesOfSeveralKinds)
                    : null,
                @event => EventModel.From(@event, memberAccess),
                isInterface ? InterfaceSetupMemberModel.From : null
            ),
            GetAccessibleConstructors(target, memberAccess),
            isInterface ? InterfaceSetupTargetModel.From(target) : null
        );
    }

    // Only an interface's members get setup views, which describe them with their models' types.
    private static EquatableArray<TargetMemberModel<TModel>> ToTargetMembers<TSymbol, TModel>(
        IEnumerable<TSymbol> members,
        HashSet<TSymbol>? collidingMembers,
        Func<TSymbol, TModel> createModel,
        Func<TSymbol, TModel, InterfaceSetupMemberModel>? createSetup
    )
        where TSymbol : class, ISymbol
        where TModel : IEquatable<TModel> =>
        members
            .Select(member =>
            {
                var model = createModel(member);
                return new TargetMemberModel<TModel>(
                    model,
                    collidingMembers?.Contains(member) == true,
                    createSetup?.Invoke(member, model)
                );
            })
            .ToEquatableArray();

    private static IEnumerable<TSymbol> InEmitOrder<TSymbol>(IEnumerable<TSymbol> members)
        where TSymbol : ISymbol =>
        members
            .OrderBy(member => member.MetadataName, StringComparer.Ordinal)
            .ThenBy(
                member => member.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                StringComparer.Ordinal
            );

    // Members of different interfaces that collide on the instance are implemented explicitly: members of one kind with
    // the same key, and members whose name another kind of member has.
    private static HashSet<TSymbol> DetectCollisions<TSymbol>(
        IReadOnlyCollection<TSymbol> members,
        Func<TSymbol, string> key,
        HashSet<string> namesOfSeveralKinds
    )
        where TSymbol : class, ISymbol =>
        new(
            WithTheSameKey(members, key).Concat(NamedLikeAnotherKind(members, namesOfSeveralKinds)),
            SymbolEqualityComparer.Default
        );

    private static IEnumerable<TSymbol> WithTheSameKey<TSymbol>(
        IEnumerable<TSymbol> members,
        Func<TSymbol, string> key
    ) => members.GroupBy(key).Where(group => group.Count() > 1).SelectMany(group => group);

    private static IEnumerable<TSymbol> NamedLikeAnotherKind<TSymbol>(
        IEnumerable<TSymbol> members,
        HashSet<string> namesOfSeveralKinds
    )
        where TSymbol : ISymbol =>
        members.Where(member => namesOfSeveralKinds.Contains(member.Name));

    private static HashSet<string> NamesOfSeveralKinds(params IEnumerable<ISymbol>[] kinds) =>
        [
            .. kinds
                .SelectMany(kind => kind.Select(member => member.Name).Distinct())
                .GroupBy(name => name)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key),
        ];

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

    // A method has the same setup and signature as itself, so only another one is compared.
    private static bool HaveTheSameSetupButDiffer(IMethodSymbol method, IMethodSymbol other) =>
        !SymbolEqualityComparer.Default.Equals(method, other)
        && MethodModel.HaveTheSameSetup(method, other)
        && !HaveTheSameSignature(method.Parameters, other.Parameters);

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
                    !SymbolEqualityComparer.Default.Equals(indexer, other)
                    && ParameterModel.HaveTheSameMatchers(
                        ParameterModel.MatchedParameters(indexer.Parameters),
                        ParameterModel.MatchedParameters(other.Parameters)
                    )
                    && IndexerKey(indexer) != IndexerKey(other)
                )
            ),
            SymbolEqualityComparer.Default
        );

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
                    && parameter.Type.ToSignatureKey() == other.Type.ToSignatureKey()
            )
            .All(same => same);

    // Setup and implementation signatures can collide independently: allows ref struct removes a parameter from
    // the setup but not the implementation. Preserve numbered setups for setup collisions, and implement methods
    // explicitly when their full signatures collide even if their setups take different matchers. Both keys start with
    // the method's name, so only methods that share one get keys built.
    private static HashSet<IMethodSymbol> DetectMethodCollisions(
        IReadOnlyCollection<IMethodSymbol> methods,
        HashSet<string> namesOfSeveralKinds
    )
    {
        var namesakes = WithTheSameKey(methods, method => method.Name).ToArray();

        return new(
            WithTheSameKey(namesakes, MethodSetupKey)
                .Concat(WithTheSameKey(namesakes, MethodImplementationKey))
                .Concat(NamedLikeAnotherKind(methods, namesOfSeveralKinds)),
            SymbolEqualityComparer.Default
        );
    }

    private static string MethodSetupKey(IMethodSymbol method) =>
        $"{method.Name}`{method.Arity}({ParameterTypesKey(ParameterModel.MatchedParameters(method.Parameters))})";

    // Method type parameters compare by position. Return types and constraints cannot distinguish overloads, nor
    // can ref, in and out from each other; passing by value can distinguish an overload from passing by reference.
    private static string MethodImplementationKey(IMethodSymbol method)
    {
        var parameterTypes = method.Parameters.Select(parameter =>
            (parameter.RefKind == RefKind.None ? "" : "&") + parameter.Type.ToSignatureKey()
        );
        return $"{method.Name}`{method.Arity}({string.Join(",", parameterTypes)})";
    }

    // Indexers with the same parameter types collide on the instance, and their setup indexers, which take Arg<T>
    // whatever the ref kind, would collide on the imposter.
    private static string IndexerKey(IPropertySymbol indexer) =>
        ParameterTypesKey(indexer.Parameters);

    private static string ParameterTypesKey(IEnumerable<IParameterSymbol> parameters) =>
        string.Join(",", parameters.Select(parameter => parameter.Type.ToSignatureKey()));

    // A constructor the imposter forwards: an accessible one without a pointer parameter, which the imposter's own
    // constructor couldn't take outside unsafe code.
    internal static bool IsCallableConstructor(
        IMethodSymbol constructor,
        MemberAccess memberAccess
    ) =>
        memberAccess.IsAccessible(constructor)
        && !constructor.Parameters.Any(parameter => parameter.Type.ContainsPointer());

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
                !constructor.IsImplicitlyDeclared
                && IsCallableConstructor(constructor, memberAccess)
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
