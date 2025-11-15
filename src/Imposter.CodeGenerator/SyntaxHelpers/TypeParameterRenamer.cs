using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

//TODO: Not sure if it's the best way, leaving for now
public class TypeParameterRenamer : CSharpSyntaxRewriter
{
    private readonly HashSet<string> _typeParameterNames;
    private readonly Dictionary<string, string>? _nameMap;
    private readonly string? _suffix;

    public TypeParameterRenamer(IReadOnlyList<ITypeParameterSymbol> typeParameters, string suffix)
    {
        _typeParameterNames = [.. typeParameters.Select(it => it.Name)];
        _suffix = suffix;
    }

    public TypeParameterRenamer(
        IReadOnlyList<ITypeParameterSymbol> typeParameters,
        IReadOnlyList<NameSyntax> replacementNames
    )
    {
        _typeParameterNames = [.. typeParameters.Select(it => it.Name)];

        if (typeParameters.Count != replacementNames.Count)
        {
            throw new ArgumentException(
                "Replacement names count must match type parameters count.",
                nameof(replacementNames)
            );
        }

        _nameMap = typeParameters
            .Select(
                (tp, index) => (tp.Name, Replacement: ToIdentifierText(replacementNames[index]))
            )
            .ToDictionary(pair => pair.Name, pair => pair.Replacement);
    }

    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        if (_typeParameterNames.Contains(node.Identifier.Text))
        {
            var replacement = _nameMap is not null
                ? _nameMap[node.Identifier.Text]
                : node.Identifier.Text + (_suffix ?? string.Empty);

            return IdentifierName(replacement);
        }

        return base.VisitIdentifierName(node);
    }

    private static string ToIdentifierText(NameSyntax nameSyntax) =>
        nameSyntax switch
        {
            SimpleNameSyntax simpleName => simpleName.Identifier.Text,
            QualifiedNameSyntax qualifiedName => qualifiedName.Right.Identifier.Text,
            _ => nameSyntax.ToString(),
        };
}
