using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal partial class MethodImposterBuilder
{
    internal static MemberDeclarationSyntax BuildFindMatchingSetupMethod(ImposterTargetMethodMetadata method)
    {
        var argumentsParameter = Parameter(Identifier("arguments"))
            .WithType(method.ArgumentsType.Syntax);

        var findMatchingSetupMethod = MethodDeclaration(
                NullableType(method.InvocationSetupType.Syntax),
                "FindMatchingSetup"
            )
            .AddModifiers(Token(SyntaxKind.PrivateKeyword))
            .AddParameterListParameters(argumentsParameter);

        if (method.HasInputParameters)
        {
            return findMatchingSetupMethod
                .WithBody(Block(
                    ForEachStatement(
                        IdentifierName("var"),
                        Identifier("setup"),
                        IdentifierName("_invocationSetups"),
                        Block(
                            IfStatement(
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("setup"),
                                            IdentifierName("ArgumentsCriteria")
                                        ),
                                        IdentifierName("Matches")
                                    ),
                                    ArgumentList(SingletonSeparatedList(Argument(IdentifierName("arguments"))))
                                ),
                                ReturnStatement(IdentifierName("setup"))
                            )
                        )
                    ),
                    ReturnStatement(LiteralExpression(SyntaxKind.NullLiteralExpression))
                ));
        }

        return findMatchingSetupMethod
            .WithBody(Block(
                IdentifierName("_invocationSetups")
                    .Dot(IdentifierName("TryPeek"))
                    .Call(new ArgumentListBuilder()
                        .AddArgument(Argument(
                                null,
                                Token(SyntaxKind.OutKeyword),
                                IdentifierName("setup")
                            )
                        )
                        .Build()
                    )
                    .AsStatement()
            ));

        return findMatchingSetupMethod
            .WithBody(Block(
                    ReturnStatement(
                        ConditionalExpression(
                            BinaryExpression(
                                SyntaxKind.EqualsExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("_invocationSetups"),
                                    IdentifierName("Count")
                                ),
                                LiteralExpression(
                                    SyntaxKind.NumericLiteralExpression,
                                    Literal(0)
                                )
                            ),
                            LiteralExpression(
                                SyntaxKind.NullLiteralExpression
                            ),
                            ElementAccessExpression(
                                IdentifierName("_invocationSetups"),
                                BracketedArgumentList(
                                    SingletonSeparatedList(
                                        Argument(
                                            BinaryExpression(
                                                SyntaxKind.SubtractExpression,
                                                MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    IdentifierName("_invocationSetups"),
                                                    IdentifierName("Count")
                                                ),
                                                LiteralExpression(
                                                    SyntaxKind.NumericLiteralExpression,
                                                    Literal(1)
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            );
    }
}