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
    internal static MethodDeclarationSyntax BuildVerifyMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var names = method.ReservedParameterNames.CreateNameSet();
        var count = IdentifierName(names.Use("count"));
        var head = IdentifierName(names.Use("head"));
        var entry = IdentifierName(names.Use("entry"));
        var expected = IdentifierName(names.Use("expected"));
        var criteria = IdentifierName(names.Use("criteria"));
        ExpressionSyntax matches = ParenthesizedExpression(
                CastExpression(method.InvocationHistory.Interface.Syntax, entry)
            )
            .Dot(method.WithGenericArguments(InvocationHistoryMatchesMethodMetadata.Name))
            .Call(
                method.Parameters.HasInputParameters
                    ? Argument(criteria).ToSingleArgumentList()
                    : EmptyArgumentListSyntax
            );

        var builder = new MethodDeclarationBuilder(
            WellKnownTypes.Int,
            InvocationHistoryCollectionMetadata.VerifyMethodName
        )
            .WithTypeParameters(method.GenericTypeParameterListSyntax)
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(
                    WellKnownTypes.Imposter.Abstractions.Count,
                    expected.Identifier.Text
                )
            );
        if (method.Parameters.HasInputParameters)
            builder.AddParameter(
                ParameterSyntax(method.ArgumentsCriteria.Syntax, criteria.Identifier.Text)
            );

        return builder
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        head.Identifier.Text,
                        VolatileSyntaxHelper.VolatileRead(
                            InvocationHistoryCollectionMetadata.HeadFieldName
                        )
                    ),
                    LocalVariableDeclarationSyntax(Var, entry.Identifier.Text, head),
                    LocalVariableDeclarationSyntax(Var, count.Identifier.Text, NumericLiteral(0)),
                    WhileStatement(
                        entry.IsNotNull(),
                        Block(
                            IfStatement(
                                matches,
                                PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, count)
                                    .ToStatementSyntax()
                            ),
                            entry
                                .Assign(
                                    entry.Dot(
                                        IdentifierName(
                                            InvocationHistoryCollectionMetadata.NextFieldName
                                        )
                                    )
                                )
                                .ToStatementSyntax()
                        )
                    ),
                    IfStatement(
                        expected.Dot(IdentifierName("Matches")).Call(Argument(count)),
                        Block(
                            entry.Assign(head).ToStatementSyntax(),
                            WhileStatement(
                                entry.IsNotNull(),
                                Block(
                                    IfStatement(
                                        matches,
                                        entry
                                            .Dot(
                                                IdentifierName(
                                                    InvocationHistoryCollectionMetadata.MarkVerifiedMethodName
                                                )
                                            )
                                            .Call()
                                            .ToStatementSyntax()
                                    ),
                                    entry
                                        .Assign(
                                            entry.Dot(
                                                IdentifierName(
                                                    InvocationHistoryCollectionMetadata.NextFieldName
                                                )
                                            )
                                        )
                                        .ToStatementSyntax()
                                )
                            )
                        )
                    ),
                    ReturnStatement(count)
                )
            )
            .Build();
    }
}
