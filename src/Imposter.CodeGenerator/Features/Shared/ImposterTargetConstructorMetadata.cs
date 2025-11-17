using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterTargetConstructorMetadata
{
    internal readonly Accessibility Accessibility;
    internal readonly ImmutableArray<IParameterSymbol> Parameters;

    private ImposterTargetConstructorMetadata(
        Accessibility accessibility,
        ImmutableArray<IParameterSymbol> parameters
    )
    {
        Accessibility = accessibility;
        Parameters = parameters;
    }

    internal static ImposterTargetConstructorMetadata FromSymbol(IMethodSymbol constructorSymbol) =>
        new(constructorSymbol.DeclaredAccessibility, constructorSymbol.Parameters);

    internal static ImposterTargetConstructorMetadata CreateImplicitParameterless(
        Accessibility accessibility
    ) => new(accessibility, ImmutableArray<IParameterSymbol>.Empty);
}
