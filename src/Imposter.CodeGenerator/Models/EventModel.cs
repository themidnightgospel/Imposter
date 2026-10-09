using System;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// An event the imposter implements or overrides, with the parameters and return type of its delegate.
/// <see cref="OverrideAccessibility"/> is the accessibility an override in the imposter's assembly must declare.
/// </summary>
internal sealed record EventModel(
    string Name,
    string DisplayName,
    TypeModel Type,
    TypeModel ContainingType,
    bool IsClassMember,
    Accessibility OverrideAccessibility,
    bool HasConcreteAccessors,
    EquatableArray<ParameterModel> DelegateParameters,
    bool IsAsync,
    bool ReturnsNonGenericValueTask
)
{
    internal static EventModel From(IEventSymbol @event, MemberAccess memberAccess)
    {
        if (
            @event.Type is not INamedTypeSymbol delegateType
            || delegateType.DelegateInvokeMethod is not { } invokeMethod
        )
        {
            throw new InvalidOperationException("Events must expose a delegate invoke method.");
        }

        return new EventModel(
            @event.Name,
            $"{@event.ContainingType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)}.{@event.Name}",
            TypeModel.From(@event.Type),
            TypeModel.From(@event.ContainingType),
            @event.ContainingType.TypeKind == TypeKind.Class,
            memberAccess.GetOverrideAccessibility(@event),
            @event is { AddMethod.IsAbstract: false, RemoveMethod.IsAbstract: false },
            invokeMethod.Parameters.Select(ParameterModel.From).ToEquatableArray(),
            invokeMethod.ReturnType.IsAwaitable(),
            invokeMethod.ReturnType.IsNonGenericValueTask()
        );
    }
}
