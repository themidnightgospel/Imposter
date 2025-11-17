using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static MethodDeclarationSyntax BuildCalledMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(WellKnownTypes.Void, CalledMethodMetadata.Name)
            .AddParameter(
                ParameterSyntax(
                    method.InvocationVerifierInterface.CalledMethod.CountParameter.Type,
                    method.InvocationVerifierInterface.CalledMethod.CountParameter.Name
                )
            )
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(method.InvocationVerifierInterface.Syntax)
            )
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        "invocationCount",
                        IdentifierName(method.InvocationHistory.Collection.AsField.Name)
                            .Dot(
                                WithMethodGenericArguments(
                                    InvocationHistoryCollectionCountMethodMetadata.Name,
                                    method
                                )
                            )
                            .Call(
                                method.Parameters.HasInputParameters
                                    ? Argument(
                                            IdentifierName(
                                                method
                                                    .MethodImposter
                                                    .Builder
                                                    .ArgumentsCriteriaField
                                                    .Name
                                            )
                                        )
                                        .ToSingleArgumentList()
                                    : EmptyArgumentListSyntax
                            )
                    ),
                    ThrowIfCountDoesNotMatch(method)
                )
            )
            .Build();

        static IfStatementSyntax ThrowIfCountDoesNotMatch(in ImposterTargetMethodMetadata method)
        {
            var invocationHistoryIdentifier = IdentifierName(
                method.InvocationHistory.Collection.AsField.Name
            );

            return IfStatement(
                Not(
                    IdentifierName("count")
                        .Dot(IdentifierName("Matches"))
                        .Call(
                            ArgumentList(
                                SingletonSeparatedList(Argument(IdentifierName("invocationCount")))
                            )
                        )
                ),
                Block(
                    ThrowStatement(
                        WellKnownTypes.Imposter.Abstractions.VerificationFailedException.New(
                            ArgumentList(
                                SeparatedList<ArgumentSyntax>([
                                    Argument(IdentifierName("count")),
                                    Argument(IdentifierName("invocationCount")),
                                    Argument(
                                        invocationHistoryIdentifier
                                            .Dot(IdentifierName("ToString"))
                                            .Call()
                                    ),
                                ])
                            )
                        )
                    )
                )
            );
        }
    }
}
