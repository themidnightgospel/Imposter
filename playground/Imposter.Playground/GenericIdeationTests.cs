using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Imposter.CodeGenerator.Tests.Setup.Methods.Generics;
using Xunit;

namespace Imposter.Playground;

public class CustomArgType : List<int>, IArgType<List<int>>
{
    public static bool Matches(Type type)
    {
        return type.IsAssignableTo(typeof(List<int>));
    }

    public List<int> Value { get; }

    public CustomArgType(object? value)
    {
        Value = (List<int>)value;
    }
}

public class GenericIdeationTests
{
    [Fact]
    public void Test1()
    {
        var imposter = new ISutWithGenericMethodImposter();
        
        imposter
            .ConvertTo<ArgType.Any, string>(Arg<ArgType.Any>.Any)
            .Returns(input => input.Value?.ToString() ?? string.Empty);

        var res = imposter.Instance().ConvertTo<int, string>(32);
    }
}