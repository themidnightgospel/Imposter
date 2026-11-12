using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

// A dictionary key owns only index values, never a record or its retained history chain.
internal static class IndexerStoredKeyBuilder
{
    internal static StructDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var type = indexer.DefaultIndexerBehaviour.StoredKeyType;
        var members = new List<MemberDeclarationSyntax>(IndexerArgumentsBuilder.Fields(indexer));
        if (indexer.Core.MatchedParameters.Length != 0)
            members.Add(IndexerArgumentsBuilder.Constructor(indexer, type.ToString()));
        members.Add(BuildEqualsMethod(indexer));
        members.Add(BuildObjectEqualsMethod(indexer));
        members.Add(BuildGetHashCodeMethod(indexer));
        return StructDeclaration(type.ToString())
            .WithModifiers(
                TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.ReadOnlyKeyword))
            )
            .WithBaseList(
                BaseList(
                    SingletonSeparatedList<BaseTypeSyntax>(
                        SimpleBaseType(WellKnownTypes.System.IEquatable(type))
                    )
                )
            )
            .WithMembers(List(members));
    }

    private static MethodDeclarationSyntax BuildEqualsMethod(in ImposterIndexerMetadata indexer)
    {
        var otherIdentifierName = IdentifierName(indexer.Arguments.OtherVariableName);
        var otherParameter = ParameterSyntax(
            indexer.DefaultIndexerBehaviour.StoredKeyType,
            indexer.Arguments.OtherVariableName
        );

        // A comparer keeps Equals consistent with the generated GetHashCode and with Arg<T>.Is, and works for type
        // parameters and structs without ==.
        var comparison = indexer
            .Core.MatchedParameters.Select(parameter =>
                (ExpressionSyntax)
                    parameter
                        .EqualityComparer.Dot(IdentifierName("Equals"))
                        .Call([
                            Argument(IdentifierName(parameter.FieldName)),
                            Argument(otherIdentifierName.Dot(IdentifierName(parameter.FieldName))),
                        ])
            )
            .DefaultIfEmpty(True)
            .Aggregate((current, next) => current.And(next));

        return new MethodDeclarationBuilder(WellKnownTypes.Bool, "Equals")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(otherParameter)
            .WithBody(Block(ReturnStatement(comparison)))
            .Build();
    }

    private static MethodDeclarationSyntax BuildObjectEqualsMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var other = IdentifierName(indexer.Arguments.OtherVariableName);

        return new MethodDeclarationBuilder(WellKnownTypes.Bool, "Equals")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.OverrideKeyword))
            .AddParameter(
                ParameterSyntax(WellKnownTypes.Object.ToNullableType(), indexer.Locals["obj"])
            )
            .WithBody(
                Block(
                    ReturnStatement(
                        IsPatternExpression(
                                IdentifierName(indexer.Locals["obj"]),
                                DeclarationPattern(
                                    indexer.DefaultIndexerBehaviour.StoredKeyType,
                                    SingleVariableDesignation(other.Identifier)
                                )
                            )
                            .And(IdentifierName("Equals").Call(Argument(other)))
                    )
                )
            )
            .Build();
    }

    // A manual combine instead of System.HashCode, which .NET Standard 2.0 and .NET Framework lack. The `!` only
    // silences a nullability warning: both comparers return 0 for null.
    private static MethodDeclarationSyntax BuildGetHashCodeMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var hash = IdentifierName(indexer.Arguments.HashVariableName);
        var statements = new List<StatementSyntax>
        {
            LocalVariableDeclarationSyntax(Var, hash.Identifier.Text, NumericLiteral(17)),
        };

        statements.AddRange(
            indexer.Core.MatchedParameters.Select(parameter =>
                hash.Assign(
                        BinaryExpression(
                            SyntaxKind.AddExpression,
                            BinaryExpression(
                                SyntaxKind.MultiplyExpression,
                                hash,
                                NumericLiteral(31)
                            ),
                            parameter
                                .EqualityComparer.Dot(IdentifierName("GetHashCode"))
                                .Call(
                                    Argument(
                                        PostfixUnaryExpression(
                                            SyntaxKind.SuppressNullableWarningExpression,
                                            IdentifierName(parameter.FieldName)
                                        )
                                    )
                                )
                        )
                    )
                    .ToStatementSyntax()
            )
        );

        statements.Add(ReturnStatement(hash));

        return new MethodDeclarationBuilder(WellKnownTypes.Int, "GetHashCode")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.OverrideKeyword))
            .WithBody(Block(CheckedStatement(SyntaxKind.UncheckedStatement, Block(statements))))
            .Build();
    }
}
