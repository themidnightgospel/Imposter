using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata;

internal readonly struct IndexerParameterMetadata
{
    internal readonly IParameterSymbol Symbol;

    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    internal readonly ParameterSyntax ParameterSyntax;

    internal IndexerParameterMetadata(IParameterSymbol parameterSymbol)
    {
        Symbol = parameterSymbol;
        Name = parameterSymbol.Name;
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(parameterSymbol.Type);
        ArgTypeSyntax = WellKnownTypes.Imposter.Abstractions.Arg(TypeSyntax);
        ParameterSyntax = SyntaxFactoryHelper.ParameterSyntax(parameterSymbol);
    }
}
