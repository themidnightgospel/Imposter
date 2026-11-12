using System.Collections.Generic;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterEventMetadata @event) =>
        new ClassDeclarationBuilder(@event.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.TypeSyntax))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.SetupInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.VerificationInterfaceTypeSyntax))
            .AddMembers(BuildFields(@event))
            .AddMember(EventHistoryBuilder.Build(@event, true, true))
            .AddMember(EventHistoryBuilder.Build(@event, false, true))
            .AddMember(
                new MethodDeclarationBuilder(
                    WellKnownTypes.System.Collections.Generic.IReadOnlyList(
                        WellKnownTypes.Imposter.Abstractions.IImposterInvocation
                    ),
                    "Snapshot"
                )
                    .AddModifier(Token(SyntaxKind.InternalKeyword))
                    .AddParameter(
                        ParameterSyntax(
                                WellKnownTypes.Bool,
                                @event.Core.CreateParameterNameSet().Use("orderBySequence")
                            )
                            .WithDefault(EqualsValueClause(True))
                    )
                    .WithBody(
                        Block(
                            ReturnStatement(
                                WellKnownTypes
                                    .Imposter.Abstractions.InvocationSnapshots.Dot(
                                        IdentifierName("Combine")
                                    )
                                    .Call([
                                        Argument(EventHistoryBuilder.Snapshot(@event, true, false)),
                                        Argument(
                                            EventHistoryBuilder.Snapshot(@event, false, false)
                                        ),
                                        Argument(
                                            IdentifierName(
                                                @event
                                                    .Core.CreateParameterNameSet()
                                                    .Use("orderBySequence")
                                            )
                                        ),
                                    ])
                            )
                        )
                    )
                    .Build()
            )
            .AddMember(EventImposterSubscriptionsBuilder.BuildSubscribeMethod(@event))
            .AddMember(EventImposterSubscriptionsBuilder.BuildUnsubscribeMethod(@event))
            .AddMember(EventImposterSubscriptionsBuilder.BuildCallbackMethod(@event))
            .AddMember(EventImposterRaiseBuilder.BuildRaiseMethod(@event))
            .AddMember(EventImposterVerificationBuilder.BuildSubscribedVerificationMethod(@event))
            .AddMember(EventImposterVerificationBuilder.BuildUnsubscribedVerificationMethod(@event))
            .AddMember(EventImposterSubscriptionsBuilder.BuildOnSubscribeMethod(@event))
            .AddMember(EventImposterSubscriptionsBuilder.BuildOnUnsubscribeMethod(@event))
            .AddMember(EventImposterVerificationBuilder.BuildRaisedVerificationMethod(@event))
            .AddMember(
                EventImposterVerificationBuilder.BuildHandlerInvokedVerificationMethod(@event)
            )
            .AddMember(
                @event.Core.IsAsync
                    ? null
                    : EventImposterRaiseBuilder.BuildRaiseInternalMethod(@event)
            )
            .AddMember(
                @event.Core.IsAsync
                    ? EventImposterRaiseBuilder.BuildRaiseCoreAsyncMethod(@event)
                    : null
            )
            .AddMember(FormatValueMethodBuilder.Build())
            .AddMember(BuildUseBaseImplementationMethod(@event))
            .Build();

    private static MemberDeclarationSyntax[] BuildFields(in ImposterEventMetadata @event)
    {
        var members = new List<MemberDeclarationSyntax>();

        members.AddRange(EventImposterSubscriptionsBuilder.BuildFields(@event));
        members.AddRange(EventImposterRaiseBuilder.BuildFields(@event));
        if (@event.Core.SupportsBaseImplementation)
        {
            members.Add(SingleVariableField(@event.Builder.Fields.UseBaseImplementation));
            members.Add(SingleVariableField(@event.Builder.Fields.EventDisplayName));
        }

        return members.ToArray();
    }

    private static MethodDeclarationSyntax? BuildUseBaseImplementationMethod(
        in ImposterEventMetadata @event
    )
    {
        if (@event.BuilderInterface.UseBaseImplementationMethod is not { } methodMetadata)
        {
            return null;
        }

        return new MethodDeclarationBuilder(methodMetadata.ReturnType, methodMetadata.Name)
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.TypeSyntax)
            .WithBody(
                Block(
                    IdentifierName(@event.Builder.Fields.UseBaseImplementation.Name)
                        .Assign(True)
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
    }
}
