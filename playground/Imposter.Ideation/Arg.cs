namespace Imposter.Ideation;

public class TestArg<T>(Func<T, bool> predicate)
{
    public Func<T, bool> Predicate = predicate;

    public static implicit operator TestArg<T>(T value)
    {
        // TODO possible null check
        return new TestArg<T>(t => t.Equals(value));
    }

    public static TestArg<T> Is(Func<T, bool> predicate) => new(predicate);

    public static TestArg<T> IsAny() => new(_ => true);

    // TODO Add more utility factory methods similar to Moq.It
}