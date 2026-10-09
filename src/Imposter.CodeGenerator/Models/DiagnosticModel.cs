using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A diagnostic about a target, kept as data so the declaration that carries it compares equal across compilations.
/// </summary>
internal sealed record DiagnosticModel(
    DiagnosticDescriptor Descriptor,
    LocationModel? Location,
    EquatableArray<string> Arguments
)
{
    internal static DiagnosticModel Create(
        DiagnosticDescriptor descriptor,
        LocationModel? location,
        params string[] arguments
    ) => new(descriptor, location, arguments.ToEquatableArray());

    internal Diagnostic ToDiagnostic(Compilation compilation) =>
        Diagnostic.Create(
            Descriptor,
            Location?.ToLocation(compilation) ?? Microsoft.CodeAnalysis.Location.None,
            Arguments.Cast<object>().ToArray()
        );
}
