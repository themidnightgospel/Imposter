using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FormatValueMethodBuilder;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory;

internal static partial class InvocationHistoryBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var fields = GetFields(method).ToArray();

        return new ClassDeclarationBuilder(
            method.InvocationHistory.Name,
            method.GenericTypeParameterListSyntax
        )
            .WithTypeParameterConstraintClauses(method.GenericTypeConstraintClauses)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(method.InvocationHistory.Interface.Syntax))
            .AddMembers(fields)
            .AddMember(BuildConstructor(method.InvocationHistory.Name, fields))
            .AddMember(BuildMatchesMethod(method))
            .AddMember(BuildToStringMethod(method))
            .AddMember(FormatValueMethodBuilder.Build())
            .Build();

        static ConstructorDeclarationSyntax BuildConstructor(
            in string className,
            IReadOnlyList<FieldDeclarationSyntax> fields
        ) =>
            new ConstructorBuilder(className)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .AddParameters(
                    fields.Select(field =>
                        SyntaxFactoryHelper.ParameterSyntax(
                            field.Declaration.Type,
                            field.Declaration.Variables[0].Identifier.Text
                        )
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
        new MethodDeclarationBuilder(WellKnownTypes.String, "ToString")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.OverrideKeyword))
            .WithBody(Block(ReturnStatement(BuildInvocationDescription(method))))
            .Build();

    private static BinaryExpressionSyntax BuildInvocationDescription(
        in ImposterTargetMethodMetadata method
    )
    {
        var methodNameLiteral = $"{method.Symbol.Name}(".StringLiteral();

        ExpressionSyntax argumentsExpression = method.Parameters.HasInputParameters
            ? BuildArgumentsText(method)
            : string.Empty.StringLiteral();

        var closingLiteral = ")".StringLiteral();

        ExpressionSyntax description = methodNameLiteral
            .Add(argumentsExpression)
            .Add(closingLiteral);

        if (method.HasReturnValue)
        {
            description = description.Add(
                " => "
                    .StringLiteral()
                    .Add(Invocation(IdentifierName(InvocationHistoryTypeMetadata.ResultFieldName)))
            );
        }

        return description.Add(ParenthesizedExpression(BuildExceptionText()));
    }

    private static InvocationExpressionSyntax BuildArgumentsText(
        in ImposterTargetMethodMetadata method
    )
    {
        var argumentsIdentifier = IdentifierName(InvocationHistoryTypeMetadata.ArgumentsFieldName);

        var argumentDescriptions = method
            .Parameters.InputParameterMetadata.Select(
                ExpressionSyntax (parameter) =>
                    $"{parameter.Symbol.Name}: "
                        .StringLiteral()
                        .Add(Invocation(argumentsIdentifier.Dot(IdentifierName(parameter.Name))))
            )
            .ToArray();

        return WellKnownTypes
            .System.String.Dot(IdentifierName("Join"))
            .Call(
                ArgumentList(
                    SeparatedList<ArgumentSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            Argument(", ".StringLiteral()),
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
            exceptionIdentifier.IsNull(),
            string.Empty.StringLiteral(),
            " threw ".StringLiteral().Add(Invocation(exceptionIdentifier))
        );
    }
}
