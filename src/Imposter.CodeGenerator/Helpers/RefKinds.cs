using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

internal static class RefKinds
{
    // RefKind.RefReadOnlyParameter, the kind of a C# 12 `ref readonly` parameter. The Roslyn 4.0 and 4.4 packages
    // the generator also builds against predate it; the 4.4 build still meets it on compilers 4.8 to 4.13.
    internal const RefKind RefReadOnlyParameter = (RefKind)4;
}
