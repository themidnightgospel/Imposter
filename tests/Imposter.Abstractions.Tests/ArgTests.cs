using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class ArgTests
{
    [Fact]
    public void Arg_Any_ShouldMatchAnyValue()
    {
        // Arrange
        var arg = Arg<int>.Any();
            
        // Act & Assert
        arg.Matches(100).ShouldBeTrue();
        arg.Matches(-50).ShouldBeTrue();
        arg.Matches(0).ShouldBeTrue();
    }

    [Fact]
    public void Arg_Any_ShouldMatchAnyReferenceValueIncludingNull()
    {
        // Arrange
        var arg = Arg<string>.Any();
            
        // Act & Assert
        arg.Matches("hello").ShouldBeTrue();
        arg.Matches("").ShouldBeTrue();
        arg.Matches(null!).ShouldBeTrue();
    }

    [Fact]
    public void Arg_Is_Value_ShouldMatchEqualValue()
    {
        // Arrange
        var arg = Arg<int>.Is(42);
            
        // Act & Assert
        arg.Matches(42).ShouldBeTrue();
        arg.Matches(43).ShouldBeFalse();
    }

    [Fact]
    public void Arg_IsNot_Value_ShouldMatchAnythingExceptProvidedValue()
    {
        // Arrange
        var arg = Arg<int>.IsNot(42);

        // Act & Assert
        arg.Matches(41).ShouldBeTrue();
        arg.Matches(42).ShouldBeFalse();
    }

    [Fact]
    public void Arg_IsNot_Null_ShouldMatchNonNullReferencesOnly()
    {
        // Arrange
        var arg = Arg<string>.IsNot((string?)null);

        // Act & Assert
        arg.Matches("value").ShouldBeTrue();
        arg.Matches(null!).ShouldBeFalse();
    }

    [Fact]
    public void Arg_Is_WithCustomComparer_ShouldUseComparer()
    {
        // Arrange
        var arg = Arg<string>.Is("foo", StringComparer.OrdinalIgnoreCase);

        // Act & Assert
        arg.Matches("FOO").ShouldBeTrue();
        arg.Matches("bar").ShouldBeFalse();
    }

    [Fact]
    public void Arg_IsNot_WithCustomComparer_ShouldUseComparer()
    {
        // Arrange
        var arg = Arg<string>.IsNot("foo", StringComparer.OrdinalIgnoreCase);

        // Act & Assert
        arg.Matches("bar").ShouldBeTrue();
        arg.Matches("FOO").ShouldBeFalse();
    }
        
    [Fact]
    public void Arg_Is_Reference_ShouldMatchEqualReference()
    {
        // Arrange
        var arg = Arg<string>.Is("test");
            
        // Act & Assert
        arg.Matches("test").ShouldBeTrue();
        arg.Matches("different").ShouldBeFalse();
    }
        
    [Fact]
    public void Arg_Is_Null_ShouldMatchNull()
    {
        // Arrange
        var arg = Arg<string>.Is((string?)null);
            
        // Act & Assert
        arg.Matches(null!).ShouldBeTrue();
        arg.Matches("not null").ShouldBeFalse();
    }

    [Fact]
    public void Arg_IsNot_Predicate_ShouldMatchWhenPredicateIsFalse()
    {
        // Arrange
        var arg = Arg<int>.IsNot(x => x % 2 == 0);

        // Act & Assert
        arg.Matches(3).ShouldBeTrue();
        arg.Matches(4).ShouldBeFalse();
    }

    [Fact]
    public void Arg_Is_Predicate_ShouldMatchBasedOnPredicate()
    {
        // Arrange
        var arg = Arg<int>.Is(i => i > 10);
            
        // Act & Assert
        arg.Matches(11).ShouldBeTrue();
        arg.Matches(10).ShouldBeFalse();
        arg.Matches(9).ShouldBeFalse();
    }

    [Fact]
    public void Arg_IsIn_ShouldMatchValuesPresentInCollection()
    {
        // Arrange
        var arg = Arg<int>.IsIn(new[] { 1, 2, 3 });

        // Act & Assert
        arg.Matches(2).ShouldBeTrue();
        arg.Matches(4).ShouldBeFalse();
    }

    [Fact]
    public void Arg_IsIn_WithCustomComparer_ShouldUseComparer()
    {
        // Arrange
        var arg = Arg<string>.IsIn(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase);

        // Act & Assert
        arg.Matches("FOO").ShouldBeTrue();
        arg.Matches("baz").ShouldBeFalse();
    }

    [Fact]
    public void Arg_IsNotIn_ShouldRejectValuesPresentInCollection()
    {
        // Arrange
        var forbidden = new[] { "alpha", "beta" };
        var arg = Arg<string>.IsNotIn(forbidden);

        // Act & Assert
        arg.Matches("gamma").ShouldBeTrue();
        arg.Matches("beta").ShouldBeFalse();
    }

    [Fact]
    public void Arg_IsNotIn_WithCustomComparer_ShouldUseComparer()
    {
        // Arrange
        var forbidden = new[] { "foo", "bar" };
        var arg = Arg<string>.IsNotIn(forbidden, StringComparer.OrdinalIgnoreCase);

        // Act & Assert
        arg.Matches("baz").ShouldBeTrue();
        arg.Matches("FOO").ShouldBeFalse();
    }
        
    [Fact]
    public void Arg_IsDefault_ShouldMatchDefaultValueType()
    {
        // Arrange
        var arg = Arg<int>.IsDefault();
            
        // Act & Assert
        arg.Matches(0).ShouldBeTrue();
        arg.Matches(1).ShouldBeFalse();
    }

    [Fact]
    public void Arg_IsDefault_ShouldMatchDefaultReferenceType()
    {
        // Arrange
        var arg = Arg<string>.IsDefault();
            
        // Act & Assert
        arg.Matches(null!).ShouldBeTrue();
        arg.Matches("").ShouldBeFalse();
    }

    [Fact]
    public void Arg_IsDefault_ShouldMatchDefaultStruct()
    {
        // Arrange
        var arg = Arg<TestStruct>.IsDefault();
            
        // Act & Assert
        arg.Matches(default).ShouldBeTrue();
        arg.Matches(new TestStruct(1)).ShouldBeFalse();
    }
        
    [Fact]
    public void OutArg_Any_ShouldAlwaysMatch()
    {
        // Arrange
        var arg = OutArg<int>.Any();
            
        // Act & Assert
        // For an 'out' param, the value passed to Matches will be the default,
        // but the matcher should be independent of it.
        arg.Matches(0).ShouldBeTrue();
        arg.Matches(100).ShouldBeTrue(); // Hypothetical value
    }
        
    [Fact]
    public void OutArg_Any_ShouldAlwaysMatchForReferenceTypes()
    {
        // Arrange
        var arg = OutArg<string>.Any();
            
        // Act & Assert
        arg.Matches(null!).ShouldBeTrue();

        // The value of an 'out' variable is not defined before entering the method,
        // so this just confirms the matcher always returns true.
        arg.Matches("some value").ShouldBeTrue();
    }
        
    private struct TestStruct
    {
        public int Value { get; }
        public TestStruct(int value) => Value = value;
    }
}
