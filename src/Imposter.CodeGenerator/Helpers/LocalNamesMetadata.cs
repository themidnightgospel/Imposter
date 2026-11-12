using System.Collections.Generic;

namespace Imposter.CodeGenerator.Helpers;

internal readonly struct LocalNamesMetadata
{
    private readonly Dictionary<string, string> _names;

    internal LocalNamesMetadata(NameSet scope, params string[] names)
    {
        _names = new Dictionary<string, string>();
        foreach (var name in names)
            _names.Add(name, scope.Use(name));
    }

    internal string this[string name] => _names[name];
}
