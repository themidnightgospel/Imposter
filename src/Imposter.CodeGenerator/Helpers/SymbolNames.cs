using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

public static class SymbolNames
{
    public static IEnumerable<string> GetParameterNames(IMethodSymbol method)
    {
        return method.Parameters.Select(p => p.Name);
    }
    
    public static IEnumerable<string> GetMemberNames(INamedTypeSymbol type)
    {
        return type.GetMembers().Select(m => m.Name);
    }
}