using System;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator;

public static class ImposterSyntaxFactory
{
    internal static ParameterSyntax CreateParameterSyntax(IParameterSymbol symbol, bool includeRefKind = true)
    {
        var type = SyntaxFactory.IdentifierName(symbol.Type.ToFullyQualifiedName());
        var param = SyntaxFactory
            .Parameter(SyntaxFactory.Identifier(symbol.Name))
            .WithType(type);

        if (includeRefKind && symbol.RefKind != RefKind.None)
        {
            var modifierKind = symbol.RefKind switch
            {
                RefKind.Ref => SyntaxKind.RefKeyword,
                RefKind.In => SyntaxKind.InKeyword,
                RefKind.Out => SyntaxKind.OutKeyword,
                _ => throw new ArgumentException(nameof(symbol.RefKind))
            };

            param = param.WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(modifierKind)));
        }

        if (symbol.HasExplicitDefaultValue)
        {
            param = param.WithDefault(SyntaxFactory.EqualsValueClause(SyntaxFactoryHelper.Literal(symbol.ExplicitDefaultValue)));
        }

        return param;
    }
}