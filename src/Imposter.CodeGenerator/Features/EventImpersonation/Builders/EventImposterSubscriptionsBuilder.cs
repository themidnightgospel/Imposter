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
            LazyCollectionSyntaxHelper.Field(fields.ActiveHandlers),
            LazyCollectionSyntaxHelper.Field(fields.SubscribeHistory),
            LazyCollectionSyntaxHelper.Field(fields.UnsubscribeHistory),
            LazyCollectionSyntaxHelper.Field(fields.SubscribeInterceptors),
            LazyCollectionSyntaxHelper.Field(fields.UnsubscribeInterceptors),
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
                LazyCollectionSyntaxHelper
                    .EnsureInitialized(history.Name)
                    .Dot(ConcurrentStackSyntaxHelper.Push)
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
                        LazyCollectionSyntaxHelper
                            .EnsureInitialized(@event.Builder.Fields.Callbacks.Name)
                            .Dot(IdentifierName("Add"))
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

    private static StatementSyntax[] UpdateActiveHandlers(
        in ImposterEventMetadata @event,
        IdentifierNameSyntax handler,
        string delegateOperation
    ) =>
        [
            LazyCollectionSyntaxHelper
                .EnsureInitialized(@event.Builder.Fields.ActiveHandlers.Name)
                .Dot(IdentifierName(delegateOperation))
                .Call(Argument(handler))
                .ToStatementSyntax(),
        ];

    private static StatementSyntax ForEachInterceptor(
        in FieldMetadata interceptorsField,
        string handlerIdentifier
    ) =>
        LazyCollectionSyntaxHelper.UnlessNull(
            ForEachStatement(
                Var,
                Identifier("interceptor"),
                FieldIdentifier(interceptorsField),
                Block(
                    IdentifierName("interceptor")
                        .Call(Argument(IdentifierName(handlerIdentifier)))
                        .ToStatementSyntax()
                )
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
                        LazyCollectionSyntaxHelper
                            .EnsureInitialized(interceptorsField.Name)
                            .Dot(IdentifierName("Add"))
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
        CallBaseImplementationIfUsed(
            FieldIdentifier(@event.Builder.Fields.UseBaseImplementation),
            baseImplementationIdentifier,
            Block(baseImplementationIdentifier.Call().ToStatementSyntax()),
            ThrowMissingImposter(@event.Builder.Fields.EventDisplayName.Name, " (event)")
        );
}
