using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal readonly struct MethodParameterMetadata
{
    internal readonly IParameterSymbol Symbol;

    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax NullableAwareTypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    internal MethodParameterMetadata(IParameterSymbol symbol)
    {
        Symbol = symbol;
        Name = SyntaxFactoryHelper.EscapeKeyword(symbol.Name);
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(symbol.Type);
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(symbol.Type);
        ArgTypeSyntax =
            symbol.RefKind == RefKind.Out
                ? WellKnownTypes.Imposter.Abstractions.OutArg(NullableAwareTypeSyntax)
                : WellKnownTypes.Imposter.Abstractions.Arg(NullableAwareTypeSyntax);
    }
}
