using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterTargetConstructorMetadata
{
    internal readonly IMethodSymbol? Symbol;
    internal readonly Accessibility Accessibility;
    internal readonly ImmutableArray<IParameterSymbol> Parameters;
    internal readonly bool IsImplicit;

    private ImposterTargetConstructorMetadata(
        IMethodSymbol? symbol,
        Accessibility accessibility,
        ImmutableArray<IParameterSymbol> parameters,
        bool isImplicit)
    {
        Symbol = symbol;
        Accessibility = accessibility;
        Parameters = parameters;
        IsImplicit = isImplicit;
    }

    internal static ImposterTargetConstructorMetadata FromSymbol(IMethodSymbol constructorSymbol)
        => new(
            constructorSymbol,
            constructorSymbol.DeclaredAccessibility,
            constructorSymbol.Parameters,
            constructorSymbol.IsImplicitlyDeclared);

    internal static ImposterTargetConstructorMetadata CreateImplicitParameterless(Accessibility accessibility)
        => new(
            null,
            accessibility,
            ImmutableArray<IParameterSymbol>.Empty,
            true);
}
