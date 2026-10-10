using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly ref struct ImposterEventCoreMetadata
{
    internal readonly string Name;

    internal readonly string UniqueName;

    internal readonly string DisplayName;

    internal readonly TypeSyntax HandlerTypeSyntax;

    // The type the event is declared with, including its own nullable annotation, which a handler's type drops.
    internal readonly TypeSyntax DeclaredTypeSyntax;

    internal readonly TypeSyntax HandlerArgTypeSyntax;

    internal readonly EventParameterMetadata[] Parameters;

    // The parameters the raise and handler-invocation histories keep and Raised matches: all but the ref structs the
    // raise only passes through.
    internal readonly EventParameterMetadata[] MatchedParameters;

    // The handler's element in the handler-invocation history tuple, next to the parameters' elements.
    internal readonly string HandlerTupleElementName;

    // An async method cannot take `in` parameters (CS1988) or a span (CS4012), so an async event's raise methods take
    // the delegate's parameters by value, and a span as the array it covers. The handlers and callbacks they call keep
    // the delegate's own modifiers, and get a span over the array.
    internal readonly ParameterSyntax[] RaiseParameterSyntaxes;

    internal readonly bool IsAsync;

    internal readonly bool ReturnsNonGenericValueTask;

    internal readonly bool SupportsBaseImplementation;

    internal ImposterEventCoreMetadata(EventModel @event, string uniqueName)
    {
        UniqueName = uniqueName;
        Name = @event.Name;
        DisplayName = @event.DisplayName;
        HandlerTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(@event.HandlerType);
        DeclaredTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(@event.Type);
        HandlerArgTypeSyntax = WellKnownTypes.Imposter.Abstractions.Arg(HandlerTypeSyntax);
        var tupleElementNames = new NameSet(
            @event.DelegateParameters.Select(model => SyntaxFactoryHelper.EscapeKeyword(model.Name))
        );
        Parameters = @event
            .DelegateParameters.Select(model => new EventParameterMetadata(
                model,
                tupleElementNames
            ))
            .ToArray();
        MatchedParameters = Parameters.Where(parameter => !parameter.IsPassedThrough).ToArray();
        HandlerTupleElementName = tupleElementNames.Use("Handler");
        IsAsync = @event.IsAsync;
        ReturnsNonGenericValueTask = @event.ReturnsNonGenericValueTask;
        RaiseParameterSyntaxes = IsAsync
            ? @event.DelegateParameters.Select(AsyncRaiseParameterSyntax).ToArray()
            : @event
                .DelegateParameters.Select(model =>
                    SyntaxFactoryHelper.ParameterSyntaxIncludingNullable(model)
                )
                .ToArray();
        SupportsBaseImplementation = @event.IsClassMember && @event.HasConcreteAccessors;
    }

    private static ParameterSyntax AsyncRaiseParameterSyntax(ParameterModel model) =>
        model.Span is null
            ? SyntaxFactoryHelper.ParameterSyntaxIncludingNullable(model, includeRefKind: false)
            : SyntaxFactoryHelper.ParameterSyntax(
                SyntaxFactoryHelper.StoredTypeSyntaxIncludingNullable(model),
                SyntaxFactoryHelper.EscapeKeyword(model.Name)
            );

    // The builder's raise methods take the delegate's parameters, so the names they declare or refer to avoid these.
    internal NameSet CreateParameterNameSet() =>
        new(Parameters.Select(parameter => parameter.Name));
}
