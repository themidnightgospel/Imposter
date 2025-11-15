using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

/// <summary>
/// Rewrites identifier names that refer to a method's type parameters, either by appending a suffix
/// or by replacing them with explicit target type argument syntax. Intended for use on syntax that
/// originates from a single <see cref="IMethodSymbol"/> to avoid semantic mismatches.
/// </summary>
internal sealed class TypeParameterRenamer : CSharpSyntaxRewriter
{
    private readonly HashSet<string>? _typeParameterNames;
    private readonly Dictionary<string, NameSyntax>? _replacementMap;
    private readonly string? _suffix;

    public TypeParameterRenamer(IReadOnlyList<ITypeParameterSymbol> typeParameters, string suffix)
    {
        _typeParameterNames = CreateTypeParameterNameSet(typeParameters);
        _suffix = suffix;
    }

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

    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        var identifierValueText = node.Identifier.ValueText;

        if (
            _replacementMap is not null
            && _replacementMap.TryGetValue(identifierValueText, out var replacementName)
        )
        {
            return replacementName.WithTriviaFrom(node);
        }

        if (
            _suffix is not null
            && _typeParameterNames is not null
            && _typeParameterNames.Contains(identifierValueText)
        )
        {
            return IdentifierName(identifierValueText + _suffix).WithTriviaFrom(node);
        }

        return base.VisitIdentifierName(node);
    }

    private static HashSet<string> CreateTypeParameterNameSet(
        IReadOnlyList<ITypeParameterSymbol> typeParameters
    ) => new(typeParameters.Select(tp => tp.Name), StringComparer.Ordinal);
}
