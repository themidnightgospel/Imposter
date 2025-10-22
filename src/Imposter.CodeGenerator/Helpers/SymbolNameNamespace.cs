using System.Collections.Generic;

namespace Imposter.CodeGenerator.Helpers;

internal readonly struct SymbolNameNamespace(IEnumerable<string> alreadyUsedNames)
{
    private readonly HashSet<string> _alreadyUsedNames = new(alreadyUsedNames);

    public string Use(string key)
    {
        var counter = 0;
        var value = key;

        while (IsNameUsed(value))
        {
            value = $"{key}_{++counter}";
        }

        _alreadyUsedNames.Add(value);

        return value;
    }

    private bool IsNameUsed(string name) => _alreadyUsedNames.Contains(name);
}