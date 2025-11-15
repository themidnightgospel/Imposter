using System;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.EventImposter.Metadata;

internal readonly ref struct ImposterEventCoreMetadata
{
    internal readonly string Name;

    internal readonly string UniqueName;

    internal readonly string DisplayName;

    internal readonly TypeSyntax HandlerTypeSyntax;

    internal readonly TypeSyntax NullableAwareHandlerTypeSyntax;

    internal readonly TypeSyntax HandlerArgTypeSyntax;

    internal readonly EventParameterMetadata[] Parameters;

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

        Parameters = delegateSymbol
            .DelegateInvokeMethod.Parameters.Select(parameter => new EventParameterMetadata(
                parameter
            ))
            .ToArray();

        DelegateReturnTypeSymbol = delegateSymbol.DelegateInvokeMethod.ReturnType;
        IsAsync = DelegateReturnTypeSymbol.IsAwaitable();

        var containingTypeIsClass = eventSymbol.ContainingType?.TypeKind == TypeKind.Class;
        var addSupportsBaseImplementation =
            containingTypeIsClass && eventSymbol.AddMethod is { IsAbstract: false };
        var removeSupportsBaseImplementation =
            containingTypeIsClass && eventSymbol.RemoveMethod is { IsAbstract: false };
        SupportsBaseImplementation =
            addSupportsBaseImplementation && removeSupportsBaseImplementation;
    }
}
