using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal partial class MethodImposterBuilder
{
    internal static MemberDeclarationSyntax BuildFindMatchingInvocationImposterGroupMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var groupIdentifier = Identifier(
            method.MethodImposter.FindMatchingInvocationImposterGroupMethod.GroupVariableName
        );
        var groupIdentifierName = IdentifierName(groupIdentifier);

        var findMatchingGroupMethod = new MethodDeclarationBuilder(
            NullableType(method.MethodInvocationImposterGroup.Syntax),
            method.MethodImposter.FindMatchingInvocationImposterGroupMethod.Name
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(GetArgumentsParameter(method));

        if (method.Parameters.HasInputParameters)
        {
            return findMatchingGroupMethod
                .WithBody(
                    Block(
                        ForEachStatement(
                            Var,
                            groupIdentifier,
                            IdentifierName(
                                method.MethodImposter.InvocationImposterGroupsField.Name
                            ),
                            Block(
                                IfStatement(
                                    groupIdentifierName
                                        .Dot(IdentifierName("ArgumentsCriteria"))
                                        .Dot(
                                            IdentifierName(
                                                method.ArgumentsCriteria.MatchesMethod.Name
                                            )
                                        )
                                        .Call(Argument(IdentifierName("arguments"))),
                                    ReturnStatement(groupIdentifierName)
                                )
                            )
                        ),
                        ReturnStatement(Null)
                    )
                )
                .Build();
        }

        return findMatchingGroupMethod
            .WithBody(
                Block(
                    IfStatement(
                        IdentifierName(method.MethodImposter.InvocationImposterGroupsField.Name)
                            .Dot(ConcurrentStackSyntaxHelper.TryPeek)
                            .Call(
                                OutVarArgument(
                                    method
                                        .MethodImposter
                                        .FindMatchingInvocationImposterGroupMethod
                                        .GroupVariableName
                                )
                            ),
                        ReturnStatement(groupIdentifierName),
                        ElseClause(ReturnStatement(Null))
                    )
                )
            )
            .Build();

        static ParameterSyntax? GetArgumentsParameter(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? ParameterSyntax(method.Arguments.Syntax, "arguments")
                : null;
    }
}
