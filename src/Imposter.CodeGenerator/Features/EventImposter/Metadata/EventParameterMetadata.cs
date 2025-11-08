using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.EventImposter.Metadata;

internal readonly struct EventParameterMetadata
{
    internal readonly IParameterSymbol Symbol;

    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    internal readonly ParameterSyntax ParameterSyntax;

    internal EventParameterMetadata(IParameterSymbol parameterSymbol)
    {
        Symbol = parameterSymbol;
        Name = parameterSymbol.Name;
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(parameterSymbol.Type);
        ArgTypeSyntax = WellKnownTypes.Imposter.Abstractions.Arg(TypeSyntax);
        ParameterSyntax = SyntaxFactoryHelper.ParameterSyntax(parameterSymbol);
    }
}
