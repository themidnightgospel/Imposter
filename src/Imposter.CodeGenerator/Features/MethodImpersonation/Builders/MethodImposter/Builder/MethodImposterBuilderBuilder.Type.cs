using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static List<StatementSyntax> BuildInvocationImposterGroupInitializationStatements(
        in ImposterTargetMethodMetadata method
    )
    {
        var statements = new List<StatementSyntax>();

        var groupCreation = method.MethodInvocationImposterGroup.Syntax.New(
            method.Parameters.HasInputParameters
                ? IdentifierName(method.MethodImposter.Builder.ArgumentsCriteriaField.Name)
                    .ToSingleArgumentList()
                : EmptyArgumentListSyntax
        );

        statements.Add(
            ThisExpression()
                .Dot(
                    IdentifierName(method.MethodImposter.Builder.InvocationImposterGroupField.Name)
                )
                .Assign(groupCreation)
                .ToStatementSyntax()
        );

        ExpressionSyntax methodImposterAccess = IdentifierName(
            method.MethodImposter.Builder.ImposterParameter.Name
        );

        if (method.Model.IsGenericMethod)
        {
            // Disable reuse before publishing the group: arbitrary predicates may retain the
            // array they see, so they must keep receiving independent snapshots on each pass.
            ExpressionSyntax? exposesArray = null;
            foreach (var parameter in method.Parameters.InputParameterMetadata.Where(p => p.IsSpan))
            {
                var unsafeMatcher = PrefixUnaryExpression(
                    Microsoft.CodeAnalysis.CSharp.SyntaxKind.LogicalNotExpression,
                    IdentifierName(method.MethodImposter.Builder.ArgumentsCriteriaField.Name)
                        .Dot(IdentifierName(parameter.Name))
                        .Dot(IdentifierName("CanReuseCapturedArray"))
                );
                exposesArray = exposesArray is null
                    ? unsafeMatcher
                    : BinaryExpression(
                        Microsoft.CodeAnalysis.CSharp.SyntaxKind.LogicalOrExpression,
                        exposesArray,
                        unsafeMatcher
                    );
            }
            if (exposesArray is not null)
                statements.Add(
                    IfStatement(
                        exposesArray,
                        methodImposterAccess
                            .Dot(
                                IdentifierName(
                                    MethodImposterCollectionMetadata.CanReuseCapturedSpansFieldName
                                )
                            )
                            .Assign(
                                LiteralExpression(
                                    Microsoft.CodeAnalysis.CSharp.SyntaxKind.FalseLiteralExpression
                                )
                            )
                            .ToStatementSyntax()
                    )
                );

            var addNewCall = methodImposterAccess
                .Dot(method.WithGenericArguments(MethodImposterCollectionMetadata.AddNewMethodName))
                .Call();

            statements.Add(
                LocalVariableDeclarationSyntax(
                    method.MethodImposter.Syntax,
                    "methodImposter",
                    addNewCall
                )
            );

            methodImposterAccess = IdentifierName("methodImposter");
        }

        statements.Add(
            AdvanceToNewInvocationImposter(
                method,
                ThisExpression()
                    .Dot(
                        IdentifierName(
                            method.MethodImposter.Builder.InvocationImposterGroupField.Name
                        )
                    )
            )
        );

        statements.Add(
            methodImposterAccess
                .Dot(IdentifierName(method.MethodImposter.InvocationImposterGroupsField.Name))
                .Dot(SetupCollectionSyntaxHelper.Push)
                .Call(
                    Argument(
                        IdentifierName(
                            method.MethodImposter.Builder.InvocationImposterGroupField.Name
                        )
                    )
                )
                .ToStatementSyntax()
        );

        return statements;
    }
}
