using System.Collections.Immutable;
using System.Linq;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterTargetConstructorMetadata
{
    internal readonly ImmutableArray<ParameterModel> Parameters;

    private ImposterTargetConstructorMetadata(ImmutableArray<ParameterModel> parameters)
    {
        Parameters = parameters;
    }

    internal static ImposterTargetConstructorMetadata FromSymbol(IMethodSymbol constructorSymbol) =>
        new(constructorSymbol.Parameters.Select(ParameterModel.From).ToImmutableArray());

    internal static ImposterTargetConstructorMetadata CreateImplicitParameterless() =>
        new(ImmutableArray<ParameterModel>.Empty);
}
