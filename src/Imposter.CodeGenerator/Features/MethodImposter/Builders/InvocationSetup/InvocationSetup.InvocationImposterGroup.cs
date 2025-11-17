using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static FieldDeclarationSyntax InvocationImpostersFieldDeclaration(
        in ImposterTargetMethodMetadata method
    )
    {
        var invocationImposterType = IdentifierName(
            MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName
        );
        var queueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            invocationImposterType
        );

        return SinglePrivateReadonlyVariableField(
            queueType,
            "_invocationImposters",
            queueType.New(ArgumentList())
        );
    }

    internal static FieldDeclarationSyntax LastInvocationImposterFieldDeclaration(
        in ImposterTargetMethodMetadata method
    ) =>
        SingleVariableField(
            IdentifierName(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName)
                .ToNullableType(),
            "_lastestInvocationImposter",
            SyntaxKind.PrivateKeyword
        );

    internal static MethodDeclarationSyntax AddInvocationImposterMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var invocationImposterType = IdentifierName(
            MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName
        );

        return new MethodDeclarationBuilder(invocationImposterType, "AddInvocationImposter")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        invocationImposterType,
                        "invocationImposter",
                        invocationImposterType.New(ArgumentList())
                    ),
                    IdentifierName("_invocationImposters")
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(IdentifierName("invocationImposter"))
                                )
                            )
                        )
                        .ToStatementSyntax(),
                    ReturnStatement(IdentifierName("invocationImposter"))
                )
            )
            .Build();
    }

    internal static MethodDeclarationSyntax GetInvocationImposterMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var invocationImposterType = IdentifierName(
            MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName
        );

        return new MethodDeclarationBuilder(
            invocationImposterType.ToNullableType(),
            "GetInvocationImposter"
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                Block(
                    IfStatement(
                        IdentifierName("_invocationImposters")
                            .Dot(IdentifierName("TryDequeue"))
                            .Call(
                                ArgumentList(
                                    SingletonSeparatedList(OutVarArgument("invocationImposter"))
                                )
                            ),
                        Block(
                            IfStatement(
                                Not(
                                    IdentifierName("invocationImposter")
                                        .Dot(IdentifierName("IsEmpty"))
                                ),
                                Block(
                                    IdentifierName("_lastestInvocationImposter")
                                        .Assign(IdentifierName("invocationImposter"))
                                        .ToStatementSyntax()
                                )
                            ),
                            ReturnStatement(IdentifierName("invocationImposter"))
                        )
                    ),
                    ReturnStatement(IdentifierName("_lastestInvocationImposter"))
                )
            )
            .Build();
    }
}
