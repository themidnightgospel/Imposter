using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static List<StatementSyntax> BuildInvocationSetupInitializationStatements(
        in ImposterTargetMethodMetadata method
    )
    {
        var statements = new List<StatementSyntax>();

        var groupCreation = method.MethodInvocationImposterGroup.Syntax.New(
            method.Parameters.HasInputParameters
                ? Argument(
                        IdentifierName(method.MethodImposter.Builder.ArgumentsCriteriaField.Name)
                    )
                    .AsSingleArgumentListSyntax()
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
            var addNewCall = methodImposterAccess
                .Dot(
                    GenericName(
                        Identifier(MethodImposterCollectionMetadata.AddNewMethodName),
                        method.GenericTypeArguments.ToTypeArguments()
                    )
                )
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
            methodImposterAccess
                .Dot(IdentifierName(method.MethodImposter.InvocationImpostersField.Name))
                .Dot(ConcurrentStackSyntaxHelper.Push)
                .Call(
                    Argument(
                            IdentifierName(
                                method.MethodImposter.Builder.InvocationImposterGroupField.Name
                            )
                        )
                        .AsSingleArgumentListSyntax()
                )
                .ToStatementSyntax()
        );

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

        return statements;
    }
}
