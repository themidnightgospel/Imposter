using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

internal class MethodUniqueNames
{
    private readonly Dictionary<IMethodSymbol, string> _methodUniqueNames = new(SymbolEqualityComparer.Default);

    internal string GetUniqueNameForMethod(IMethodSymbol method)
    {
        if (_methodUniqueNames.TryGetValue(method, out var name))
        {
            return name;
        }

        var baseName = method.Name;
        var postfixCounter = 0;
        while (_methodUniqueNames.Values.Any(it => it == baseName))
        {
            baseName = $"{method.Name}_{postfixCounter}_";
        }

        _methodUniqueNames.Add(method, baseName);
        return baseName;
    }
}