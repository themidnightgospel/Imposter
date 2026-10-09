using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A source location kept as its file path and span, so it compares equal across compilations. It turns back into a
/// location in the reporting compilation's syntax tree, where <c>#pragma warning disable</c> still applies.
/// </summary>
internal sealed record LocationModel(string FilePath, TextSpan Span)
{
    // Null for a symbol without source, such as a type from another assembly.
    internal static LocationModel? From(ISymbol symbol) =>
        symbol.Locations.FirstOrDefault(location => location.IsInSource)
            is { SourceTree: { } tree } location
            ? new LocationModel(tree.FilePath, location.SourceSpan)
            : null;

    internal Location ToLocation(Compilation compilation) =>
        compilation.SyntaxTrees.FirstOrDefault(tree => tree.FilePath == FilePath) is { } tree
            ? Location.Create(tree, Span)
            : Location.None;
}
