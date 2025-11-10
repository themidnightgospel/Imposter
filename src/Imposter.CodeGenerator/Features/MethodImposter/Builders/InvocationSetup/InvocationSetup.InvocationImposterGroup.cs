using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static FieldDeclarationSyntax InvocationImpostersFieldDeclaration(in ImposterTargetMethodMetadata method)
    {
        var invocationImposterType = IdentifierName(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName);
        var queueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(invocationImposterType);

        return SinglePrivateReadonlyVariableField(
            queueType,
            "_invocationImposters",
            queueType.New(ArgumentList()));
    }

internal static FieldDeclarationSyntax LastInvocationImposterFieldDeclaration(in ImposterTargetMethodMetadata method) =>
    SingleVariableField(
        IdentifierName(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName),
        "_lastestInvocationImposter",
        SyntaxKind.PrivateKeyword);

    internal static MethodDeclarationSyntax AddInvocationImposterMethod(in ImposterTargetMethodMetadata method)
    {
        var invocationImposterType = IdentifierName(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName);

        return new MethodDeclarationBuilder(invocationImposterType, "AddInvocationImposter")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        invocationImposterType,
                        "invocationImposter",
                        invocationImposterType.New(ArgumentList())),
                    ExpressionStatement(
                        IdentifierName("_invocationImposters")
                            .Dot(IdentifierName("Enqueue"))
                            .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("invocationImposter")))))
                    ),
                    ReturnStatement(IdentifierName("invocationImposter"))
                )
            )
            .Build();
    }

    internal static MethodDeclarationSyntax GetInvocationImposterMethod(in ImposterTargetMethodMetadata method)
    {
        var invocationImposterType = IdentifierName(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName);

        return new MethodDeclarationBuilder(NullableType(invocationImposterType), "GetInvocationImposter")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(invocationImposterType, "invocationImposter"),
                    IfStatement(
                        InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("_invocationImposters"),
                                    IdentifierName("TryDequeue")))
                            .WithArgumentList(
                                ArgumentList(
                                    SingletonSeparatedList(
                                        Argument(IdentifierName("invocationImposter")).WithRefKindKeyword(Token(SyntaxKind.OutKeyword))))),
                        Block(
                            IfStatement(
                                PrefixUnaryExpression(
                                    SyntaxKind.LogicalNotExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("invocationImposter"),
                                        IdentifierName("IsEmpty"))),
                                Block(
                                    ExpressionStatement(
                                        AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            IdentifierName("_lastestInvocationImposter"),
                                            IdentifierName("invocationImposter")))
                                )
                            )
                        )
                    ),
                    ReturnStatement(IdentifierName("_lastestInvocationImposter"))
                )
            )
            .Build();
    }
}
