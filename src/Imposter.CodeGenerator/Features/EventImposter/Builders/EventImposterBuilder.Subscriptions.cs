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
                    Argument(CounterIncrementLambda())
                ]);

        var methodBuilder = new MethodDeclarationBuilder(WellKnownTypes.Void, method.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.HandlerParameter));

        if (method.BaseImplementationParameter is { } subscribeBaseParameter)
        {
            methodBuilder = methodBuilder.AddParameter(ParameterSyntax(subscribeBaseParameter));
        }

        var blockBuilder = new BlockBuilder()
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
            .AddStatement(ForEachInterceptor(fields.SubscribeInterceptors, method.HandlerParameter.Name));

        if (method.BaseImplementationParameter is { } subscribeBaseImplementationParameter)
        {
            blockBuilder.AddStatement(
                BuildBaseImplementationInvocation(IdentifierName(subscribeBaseImplementationParameter.Name)));
        }

        return methodBuilder
            .WithBody(blockBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildUnsubscribeMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Unsubscribe;
        var handlerIdentifier = IdentifierName(method.HandlerParameter.Name);

        var unsubscribeBuilder = new MethodDeclarationBuilder(WellKnownTypes.Void, method.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.HandlerParameter));

        if (method.BaseImplementationParameter is { } unsubscribeBaseParameter)
        {
            unsubscribeBuilder = unsubscribeBuilder.AddParameter(ParameterSyntax(unsubscribeBaseParameter));
        }

        var unsubscribeBlockBuilder = new BlockBuilder()
            .AddExpression(ThrowIfNull(method.HandlerParameter.Name))
            .AddExpression(
                FieldIdentifier(@event.Builder.Fields.HandlerCounts)
                    .Dot(IdentifierName("AddOrUpdate"))
                    .Call([
                        Argument(handlerIdentifier),
                        Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))),
                        Argument(CounterDecrementLambda())
                    ]))
            .AddExpression(
                FieldIdentifier(@event.Builder.Fields.UnsubscribeHistory)
                    .Dot(IdentifierName("Enqueue"))
                    .Call(Argument(handlerIdentifier)))
            .AddStatement(ForEachInterceptor(@event.Builder.Fields.UnsubscribeInterceptors, method.HandlerParameter.Name));

        if (method.BaseImplementationParameter is { } unsubscribeBaseImplementationParameter)
        {
            unsubscribeBlockBuilder.AddStatement(
                BuildBaseImplementationInvocation(IdentifierName(unsubscribeBaseImplementationParameter.Name)));
        }

        return unsubscribeBuilder
            .WithBody(unsubscribeBlockBuilder.Build())
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

    private static MethodDeclarationSyntax BuildOnSubscribeMethod(in ImposterEventMetadata @event) =>
        BuildInterceptorRegistrationMethod(
            @event,
            @event.Builder.Methods.OnSubscribe,
            @event.Builder.Fields.SubscribeInterceptors);

    private static MethodDeclarationSyntax BuildOnUnsubscribeMethod(in ImposterEventMetadata @event) =>
        BuildInterceptorRegistrationMethod(
            @event,
            @event.Builder.Methods.OnUnsubscribe,
            @event.Builder.Fields.UnsubscribeInterceptors);

    private static ForEachStatementSyntax ForEachInterceptor(in FieldMetadata interceptorsField, string handlerIdentifier) =>
        ForEachStatement(
            Var,
            Identifier("interceptor"),
            FieldIdentifier(interceptorsField),
            Block(
                ExpressionStatement(
                    IdentifierName("interceptor")
                        .Call(Argument(IdentifierName(handlerIdentifier))))));

    private static MethodDeclarationSyntax BuildInterceptorRegistrationMethod(
        in ImposterEventMetadata @event,
        in EventImposterBuilderMethodsMetadata.InterceptorMethodMetadata method,
        in FieldMetadata interceptorsField)
    {
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
                        FieldIdentifier(interceptorsField)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(interceptorIdentifier)))
                    .AddStatement(ReturnStatement(ThisExpression()))
                    .Build())
            .Build();
    }

    private static IfStatementSyntax BuildBaseImplementationInvocation(IdentifierNameSyntax baseImplementationIdentifier)
    {
        var baseProvidedCheck = BinaryExpression(
            SyntaxKind.NotEqualsExpression,
            baseImplementationIdentifier,
            LiteralExpression(SyntaxKind.NullLiteralExpression));

        return IfStatement(
            IdentifierName("_useBaseImplementation"),
            Block(
                IfStatement(
                    baseProvidedCheck,
                    Block(baseImplementationIdentifier.Call().ToStatementSyntax()),
                    ElseClause(
                        ThrowStatement(
                            ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.MissingImposterException)
                                .WithArgumentList(
                                    Argument(
                                            BinaryExpression(
                                                SyntaxKind.AddExpression,
                                                IdentifierName("_eventDisplayName"),
                                                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(" (event)"))))
                                        .AsSingleArgumentListSyntax()))))));
    }
}
