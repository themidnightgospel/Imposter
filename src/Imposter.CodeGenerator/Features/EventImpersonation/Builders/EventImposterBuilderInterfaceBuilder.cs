using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.InterfaceMethodBuilder;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterBuilderInterfaceBuilder
{
    internal static MemberDeclarationSyntax[] Build(in ImposterEventMetadata @event) =>
        [
            BuildInitialInterface(@event),
            BuildSetupInterface(@event),
            BuildVerificationInterface(@event),
        ];

    private static InterfaceDeclarationSyntax BuildInitialInterface(
        in ImposterEventMetadata @event
    ) =>
        new InterfaceDeclarationBuilder(@event.BuilderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.SetupInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.VerificationInterfaceTypeSyntax))
            .AddMember(
                @event.BuilderInterface.UseBaseImplementationMethod is { } useBaseImplementation
                    ? InterfaceMethod(useBaseImplementation.ReturnType, useBaseImplementation.Name)
                    : null
            )
            .Build();

    private static InterfaceDeclarationSyntax BuildSetupInterface(in ImposterEventMetadata @event)
    {
        var setupInterface = @event.BuilderInterface.SetupInterfaceTypeSyntax;
        var methods = @event.Builder.Methods;

        return new InterfaceDeclarationBuilder(@event.BuilderInterface.SetupInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                InterfaceMethod(
                    setupInterface,
                    methods.Callback.Name,
                    methods.Callback.CallbackParameter
                )
            )
            .AddMember(BuildRaiseMethod(@event))
            .AddMember(
                InterfaceMethod(
                    setupInterface,
                    methods.OnSubscribe.Name,
                    methods.OnSubscribe.InterceptorParameter
                )
            )
            .AddMember(
                InterfaceMethod(
                    setupInterface,
                    methods.OnUnsubscribe.Name,
                    methods.OnUnsubscribe.InterceptorParameter
                )
            )
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildVerificationInterface(
        in ImposterEventMetadata @event
    )
    {
        var verificationInterface = @event.BuilderInterface.VerificationInterfaceTypeSyntax;
        var methods = @event.Builder.Methods;

        return new InterfaceDeclarationBuilder(@event.BuilderInterface.VerificationInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                InterfaceMethod(
                    verificationInterface,
                    methods.Subscribed.Name,
                    methods.Subscribed.CriteriaParameter,
                    methods.CountParameter
                )
            )
            .AddMember(
                InterfaceMethod(
                    verificationInterface,
                    methods.Unsubscribed.Name,
                    methods.Unsubscribed.CriteriaParameter,
                    methods.CountParameter
                )
            )
            .AddMember(
                InterfaceMethod(
                    verificationInterface,
                    methods.RaisedVerification.Name,
                    [.. methods.RaisedCriteriaParameters, methods.CountParameter]
                )
            )
            .AddMember(
                InterfaceMethod(
                    verificationInterface,
                    methods.HandlerInvoked.Name,
                    methods.HandlerInvoked.HandlerCriteriaParameter,
                    methods.CountParameter
                )
            )
            .Build();
    }

    // Raise takes the event delegate's parameters, which the core metadata holds as syntax.
    private static MethodDeclarationSyntax BuildRaiseMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.RaiseMethod.ReturnType,
            @event.BuilderInterface.RaiseMethod.Name
        )
            .AddParameters(@event.Core.RaiseParameterSyntaxes)
            .WithSemicolon()
            .Build();
}
