using System;
using System.ComponentModel;
using System.Reflection;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class GeneratedCodeOnlyTypesTests
{
    [Theory]
    [InlineData(typeof(TypeCaster))]
    [InlineData(typeof(SpanElementsComparer<>))]
    [InlineData(typeof(SetupCollection<>))]
    [InlineData(typeof(AsyncCallbacks))]
    public void GivenGeneratedCodeOnlyType_WhenInspected_ShouldBeHiddenFromIntelliSense(Type type)
    {
        type.GetCustomAttribute<EditorBrowsableAttribute>()
            .ShouldNotBeNull()
            .State.ShouldBe(EditorBrowsableState.Never);
    }
}
