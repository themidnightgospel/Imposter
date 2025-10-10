using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.Imposter;

internal static partial class ImposterBuilder
{
    private static IEnumerable<MethodDeclarationSyntax> SetupBuilderMethods(ImposterGenerationContext imposterGenerationContext) =>
        imposterGenerationContext.Methods
            .Select(method => MethodDeclaration(
                    method.MethodImposter.BuilderInterface.Syntax,
                    Identifier(method.Symbol.Name)
                )
                .WithTypeParameterList(SyntaxFactoryHelper.TypeParameterList(method.Symbol))
                .WithParameterList(SyntaxFactoryHelper.ArgParameters(method.Symbol.Parameters))
                .WithBody(Block(ReturnStatement(
                            method
                                .MethodImposter
                                .Builder
                                .Syntax
                                .New(ArgumentList(
                                        SeparatedList(
                                            [
                                                Argument(IdentifierName(
                                                    method.Symbol.IsGenericMethod
                                                        ? method.MethodImposter.Collection.AsField.Name
                                                        : method.MethodImposter.AsField.Name
                                                )),
                                                Argument(IdentifierName(method.InvocationHistory.Collection.AsField.Name)),
                                                Argument(SyntaxFactoryHelper.ArgumentCriteriaCreationExpression(method))
                                            ]
                                        )
                                    )
                                )
                        )
                    )
                )
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword))));
}