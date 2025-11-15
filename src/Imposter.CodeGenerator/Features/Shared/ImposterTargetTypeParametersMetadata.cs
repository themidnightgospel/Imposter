using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterTargetTypeParametersMetadata
{
    internal readonly IReadOnlyList<NameSyntax> TypeArguments;

    internal readonly TypeParameterListSyntax? TypeParameterListSyntax;

    internal readonly IReadOnlyList<TypeParameterConstraintClauseSyntax> ConstraintClauses;

    internal ImposterTargetTypeParametersMetadata(INamedTypeSymbol targetSymbol)
    {
        TypeArguments = targetSymbol
            .TypeParameters.Select(parameter =>
                (NameSyntax)SyntaxFactory.IdentifierName((string)parameter.Name)
            )
            .ToArray();
        TypeParameterListSyntax = SyntaxFactoryHelper.TypeParameterListSyntax(TypeArguments);
        ConstraintClauses = SyntaxFactoryHelper.TypeParameterConstraintClauses(
            targetSymbol.TypeParameters
        );
    }
}
