using Imposter.Playground;

namespace Imposter.Abstractions;

public class ValueConverter
{
    public static TTarget ConvertTo<TTarget>(object value)
    {
        if (value == default)
        {
            return default;
        }
        
        if (typeof(IArgType).IsAssignableFrom(typeof(TTarget)))
        {
            return (TTarget)ArgTypeMethodsAccessor.Constructor(typeof(TTarget), value);
        }

        if ( value is TTarget valueAsTarget)
        {
            return valueAsTarget;
        }

        throw new InvalidOperationException($"Value {value} cannot be converted to {typeof(TTarget).Name}");
    }
}