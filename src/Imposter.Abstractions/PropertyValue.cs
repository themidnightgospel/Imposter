using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Shares the default value between a generated property's accessors.</summary>
/// <typeparam name="T">The property's value type.</typeparam>
/// <remarks>Public for generated code only and may change with the generator in the same package.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class PropertyValue<T>
{
    internal volatile bool IsOn = true;
    internal volatile bool HasValueSet;
    internal T Value = default!;
}
