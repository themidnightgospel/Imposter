using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter;

internal readonly partial struct ImposterBuilder
{
    private static IEnumerable<MethodDeclarationSyntax> BuildImposterMethods(in ImposterGenerationContext imposterGenerationContext)
    {
        return imposterGenerationContext
            .Imposter
            .Methods
            .Select(method => MethodDeclaration(
                    method.MethodImposter.BuilderInterface.Syntax,
                    Identifier(method.Symbol.Name)
                )
                .WithTypeParameterList(SyntaxFactoryHelper.TypeParameterListSyntax(method.Symbol))
                .WithParameterList(SyntaxFactoryHelper.ArgParameters(method.Symbol.Parameters))
                .WithBody(Block(ReturnStatement(
                            method
                                .MethodImposter
                                .Builder
                                .Syntax
                                .New(ArgumentList(SeparatedList(GetBuilderClassArguments(method))))
                        )
                    )
                )
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword))));

        IEnumerable<ArgumentSyntax> GetBuilderClassArguments(ImposterTargetMethodMetadata method)
        {
            yield return Argument(IdentifierName(
                method.Symbol.IsGenericMethod
                    ? method.MethodImposter.Collection.AsField.Name
                    : method.MethodImposter.AsField.Name
            ));

            yield return Argument(IdentifierName(method.InvocationHistory.Collection.AsField.Name));

            if (method.Parameters.HasInputParameters)
            {
                yield return Argument(SyntaxFactoryHelper.NewArgumentsCriteria(method));
            }
        }
    }
}