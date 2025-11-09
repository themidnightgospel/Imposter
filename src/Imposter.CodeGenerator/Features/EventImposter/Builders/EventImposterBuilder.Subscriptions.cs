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
    private static MethodDeclarationSyntax BuildSubscribeMethod(in ImposterEventMetadata @event)
    {
        var fields = @event.Builder.Fields;
        var method = @event.Builder.Methods.Subscribe;
        var handlerIdentifier = IdentifierName(method.HandlerParameter.Name);

        var addOrUpdateExpression =
            FieldIdentifier(fields.HandlerCounts)
                .Dot(IdentifierName("AddOrUpdate"))
                .Call([
                    Argument(handlerIdentifier),
                    Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1))),
                    Argument(BuildIncrementLambda())
                ]);

        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.VoidKeyword)), method.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.HandlerParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.HandlerParameter.Name))
                    .AddExpression(
                        FieldIdentifier(fields.HandlerOrder)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(handlerIdentifier)))
                    .AddExpression(addOrUpdateExpression)
                    .AddExpression(
                        FieldIdentifier(fields.SubscribeHistory)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(handlerIdentifier)))
                    .AddStatement(ForEachInterceptor(fields.SubscribeInterceptors, method.HandlerParameter.Name))
                    .Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildUnsubscribeMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Unsubscribe;
        var handlerIdentifier = IdentifierName(method.HandlerParameter.Name);

        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.VoidKeyword)), method.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.HandlerParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.HandlerParameter.Name))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.HandlerCounts)
                            .Dot(IdentifierName("AddOrUpdate"))
                            .Call([
                                Argument(handlerIdentifier),
                                Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))),
                                Argument(BuildUnsubscribeLambda())
                            ]))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.UnsubscribeHistory)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(handlerIdentifier)))
                    .AddStatement(ForEachInterceptor(@event.Builder.Fields.UnsubscribeInterceptors, method.HandlerParameter.Name))
                    .Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildCallbackMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Callback;
        var callbackIdentifier = IdentifierName(method.CallbackParameter.Name);

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.CallbackParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.CallbackParameter.Name))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.Callbacks)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(callbackIdentifier)))
                    .AddStatement(ReturnStatement(ThisExpression()))
                    .Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildOnSubscribeMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.OnSubscribe;
        var interceptorIdentifier = IdentifierName(method.InterceptorParameter.Name);

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.InterceptorParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.InterceptorParameter.Name))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.SubscribeInterceptors)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(interceptorIdentifier)))
                    .AddStatement(ReturnStatement(ThisExpression()))
                    .Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildOnUnsubscribeMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.OnUnsubscribe;
        var interceptorIdentifier = IdentifierName(method.InterceptorParameter.Name);

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.InterceptorParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.InterceptorParameter.Name))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.UnsubscribeInterceptors)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(interceptorIdentifier)))
                    .AddStatement(ReturnStatement(ThisExpression()))
                    .Build())
            .Build();
    }

    private static ForEachStatementSyntax ForEachInterceptor(in FieldMetadata interceptorsField, string handlerIdentifier) =>
        ForEachStatement(
            Var,
            Identifier("interceptor"),
            FieldIdentifier(interceptorsField),
            Block(
                ExpressionStatement(
                    IdentifierName("interceptor")
                        .Call(Argument(IdentifierName(handlerIdentifier))))));

    private static ParenthesizedLambdaExpressionSyntax BuildUnsubscribeLambda() =>
        ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SeparatedList([
                        Parameter(Identifier("_")),
                        Parameter(Identifier("count"))
                    ])))
            .WithBlock(
                Block(
                    IfStatement(
                        BinaryExpression(
                            SyntaxKind.GreaterThanExpression,
                            IdentifierName("count"),
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))),
                        Block(
                            ReturnStatement(
                                BinaryExpression(
                                    SyntaxKind.SubtractExpression,
                                    IdentifierName("count"),
                                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)))))),
                    ReturnStatement(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)))));

    private static ParenthesizedLambdaExpressionSyntax BuildIncrementLambda() =>
        ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SeparatedList([
                        Parameter(Identifier("_")),
                        Parameter(Identifier("count"))
                    ])))
            .WithExpressionBody(
                BinaryExpression(
                    SyntaxKind.AddExpression,
                    IdentifierName("count"),
                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1))));
}
