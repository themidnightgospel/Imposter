using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.EventImpersonation.Builders.EventImposterBuilderCommon;
using static Imposter.CodeGenerator.Features.Shared.Builders.MissingImposterBuilder;
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

    internal static MethodDeclarationSyntax BuildSubscribeMethod(in ImposterEventMetadata @event) =>
        BuildSubscriptionMethod(
            @event,
            @event.Builder.Methods.Subscribe,
            @event.Builder.Fields.SubscribeHistory,
            @event.Builder.Fields.SubscribeInterceptors
        );

    internal static MethodDeclarationSyntax BuildUnsubscribeMethod(
        in ImposterEventMetadata @event
    ) =>
        BuildSubscriptionMethod(
            @event,
            @event.Builder.Methods.Unsubscribe,
            @event.Builder.Fields.UnsubscribeHistory,
            @event.Builder.Fields.UnsubscribeInterceptors
        );

    private static MethodDeclarationSyntax BuildSubscriptionMethod(
        in ImposterEventMetadata @event,
        in SubscriptionMethodMetadata method,
        in FieldMetadata history,
        in FieldMetadata interceptors
    )
    {
        var handlerIdentifier = IdentifierName(method.HandlerParameter.Name);

        var methodBuilder = new MethodDeclarationBuilder(WellKnownTypes.Void, method.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.HandlerParameter));

        if (method.BaseImplementationParameter is { } baseImplementationParameter)
        {
            methodBuilder = methodBuilder.AddParameter(
                ParameterSyntax(baseImplementationParameter)
            );
        }

        var blockBuilder = new BlockBuilder()
            .AddStatement(ThrowIfNull(method.HandlerParameter.Name))
            .AddStatements(
                UpdateActiveHandlers(@event, handlerIdentifier, method.DelegateOperation)
            )
            .AddExpression(
                FieldIdentifier(history)
                    .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                    .Call(Argument(handlerIdentifier))
            )
            .AddStatement(ForEachInterceptor(interceptors, method.HandlerParameter.Name));

        if (method.BaseImplementationParameter is { } baseImplementation)
        {
            blockBuilder.AddStatement(
                BuildBaseImplementationInvocation(@event, IdentifierName(baseImplementation.Name))
            );
        }

        return methodBuilder.WithBody(blockBuilder.Build()).Build();
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
                    .AddStatement(ThrowIfNull(method.CallbackParameter.Name))
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
                    .AddStatement(ThrowIfNull(method.InterceptorParameter.Name))
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
                        ThrowMissingImposter(
                            @event.Builder.Fields.EventDisplayName.Name,
                            " (event)"
                        )
                    )
                )
            )
        );
}
