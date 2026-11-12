using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventInvocationStepBuilder
{
    internal static MethodDeclarationSyntax Selector(
        in ImposterEventMetadata @event,
        bool subscription,
        bool implementation
    )
    {
        var method = subscription
            ? @event.Builder.Methods.Subscribed
            : @event.Builder.Methods.Unsubscribed;
        var builder = new MethodDeclarationBuilder(
            WellKnownTypes.Imposter.Abstractions.IInvocationStep,
            method.Name
        ).AddParameter(ParameterSyntax(method.CriteriaParameter));
        if (!implementation)
            return builder.WithSemicolon().Build();

        return builder
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.VerificationInterfaceTypeSyntax)
            .WithBody(
                Block(
                    ThrowIfNull(method.CriteriaParameter.Name),
                    ReturnStatement(
                        @event.Builder.InvocationStep.Type.New(
                            ArgumentListSyntax([
                                Argument(ThisExpression()),
                                Argument(IdentifierName(method.CriteriaParameter.Name)),
                                Argument(subscription ? True : False),
                            ])
                        )
                    )
                )
            )
            .Build();
    }

    internal static ClassDeclarationSyntax Build(in ImposterEventMetadata @event)
    {
        var step = @event.Builder.InvocationStep;
        var parent = IdentifierName(step.Parent.Name);
        var history = ConditionalExpression(
            IdentifierName(step.Subscription.Name),
            parent.Dot(IdentifierName(@event.Builder.Fields.SubscribeHistory.Name)),
            parent.Dot(IdentifierName(@event.Builder.Fields.UnsubscribeHistory.Name))
        );
        var invocationType = WellKnownTypes.Imposter.Abstractions.EventSubscriptionInvocation(
            @event.Core.HandlerTypeSyntax
        );
        var criteria = IdentifierName(step.Criteria.Name);
        return new ClassDeclarationBuilder(step.Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddBaseType(SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IInvocationStep))
            .AddMember(SinglePrivateReadonlyVariableField(step.Parent))
            .AddMember(SinglePrivateReadonlyVariableField(step.Criteria))
            .AddMember(SinglePrivateReadonlyVariableField(step.Subscription))
            .AddMember(
                new ConstructorWithFieldInitializationBuilder(step.Name)
                    .WithModifiers(Token(SyntaxKind.InternalKeyword))
                    .AddParameter(step.Parent)
                    .AddParameter(step.Criteria)
                    .AddParameter(step.Subscription)
                    .Build()
            )
            .AddMembers(
                InvocationStepMembersBuilder.Build(
                    parent.Dot(IdentifierName(InvocationStepNames.OwnerPropertyName)),
                    @event
                        .Core.Name.StringLiteral()
                        .Add(
                            ParenthesizedExpression(
                                ConditionalExpression(
                                    IdentifierName(step.Subscription.Name),
                                    " subscribed(handler: ".StringLiteral(),
                                    " unsubscribed(handler: ".StringLiteral()
                                )
                            )
                        )
                        .Add(
                            WellKnownTypes
                                .Imposter.Abstractions.MatcherDescription.Dot(
                                    IdentifierName("Format")
                                )
                                .Call(Argument(IdentifierName(step.Criteria.Name)))
                        )
                        .Add(")".StringLiteral()),
                    @event.Core.CreateParameterNameSet(),
                    (sequence, match) =>
                        invocationType
                            .Dot(IdentifierName(InvocationStepNames.TryFindAfterMethodName))
                            .Call(
                                ArgumentListSyntax([
                                    Argument(history),
                                    Argument(sequence),
                                    Argument(criteria),
                                    Argument(match)
                                        .WithRefOrOutKeyword(Token(SyntaxKind.OutKeyword)),
                                ])
                            )
                )
            )
            .Build();
    }
}
