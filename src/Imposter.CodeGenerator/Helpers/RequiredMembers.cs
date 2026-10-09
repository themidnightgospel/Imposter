using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

// C# 11 required members. The Roslyn 4.0 build can't see them, and the compilers it serves don't support them.
internal static class RequiredMembers
{
    internal static bool IsRequiredMember(this ISymbol member) =>
#if ROSLYN4_4_OR_GREATER
        member is IPropertySymbol { IsRequired: true } or IFieldSymbol { IsRequired: true };
#else
        false;
#endif

    internal static bool HasRequiredMembers(this INamedTypeSymbol type)
    {
        for (var current = type; current is not null; current = current.BaseType)
        {
            if (current.GetMembers().Any(IsRequiredMember))
            {
                return true;
            }
        }

        return false;
    }
}
