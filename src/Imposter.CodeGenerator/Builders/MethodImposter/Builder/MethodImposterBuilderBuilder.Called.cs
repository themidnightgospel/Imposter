using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static MemberDeclarationSyntax BuildCalledMethod(in ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(
                PredefinedType(Token(SyntaxKind.VoidKeyword)),
                Identifier(InvocationVerifierInterfaceMetadata.CalledMethodMetadata.Name))
            .AddParameterListParameters(
                ParameterSyntax(method.InvocationVerifierInterface.CalledMethod.CountParameter.Type,
                    method.InvocationVerifierInterface.CalledMethod.CountParameter.Name))
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    method.InvocationVerifierInterface.Syntax
                )
            )
            .WithBody(Block(
                    LocalDeclarationStatement(
                        VariableDeclarationSyntax(
                            Var,
                            "invocationCount",
                            initializer:
                            IdentifierName(method.InvocationHistory.Collection.AsField.Name)
                                .Dot(WithMethodGenericArguments(InvocationHistoryTypeMetadata.CollectionMetadata.CountMethodMetadata.Name, method))
                                .Call(method.Parameters.HasInputParameters
                                    ? Argument(IdentifierName(method.MethodImposter.Builder.ArgumentsCriteriaField.Name)).AsSingleArgumentList()
                                    : EmptyArgumentListSyntax)
                        )
                    ),
                    ThrowIfCountDoesNotMatch()
                )
            );

        IfStatementSyntax ThrowIfCountDoesNotMatch() =>
            IfStatement(Not(
                    IdentifierName("count")
                        .Dot(IdentifierName("Matches"))
                        .Call(ArgumentList(
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
    }
}