using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory;

internal static partial class InvocationHistoryBuilder
{
    // A Task and a synchronous Exception cannot be the same object. Successful calls retain
    // the exact Task (including faulted tasks); failed calls retain only the thrown exception.
    // Sharing their slot avoids another eight bytes when history also needs a final out value.
    private static bool SharesTaskOutcome(in ImposterTargetMethodMetadata method) =>
        method.ReturnType.IsAwaitable && !method.ReturnType.IsValueTask;

    private static MemberDeclarationSyntax[] BuildStoredFields(
        in ImposterTargetMethodMetadata method,
        FieldDeclarationSyntax[] fields
    )
    {
        if (!SharesTaskOutcome(method))
            return fields;

        var members = new List<MemberDeclarationSyntax>(
            fields.Where(field => !IsOutcomeField(field.Declaration.Variables[0].Identifier.Text))
        );

        members.Add(
            SingleVariableField(
                WellKnownTypes.Object.ToNullableType(),
                InvocationHistoryTypeMetadata.OutcomeFieldName,
                TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword))
            )
        );
        members.Add(
            new PropertyDeclarationBuilder(
                method.ReturnType.KeptTypeSyntax,
                InvocationHistoryTypeMetadata.ResultFieldName
            )
                .AddModifier(Token(SyntaxKind.PrivateKeyword))
                .WithGetterBody(
                    Block(
                        ReturnStatement(
                            PostfixUnaryExpression(
                                SyntaxKind.SuppressNullableWarningExpression,
                                ParenthesizedExpression(
                                    BinaryExpression(
                                        SyntaxKind.AsExpression,
                                        IdentifierName(
                                            InvocationHistoryTypeMetadata.OutcomeFieldName
                                        ),
                                        method.ReturnType.KeptTypeSyntax
                                            is NullableTypeSyntax nullable
                                            ? nullable.ElementType
                                            : method.ReturnType.KeptTypeSyntax
                                    )
                                )
                            )
                        )
                    )
                )
                .Build()
        );
        members.Add(
            new PropertyDeclarationBuilder(
                WellKnownTypes.System.Exception.ToNullableType(),
                InvocationHistoryTypeMetadata.ExceptionFieldName
            )
                .AddModifier(Token(SyntaxKind.PrivateKeyword))
                .WithGetterBody(
                    Block(
                        ReturnStatement(
                            BinaryExpression(
                                SyntaxKind.AsExpression,
                                IdentifierName(InvocationHistoryTypeMetadata.OutcomeFieldName),
                                WellKnownTypes.System.Exception
                            )
                        )
                    )
                )
                .Build()
        );
        return members.ToArray();
    }

    private static ConstructorDeclarationSyntax BuildHistoryConstructor(
        in ImposterTargetMethodMetadata method,
        FieldDeclarationSyntax[] fields
    )
    {
        var sharesOutcome = SharesTaskOutcome(method);

        var builder = new ConstructorWithFieldInitializationBuilder(
            method.InvocationHistory.Name
        ).WithModifiers(Token(SyntaxKind.PublicKeyword));
        foreach (var field in fields)
        {
            var name = field.Declaration.Variables[0].Identifier.Text;
            var parameter = new ParameterMetadata(
                method.InvocationHistory.ConstructorParameterNames[name],
                field.Declaration.Type
            );
            if (sharesOutcome && IsOutcomeField(name))
                builder.AddParameterWithoutField(parameter);
            else
                builder.AddParameter(parameter, name);
        }
        if (!sharesOutcome)
            return builder.Build();
        return builder
            .AddStatements([
                ThisExpression()
                    .Dot(IdentifierName(InvocationHistoryTypeMetadata.OutcomeFieldName))
                    .Assign(
                        ConditionalExpression(
                            IdentifierName(
                                    method.InvocationHistory.ConstructorParameterNames[
                                        InvocationHistoryTypeMetadata.ExceptionFieldName
                                    ]
                                )
                                .IsNotNull(),
                            IdentifierName(
                                method.InvocationHistory.ConstructorParameterNames[
                                    InvocationHistoryTypeMetadata.ExceptionFieldName
                                ]
                            ),
                            CastExpression(
                                WellKnownTypes.Object.ToNullableType(),
                                IdentifierName(
                                    method.InvocationHistory.ConstructorParameterNames[
                                        InvocationHistoryTypeMetadata.ResultFieldName
                                    ]
                                )
                            )
                        )
                    )
                    .ToStatementSyntax(),
            ])
            .Build();
    }

    private static bool IsOutcomeField(string name) =>
        name
            is InvocationHistoryTypeMetadata.ResultFieldName
                or InvocationHistoryTypeMetadata.ExceptionFieldName;
}
