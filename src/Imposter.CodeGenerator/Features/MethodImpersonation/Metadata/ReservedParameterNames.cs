using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

// Names a method's generated members and locals must not collide with: its parameter names,
// its unique name and its namespace. Each consumer allocates its own names from a fresh NameSet.
internal readonly struct ReservedParameterNames
{
    private readonly string[] _names;

    internal ReservedParameterNames(IEnumerable<string> names)
    {
        _names = names.ToArray();
    }

    internal NameSet CreateNameSet() => new(_names);
}
