using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// An interface the target is or inherits, which gets its own setup view. <see cref="Events"/> are the events it
/// declares, which share a builder with an identical event the imposter already impersonates.
/// </summary>
internal sealed record InterfaceSetupInterfaceModel(
    TypeModel Type,
    string Name,
    EquatableArray<TypeModel> Parents,
    EquatableArray<InterfaceSetupMemberModel> Events
)
{
    internal static InterfaceSetupInterfaceModel From(INamedTypeSymbol @interface) =>
        new(
            TypeModel.From(@interface),
            @interface.Name,
            @interface.Interfaces.Select(TypeModel.From).ToEquatableArray(),
            @interface
                .GetInstanceMembers<IEventSymbol>()
                .Select(InterfaceSetupMemberModel.From)
                .ToEquatableArray()
        );
}
