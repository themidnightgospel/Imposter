using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.EventImpersonation.Builders.EventImposterBuilderCommon;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterSubscriptionsBuilder
{
    internal static MemberDeclarationSyntax[] BuildFields(in ImposterEventMetadata @event)
    {
        var fields = @event.Builder.Fields;

        return
        [
            SingleVariableField(fields.ActiveHandlers),
            SingleVariableField(fields.SubscribeHistory),
            SingleVariableField(fields.UnsubscribeHistory),
            SingleVariableField(fields.SubscribeInterceptors),
            SingleVariableField(fields.UnsubscribeInterceptors),
        ];
    }

    internal static MethodDeclarationSyntax BuildSubscribeMethod(in ImposterEventMetadata @event)
    {
        var fields = @event.Builder.Fields;
        var method = @event.Builder.Methods.Subscribe;
        var handlerIdentifier = IdentifierName(method.HandlerParameter.Name);

        var methodBuilder = new MethodDeclarationBuilder(WellKnownTypes.Void, method.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.HandlerParameter));

        if (method.BaseImplementationParameter is { } subscribeBaseParameter)
        {
            methodBuilder = methodBuilder.AddParameter(ParameterSyntax(subscribeBaseParameter));
        }

        var blockBuilder = new BlockBuilder()
            .AddExpression(ThrowIfNull(method.HandlerParameter.Name))
            .AddStatements(UpdateActiveHandlers(@event, handlerIdentifier, "Combine"))
            .AddExpression(
                FieldIdentifier(fields.SubscribeHistory)
                    .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                    .Call(Argument(handlerIdentifier))
            )
            .AddStatement(
                ForEachInterceptor(fields.SubscribeInterceptors, method.HandlerParameter.Name)
            );

        if (method.BaseImplementationParameter is { } subscribeBaseImplementationParameter)
        {
            blockBuilder.AddStatement(
                BuildBaseImplementationInvocation(
                    @event,
                    IdentifierName(subscribeBaseImplementationParameter.Name)
                )
            );
        }

        return methodBuilder.WithBody(blockBuilder.Build()).Build();
    }

    internal static MethodDeclarationSyntax BuildUnsubscribeMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Unsubscribe;
        var handlerIdentifier = IdentifierName(method.HandlerParameter.Name);

        var unsubscribeBuilder = new MethodDeclarationBuilder(WellKnownTypes.Void, method.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.HandlerParameter));

        if (method.BaseImplementationParameter is { } unsubscribeBaseParameter)
        {
            unsubscribeBuilder = unsubscribeBuilder.AddParameter(
                ParameterSyntax(unsubscribeBaseParameter)
            );
        }

        var unsubscribeBlockBuilder = new BlockBuilder()
            .AddExpression(ThrowIfNull(method.HandlerParameter.Name))
            .AddStatements(UpdateActiveHandlers(@event, handlerIdentifier, "Remove"))
            .AddExpression(
                FieldIdentifier(@event.Builder.Fields.UnsubscribeHistory)
                    .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                    .Call(Argument(handlerIdentifier))
            )
            .AddStatement(
                ForEachInterceptor(
                    @event.Builder.Fields.UnsubscribeInterceptors,
                    method.HandlerParameter.Name
                )
            );

        if (method.BaseImplementationParameter is { } unsubscribeBaseImplementationParameter)
        {
            unsubscribeBlockBuilder.AddStatement(
                BuildBaseImplementationInvocation(
                    @event,
                    IdentifierName(unsubscribeBaseImplementationParameter.Name)
                )
            );
        }

        return unsubscribeBuilder.WithBody(unsubscribeBlockBuilder.Build()).Build();
    }

    internal static MethodDeclarationSyntax BuildCallbackMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Callback;
        var callbackIdentifier = IdentifierName(method.CallbackParameter.Name);

        return new MethodDeclarationBuilder(
            @event.BuilderInterface.SetupInterfaceTypeSyntax,
            method.Name
        )
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.SetupInterfaceTypeSyntax)
            .AddParameter(ParameterSyntax(method.CallbackParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.CallbackParameter.Name))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.Callbacks)
                            .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                            .Call(Argument(callbackIdentifier))
                    )
                    .AddStatement(ReturnThis)
                    .Build()
            )
            .Build();
    }

    internal static MethodDeclarationSyntax BuildOnSubscribeMethod(
        in ImposterEventMetadata @event
    ) =>
        BuildInterceptorRegistrationMethod(
            @event,
            @event.Builder.Methods.OnSubscribe,
            @event.Builder.Fields.SubscribeInterceptors
        );

    internal static MethodDeclarationSyntax BuildOnUnsubscribeMethod(
        in ImposterEventMetadata @event
    ) =>
        BuildInterceptorRegistrationMethod(
            @event,
            @event.Builder.Methods.OnUnsubscribe,
            @event.Builder.Fields.UnsubscribeInterceptors
        );

    // Updates the active handlers the way a field-like event's accessors do: Delegate.Combine appends a subscription,
    // Delegate.Remove drops the handler's last one, and the compare-exchange loop retries when another caller changed
    // the field in between. References are compared because delegate == compares delegates by value.
    private static StatementSyntax[] UpdateActiveHandlers(
        in ImposterEventMetadata @event,
        IdentifierNameSyntax handler,
        string delegateOperation
    )
    {
        var activeHandlers = @event.Builder.Fields.ActiveHandlers;
        var activeHandlersIdentifier = FieldIdentifier(activeHandlers);
        var handlers = IdentifierName("handlers");
        var updated = IdentifierName("updated");
        var observed = IdentifierName("observed");

        return
        [
            LocalVariableDeclarationSyntax(Var, handlers.Identifier.Text, activeHandlersIdentifier),
            WhileStatement(
                True,
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        updated.Identifier.Text,
                        CastExpression(
                            activeHandlers.Type,
                            WellKnownTypes
                                .System.Delegate.Dot(IdentifierName(delegateOperation))
                                .Call([Argument(handlers), Argument(handler)])
                        )
                    ),
                    LocalVariableDeclarationSyntax(
                        Var,
                        observed.Identifier.Text,
                        WellKnownTypes
                            .System.Threading.Interlocked.Dot(IdentifierName("CompareExchange"))
                            .Call([
                                Argument(
                                    null,
                                    Token(SyntaxKind.RefKeyword),
                                    activeHandlersIdentifier
                                ),
                                Argument(updated),
                                Argument(handlers),
                            ])
                    ),
                    IfStatement(
                        PredefinedType(Token(SyntaxKind.ObjectKeyword))
                            .Dot(IdentifierName("ReferenceEquals"))
                            .Call([Argument(observed), Argument(handlers)]),
                        Block(BreakStatement())
                    ),
                    handlers.Assign(observed).ToStatementSyntax()
                )
            ),
        ];
    }

    private static ForEachStatementSyntax ForEachInterceptor(
        in FieldMetadata interceptorsField,
        string handlerIdentifier
    ) =>
        ForEachStatement(
            Var,
            Identifier("interceptor"),
            FieldIdentifier(interceptorsField),
            Block(
                IdentifierName("interceptor")
                    .Call(Argument(IdentifierName(handlerIdentifier)))
                    .ToStatementSyntax()
            )
        );

    private static MethodDeclarationSyntax BuildInterceptorRegistrationMethod(
        in ImposterEventMetadata @event,
        in InterceptorMethodMetadata method,
        in FieldMetadata interceptorsField
    )
    {
        var interceptorIdentifier = IdentifierName(method.InterceptorParameter.Name);

        return new MethodDeclarationBuilder(
            @event.BuilderInterface.SetupInterfaceTypeSyntax,
            method.Name
        )
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.SetupInterfaceTypeSyntax)
            .AddParameter(ParameterSyntax(method.InterceptorParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.InterceptorParameter.Name))
                    .AddExpression(
                        FieldIdentifier(interceptorsField)
                            .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                            .Call(Argument(interceptorIdentifier))
                    )
                    .AddStatement(ReturnThis)
                    .Build()
            )
            .Build();
    }

    private static IfStatementSyntax BuildBaseImplementationInvocation(
        in ImposterEventMetadata @event,
        IdentifierNameSyntax baseImplementationIdentifier
    ) =>
        IfStatement(
            FieldIdentifier(@event.Builder.Fields.UseBaseImplementation),
            Block(
                IfStatement(
                    baseImplementationIdentifier.IsNotNull(),
                    Block(baseImplementationIdentifier.Call().ToStatementSyntax()),
                    ElseClause(
                        ThrowStatement(
                            ObjectCreationExpression(
                                    WellKnownTypes.Imposter.Abstractions.MissingImposterException
                                )
                                .WithArgumentList(
                                    Argument(
                                            FieldIdentifier(@event.Builder.Fields.EventDisplayName)
                                                .Add(" (event)".StringLiteral())
                                        )
                                        .AsSingleArgumentListSyntax()
                                )
                        )
                    )
                )
            )
        );
}
