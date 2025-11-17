using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    private static MethodDeclarationSyntax BuildAddNewMethod(in ImposterTargetMethodMetadata method)
    {
        var typeParameters = TypeParametersSyntax(method.Symbol).ToArray();

        var methodBuilder = new MethodDeclarationBuilder(method.MethodImposter.Syntax, "AddNew")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithTypeParameters(
                typeParameters.Length > 0 ? TypeParameterList(SeparatedList(typeParameters)) : null
            )
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        "imposter",
                        method.MethodImposter.Syntax.New(
                            ArgumentList(
                                SeparatedList<ArgumentSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        Argument(
                                            IdentifierName(
                                                method.InvocationHistory.Collection.AsField.Name
                                            )
                                        ),
                                        Token(SyntaxKind.CommaToken),
                                        Argument(IdentifierName("_invocationBehavior")),
                                    }
                                )
                            )
                        )
                    ),
                    IdentifierName("_imposters")
                        .Dot(IdentifierName("Push"))
                        .Call(Argument(IdentifierName("imposter")).AsSingleArgumentListSyntax())
                        .ToStatementSyntax(),
                    ReturnStatement(IdentifierName("imposter"))
                )
            );

        return methodBuilder.Build();
    }
}
