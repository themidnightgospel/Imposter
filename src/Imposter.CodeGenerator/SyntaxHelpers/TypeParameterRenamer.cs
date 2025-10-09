using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

//TODO: Not sure if it's the best way, leaving for now
public class TypeParameterRenamer(IReadOnlyList<ITypeParameterSymbol> typeParameters, string suffix) : CSharpSyntaxRewriter
{
    private readonly HashSet<string> _typeParameterNames = new(typeParameters.Select(it => it.Name));

    public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
    {
        if (_typeParameterNames.Contains(node.Identifier.Text))
        {
            return IdentifierName(node.Identifier.Text + suffix);
        }

        return base.VisitIdentifierName(node);
    }
}