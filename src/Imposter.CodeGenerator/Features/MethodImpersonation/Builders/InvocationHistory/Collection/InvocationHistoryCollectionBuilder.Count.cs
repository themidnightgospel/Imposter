using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory.Collection;

internal static partial class InvocationHistoryCollectionBuilder
{
    internal static MethodDeclarationSyntax BuildCountMethod(in ImposterTargetMethodMetadata method)
    {
        var names = new InvocationHistoryCollectionCountMethodMetadata(method);
        return new MethodDeclarationBuilder(
            WellKnownTypes.Int,
            InvocationHistoryCollectionCountMethodMetadata.Name
        )
            .WithTypeParameters(method.GenericTypeParameterListSyntax)
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(GetParameter(method))
            .WithBody(
                new BlockBuilder()
                    .AddStatement(
                        LocalVariableDeclarationSyntax(
                            Var,
                            names.CountVariableName,
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))
                        )
                    )
                    .AddStatements(
                        ForEachEntry(
                            names.InvocationVariableName,
                            IfStatement(
                                ParenthesizedExpression(
                                        CastExpression(
                                            method.InvocationHistory.Interface.Syntax,
                                            IdentifierName(names.InvocationVariableName)
                                        )
                                    )
                                    .Dot(
                                        method.WithGenericArguments(
                                            InvocationHistoryMatchesMethodMetadata.Name
                                        )
                                    )
                                    .Call(
                                        GetMatchesMethodArguments(method)?.ToSingleArgumentList()
                                    ),
                                PostfixUnaryExpression(
                                        SyntaxKind.PostIncrementExpression,
                                        IdentifierName(names.CountVariableName)
                                    )
                                    .ToStatementSyntax()
                            )
                        )
                    )
                    .AddStatement(ReturnStatement(IdentifierName(names.CountVariableName)))
                    .Build()
            )
            .Build();

        static ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method)
        {
            return method.Parameters.HasInputParameters
                ? ParameterSyntax(
                    method.ArgumentsCriteria.Syntax,
                    InvocationHistoryCollectionCountMethodMetadata.ArgumentsCriteriaParameterName
                )
                : null;
        }

        static ArgumentSyntax? GetMatchesMethodArguments(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? Argument(
                    IdentifierName(
                        InvocationHistoryCollectionCountMethodMetadata.ArgumentsCriteriaParameterName
                    )
                )
                : null;
    }
}
