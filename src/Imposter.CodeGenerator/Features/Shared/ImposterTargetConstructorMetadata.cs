using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterTargetConstructorMetadata
{
    internal readonly ImmutableArray<IParameterSymbol> Parameters;

    private ImposterTargetConstructorMetadata(ImmutableArray<IParameterSymbol> parameters)
    {
        Parameters = parameters;
    }

    internal static ImposterTargetConstructorMetadata FromSymbol(IMethodSymbol constructorSymbol) =>
        new(constructorSymbol.Parameters);

    internal static ImposterTargetConstructorMetadata CreateImplicitParameterless() =>
        new(ImmutableArray<IParameterSymbol>.Empty);
}
