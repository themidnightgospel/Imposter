using System.Text.RegularExpressions;

namespace Imposter.Abstractions;

public interface IArg<in T>
{
    bool Matches(T value);
}

/// <summary>
/// TODO add doc
/// </summary>
public class Arg<T> : IArg<T>
{
    private readonly Func<T, bool> matches;
    
    private Arg(Func<T, bool> matches)
    {
        this.matches = matches;
    }
    
    public bool Matches(T value) => matches(value);

    public static Arg<T> Is(Func<T, bool> predicate) => new(predicate);

    public static Arg<T> Is(T? value) => Is(it => it?.Equals(value) == true);

    public static Arg<T> IsDefault() => Is(default(T));

    public static Arg<T> Any = new(_ => true);

    public static Arg<string> MatchesRegex(string regex)
    {
        var re = new Regex(regex);

        return new Arg<string>(value => value != null && re.IsMatch(value));
    }

    // TODO Add more utility factory methods similar to Moq.It
}

public class OutArg<T>() : IArg<T>
{
    public static OutArg<T> Any = new();

    public bool Matches(T value) => true;
}