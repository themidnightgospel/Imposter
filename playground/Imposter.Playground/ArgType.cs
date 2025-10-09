using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Imposter.Abstractions;

public static class ArgTypeMethodsAccessor
{
    private static readonly ConcurrentDictionary<Type, ArgTypeMethods> _argTypeMethods = new();

    public static bool Matches(Type argType, Type argumentType)
    {
        return (bool)GetMethods(argType).MatchesMethod.Invoke(null, [argumentType]);
    }

    public static object Constructor(Type argType, object argument)
    {
        return GetMethods(argType).Constructor.Invoke([argument]);
    }

    private static MethodInfo GetMatchesMethod(Type argType)
    {
        var matchesMethodNotFoundException = new InvalidOperationException($"Type {argType} does not have a static method called Matches(Type type) that returns bool");

        var matchesMethod = argType.GetMethod("Matches", BindingFlags.Public | BindingFlags.Static, null, [typeof(Type)], null)
                            ?? throw matchesMethodNotFoundException;

        if (matchesMethod.ReturnType != typeof(bool))
        {
            throw matchesMethodNotFoundException;
        }

        return matchesMethod;
    }

    private static ConstructorInfo GetConstructorMethod(Type argType) =>
        argType
            .GetConstructor(
                BindingFlags.Public | BindingFlags.Instance,
                null,
                [typeof(object)],
                null
            )
        ?? throw new InvalidOperationException($"Type {argType} does not have a public constructor with a single parameter of type object.");


    private static ArgTypeMethods GetMethods(Type type)
    {
        return _argTypeMethods
            .GetOrAdd(type, t => new ArgTypeMethods(GetMatchesMethod(type), GetConstructorMethod(type)));
    }

    readonly record struct ArgTypeMethods(MethodInfo MatchesMethod, ConstructorInfo Constructor);
}

/// <summary>
/// marker interface
/// </summary>
public interface IArgType
{
}

/// <summary>
/// Represents a generic type constraint argument used within <see cref="TypeCriteria.Create{TArg}"/>.
/// The type parameter <typeparamref name="T"/> specifies the concrete type the constraint targets.
/// </summary>
/// <typeparam name="T">The primary type related to the constraint (e.g., the target type for 'Is', or the base type for 'IsAssignableTo').</typeparam>
public interface IArgType<out T> : IArgType
{
    /// <summary>
    /// The value associated with the argument, typically used at runtime after a match has occurred.
    /// </summary>
    T Value { get; }
}

/// <summary>
/// Provides a set of built-in type constraints used to construct <see cref="TypeCriteria"/>.
/// </summary>
public class ArgType
{
    /// <summary>
    /// A constraint that matches any type. It is the most permissive constraint,
    /// equivalent to matching against <see cref="object"/>.
    /// </summary>
    public class Any : IArgType<object>
    {
        public static bool Matches(Type type)
        {
            return true;
        }

        /// <inheritdoc />
        public object? Value { get; }

        public Any(object? value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// A constraint that requires an exact type match. It rejects derived classes,
    /// implemented interfaces, and boxed value types.
    /// </summary>
    /// <typeparam name="T">The exact type required for a match.</typeparam>
    public class Is<T> : IArgType<T>
    {
        public static bool Matches(Type type)
        {
            return typeof(T) == type;
        }

        /// <inheritdoc />
        public T Value { get; }

        public Is(object value)
        {
            Value = (T)value;
        }
    }

    /// <summary>
    /// A constraint that requires the target type to be assignable to the specified type <typeparamref name="T"/>.
    /// This includes inheritance, interface implementation, and generic variance.
    /// </summary>
    /// <typeparam name="T">The type the target type must be assignable to.</typeparam>
    public class IsAssignableTo<T> : IArgType<T>
    {
        public static bool Matches(Type type)
        {
            return typeof(T).IsAssignableFrom(type);
        }

        /// <inheritdoc />
        public T Value { get; }

        public IsAssignableTo(object value)
        {
            Value = (T)value;
        }
    }