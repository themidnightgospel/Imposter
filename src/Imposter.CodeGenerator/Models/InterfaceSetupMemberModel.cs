using System.Collections.Immutable;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A member of a target interface, as its setup view declares it. <see cref="HidesInheritedMember"/> is true when it
/// hides a member of an interface its own interface inherits, so the view declares it with <c>new</c>.
/// </summary>
internal sealed record InterfaceSetupMemberModel(
    InterfaceSetupMemberKind Kind,
    string Name,
    TypeModel ContainingInterface,
    EquatableArray<TypeParameterModel> TypeParameters,
    EquatableArray<ParameterModel> Parameters,
    TypeModel? EventType,
    bool HidesInheritedMember
)
{
    internal static InterfaceSetupMemberModel From(ISymbol member) =>
        new(
            GetKind(member),
            member.Name,
            TypeModel.From(member.ContainingType),
            member is IMethodSymbol method
                ? method.TypeParameters.Select(TypeParameterModel.From).ToEquatableArray()
                : default,
            GetParameters(member).Select(ParameterModel.From).ToEquatableArray(),
            member is IEventSymbol @event ? TypeModel.From(@event.Type) : null,
            member
                .ContainingType.AllInterfaces.SelectMany(parent => parent.GetMembers(member.Name))
                .Any(inherited => Hides(member, inherited))
        );

    private static InterfaceSetupMemberKind GetKind(ISymbol member) =>
        member switch
        {
            IMethodSymbol => InterfaceSetupMemberKind.Method,
            IPropertySymbol { IsIndexer: true } => InterfaceSetupMemberKind.Indexer,
            IPropertySymbol => InterfaceSetupMemberKind.Property,
            _ => InterfaceSetupMemberKind.Event,
        };

    private static ImmutableArray<IParameterSymbol> GetParameters(ISymbol member) =>
        member switch
        {
            IMethodSymbol method => method.Parameters,
            IPropertySymbol property => property.Parameters,
            _ => ImmutableArray<IParameterSymbol>.Empty,
        };

    // Methods and indexers hide inherited ones with the same arity and argument types; properties and events hide
    // any inherited member with their name.
    private static bool Hides(ISymbol member, ISymbol inherited) =>
        (member, inherited) switch
        {
            (IMethodSymbol method, IMethodSymbol parent) => method.Arity == parent.Arity
                && SameArgumentTypes(
                    ParameterModel.MatchedParameters(method.Parameters),
                    ParameterModel.MatchedParameters(parent.Parameters)
                ),
            (
                IPropertySymbol { IsIndexer: true } indexer,
                IPropertySymbol { IsIndexer: true } parent
            ) => SameArgumentTypes(indexer.Parameters, parent.Parameters),
            (IMethodSymbol, _) or (IPropertySymbol { IsIndexer: true }, _) => false,
            _ => true,
        };

    private static bool SameArgumentTypes(
        ImmutableArray<IParameterSymbol> parameters,
        ImmutableArray<IParameterSymbol> inheritedParameters
    ) =>
        parameters.Length == inheritedParameters.Length
        && parameters.Zip(inheritedParameters, SameArgumentType).All(matches => matches);

    private static bool SameArgumentType(IParameterSymbol left, IParameterSymbol right) =>
        (left.RefKind == RefKind.Out) == (right.RefKind == RefKind.Out)
        && (
            SymbolEqualityComparer.Default.Equals(left.Type, right.Type)
            || left.Type.ToSignatureKey() == right.Type.ToSignatureKey()
        );
}
