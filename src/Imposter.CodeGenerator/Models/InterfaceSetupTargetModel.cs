using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A target interface for its setup views. <see cref="Interfaces"/> starts with the target, followed by every interface
/// it inherits.
/// </summary>
internal sealed record InterfaceSetupTargetModel(
    EquatableArray<InterfaceSetupInterfaceModel> Interfaces
)
{
    internal static InterfaceSetupTargetModel From(INamedTypeSymbol target) =>
        new(
            target
                .AllInterfaces.Prepend(target)
                .Select(InterfaceSetupInterfaceModel.From)
                .ToEquatableArray()
        );
}
