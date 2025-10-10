using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static IEnumerable<FieldDeclarationSyntax> GetFields(ImposterTargetMethodMetadata method)
    {
        if (method.Symbol.IsGenericMethod)
        {
            yield return SyntaxFactoryHelper.SingleVariableField(method.MethodImposter.Collection.Syntax, "_imposterCollection");
        }
        else
        {
            yield return SyntaxFactoryHelper.SingleVariableField(method.MethodImposter.Syntax, "_imposter");
        }
        
        yield return SyntaxFactoryHelper.SingleVariableField(method.InvocationHistory.Collection.Syntax, "_invocationHistoryCollection");

        if (method.ParametersExceptOut.Count > 0)
        {
            yield return SyntaxFactoryHelper.SingleVariableField(method.ArgumentsCriteriaType.Syntax, "_argumentsCriteria");
        }
    }

    private static IEnumerable<MemberDeclarationSyntax> ImplementVerifierInterface(ImposterTargetMethodMetadata method)
    {
        yield return Shared.CalledMethodDeclaration
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    method.InvocationVerifierInterface.Syntax
                )
            )
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

        static ExpressionSyntax CountInvocations(ImposterTargetMethodMetadata method)
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

    private static IEnumerable<MemberDeclarationSyntax> ImplementInvocationSetupBuilderInterface(ImposterTargetMethodMetadata method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        foreach (var methodDeclaration in invocationSetupBuilderInterface.Members.OfType<MethodDeclarationSyntax>())
        {
            yield return methodDeclaration
                .WithModifiers(TokenList())
                .WithSemicolonToken(default)
                .WithConstraintClauses(List<TypeParameterConstraintClauseSyntax>())
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(
                    method.InvocationSetupType.Interface.Syntax
                ))
                .WithBody(Block(
                    LocalDeclarationStatement(
                        VariableDeclaration(
                            IdentifierName("var"),
                            SingletonSeparatedList(
                                VariableDeclarator(Identifier("invocationSetup"))
                                    .WithInitializer(
                                        EqualsValueClause(
                                            InvocationExpression(IdentifierName("GetOrAddInvocationSetup"))
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
                    ReturnStatement(
                        IdentifierName("invocationSetup")
                    )
                ));
        }
    }

    internal static ClassDeclarationSyntax Build(ImposterTargetMethodMetadata method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        var fields = GetFields(method).ToArray();

        var existingInvocationSetupField = FieldDeclaration(
            VariableDeclaration(
                    NullableType(method.InvocationSetupType.Syntax)
                )
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("_existingInvocationSetup"))
                    )
                )
        ).AddModifiers(Token(SyntaxKind.PrivateKeyword));

        return new ClassDeclarationBuilder("Builder")
            .AddBaseType(SimpleBaseType(method.MethodImposter.BuilderInterface.Syntax))
            .AddMembers(fields)
            .AddMember(existingInvocationSetupField)
            .AddMember(SyntaxFactoryHelper.DeclareConstructorAndInitializeMembers("Builder", fields))
            .AddMember(GetOrAddInvocationSetupMethod(method))
            .AddMembers(ImplementInvocationSetupBuilderInterface(method, invocationSetupBuilderInterface))
            .AddMembers(ImplementVerifierInterface(method))
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .Build();
    }
}