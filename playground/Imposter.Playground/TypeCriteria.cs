using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace Imposter.Abstractions;

/// <summary>
/// TODO Add coments
/// </summary>
#if DEBUG
[DebuggerDisplay("{DisplayName}")]
#endif
public class TypeCriteria
{
    private readonly Func<Type, bool> _matches;

    private TypeCriteria(
        Func<Type, bool> matches
#if DEBUG
        , string displayName
#endif
    )
    {
#if DEBUG
        DisplayName = displayName;
#endif
        _matches = matches;
    }

    public bool Matches(Type type) => _matches(type);

    private static readonly ConcurrentDictionary<Type, MethodInfo> CustomMatchesMethods = new ConcurrentDictionary<Type, MethodInfo>();

    public static TypeCriteria Create<TArg>() => new(
        CreateInternal(typeof(TArg))
#if DEBUG
        , typeof(TArg).ToString()
#endif
    );

    private static bool IsArgType(Type type)
    {
        return type.GetInterfaces()
            .Any(interfaceType => interfaceType == typeof(IArgType) || (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IArgType<>)));
    }

    private static Func<Type, bool> CreateInternal(Type tArg)
    {
        if (IsArgType(tArg))
        {
            return type => ArgTypeMethodsAccessor.Matches(tArg, type);
        }

        if (tArg is { IsGenericType: true, IsGenericTypeDefinition: false })
        {
            return targetType =>
            {
                foreach (var currentType in GetAllTypesToExamine(targetType))
                {
                    if (!currentType.IsGenericType || currentType.IsGenericTypeDefinition)
                    {
                        continue;
                    }

                    if (tArg.GetGenericTypeDefinition() == currentType.GetGenericTypeDefinition())
                    {
                        if (MatchGenericArgs(tArg, currentType))
                        {
                            return true;
                        }
                    }
                }

                return false;
            };
        }

        return tArg.IsAssignableFrom;

        bool MatchGenericArgs(Type criteriaType, Type targetType)
        {
            var argTypes = criteriaType.GetGenericArguments();
            var matchingTypes = targetType.GetGenericArguments();

            if (argTypes.Length != matchingTypes.Length)
            {
                return false;
            }

            for (var i = 0; i < argTypes.Length; i++)
            {
                var criteria = CreateInternal(argTypes[i]);
                if (!criteria(matchingTypes[i])) return false;
            }

            return true;
        }

        static IEnumerable<Type> GetAllTypesToExamine(Type type)
        {
            var types = new HashSet<Type>();
            types.UnionWith(type.GetInterfaces());

            var current = type;
            while (current != null && current != typeof(object))
            {
                types.Add(current);
                current = current.BaseType;
            }

            return types;
        }
    }

#if DEBUG
    private string DisplayName { get; }
#endif
}