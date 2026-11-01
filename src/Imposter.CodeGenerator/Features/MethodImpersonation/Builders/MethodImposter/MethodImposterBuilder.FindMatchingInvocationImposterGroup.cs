using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal partial class MethodImposterBuilder
{
    // Only a group with a configured outcome answers a call. A bare one, which a builder registers before it is
    // configured or only to verify, leaves the call to the default outcome, or to MissingImposterException in Explicit
    // mode.
    internal static MemberDeclarationSyntax BuildFindMatchingInvocationImposterGroupMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var lookup = method.MethodImposter.FindMatchingInvocationImposterGroupMethod;
        var group = IdentifierName(lookup.GroupVariableName);
        ExpressionSyntax answersTheCall = group.Dot(
            IdentifierName(method.MethodInvocationImposterGroup.HasConfiguredOutcomeFieldName)
        );
        if (method.Parameters.HasInputParameters)
        {
            answersTheCall = answersTheCall.And(
                group
                    .Dot(
                        IdentifierName(
                            MethodInvocationImposterGroupMetadata.ArgumentsCriteriaPropertyName
                        )
                    )
                    .Dot(IdentifierName(method.ArgumentsCriteria.MatchesCapturedMethodName))
                    .Call(CapturedArgumentsBuilder.Arguments(method))
            );
        }

        return new MethodDeclarationBuilder(
            NullableType(method.MethodInvocationImposterGroup.Syntax),
            lookup.Name
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithParameterList(CapturedArgumentsBuilder.InParameters(method))
            .WithBody(
                Block(
                    ForEachStatement(
                        Var,
                        group.Identifier,
                        IdentifierName(method.MethodImposter.InvocationImposterGroupsField.Name),
                        Block(IfStatement(answersTheCall, ReturnStatement(group)))
                    ),
                    ReturnStatement(Null)
                )
            )
            .Build();
    }
}
