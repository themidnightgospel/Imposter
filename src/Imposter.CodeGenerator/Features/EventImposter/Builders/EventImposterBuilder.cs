using System;
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
    internal static ClassDeclarationSyntax Build(in ImposterEventMetadata @event) =>
        new ClassDeclarationBuilder(@event.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.TypeSyntax))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.SetupInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.VerificationInterfaceTypeSyntax))
            .AddMembers(BuildFields(@event))
            .AddMember(BuildConstructor(@event))
            .AddMember(BuildSubscribeMethod(@event))
            .AddMember(BuildUnsubscribeMethod(@event))
            .AddMember(BuildCallbackMethod(@event))
            .AddMember(BuildRaiseMethod(@event))
            .AddMember(BuildSubscribedVerificationMethod(@event))
            .AddMember(BuildUnsubscribedVerificationMethod(@event))
            .AddMember(BuildOnSubscribeMethod(@event))
            .AddMember(BuildOnUnsubscribeMethod(@event))
            .AddMember(BuildRaisedVerificationMethod(@event))
            .AddMember(BuildHandlerInvokedVerificationMethod(@event))
            .AddMember(@event.Core.IsAsync ? null : BuildRaiseInternalMethod(@event))
            .AddMember(@event.Core.IsAsync ? BuildRaiseCoreAsyncMethod(@event) : null)
            .AddMember(BuildEnumerateHandlersMethod(@event))
            .AddMember(BuildCountMatchesMethod(@event.Builder.Methods))
            .AddMember(BuildEnsureCountMatchesMethod(@event.Builder.Methods))
            .Build();

    private static MemberDeclarationSyntax[] BuildFields(in ImposterEventMetadata @event)
    {
        var fields = @event.Builder.Fields;

        return
        [
            SinglePrivateReadonlyVariableField(fields.HandlerOrder, fields.HandlerOrder.Type.New()),
            SinglePrivateReadonlyVariableField(fields.HandlerCounts, fields.HandlerCounts.Type.New()),
            SinglePrivateReadonlyVariableField(fields.Callbacks, fields.Callbacks.Type.New()),
            SinglePrivateReadonlyVariableField(fields.History, fields.History.Type.New()),
            SinglePrivateReadonlyVariableField(fields.SubscribeHistory, fields.SubscribeHistory.Type.New()),
            SinglePrivateReadonlyVariableField(fields.UnsubscribeHistory, fields.UnsubscribeHistory.Type.New()),
            SinglePrivateReadonlyVariableField(fields.SubscribeInterceptors, fields.SubscribeInterceptors.Type.New()),
            SinglePrivateReadonlyVariableField(fields.UnsubscribeInterceptors, fields.UnsubscribeInterceptors.Type.New()),
            SinglePrivateReadonlyVariableField(fields.HandlerInvocations, fields.HandlerInvocations.Type.New())
        ];
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterEventMetadata @event) =>
        new ConstructorBuilder(@event.Builder.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .WithBody(Block())
            .Build();

    private static MethodDeclarationBuilder ExplicitInterfaceMethod(
        NameSyntax interfaceType,
        TypeSyntax returnType,
        string name) =>
        new MethodDeclarationBuilder(returnType, name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(interfaceType));

    private static InvocationExpressionSyntax ThrowIfNull(string parameterName) =>
        IdentifierName(nameof(ArgumentNullException))
            .Dot(IdentifierName("ThrowIfNull"))
            .Call(Argument(IdentifierName(parameterName)));

    private static IdentifierNameSyntax FieldIdentifier(in FieldMetadata field) =>
        IdentifierName(field.Name);
}
