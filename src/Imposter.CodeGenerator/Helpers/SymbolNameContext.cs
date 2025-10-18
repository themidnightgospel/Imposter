using System.Collections.Generic;

namespace Imposter.CodeGenerator.Helpers;

public class SymbolNameContext
{
    private readonly HashSet<string> _alreadyUsedNames;

    public SymbolNameContext(IEnumerable<string> alreadyUsedNames)
    {
        _alreadyUsedNames = new(alreadyUsedNames);
    }

    private SymbolNameContext(SymbolNameContext parent, IEnumerable<string> alreadyUsedNames)
    {
        Parent = parent;
        _alreadyUsedNames = new(alreadyUsedNames);
    }

    private SymbolNameContext? Parent { get; }

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

    private bool IsNameUsed(string name)
    {
        return _alreadyUsedNames.Contains(name) || Parent?.IsNameUsed(name) is true;
    }

    public SymbolNameContext CreateScope(IEnumerable<string> alreadyUsedNames)
    {
        return new SymbolNameContext(this, alreadyUsedNames);
    }
}