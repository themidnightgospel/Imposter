using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
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

        ExpressionSyntax methodImposterAccess;

        if (method.Symbol.IsGenericMethod)
        {
            var addNewCall = IdentifierName(
                    method.MethodImposter.Builder.ImposterCollectionField.Name
                )
                .Dot(
                    GenericName(Identifier("AddNew"), method.GenericTypeArguments.ToTypeArguments())
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
        else
        {
            methodImposterAccess = IdentifierName(
                method.MethodImposter.Builder.MethodImposterField.Name
            );
        }

        statements.Add(
            methodImposterAccess
                .Dot(IdentifierName(method.MethodImposter.InvocationImpostersField.Name))
                .Dot(IdentifierName("Push"))
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
            ThisExpression()
                .Dot(
                    IdentifierName(
                        method.MethodImposter.Builder.CurrentInvocationImposterField.Name
                    )
                )
                .Assign(
                    ThisExpression()
                        .Dot(
                            IdentifierName(
                                method.MethodImposter.Builder.InvocationImposterGroupField.Name
                            )
                        )
                        .Dot(IdentifierName("AddInvocationImposter"))
                        .Call()
                )
                .ToStatementSyntax()
        );

        return statements;
    }
}
