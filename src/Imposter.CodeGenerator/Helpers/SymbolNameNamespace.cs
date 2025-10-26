using System.Collections.Generic;

namespace Imposter.CodeGenerator.Helpers;

internal class SymbolNameNamespace
{
    private readonly HashSet<string> _alreadyUsedNames;

    public SymbolNameNamespace(IEnumerable<string> alreadyUsedNames)
    {
        _alreadyUsedNames = new(alreadyUsedNames);
    }

    internal string Use(string key)
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

    private bool IsNameUsed(string name)
    {
        return _alreadyUsedNames.Contains(name);
    }
}