using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImposter.Builders;

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
        new MethodDeclarationBuilder(@event.BuilderInterface.SetupInterfaceTypeSyntax, "Callback")
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(@event.Core.HandlerTypeSyntax, "callback")
            )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildRaiseMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.RaiseMethodReturnType,
            @event.BuilderInterface.RaiseMethodName
        )
            .AddParameters(@event.Core.Parameters.Select(parameter => parameter.ParameterSyntax))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildOnSubscribeMethod(
        in ImposterEventMetadata @event
    ) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.SetupInterfaceTypeSyntax,
            "OnSubscribe"
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    WellKnownTypes.System.ActionOfT(@event.Core.HandlerTypeSyntax),
                    "interceptor"
                )
            )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildOnUnsubscribeMethod(
        in ImposterEventMetadata @event
    ) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.SetupInterfaceTypeSyntax,
            "OnUnsubscribe"
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    WellKnownTypes.System.ActionOfT(@event.Core.HandlerTypeSyntax),
                    "interceptor"
                )
            )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildSubscribedMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            "Subscribed"
        )
            .AddParameter(HandlerCriteriaParameter("criteria", @event))
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildUnsubscribedMethod(
        in ImposterEventMetadata @event
    ) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            "Unsubscribed"
        )
            .AddParameter(HandlerCriteriaParameter("criteria", @event))
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildRaisedMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            "Raised"
        )
            .AddParameters(ParameterCriteria(@event))
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildHandlerInvokedMethod(
        in ImposterEventMetadata @event
    ) =>
        new MethodDeclarationBuilder(
            @event.BuilderInterface.VerificationInterfaceTypeSyntax,
            "HandlerInvoked"
        )
            .AddParameter(HandlerCriteriaParameter("handlerCriteria", @event))
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

    private static IEnumerable<ParameterSyntax> ParameterCriteria(
        in ImposterEventMetadata @event
    ) =>
        @event.Core.Parameters.Select(parameter =>
            SyntaxFactoryHelper.ParameterSyntax(
                parameter.ArgTypeSyntax,
                $"{parameter.Name}Criteria"
            )
        );

    private static ParameterSyntax HandlerCriteriaParameter(
        string name,
        in ImposterEventMetadata @event
    ) => SyntaxFactoryHelper.ParameterSyntax(@event.Core.HandlerArgTypeSyntax, name);

    private static ParameterSyntax CountParameter() =>
        SyntaxFactoryHelper.ParameterSyntax(WellKnownTypes.Imposter.Abstractions.Count, "count");
}
