using System;
using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.TypeCriteriaTests.CustomArgType;

public interface ICurrentInterface<T>
{
}

public interface IMarker
{
}

public class Implementer : IMarker
{
}

public class NonImplementer
{
}

public class DerivedImplementer : Implementer
{
}

public class ImplementsMarkerArgType(object value) : IArgType<IMarker>
{
    public static bool Matches(Type type)
    {
        return type == typeof(IMarker) || type.GetInterface(nameof(IMarker)) != null;
    }

    public IMarker Value { get; } = (IMarker)value;
}