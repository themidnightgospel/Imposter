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
        var arg = Arg<int>.Any;
            
        // Act & Assert
        arg.Matches(100).ShouldBeTrue();
        arg.Matches(-50).ShouldBeTrue();
        arg.Matches(0).ShouldBeTrue();
    }

    [Fact]
    public void Arg_Any_ShouldMatchAnyReferenceValueIncludingNull()
    {
        // Arrange
        var arg = Arg<string>.Any;
            
        // Act & Assert
        arg.Matches("hello").ShouldBeTrue();
        arg.Matches("").ShouldBeTrue();
        arg.Matches(null).ShouldBeTrue();
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
        arg.Matches(null).ShouldBeTrue();
        arg.Matches("not null").ShouldBeFalse();
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
        arg.Matches(null).ShouldBeTrue();
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
    public void Arg_MatchesRegex_ShouldMatchValidString()
    {
        // Arrange
        var arg = Arg<string>.MatchesRegex(@"^\d{3}-\d{2}-\d{4}$");
            
        // Act & Assert
        arg.Matches("123-45-6789").ShouldBeTrue();
    }
        
    [Fact]
    public void Arg_MatchesRegex_ShouldNotMatchInvalidString()
    {
        // Arrange
        var arg = Arg<string>.MatchesRegex(@"^\d+$");
            
        // Act & Assert
        arg.Matches("123a").ShouldBeFalse();
    }
        
    [Fact]
    public void Arg_MatchesRegex_ShouldNotMatchNull()
    {
        // Arrange
        var arg = Arg<string>.MatchesRegex(".*"); // Match any non-null string
            
        // Act & Assert
        arg.Matches(null).ShouldBeFalse();
    }

    [Fact]
    public void OutArg_Any_ShouldAlwaysMatch()
    {
        // Arrange
        var arg = OutArg<int>.Any;
            
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
        var arg = OutArg<string>.Any;
            
        // Act & Assert
        arg.Matches(null).ShouldBeTrue();

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