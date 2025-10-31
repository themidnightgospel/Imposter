using System.Text.RegularExpressions;

namespace Imposter.Abstractions;

/// <summary>
/// Provides static factory methods for creating argument matchers used in Imposter setups.
/// </summary>
/// <typeparam name="T">The type of the argument to match.</typeparam>
public class Arg<T>
{
    private readonly Func<T, bool> _matches;

    private Arg(Func<T, bool> matches)
    {
        _matches = matches;
    }

    /// <summary>
    /// Checks if criteria matches the value
    /// </summary>
    public bool Matches(T value) => _matches(value);

    /// <summary>
    /// Matches an argument based on a custom predicate.
    /// </summary>
    public static Arg<T> Is(Func<T, bool> predicate) => new(predicate);

    /// <summary>
    /// Matches an argument that is equal to the provided value.
    /// </summary>
    public static Arg<T> Is(T? value) => new(it =>
    {
        if (value == null) return it == null;
        return EqualityComparer<T>.Default.Equals(it, value);
    });

    /// <summary>
    /// Matches an argument that is the default value for its type (e.g., 0 for int, null for string).
    /// </summary>
    public static Arg<T> IsDefault() => Is(default(T));

    /// <summary>
    /// Matches any value of type T.
    /// </summary>
    public static Arg<T> Any() => _anyInstance;

    private static readonly Arg<T> _anyInstance = new(_ => true);

    /// <summary>
    /// Matches a string argument against a regular expression.
    /// </summary>
    public static Arg<string> MatchesRegex(string regex)
    {
        var re = new Regex(regex);
        return new Arg<string>(value => value != null && re.IsMatch(value));
    }
}

/// <summary>
/// A matcher for 'out' parameters. Since 'out' parameters have no input value, this always matches.
/// </summary>
public class OutArg<T>
{
    private static readonly OutArg<T> Instance = new();

    /// <summary>
    /// Matches any 'out' argument of type T.
    /// </summary>
    public static OutArg<T> Any() => Instance;

    private OutArg()
    {
    }

    /// <summary>
    /// Checks if criteria matches the value
    /// </summary>
    public bool Matches(T value) => true;
}