using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    private static IEnumerable<FieldDeclarationSyntax> GetBuilderClassFields(ImposterTargetMethod method)
    {
        yield return FieldDeclaration(
                VariableDeclaration(
                        IdentifierName(method.MethodImposter.Name)
                    )
                    .WithVariables(
                        SingletonSeparatedList(
                            VariableDeclarator(Identifier("_imposter"))
                        )
                    )
            )
            .AddModifiers(
                Token(SyntaxKind.PrivateKeyword)
            );

        if (method.ParametersExceptOut.Count > 0)
        {
            yield return FieldDeclaration(
                    VariableDeclaration(
                            IdentifierName(method.ArgumentsCriteriaClassName)
                        )
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(Identifier("_argumentsCriteria"))
                            )
                        )
                )
                .AddModifiers(
                    Token(SyntaxKind.PrivateKeyword)
                );
        }
    }

    private static IEnumerable<MemberDeclarationSyntax> ImplementVerifierInterface(ImposterTargetMethod method)
    {
        yield return CalledMethodDeclaration
            .Value
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(
                IdentifierName(method.MethodInvocationVerifierInterfaceName)
            ))
            .WithSemicolonToken(default)
            .WithBody(
                Block(LocalDeclarationStatement(VariableDeclaration(
                        IdentifierName("var"),
                        SingletonSeparatedList(VariableDeclarator(
                            Identifier("invocationCount")
                        ).WithInitializer(
                            EqualsValueClause(CountInvocations(method))
                        ))
                    )),
                    ThrowIfCountDoesNotMatch()
                )
            );

        IfStatementSyntax ThrowIfCountDoesNotMatch() =>
            IfStatement(
                PrefixUnaryExpression(
                    SyntaxKind.LogicalNotExpression,
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("count"),
                            IdentifierName("Matches")
                        ),
                        ArgumentList(
                            SingletonSeparatedList(
                                Argument(
                                    IdentifierName("invocationCount")
                                )
                            )
                        )
                    )
                ),
                Block(ThrowStatement(
                    ObjectCreationExpression(
                            IdentifierName("VerificationFailedException")
                        )
                        .WithArgumentList(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            Literal("TODO")
                                        )
                                    )
                                )
                            )
                        )
                ))
            );

        static ExpressionSyntax CountInvocations(ImposterTargetMethod method)
        {
            var invocationHistoryExpression = MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("_imposter"),
                    IdentifierName("_invocationHistory")
                ),
                IdentifierName("Count")
            );

            if (method.ParametersExceptOut.Count == 0)
            {
                return invocationHistoryExpression;
            }

            return InvocationExpression(
                invocationHistoryExpression,
                ArgumentList(
                    SingletonSeparatedList(
                        Argument(SimpleLambdaExpression(
                            Parameter(Identifier("it")),
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("_argumentsCriteria"),
                                    IdentifierName("Matches")
                                ),
                                ArgumentList(
                                    SingletonSeparatedList(
                                        Argument(MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("it"),
                                            IdentifierName("Arguments")
                                        ))
                                    )
                                )
                            )
                        ))
                    )
                )
            );
        }
    }

    private static IEnumerable<MemberDeclarationSyntax> ImplementInvocationSetupBuilderInterface(ImposterTargetMethod method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        foreach (var methodDeclaration in invocationSetupBuilderInterface.Members.OfType<MethodDeclarationSyntax>())
        {
            yield return methodDeclaration
                .WithModifiers(TokenList())
                .WithSemicolonToken(default)
                .WithConstraintClauses(List<TypeParameterConstraintClauseSyntax>())
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(
                    IdentifierName(invocationSetupBuilderInterface.Identifier.ValueText)
                ))
                .WithBody(Block(
                    LocalDeclarationStatement(
                        VariableDeclaration(
                            IdentifierName("var"),
                            SingletonSeparatedList(
                                VariableDeclarator(Identifier("invocationSetup"))
                                    .WithInitializer(
                                        EqualsValueClause(
                                            ObjectCreationExpression(
                                                IdentifierName(method.InvocationSetup)
                                            ).WithArgumentList(
                                                method.ParametersExceptOut.Count > 0
                                                    ? ArgumentList(
                                                        SingletonSeparatedList(
                                                            Argument(
                                                                IdentifierName("_argumentsCriteria")
                                                            )
                                                        )
                                                    )
                                                    : ArgumentList()
                                            )
                                        )
                                    )
                            )
                        )
                    ),
                    ExpressionStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("invocationSetup"),
                                methodDeclaration.TypeParameterList?.Parameters.Count > 0
                                    ? GenericName(methodDeclaration.Identifier)
                                        .WithTypeArgumentList(TypeArgumentList(
                                            SeparatedList<TypeSyntax>(methodDeclaration.TypeParameterList.Parameters.Select(p => IdentifierName(p.Identifier.ValueText)))
                                        ))
                                    : IdentifierName(methodDeclaration.Identifier)
                            )
                        ).WithArgumentList(
                            ArgumentList(SeparatedList(methodDeclaration
                                .ParameterList
                                .Parameters
                                .Select(p => Argument(IdentifierName(p.Identifier.ValueText)))
                                .ToArray()))
                        )
                    ),
                    ExpressionStatement(
                        InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("_imposter"),
                                        IdentifierName("_invocationSetups")
                                    ),
                                    IdentifierName("Add")
                                )
                            )
                            .WithArgumentList(
                                ArgumentList(
                                    SingletonSeparatedList(
                                        Argument(IdentifierName("invocationSetup"))
                                    )
                                )
                            )
                    ),
                    ReturnStatement(
                        IdentifierName("invocationSetup")
                    )
                ));
        }
    }

    private static ClassDeclarationSyntax BuildMethodImposterBuilderClass(ImposterTargetMethod method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        var fields = GetBuilderClassFields(method).ToArray();

        return new ClassDeclarationBuilder("Builder")
            .AddBaseType(SimpleBaseType(IdentifierName(method.MethodImposter.BuilderInterfaceName)))
            .AddMembers(fields)
            .AddMember(SyntaxFactoryHelper.DeclareConstructorAndInitializeMembers("Builder", fields))
            .AddMembers(ImplementInvocationSetupBuilderInterface(method, invocationSetupBuilderInterface))
            .AddMembers(ImplementVerifierInterface(method))
            .Build(modifiers: TokenList(Token(SyntaxKind.InternalKeyword)));
    }
}