using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.SyntaxHelpers;

/// <summary>
/// Rewrites identifier names that refer to a method's type parameters into explicit target type
/// argument syntax. Intended for use on syntax that originates from a single
/// <see cref="IMethodSymbol"/> to avoid semantic mismatches.
/// </summary>
internal sealed class TypeParameterRenamer : CSharpSyntaxRewriter
{
    private readonly Dictionary<string, NameSyntax> _replacementMap;

    public TypeParameterRenamer(
        IReadOnlyList<ITypeParameterSymbol> typeParameters,
        IReadOnlyList<NameSyntax> replacementNames
    )
    {
        if (typeParameters.Count != replacementNames.Count)
        {
            throw new ArgumentException(
                "Replacement names count must match type parameters count.",
                nameof(replacementNames)
            );
        }

        _replacementMap = typeParameters
            .Select((tp, index) => (Name: tp.Name, Replacement: replacementNames[index]))
            .ToDictionary(pair => pair.Name, pair => pair.Replacement, StringComparer.Ordinal);
    }

    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node) =>
        _replacementMap.TryGetValue(node.Identifier.ValueText, out var replacementName)
            ? replacementName.WithTriviaFrom(node)
            : base.VisitIdentifierName(node);
}
