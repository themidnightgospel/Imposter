using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            .AddMember(BuildUseBaseImplementationInterfaceMethod(@event))
            .Build();

    private static InterfaceDeclarationSyntax BuildSetupInterface(
        in ImposterEventMetadata @event
    ) =>
        new InterfaceDeclarationBuilder(@event.BuilderInterface.SetupInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildCallbackMethod(@event))
            .AddMember(BuildRaiseMethod(@event))
            .AddMember(BuildOnSubscribeMethod(@event))
            .AddMember(BuildOnUnsubscribeMethod(@event))
            .Build();

    private static InterfaceDeclarationSyntax BuildVerificationInterface(
        in ImposterEventMetadata @event
    ) =>
        new InterfaceDeclarationBuilder(@event.BuilderInterface.VerificationInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildSubscribedMethod(@event))
            .AddMember(BuildUnsubscribedMethod(@event))
            .AddMember(BuildRaisedMethod(@event))
            .AddMember(BuildHandlerInvokedMethod(@event))
            .Build();

    private static MethodDeclarationSyntax BuildCallbackMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.SetupInterfaceTypeSyntax,
            @event.Builder.Methods.Callback.Name
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    @event.Builder.Methods.Callback.CallbackParameter
                )
            )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildRaiseMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.RaiseMethod.ReturnType,
            @event.BuilderInterface.RaiseMethod.Name
        )
            .AddParameters(@event.Core.Parameters.Select(parameter => parameter.ParameterSyntax))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildOnSubscribeMethod(
        in ImposterEventMetadata @event
    ) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.SetupInterfaceTypeSyntax,
            @event.Builder.Methods.OnSubscribe.Name
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    @event.Builder.Methods.OnSubscribe.InterceptorParameter
                )
            )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildOnUnsubscribeMethod(
        in ImposterEventMetadata @event
    ) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.SetupInterfaceTypeSyntax,
            @event.Builder.Methods.OnUnsubscribe.Name
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    @event.Builder.Methods.OnUnsubscribe.InterceptorParameter
                )
            )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildSubscribedMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            @event.Builder.Methods.Subscribed.Name
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    @event.Builder.Methods.Subscribed.CriteriaParameter
                )
            )
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildUnsubscribedMethod(
        in ImposterEventMetadata @event
    ) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            @event.Builder.Methods.Unsubscribed.Name
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    @event.Builder.Methods.Unsubscribed.CriteriaParameter
                )
            )
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildRaisedMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            @event.Builder.Methods.RaisedVerification.Name
        )
            .AddParameters(
                @event.Builder.Methods.RaisedCriteriaParameters.Select(it =>
                    SyntaxFactoryHelper.ParameterSyntax(it)
                )
            )
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildHandlerInvokedMethod(
        in ImposterEventMetadata @event
    ) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            @event.Builder.Methods.HandlerInvoked.Name
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    @event.Builder.Methods.HandlerInvoked.HandlerCriteriaParameter
                )
            )
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax? BuildUseBaseImplementationInterfaceMethod(
        in ImposterEventMetadata @event
    )
    {
        if (@event.BuilderInterface.UseBaseImplementationMethod is not { } methodMetadata)
        {
            return null;
        }

        return new MethodDeclarationBuilder(methodMetadata.ReturnType, methodMetadata.Name)
            .WithSemicolon()
            .Build();
    }

    private static ParameterSyntax CountParameter() =>
        SyntaxFactoryHelper.ParameterSyntax(WellKnownTypes.Imposter.Abstractions.Count, "count");
}
