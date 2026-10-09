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

    internal readonly TypeSyntax NullableAwareHandlerTypeSyntax;

    internal readonly TypeSyntax HandlerArgTypeSyntax;

    internal readonly EventParameterMetadata[] Parameters;

    // An async method cannot take `in` parameters (CS1988), so an async event's raise methods take the delegate's
    // parameters by value. The handlers and callbacks they call keep the delegate's own modifiers.
    internal readonly ParameterSyntax[] RaiseParameterSyntaxes;

    internal readonly bool IsAsync;

    internal readonly bool ReturnsNonGenericValueTask;

    internal readonly bool SupportsBaseImplementation;

    internal ImposterEventCoreMetadata(EventModel @event, string uniqueName)
    {
        UniqueName = uniqueName;
        Name = @event.Name;
        DisplayName = @event.DisplayName;
        HandlerTypeSyntax = SyntaxFactoryHelper.TypeSyntax(@event.Type);
        NullableAwareHandlerTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(
            @event.Type
        );
        HandlerArgTypeSyntax = WellKnownTypes.Imposter.Abstractions.Arg(HandlerTypeSyntax);
        Parameters = @event
            .DelegateParameters.Select(model => new EventParameterMetadata(model))
            .ToArray();
        IsAsync = @event.IsAsync;
        ReturnsNonGenericValueTask = @event.ReturnsNonGenericValueTask;
        var includeRefKind = !IsAsync;
        RaiseParameterSyntaxes = @event
            .DelegateParameters.Select(model =>
                SyntaxFactoryHelper.ParameterSyntax(model, includeRefKind)
            )
            .ToArray();
        SupportsBaseImplementation = @event.IsClassMember && @event.HasConcreteAccessors;
    }

    // The builder's raise methods take the delegate's parameters, so the names they declare or refer to avoid these.
    internal NameSet CreateParameterNameSet() =>
        new(Parameters.Select(parameter => parameter.Name));
}
