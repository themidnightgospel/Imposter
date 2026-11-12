using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static MemberDeclarationSyntax[] BuildOrderedStepMembers(
        in ImposterTargetMethodMetadata method
    )
    {
        var history = IdentifierName(method.InvocationHistory.Collection.AsField.Name);
        var criteria = method.Parameters.HasInputParameters
            ? Argument(IdentifierName(method.MethodImposter.Builder.ArgumentsCriteriaField.Name))
                .ToSingleArgumentList()
            : EmptyArgumentListSyntax;
        var findAfterName = method.WithGenericArguments(InvocationStepNames.TryFindAfterMethodName);
        return InvocationStepMembersBuilder.Build(
            history.Dot(IdentifierName(InvocationStepNames.OwnerPropertyName)),
            BuildExpectedInvocation(method),
            method.ReservedParameterNames.CreateNameSet(),
            (sequence, match) =>
                history
                    .Dot(findAfterName)
                    .Call(
                        ArgumentListSyntax([
                            Argument(sequence),
                            .. criteria.Arguments,
                            Argument(match).WithRefOrOutKeyword(Token(SyntaxKind.OutKeyword)),
                        ])
                    )
        );
    }
}
