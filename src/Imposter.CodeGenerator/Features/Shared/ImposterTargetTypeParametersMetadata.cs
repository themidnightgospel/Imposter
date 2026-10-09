using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Models;
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

    internal ImposterTargetTypeParametersMetadata(IReadOnlyList<TypeParameterModel> typeParameters)
    {
        TypeArguments = typeParameters
            .Select(parameter =>
                (NameSyntax)
                    SyntaxFactory.IdentifierName(
                        SyntaxFactoryHelper.EscapedIdentifier(parameter.Name)
                    )
            )
            .ToArray();
        TypeParameterListSyntax = SyntaxFactoryHelper.TypeParameterListSyntax(TypeArguments);
        ConstraintClauses = SyntaxFactoryHelper.TypeParameterConstraintClauses(typeParameters);
    }
}
