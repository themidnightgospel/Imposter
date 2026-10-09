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

internal static class IndexerArgumentsBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var equatableType = WellKnownTypes.System.IEquatable(indexer.Arguments.TypeSyntax);

        var classBuilder = new ClassDeclarationBuilder(indexer.Arguments.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(equatableType));

        foreach (var parameter in indexer.Core.Parameters)
        {
            classBuilder = classBuilder.AddMember(
                SingleVariableField(
                    new FieldMetadata(parameter.FieldName, parameter.TypeSyntax),
                    SyntaxKind.PublicKeyword
                )
            );
        }

        classBuilder = classBuilder.AddMember(BuildConstructor(indexer));
        classBuilder = classBuilder.AddMember(BuildEqualsMethod(indexer));
        classBuilder = classBuilder.AddMember(BuildObjectEqualsMethod(indexer));
        classBuilder = classBuilder.AddMember(BuildGetHashCodeMethod(indexer));

        return classBuilder.Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterIndexerMetadata indexer)
    {
        var constructorBuilder = new ConstructorBuilder(indexer.Arguments.Name).WithModifiers(
            TokenList(Token(SyntaxKind.InternalKeyword))
        );

        var bodyBuilder = new BlockBuilder();

        foreach (var parameter in indexer.Core.Parameters)
        {
            constructorBuilder = constructorBuilder.AddParameter(parameter.ParameterSyntax);
            bodyBuilder.AddStatement(
                ThisExpression()
                    .Dot(IdentifierName(parameter.FieldName))
                    .Assign(IdentifierName(parameter.Name))
                    .ToStatementSyntax()
            );
        }

        return constructorBuilder.WithBody(bodyBuilder.Build()).Build();
    }

    private static MethodDeclarationSyntax BuildEqualsMethod(in ImposterIndexerMetadata indexer)
    {
        var otherIdentifier = Identifier(indexer.Arguments.OtherVariableName);
        var otherIdentifierName = IdentifierName(otherIdentifier);
        var otherParameter = Parameter(otherIdentifier)
            .WithType(NullableType(indexer.Arguments.TypeSyntax));

        // EqualityComparer<T>.Default keeps Equals consistent with the generated GetHashCode and with Arg<T>.Is, and
        // works for type parameters and structs without ==.
        ExpressionSyntax? comparison = null;
        foreach (var parameter in indexer.Core.Parameters)
        {
            var equalsExpression = WellKnownTypes
                .System.Collections.Generic.EqualityComparer(parameter.TypeSyntax)
                .Dot(IdentifierName("Default"))
                .Dot(IdentifierName("Equals"))
                .Call([
                    Argument(IdentifierName(parameter.FieldName)),
                    Argument(otherIdentifierName.Dot(IdentifierName(parameter.FieldName))),
                ]);

            comparison = comparison is null ? equalsExpression : comparison.And(equalsExpression);
        }

        comparison ??= True;

        return new MethodDeclarationBuilder(WellKnownTypes.Bool, "Equals")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(otherParameter)
            .WithBody(
                Block(
                    IfStatement(otherIdentifierName.IsNull(), Block(ReturnStatement(False))),
                    ReturnStatement(comparison)
                )
            )
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
                ParameterSyntax(
                    NullableType(PredefinedType(Token(SyntaxKind.ObjectKeyword))),
                    "obj"
                )
            )
            .WithBody(
                Block(
                    ReturnStatement(
                        IsPatternExpression(
                                IdentifierName("obj"),
                                DeclarationPattern(
                                    indexer.Arguments.TypeSyntax,
                                    SingleVariableDesignation(other.Identifier)
                                )
                            )
                            .And(
                                IdentifierName("Equals")
                                    .Call(ArgumentList(SingletonSeparatedList(Argument(other))))
                            )
                    )
                )
            )
            .Build();
    }

    // A manual combine instead of System.HashCode, which .NET Standard 2.0 and .NET Framework lack. The `!` only
    // silences a nullability warning: EqualityComparer<T>.Default returns 0 for null.
    private static MethodDeclarationSyntax BuildGetHashCodeMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var hash = IdentifierName(indexer.Arguments.HashVariableName);
        var statements = new List<StatementSyntax>
        {
            LocalVariableDeclarationSyntax(
                Var,
                hash.Identifier.Text,
                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(17))
            ),
        };

        statements.AddRange(
            indexer.Core.Parameters.Select(parameter =>
                hash.Assign(
                        BinaryExpression(
                            SyntaxKind.AddExpression,
                            BinaryExpression(
                                SyntaxKind.MultiplyExpression,
                                hash,
                                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(31))
                            ),
                            WellKnownTypes
                                .System.Collections.Generic.EqualityComparer(parameter.TypeSyntax)
                                .Dot(IdentifierName("Default"))
                                .Dot(IdentifierName("GetHashCode"))
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
