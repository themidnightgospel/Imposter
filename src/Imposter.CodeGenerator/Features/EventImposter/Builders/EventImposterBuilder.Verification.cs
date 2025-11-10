using System;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImposter.Builders;

internal static partial class EventImposterBuilder
{
    private static MethodDeclarationSyntax BuildSubscribedVerificationMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Subscribed;
        var criteriaName = method.CriteriaParameter.Name;

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.CriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.SubscribeHistory,
                    criteriaParameterName: criteriaName,
                    predicateFactory: handler => IdentifierName(criteriaName)
                        .Dot(IdentifierName("Matches"))
                        .Call(Argument(handler)),
                    action: "subscribed"))
            .Build();
    }

    private static MethodDeclarationSyntax BuildUnsubscribedVerificationMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Unsubscribed;
        var criteriaName = method.CriteriaParameter.Name;

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.CriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.UnsubscribeHistory,
                    criteriaParameterName: criteriaName,
                    predicateFactory: handler => IdentifierName(criteriaName)
                        .Dot(IdentifierName("Matches"))
                        .Call(Argument(handler)),
                    action: "unsubscribed"))
            .Build();
    }

    private static MethodDeclarationSyntax BuildRaisedVerificationMethod(in ImposterEventMetadata @event)
    {
        var methodBuilder = ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.Builder.Methods.RaisedVerificationName)
            .AddParameters(@event.Core.Parameters.Select(parameter =>
                ParameterSyntax(parameter.ArgTypeSyntax, $"{parameter.Name}Criteria")))
            .AddParameter(CountParameter(@event));

        return methodBuilder.WithBody(BuildRaisedBody(@event)).Build();
    }

    private static BlockSyntax BuildRaisedBody(in ImposterEventMetadata @event)
    {
        var blockBuilder = new BlockBuilder();
        var countParameterName = @event.Builder.Methods.CountParameter.Name;
        var countIdentifier = IdentifierName(countParameterName);
        var countMatchesIdentifier = IdentifierName(@event.Builder.Methods.CountMatchesName);
        var ensureCountMatchesIdentifier = IdentifierName(@event.Builder.Methods.EnsureCountMatchesName);

        foreach (var parameter in @event.Core.Parameters)
        {
            blockBuilder.AddExpression(ThrowIfNull($"{parameter.Name}Criteria"));
        }

        blockBuilder.AddExpression(ThrowIfNull(countParameterName));

        var predicate = BuildRaisedPredicate(@event);

        blockBuilder.AddStatement(
            LocalVariableDeclarationSyntax(
                PredefinedType(Token(SyntaxKind.IntKeyword)),
                "actual",
                countMatchesIdentifier
                    .Call([
                        Argument(FieldIdentifier(@event.Builder.Fields.History)),
                        Argument(predicate)
                    ])));

        blockBuilder.AddExpression(
            ensureCountMatchesIdentifier
                .Call([
                    Argument(IdentifierName("actual")),
                    Argument(countIdentifier),
                    Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal("raised")))
                ]));

        blockBuilder.AddStatement(ReturnStatement(ThisExpression()));

        return blockBuilder.Build();
    }

    private static SimpleLambdaExpressionSyntax BuildRaisedPredicate(in ImposterEventMetadata @event)
    {
        var entryIdentifier = IdentifierName("entry");
        ExpressionSyntax? predicateBody = null;

        if (@event.Core.Parameters.Length > 0)
        {
            foreach (var parameter in @event.Core.Parameters)
            {
                var matchCall = IdentifierName($"{parameter.Name}Criteria")
                    .Dot(IdentifierName("Matches"))
                    .Call(Argument(entryIdentifier.Dot(IdentifierName(parameter.Name))));

                predicateBody = predicateBody is null
                    ? matchCall
                    : BinaryExpression(SyntaxKind.LogicalAndExpression, predicateBody, matchCall);
            }
        }
        else
        {
            predicateBody = LiteralExpression(SyntaxKind.TrueLiteralExpression);
        }

        return SimpleLambdaExpression(Parameter(Identifier("entry")), predicateBody!);
    }

    private static MethodDeclarationSyntax BuildHandlerInvokedVerificationMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.HandlerInvoked;
        var criteriaName = method.HandlerCriteriaParameter.Name;

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.HandlerCriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.HandlerInvocations,
                    criteriaParameterName: criteriaName,
                    predicateFactory: entry => IdentifierName(criteriaName)
                        .Dot(IdentifierName("Matches"))
                        .Call(Argument(entry.Dot(IdentifierName("Handler")))),
                    action: "invoked"))
            .Build();
    }

    private static BlockSyntax BuildHistoryVerificationBody(
        in ImposterEventMetadata @event,
        in FieldMetadata historyField,
        string criteriaParameterName,
        Func<ExpressionSyntax, ExpressionSyntax> predicateFactory,
        string action)
    {
        var countParameterName = @event.Builder.Methods.CountParameter.Name;
        var countIdentifier = IdentifierName(countParameterName);
        var countMatchesIdentifier = IdentifierName(@event.Builder.Methods.CountMatchesName);
        var ensureCountMatchesIdentifier = IdentifierName(@event.Builder.Methods.EnsureCountMatchesName);

        var blockBuilder = new BlockBuilder()
            .AddExpression(ThrowIfNull(criteriaParameterName))
            .AddExpression(ThrowIfNull(countParameterName));

        blockBuilder.AddStatement(
            LocalVariableDeclarationSyntax(
                PredefinedType(Token(SyntaxKind.IntKeyword)),
                "actual",
                countMatchesIdentifier
                    .Call([
                        Argument(FieldIdentifier(historyField)),
                        Argument(
                            SimpleLambdaExpression(
                                Parameter(Identifier("entry")),
                                predicateFactory(IdentifierName("entry"))))
                    ])));

        blockBuilder.AddExpression(
            ensureCountMatchesIdentifier
                .Call([
                    Argument(IdentifierName("actual")),
                    Argument(countIdentifier),
                    Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(action)))
                ]));

        blockBuilder.AddStatement(ReturnStatement(ThisExpression()));

        return blockBuilder.Build();
    }

    private static MethodDeclarationSyntax BuildCountMatchesMethod(in EventImposterBuilderMethodsMetadata methods)
    {
        var enumerableType = QualifiedName(
            WellKnownTypes.System.Collections.Generic.Namespace,
            GenericName(
                Identifier("IEnumerable"),
                TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName("T")))));

        var funcType = QualifiedName(
            WellKnownTypes.System.Namespace,
            GenericName(
                Identifier("Func"),
                TypeArgumentList(
                    SeparatedList<TypeSyntax>([
                        IdentifierName("T"),
                        PredefinedType(Token(SyntaxKind.BoolKeyword))
                    ]))));

        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.IntKeyword)), methods.CountMatchesName)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithTypeParameters(TypeParameterList(SingletonSeparatedList(TypeParameter("T"))))
            .WithParameterList(
                ParameterList(
                    SeparatedList([
                        Parameter(Identifier("source")).WithType(enumerableType),
                        Parameter(Identifier("predicate")).WithType(funcType)
                    ])))
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        PredefinedType(Token(SyntaxKind.IntKeyword)),
                        "count",
                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))),
                    ForEachStatement(
                        Var,
                        Identifier("item"),
                        IdentifierName("source"),
                        Block(
                            IfStatement(
                                IdentifierName("predicate").Call(
                                    ArgumentList(SingletonSeparatedList(Argument(IdentifierName("item"))))),
                                Block(
                                    ExpressionStatement(
                                        PostfixUnaryExpression(
                                            SyntaxKind.PostIncrementExpression,
                                            IdentifierName("count"))))))),
                    ReturnStatement(IdentifierName("count"))))
            .Build();
    }

    private static MethodDeclarationSyntax BuildEnsureCountMatchesMethod(in EventImposterBuilderMethodsMetadata methods) =>
        new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.VoidKeyword)), methods.EnsureCountMatchesName)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithParameterList(
                ParameterList(SeparatedList([
                    Parameter(Identifier("actual")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("expected")).WithType(WellKnownTypes.Imposter.Abstractions.Count),
                    Parameter(Identifier("action")).WithType(PredefinedType(Token(SyntaxKind.StringKeyword)))
                ])))
            .WithBody(
                Block(
                    IfStatement(
                        PrefixUnaryExpression(
                            SyntaxKind.LogicalNotExpression,
                            IdentifierName("expected")
                                .Dot(IdentifierName("Matches"))
                                .Call(Argument(IdentifierName("actual")))),
                        Block(
                            ThrowStatement(
                                ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.VerificationFailedException)
                                    .WithArgumentList(
                                        ArgumentList(SeparatedList([
                                            Argument(IdentifierName("expected")),
                                            Argument(IdentifierName("actual"))
                                        ]))))))))
            .Build();

    private static ParameterSyntax CountParameter(in ImposterEventMetadata @event) =>
        ParameterSyntax(@event.Builder.Methods.CountParameter);
}
