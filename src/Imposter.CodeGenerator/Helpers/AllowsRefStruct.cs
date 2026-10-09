using Microsoft.CodeAnalysis;
#if !ROSLYN4_14_OR_GREATER
using System.Reflection;
#endif

namespace Imposter.CodeGenerator.Helpers;

// C# 13's allows ref struct anti-constraint. The Roslyn 4.4 build also runs in the 4.12 and 4.13 compilers, which
// support it, so the builds older than 4.14 read the property their Roslyn API lacks through reflection.
internal static class AllowsRefStruct
{
#if ROSLYN4_14_OR_GREATER
    internal static bool AllowsRefStructs(this ITypeParameterSymbol typeParameter) =>
        typeParameter.AllowsRefLikeType;
#else
    private static readonly PropertyInfo? AllowsRefLikeTypeProperty =
        typeof(ITypeParameterSymbol).GetProperty("AllowsRefLikeType");

    internal static bool AllowsRefStructs(this ITypeParameterSymbol typeParameter) =>
        AllowsRefLikeTypeProperty?.GetValue(typeParameter) is true;
#endif
}
