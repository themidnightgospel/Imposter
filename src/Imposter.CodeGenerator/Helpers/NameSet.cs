using System.Collections.Generic;

namespace Imposter.CodeGenerator.Helpers;

internal class NameSet(IEnumerable<string> alreadyUsedNames)
{
    private readonly HashSet<string> _alreadyUsedNames = [.. alreadyUsedNames];

    internal string Use(string key)
    {
        var value = key;
        var counter = 0;

        while (!_alreadyUsedNames.Add(value))
        {
            value = $"{key}_{++counter}";
        }

        return value;
    }
}
