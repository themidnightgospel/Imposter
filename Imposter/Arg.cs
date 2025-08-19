namespace Imposter;

public class Arg<T>(Func<T, bool> predicate)
{
    public Func<T, bool> Predicate = predicate;

    public static implicit operator Arg<T>(T value)
    {
        // TODO possible null check
        return new Arg<T>(t => t.Equals(value));
    }

    public static Arg<T> Is(Func<T, bool> predicate) => new(predicate);

    public static Arg<T> IsAny() => new(_ => true);

    // TODO Add more utility factory methods similar to Moq.It
}