using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.ImposterInstance;

internal static class ConstructorDispatchBuilder
{
    internal static BlockSyntax WithFallback(
        BlockSyntax body,
        BlockSyntax fallback,
        string imposterFieldName
    ) =>
        body.WithStatements(
            body.Statements.Insert(
                0,
                IfStatement(
                    ThisExpression().Dot(IdentifierName(imposterFieldName)).IsNull(),
                    fallback
                )
            )
        );

    internal static BlockSyntax SetterFallback(ExpressionSyntax? baseAssignment) =>
        baseAssignment is null
            ? Block(ReturnVoid)
            : Block(baseAssignment.ToStatementSyntax(), ReturnVoid);

    internal static BlockSyntax MethodFallback(in ImposterTargetMethodMetadata method)
    {
        if (method.SupportsBaseImplementation)
        {
            SimpleNameSyntax name = method.Model.IsGenericMethod
                ? GenericName(
                    Identifier(method.Model.Name),
                    method.GenericTypeArguments.ToTypeArguments()
                )
                : IdentifierName(method.Model.Name);
            var call = BaseExpression()
                .Dot(name)
                .Call(ArgumentListSyntax(method.Parameters.AllParameters, includeRefKind: true));
            return method.HasReturnValue
                ? Block(ReturnStatement(call))
                : Block(call.ToStatementSyntax(), ReturnVoid);
        }

        var statements = new List<StatementSyntax>(
            method.Parameters.OutputParameters.Select(AssignDefaultValueStatementSyntax)
        );
        statements.Add(method.HasReturnValue ? ReturnDefaultNonNullable : ReturnVoid);
        return Block(statements);
    }
}
