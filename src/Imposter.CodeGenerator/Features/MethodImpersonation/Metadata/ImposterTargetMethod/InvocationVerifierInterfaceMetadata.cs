using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;

internal readonly struct InvocationVerifierInterfaceMetadata
{
    internal const string CallCountMethodName = "CallCount";

    internal readonly string Name;

    internal readonly TypeMetadata Interface;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly NameSyntax Syntax;

    internal readonly TypeParameterListSyntax? TypeParameterList;

    internal readonly IReadOnlyList<TypeParameterConstraintClauseSyntax> ConstraintClauses;

    internal InvocationVerifierInterfaceMetadata(in ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}InvocationVerifier";
        Interface = new TypeMetadata(Name);
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
        CalledMethod = new CalledMethodMetadata();

        // A type parameter cannot share the name of a member declared on this interface.
        var names = new NameSet(
            method
                .Symbol.TypeParameters.Select(parameter => parameter.Name)
                .Concat(
                    method.Symbol.ContainingType.TypeParameters.Select(parameter => parameter.Name)
                )
        );
        var declarationTypeArguments = method
            .Symbol.TypeParameters.Select(parameter =>
                IdentifierName(
                    parameter.Name == CallCountMethodName
                        ? names.Use(parameter.Name)
                        : parameter.Name
                )
            )
            .ToArray();
        TypeParameterList = SyntaxFactoryHelper.TypeParameterListSyntax(declarationTypeArguments);
        var renamer = new TypeParameterRenamer(
            method.Symbol.TypeParameters,
            declarationTypeArguments
        );
        ConstraintClauses = method
            .GenericTypeConstraintClauses.Select(clause =>
                (TypeParameterConstraintClauseSyntax)renamer.Visit(clause)
            )
            .ToArray();
    }
}
