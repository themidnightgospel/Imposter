using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationHistory;

internal static partial class InvocationHistoryBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var fields = GetFields(method).ToArray();

        return new ClassDeclarationBuilder(
            method.InvocationHistory.Name,
            method.GenericTypeParameterListSyntax
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(method.InvocationHistory.Interface.Syntax))
            .AddMembers(fields)
            .AddMember(BuildConstructor(method.InvocationHistory.Name, fields))
            .AddMember(BuildMatchesMethod(method))
            .AddMember(BuildToStringMethod(method))
            .AddMember(BuildFormatValueMethod())
            .Build();

        static ConstructorDeclarationSyntax BuildConstructor(
            in string className,
            IReadOnlyList<FieldDeclarationSyntax> fields
        ) =>
            new ConstructorBuilder(className)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .AddParameters(
                    fields.Select(field =>
                        Parameter(Identifier(field.Declaration.Variables[0].Identifier.Text))
                            .WithType(field.Declaration.Type)
                    )
                )
                .WithBody(
                    Block(
                        fields.Select(field =>
                            ThisExpression()
                                .Dot(IdentifierName(field.Declaration.Variables[0].Identifier.Text))
                                .Assign(
                                    IdentifierName(field.Declaration.Variables[0].Identifier.Text)
                                )
                                .ToStatementSyntax()
                        )
                    )
                )
                .Build();
    }

    private static MethodDeclarationSyntax BuildToStringMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.StringKeyword)), "ToString")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.OverrideKeyword))
            .WithBody(Block(ReturnStatement(BuildInvocationDescription(method))))
            .Build();

    private static BinaryExpressionSyntax BuildInvocationDescription(
        in ImposterTargetMethodMetadata method
    )
    {
        var methodNameLiteral = LiteralExpression(
            SyntaxKind.StringLiteralExpression,
            Literal($"{method.Symbol.Name}(")
        );

        var argumentsExpression = method.Parameters.HasInputParameters
            ? BuildArgumentsText(method)
            : LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(string.Empty));

        var closingLiteral = LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(")"));

        ExpressionSyntax description = AddStrings(
            AddStrings(methodNameLiteral, argumentsExpression),
            closingLiteral
        );

        if (method.HasReturnValue)
        {
            description = AddStrings(
                description,
                AddStrings(
                    LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(" => ")),
                    FormatValueInvocation(
                        IdentifierName(InvocationHistoryTypeMetadata.ResultFieldName)
                    )
                )
            );
        }

        return AddStrings(description, BuildExceptionText());
    }

    private static ExpressionSyntax BuildArgumentsText(in ImposterTargetMethodMetadata method)
    {
        var argumentsIdentifier = IdentifierName(InvocationHistoryTypeMetadata.ArgumentsFieldName);

        var argumentDescriptions = method
            .Parameters.InputParameters.Select(
                ExpressionSyntax (parameter) =>
                    AddStrings(
                        LiteralExpression(
                            SyntaxKind.StringLiteralExpression,
                            Literal($"{parameter.Name}: ")
                        ),
                        FormatValueInvocation(
                            argumentsIdentifier.Dot(IdentifierName(parameter.Name))
                        )
                    )
            )
            .ToArray();

        if (argumentDescriptions.Length == 0)
        {
            return LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(string.Empty));
        }

        return WellKnownTypes
            .System.String.Dot(IdentifierName("Join"))
            .Call(
                ArgumentList(
                    SeparatedList<ArgumentSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            Argument(
                                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(", "))
                            ),
                            Token(SyntaxKind.CommaToken),
                            Argument(
                                ImplicitArrayCreationExpression(
                                    InitializerExpression(
                                        SyntaxKind.ArrayInitializerExpression,
                                        SeparatedList(argumentDescriptions)
                                    )
                                )
                            ),
                        }
                    )
                )
            );
    }

    private static ConditionalExpressionSyntax BuildExceptionText()
    {
        var exceptionIdentifier = IdentifierName(InvocationHistoryTypeMetadata.ExceptionFieldName);

        return ConditionalExpression(
            BinaryExpression(
                SyntaxKind.EqualsExpression,
                exceptionIdentifier,
                LiteralExpression(SyntaxKind.NullLiteralExpression)
            ),
            LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(string.Empty)),
            AddStrings(
                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(" threw ")),
                FormatValueInvocation(exceptionIdentifier)
            )
        );
    }

    private static MethodDeclarationSyntax BuildFormatValueMethod() =>
        new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.StringKeyword)), "FormatValue")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .AddParameter(
                Parameter(Identifier("value"))
                    .WithType(NullableType(PredefinedType(Token(SyntaxKind.ObjectKeyword))))
            )
            .WithBody(
                Block(
                    ReturnStatement(
                        AddStrings(
                            LiteralExpression(SyntaxKind.StringLiteralExpression, Literal("<")),
                            AddStrings(
                                BinaryExpression(
                                    SyntaxKind.CoalesceExpression,
                                    ConditionalAccessExpression(
                                        IdentifierName("value"),
                                        InvocationExpression(
                                            MemberBindingExpression(IdentifierName("ToString"))
                                        )
                                    ),
                                    LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        Literal("null")
                                    )
                                ),
                                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(">"))
                            )
                        )
                    )
                )
            )
            .Build();

    private static InvocationExpressionSyntax FormatValueInvocation(ExpressionSyntax value) =>
        IdentifierName("FormatValue").Call(Argument(value));

    private static BinaryExpressionSyntax AddStrings(
        ExpressionSyntax left,
        ExpressionSyntax right
    ) => BinaryExpression(SyntaxKind.AddExpression, left, right);
}
