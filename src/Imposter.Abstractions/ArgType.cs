namespace Imposter.Abstractions;

/// <summary>
/// TODO add comments
/// </summary>
public interface IArgType<out T>
{
    T Value { get; }
}

public interface IArgType : IArgType<object>
{
}

public class ArgType
{
    public class Any : IArgType<object>
    {
        public static bool Matches(Type type)
        {
            return true;
        }

        public object? Value { get; }

        public Any(object? value)
        {
            Value = value;
        }
    }

    public class Is<T> : IArgType<T>
    {
        public static bool Matches(Type type)
        {
            return type == typeof(T);
        }

        public T Value { get; }

        public Is(T value)
        {
            Value = value;
        }
    }

    public class IsAssignableTo<T> : IArgType<T>
    {
        public T Value { get; }

        public IsAssignableTo(T value)
        {
            Value = value;
        }
    }

    public class IsSubclassOf<T> : IArgType<T>
    {
        public static bool Matches(Type type)
        {
            // Excludes T, only matches direct or indirect subclasses of T
            return type.IsSubclassOf(typeof(T));
        }

        public T Value { get; }

        public IsSubclassOf(T value)
        {
            Value = value;
        }
    }
}