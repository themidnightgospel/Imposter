using System;

namespace Imposter.CodeGenerator.Tests;


public class Arg2<T>(Func<T, bool> predicate)
{
    public Func<T, bool> Predicate = predicate;

    public static implicit operator Arg2<T>(T value)
    {
        // TODO possible null check
        return new Arg2<T>(t => t.Equals(value));
    }

    public static Arg<T> Is(Func<T, bool> predicate) => new(predicate);
    
    public static Arg<T> Is(T? value) => Is(it => it?.Equals(value) == true);
    
    public static Arg<T> IsDefault() => Is(default(T));


    public static Arg<T> Any = new(_ => true);

    // TODO Add more utility factory methods similar to Moq.It
} 