using System.Collections.Immutable;
using System.Linq;
using Imposter.CodeGenerator.Models;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterTargetConstructorMetadata
{
    internal readonly ImmutableArray<ParameterModel> Parameters;

    internal ImposterTargetConstructorMetadata(ConstructorModel constructor)
    {
        Parameters = constructor.Parameters.ToImmutableArray();
    }
}
