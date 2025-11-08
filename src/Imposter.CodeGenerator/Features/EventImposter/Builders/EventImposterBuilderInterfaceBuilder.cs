using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.EventImposter.Builders;

internal static class EventImposterBuilderInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax Build(in ImposterEventMetadata @event) =>
        new InterfaceDeclarationBuilder(@event.BuilderInterface.Name)
            .AddModifier(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildCallbackMethod(@event))
            .AddMember(BuildRaiseMethod(@event))
            .AddMember(BuildSubscribedMethod(@event))
            .AddMember(BuildUnsubscribedMethod(@event))
            .AddMember(BuildOnSubscribeMethod(@event))
            .AddMember(BuildOnUnsubscribeMethod(@event))
            .AddMember(BuildRaisedMethod(@event))
            .AddMember(BuildHandlerInvokedMethod(@event))
            .Build();

    private static MethodDeclarationSyntax BuildCallbackMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(@event.BuilderInterface.TypeSyntax, "Callback")
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(@event.Core.HandlerTypeSyntax, "callback"))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildRaiseMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(@event.BuilderInterface.RaiseMethodReturnType, @event.BuilderInterface.RaiseMethodName)
            .AddParameters(@event.Core.Parameters.Select(parameter => parameter.ParameterSyntax))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildSubscribedMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(@event.BuilderInterface.TypeSyntax, "Subscribed")
            .AddParameter(HandlerCriteriaParameter("criteria", @event))
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildUnsubscribedMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(@event.BuilderInterface.TypeSyntax, "Unsubscribed")
            .AddParameter(HandlerCriteriaParameter("criteria", @event))
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildOnSubscribeMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(@event.BuilderInterface.TypeSyntax, "OnSubscribe")
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(WellKnownTypes.System.ActionOfT(@event.Core.HandlerTypeSyntax), "interceptor"))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildOnUnsubscribeMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(@event.BuilderInterface.TypeSyntax, "OnUnsubscribe")
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(WellKnownTypes.System.ActionOfT(@event.Core.HandlerTypeSyntax), "interceptor"))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildRaisedMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(@event.BuilderInterface.TypeSyntax, "Raised")
            .AddParameters(ParameterCriteria(@event))
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildHandlerInvokedMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(@event.BuilderInterface.TypeSyntax, "HandlerInvoked")
            .AddParameter(HandlerCriteriaParameter("handlerCriteria", @event))
            .AddParameter(CountParameter())
            .WithSemicolon()
            .Build();

    private static IEnumerable<ParameterSyntax> ParameterCriteria(in ImposterEventMetadata @event) =>
        @event.Core.Parameters.Select(parameter =>
            SyntaxFactoryHelper.ParameterSyntax(parameter.ArgTypeSyntax, $"{parameter.Name}Criteria"));

    private static ParameterSyntax HandlerCriteriaParameter(string name, in ImposterEventMetadata @event) =>
        SyntaxFactoryHelper.ParameterSyntax(@event.Core.HandlerArgTypeSyntax, name);

    private static ParameterSyntax CountParameter() =>
        SyntaxFactoryHelper.ParameterSyntax(WellKnownTypes.Imposter.Abstractions.Count, "count");
}
