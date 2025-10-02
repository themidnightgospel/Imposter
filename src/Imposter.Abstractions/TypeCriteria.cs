using System.Collections.Concurrent;
using System.Reflection;

namespace Imposter.Abstractions;

/// <summary>
/// TODO Add coments
/// </summary>
public class TypeCriteria
{
    private Func<Type, bool> _matches;

    public TypeCriteria(Func<Type, bool> matches)
    {
        _matches = matches;
    }

    public bool Matches(Type type) => _matches(type);

    private static readonly ConcurrentDictionary<Type, MethodInfo> CustomMatchesMethods = new ConcurrentDictionary<Type, MethodInfo>();

    private static MethodInfo GetMatchesMethod(Type type)
    {
        return CustomMatchesMethods
            .GetOrAdd(type, t =>
            {
                var matchesMethodNotFoundException = new InvalidOperationException($"Type {t} does not have a static method called Matches(Type type) that returns bool");

                var method = t.GetMethod("Matches", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(Type) }, null)
                             ?? throw matchesMethodNotFoundException;

                if (method.ReturnType != typeof(bool))
                {
                    throw matchesMethodNotFoundException;
                }

                return method;
            });
    }

    public static TypeCriteria Create<TArg>()
    {
        Func<Type, bool> matches = typeof(TArg) switch
        {
            var tArg when tArg == typeof(ArgType.Any) => ArgType.Any.Matches,
            { IsGenericType: true } tArg when tArg.GetGenericTypeDefinition() == typeof(ArgType.IsAssignableTo<>) => type => tArg.GetGenericArguments()[0].IsAssignableFrom(type),
            { IsGenericType: true } tArg when tArg.GetGenericTypeDefinition() == typeof(ArgType.Is<>) => type => tArg.GetGenericArguments()[0] == type,
            { IsGenericType: true } tArg when tArg.GetGenericTypeDefinition() == typeof(ArgType.IsSubclassOf<>) => type => type.IsSubclassOf(tArg.GetGenericArguments()[0]),
            var tArg when tArg
                    .GetInterfaces()
                    .Any(interfaceType => interfaceType == typeof(IArgType) || (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IArgType<>)))
                => type => (bool)GetMatchesMethod(tArg).Invoke(null, new[] { type }),
            _ => _ => true
        };

        return new TypeCriteria(matches);
    }
}