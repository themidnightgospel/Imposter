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
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddBaseType(SimpleBaseType(method.InvocationHistory.Collection.NodeSyntax))
            .AddBaseType(SimpleBaseType(method.InvocationHistory.Interface.Syntax))
            .AddMembers(BuildStoredFields(method, fields))
            .AddMembers(BuildPublicProperties(method))
            .AddMember(BuildHistoryConstructor(method, fields))
            .AddMember(BuildMatchesMethod(method))
            .AddMember(BuildToStringMethod(method))
            .AddMember(FormatValueMethodBuilder.Build())
            .Build();
    }

    private static MemberDeclarationSyntax[] BuildPublicProperties(
        in ImposterTargetMethodMetadata method
    )
    {
        var properties = new System.Collections.Generic.List<MemberDeclarationSyntax>();
        foreach (var parameter in method.Parameters.MatchedParameterMetadata)
        {
            TypeSyntax type = parameter.NullableAwareKeptTypeSyntax;
            if (parameter.Model.Span is { } span)
                type = WellKnownTypes.System.ReadOnlyMemory(
                    SyntaxFactoryHelper.TypeSyntaxIncludingNullable(span.ElementType)
                );
            properties.Add(
                PublicProperty(
                    type,
                    method.InvocationHistory.ArgumentProperties[parameter.Name],
                    IdentifierName(method.InvocationHistory.ArgumentFields[parameter.Name])
                )
            );
        }
        if (method.KeepsResult)
        {
            TypeSyntax resultType = method.ReturnType.KeptTypeSyntax;
            ExpressionSyntax result = IdentifierName(InvocationHistoryTypeMetadata.ResultFieldName);
            if (method.ReturnType.IsAwaitable)
            {
                resultType =
                    resultType is NullableTypeSyntax ? resultType : NullableType(resultType);
                if (method.ReturnType.IsValueTask)
                    result = ConditionalExpression(
                        IdentifierName(InvocationHistoryTypeMetadata.ExceptionFieldName).IsNull(),
                        CastExpression(resultType, result),
                        SyntaxFactoryHelper.Null
                    );
            }
            if (method.Model.ReturnType.Span is { } span)
                resultType = WellKnownTypes.System.ReadOnlyMemory(
                    SyntaxFactoryHelper.TypeSyntaxIncludingNullable(span.ElementType)
                );
            properties.Add(PublicProperty(resultType, "Result", result));
        }
        properties.Add(
            PublicProperty(WellKnownTypes.String, "Member", method.Model.Name.StringLiteral(), true)
        );
        var stamp = VolatileSyntaxHelper.VolatileRead(
            InvocationHistoryTypeMetadata.SequenceFieldName
        );
        properties.Add(
            PublicProperty(
                WellKnownTypes.Long,
                "Sequence",
                BinaryExpression(
                    SyntaxKind.BitwiseAndExpression,
                    stamp,
                    WellKnownTypes.Long.Dot(IdentifierName("MaxValue"))
                ),
                true
            )
        );
        properties.Add(
            PublicProperty(
                WellKnownTypes.Bool,
                "IsVerified",
                BinaryExpression(
                    SyntaxKind.LessThanExpression,
                    stamp,
                    SyntaxFactoryHelper.NumericLiteral(0)
                ),
                true,
                infrastructure: true
            )
        );
        properties.Add(
            new MethodDeclarationBuilder(WellKnownTypes.Void, "MarkVerified")
                .AddModifier(Token(SyntaxKind.InternalKeyword))
                .AddModifier(Token(SyntaxKind.OverrideKeyword))
                .WithBody(
                    Block(
                        IfStatement(IdentifierName("IsVerified"), ReturnStatement()),
                        WellKnownTypes
                            .System.Threading.Volatile.Dot(IdentifierName("Write"))
                            .Call([
                                SyntaxFactoryHelper.RefArgument(
                                    IdentifierName(InvocationHistoryTypeMetadata.SequenceFieldName)
                                ),
                                Argument(
                                    BinaryExpression(
                                        SyntaxKind.BitwiseOrExpression,
                                        stamp,
                                        WellKnownTypes.Long.Dot(IdentifierName("MinValue"))
                                    )
                                ),
                            ])
                            .ToStatementSyntax()
                    )
                )
                .Build()
        );
        properties.Add(
            PublicProperty(
                WellKnownTypes.System.Exception.ToNullableType(),
                "Exception",
                IdentifierName(InvocationHistoryTypeMetadata.ExceptionFieldName),
                true
            )
        );
        return properties.ToArray();
    }

    private static PropertyDeclarationSyntax PublicProperty(
        TypeSyntax type,
        string name,
        ExpressionSyntax value,
        bool isOverride = false,
        bool infrastructure = false
    )
    {
        var builder = new PropertyDeclarationBuilder(type, name).AddModifier(
            Token(infrastructure ? SyntaxKind.InternalKeyword : SyntaxKind.PublicKeyword)
        );
        if (isOverride)
            builder.AddModifier(Token(SyntaxKind.OverrideKeyword));
        return builder.WithGetterBody(Block(ReturnStatement(value))).Build();
    }

    private static MethodDeclarationSyntax BuildToStringMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.String, nameof(ToString))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.OverrideKeyword))
            .WithBody(Block(ReturnStatement(BuildInvocationDescription(method))))
            .Build();

    private static BinaryExpressionSyntax BuildInvocationDescription(
        in ImposterTargetMethodMetadata method
    )
    {
        var methodNameLiteral = $"{method.Model.Name}(".StringLiteral();

        ExpressionSyntax argumentsExpression = method.Parameters.HasInputParameters
            ? BuildArgumentsText(method)
            : string.Empty.StringLiteral();

        var closingLiteral = ")".StringLiteral();

        ExpressionSyntax description = methodNameLiteral
            .Add(argumentsExpression)
            .Add(closingLiteral);

        if (method.KeepsResult)
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
        var argumentFields = method.InvocationHistory.ArgumentFields;

        var argumentDescriptions = method
            .Parameters.InputParameterMetadata.Select(
                ExpressionSyntax (parameter) =>
                    $"{parameter.Model.Name}: "
                        .StringLiteral()
                        .Add(Invocation(IdentifierName(argumentFields[parameter.Name])))
            )
            .ToArray();

        return WellKnownTypes
            .System.String.Dot(IdentifierName("Join"))
            .Call([
                Argument(", ".StringLiteral()),
                Argument(
                    ImplicitArrayCreationExpression(
                        InitializerExpression(
                            SyntaxKind.ArrayInitializerExpression,
                            SeparatedList(argumentDescriptions)
                        )
                    )
                ),
            ]);
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
