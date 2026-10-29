using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A member of a target interface, as its setup view declares it. <see cref="HidesInheritedMember"/> is true when it
/// hides a member of an interface its own interface inherits, so the view declares it with <c>new</c>.
/// <see cref="MatchedParameters"/> leaves out the parameters the imposter only passes through.
/// </summary>
internal sealed record InterfaceSetupMemberModel(
    InterfaceSetupMemberKind Kind,
    string Name,
    TypeModel ContainingInterface,
    EquatableArray<TypeParameterModel> TypeParameters,
    EquatableArray<ParameterModel> MatchedParameters,
    TypeModel? EventType,
    bool HidesInheritedMember
)
{
    // A member the imposter impersonates takes the types its own model describes.
    internal static InterfaceSetupMemberModel From(IMethodSymbol method, MethodModel model) =>
        new(
            InterfaceSetupMemberKind.Method,
            method.Name,
            model.ContainingType,
            model.TypeParameters,
            ParameterModel.MatchedParameters(model.Parameters),
            EventType: null,
            HidesAnInheritedMember(method)
        );

    internal static InterfaceSetupMemberModel From(IPropertySymbol property, PropertyModel model) =>
        new(
            property.IsIndexer
                ? InterfaceSetupMemberKind.Indexer
                : InterfaceSetupMemberKind.Property,
            property.Name,
            model.ContainingType,
            TypeParameters: default,
            ParameterModel.MatchedParameters(model.Parameters),
            EventType: null,
            HidesAnInheritedMember(property)
        );

    internal static InterfaceSetupMemberModel From(IEventSymbol @event, EventModel model) =>
        FromEvent(@event, model.ContainingType, model.Type);

    // An event an interface declares, which its setup view finds among the target's by equality (see
    // InterfaceSetupViewMetadata), so it has to be modelled the same.
    internal static InterfaceSetupMemberModel From(IEventSymbol @event) =>
        FromEvent(@event, TypeModel.From(@event.ContainingType), TypeModel.From(@event.Type));

    private static InterfaceSetupMemberModel FromEvent(
        IEventSymbol @event,
        TypeModel containingInterface,
        TypeModel eventType
    ) =>
        new(
            InterfaceSetupMemberKind.Event,
            @event.Name,
            containingInterface,
            TypeParameters: default,
            MatchedParameters: default,
            eventType,
            HidesAnInheritedMember(@event)
        );

    private static bool HidesAnInheritedMember(ISymbol member) =>
        member
            .ContainingType.AllInterfaces.SelectMany(parent => parent.GetMembers(member.Name))
            .Any(inherited => Hides(member, inherited));

    // Methods and indexers hide inherited ones with the same arity and argument types; properties and events hide
    // any inherited member with their name.
    private static bool Hides(ISymbol member, ISymbol inherited) =>
        (member, inherited) switch
        {
            (IMethodSymbol method, IMethodSymbol parent) => MethodModel.HaveTheSameSetup(
                method,
                parent
            ),
            (
                IPropertySymbol { IsIndexer: true } indexer,
                IPropertySymbol { IsIndexer: true } parent
            ) => ParameterModel.HaveTheSameMatchers(indexer.Parameters, parent.Parameters),
            (IMethodSymbol, _) or (IPropertySymbol { IsIndexer: true }, _) => false,
            _ => true,
        };
}
