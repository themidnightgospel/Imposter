using System;
using System.Linq;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
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

    internal readonly ITypeSymbol? DelegateReturnTypeSymbol;

    internal readonly bool SupportsBaseImplementation;

    internal ImposterEventCoreMetadata(IEventSymbol eventSymbol, string uniqueName)
    {
        UniqueName = uniqueName;
        Name = eventSymbol.Name;
        DisplayName =
            $"{eventSymbol.ContainingType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)}.{Name}";
        HandlerTypeSyntax = SyntaxFactoryHelper.TypeSyntax(eventSymbol.Type);
        NullableAwareHandlerTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(
            eventSymbol.Type
        );
        HandlerArgTypeSyntax = WellKnownTypes.Imposter.Abstractions.Arg(HandlerTypeSyntax);

        if (
            eventSymbol.Type is not INamedTypeSymbol delegateSymbol
            || delegateSymbol.DelegateInvokeMethod is null
        )
        {
            throw new InvalidOperationException("Events must expose a delegate invoke method.");
        }

        var parameterModels = delegateSymbol
            .DelegateInvokeMethod.Parameters.Select(ParameterModel.From)
            .ToArray();
        Parameters = parameterModels.Select(model => new EventParameterMetadata(model)).ToArray();

        DelegateReturnTypeSymbol = delegateSymbol.DelegateInvokeMethod.ReturnType;
        IsAsync = DelegateReturnTypeSymbol.IsAwaitable();
        var includeRefKind = !IsAsync;
        RaiseParameterSyntaxes = parameterModels
            .Select(model => SyntaxFactoryHelper.ParameterSyntax(model, includeRefKind))
            .ToArray();

        var containingTypeIsClass = eventSymbol.ContainingType?.TypeKind == TypeKind.Class;
        var addSupportsBaseImplementation =
            containingTypeIsClass && eventSymbol.AddMethod is { IsAbstract: false };
        var removeSupportsBaseImplementation =
            containingTypeIsClass && eventSymbol.RemoveMethod is { IsAbstract: false };
        SupportsBaseImplementation =
            addSupportsBaseImplementation && removeSupportsBaseImplementation;
    }
}
