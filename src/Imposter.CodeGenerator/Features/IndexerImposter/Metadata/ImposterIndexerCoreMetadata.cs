using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata;

internal readonly ref struct ImposterIndexerCoreMetadata
{
    internal readonly string UniqueName;

    internal readonly bool HasGetter;

    internal readonly bool HasSetter;

    internal readonly string DisplayName;

    internal readonly IndexerParameterMetadata[] Parameters;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax AsSystemFuncType;

    internal ImposterIndexerCoreMetadata(IPropertySymbol property, string uniqueName)
    {
        UniqueName = uniqueName;
        HasGetter = property.GetMethod is not null;
        HasSetter = property.SetMethod is not null;
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(property.Type);
        AsSystemFuncType = WellKnownTypes.System.FuncOfT(TypeSyntax);
        Parameters = property.Parameters.Select(parameter => new IndexerParameterMetadata(parameter)).ToArray();

        var parametersDisplay = string.Join(", ", Parameters.Select(p => p.Symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)));
        DisplayName = $"{property.ContainingType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)}.this[{parametersDisplay}]";
    }
}
