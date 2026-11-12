using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventHistoryBuilder
{
    internal static PropertyDeclarationSyntax Build(
        in ImposterEventMetadata @event,
        bool subscription,
        bool implementation
    )
    {
        var recordType = WellKnownTypes.Imposter.Abstractions.EventSubscriptionInvocation(
            @event.Core.HandlerTypeSyntax
        );
        var resultType = WellKnownTypes.System.Collections.Generic.IReadOnlyList(recordType);
        var builder = new PropertyDeclarationBuilder(
            resultType,
            subscription ? "Subscriptions" : "Unsubscriptions"
        );
        if (!implementation)
            return builder.WithGetter().Build();
        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(@event.BuilderInterface.VerificationInterfaceTypeSyntax)
            )
            .WithGetterBody(Block(ReturnStatement(Snapshot(@event, subscription))))
            .Build();
    }

    internal static InvocationExpressionSyntax Snapshot(
        in ImposterEventMetadata @event,
        bool subscription,
        bool orderBySequence = true
    ) =>
        WellKnownTypes
            .Imposter.Abstractions.EventSubscriptionInvocation(@event.Core.HandlerTypeSyntax)
            .Dot(IdentifierName("Snapshot"))
            .Call([
                Argument(
                    IdentifierName(
                        subscription
                            ? @event.Builder.Fields.SubscribeHistory.Name
                            : @event.Builder.Fields.UnsubscribeHistory.Name
                    )
                ),
                Argument(@event.Core.Name.StringLiteral()),
                Argument(subscription ? True : False),
                Argument(orderBySequence ? True : False),
            ]);
}
