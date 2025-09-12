using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Components;

internal class DelegateTypeGenerator
{
    internal static void AddMethodDelegate(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        var delegateDeclaration = SyntaxFactory.DelegateDeclaration(
                attributeLists: SyntaxFactory.List<AttributeListSyntax>(),
                modifiers: SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
                returnType: CreateTypeSyntax(method.Symbol.ReturnType),
                identifier: SyntaxFactory.Identifier(method.DelegateName),
                typeParameterList: null,
                parameterList: ParameterList(method.Symbol.Parameters),
                constraintClauses: SyntaxFactory.List<TypeParameterConstraintClauseSyntax>())
            .WithLeadingTrivia(
                SyntaxFactory.Comment(method.Comment),
                SyntaxFactory.CarriageReturnLineFeed
            );

        imposterGenerationContext.SourceBuilder.AppendLine(delegateDeclaration.NormalizeWhitespace().ToFullString());
        imposterGenerationContext.SourceBuilder.AppendLine();
    }

    
    public static TypeSyntax CreateTypeSyntax(ITypeSymbol typeSymbol)
    {
        var typeName = typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        return SyntaxFactory.ParseTypeName(typeName);
    }

    public static ParameterListSyntax ParameterList(IEnumerable<IParameterSymbol> parameters)
    {
        return SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(parameters.Select(it => ImposterSyntaxFactory.CreateParameterSyntax(it))));
    }
}