using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class ArgTests
{
    [Fact]
    public void GivenAnyMatcher_WhenEvaluatingValueTypes_ShouldMatchAnyValue()
    {
        // Arrange
        var arg = Arg<int>.Any();

        // Act & Assert
        arg.Matches(100).ShouldBeTrue();
        arg.Matches(-50).ShouldBeTrue();
        arg.Matches(0).ShouldBeTrue();
    }

    [Fact]
    public void GivenAnyMatcher_WhenEvaluatingReferenceTypes_ShouldMatchIncludingNull()
    {
        // Arrange
        var arg = Arg<string>.Any();

        // Act & Assert
        arg.Matches("hello").ShouldBeTrue();
        arg.Matches("").ShouldBeTrue();
        arg.Matches(null!).ShouldBeTrue();
    }

    [Fact]
    public void GivenIsMatcher_WhenValueEqualsTarget_ShouldMatchOnlyThatValue()
    {
        // Arrange
        var arg = Arg<int>.Is(42);

        // Act & Assert
        arg.Matches(42).ShouldBeTrue();
        arg.Matches(43).ShouldBeFalse();
    }

    [Fact]
    public void GivenIsNotMatcher_WhenValueEqualsTarget_ShouldRejectValue()
    {
        // Arrange
        var arg = Arg<int>.IsNot(42);

        // Act & Assert
        arg.Matches(41).ShouldBeTrue();
        arg.Matches(42).ShouldBeFalse();
    }

    [Fact]
    public void GivenIsNotMatcher_WhenReferenceIsNull_ShouldRejectNull()
    {
        // Arrange
        var arg = Arg<string>.IsNot((string?)null);

        // Act & Assert
        arg.Matches("value").ShouldBeTrue();
        arg.Matches(null!).ShouldBeFalse();
    }

    [Fact]
    public void GivenIsMatcher_WhenUsingCustomComparer_ShouldHonorComparer()
    {
        // Arrange
        var arg = Arg<string>.Is("foo", StringComparer.OrdinalIgnoreCase);

        // Act & Assert
        arg.Matches("FOO").ShouldBeTrue();
        arg.Matches("bar").ShouldBeFalse();
    }

    [Fact]
    public void GivenIsNotMatcher_WhenUsingCustomComparer_ShouldHonorComparer()
    {
        // Arrange
        var arg = Arg<string>.IsNot("foo", StringComparer.OrdinalIgnoreCase);

        // Act & Assert
        arg.Matches("bar").ShouldBeTrue();
        arg.Matches("FOO").ShouldBeFalse();
    }

    [Fact]
    public void GivenIsMatcher_WhenReferenceMatches_ShouldSucceed()
    {
        // Arrange
        var arg = Arg<string>.Is("test");

        // Act & Assert
        arg.Matches("test").ShouldBeTrue();
        arg.Matches("different").ShouldBeFalse();
    }

    [Fact]
    public void GivenIsMatcher_WhenReferenceNullIsExpected_ShouldMatchNull()
    {
        // Arrange
        var arg = Arg<string>.Is((string?)null);

        // Act & Assert
        arg.Matches(null!).ShouldBeTrue();
        arg.Matches("not null").ShouldBeFalse();
    }

    [Fact]
    public void GivenIsNotMatcher_WhenPredicateFails_ShouldRejectValue()
    {
        // Arrange
        var arg = Arg<int>.IsNot(x => x % 2 == 0);

        // Act & Assert
        arg.Matches(3).ShouldBeTrue();
        arg.Matches(4).ShouldBeFalse();
    }

    [Fact]
    public void GivenIsMatcher_WhenPredicateIsUsed_ShouldHonorPredicate()
    {
        // Arrange
        var arg = Arg<int>.Is(i => i > 10);

        // Act & Assert
        arg.Matches(11).ShouldBeTrue();
        arg.Matches(10).ShouldBeFalse();
        arg.Matches(9).ShouldBeFalse();
    }

    [Fact]
    public void GivenIsInMatcher_WhenValueIsInCollection_ShouldMatchOnlyContainedValues()
    {
        // Arrange
        var arg = Arg<int>.IsIn(new[] { 1, 2, 3 });

        // Act & Assert
        arg.Matches(2).ShouldBeTrue();
        arg.Matches(4).ShouldBeFalse();
    }

    [Fact]
    public void GivenIsInMatcher_WhenUsingComparer_ShouldHonorComparer()
    {
        // Arrange
        var arg = Arg<string>.IsIn(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase);

        // Act & Assert
        arg.Matches("FOO").ShouldBeTrue();
        arg.Matches("baz").ShouldBeFalse();
    }

    [Fact]
    public void GivenIsNotInMatcher_WhenValueInCollection_ShouldRejectIt()
    {
        // Arrange
        var forbidden = new[] { "alpha", "beta" };
        var arg = Arg<string>.IsNotIn(forbidden);

        // Act & Assert
        arg.Matches("gamma").ShouldBeTrue();
        arg.Matches("beta").ShouldBeFalse();
    }

    [Fact]
    public void GivenIsNotInMatcher_WhenUsingComparer_ShouldHonorComparer()
    {
        // Arrange
        var forbidden = new[] { "foo", "bar" };
        var arg = Arg<string>.IsNotIn(forbidden, StringComparer.OrdinalIgnoreCase);

        // Act & Assert
        arg.Matches("baz").ShouldBeTrue();
        arg.Matches("FOO").ShouldBeFalse();
    }

    [Fact]
    public void GivenIsDefaultMatcher_WhenValueTypeIsDefault_ShouldMatchDefault()
    {
        // Arrange
        var arg = Arg<int>.IsDefault();

        // Act & Assert
        arg.Matches(0).ShouldBeTrue();
        arg.Matches(1).ShouldBeFalse();
    }

    [Fact]
    public void GivenIsDefaultMatcher_WhenReferenceTypeIsDefault_ShouldMatchDefault()
    {
        // Arrange
        var arg = Arg<string>.IsDefault();

        // Act & Assert
        arg.Matches(null!).ShouldBeTrue();
        arg.Matches("").ShouldBeFalse();
    }

    [Fact]
    public void GivenIsDefaultMatcher_WhenStructIsDefault_ShouldMatchDefault()
    {
        // Arrange
        var arg = Arg<TestStruct>.IsDefault();

        // Act & Assert
        arg.Matches(default).ShouldBeTrue();
        arg.Matches(new TestStruct(1)).ShouldBeFalse();
    }

    [Fact]
    public void GivenOutArgAnyMatcher_WhenAnyValueProvided_ShouldAlwaysMatch()
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
    public void GivenOutArgAnyMatcher_WhenReferenceValuesProvided_ShouldAlwaysMatch()
    {
        // Arrange
        var arg = OutArg<string>.Any();

        // Act & Assert
        arg.Matches(null!).ShouldBeTrue();

        // The value of an 'out' variable is not defined before entering the method,
        // so this just confirms the matcher always returns true.
        arg.Matches("some value").ShouldBeTrue();
    }

    [Fact]
    public void GivenAny_WhenRequestedOrImplicitlyConverted_ShouldReturnTheSingleton()
    {
        Arg<int> converted = Arg.Any;
        Arg<int>.Any().ShouldBeSameAs(Arg<int>.Any());
        converted.ShouldBeSameAs(Arg<int>.Any());
        Arg<string>.Any().ShouldBeSameAs(Arg<string>.Any());
    }

    [Fact]
    public void GivenImplicitConversion_WhenMatched_ShouldPreserveValueAndNullEquality()
    {
        Arg<int> number = 5;
        number.Matches(5).ShouldBeTrue();
        number.Matches(6).ShouldBeFalse();
        Arg<string?> text = (string?)null;
        text.Matches(null).ShouldBeTrue();
        text.Matches("value").ShouldBeFalse();
    }

    [Fact]
    public void GivenNullableValue_WhenMatched_ShouldDistinguishNullFromZero()
    {
        var empty = Arg<int?>.IsDefault();
        empty.Matches(null).ShouldBeTrue();
        empty.Matches(0).ShouldBeFalse();
        var nonempty = Arg<int?>.IsNot((int?)null);
        nonempty.Matches(null).ShouldBeFalse();
        nonempty.Matches(0).ShouldBeTrue();
    }

    [Theory]
    [InlineData(false, "candidate", "target")]
    [InlineData(true, "candidate", "target")]
    [InlineData(false, null, "target")]
    [InlineData(true, null, "target")]
    [InlineData(false, "candidate", null)]
    [InlineData(true, "candidate", null)]
    public void GivenCustomComparer_WhenMatched_ShouldPassActualBeforeExpected(
        bool negated,
        string? actual,
        string? expected
    )
    {
        var comparer = new OrderedComparer();
        var arg = negated
            ? Arg<string?>.IsNot(expected, comparer)
            : Arg<string?>.Is(expected, comparer);

        arg.Matches(actual).ShouldBe((actual == "candidate" && expected == "target") != negated);
        comparer.Actual.ShouldBe(actual);
        comparer.Expected.ShouldBe(expected);
        comparer.Calls.ShouldBe(1);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GivenPredicate_WhenMatched_ShouldEvaluateOncePerCallAndPropagateExceptions(
        bool negated
    )
    {
        var calls = 0;
        var result = true;
        Func<int, bool> predicate = _ =>
        {
            calls++;
            return result;
        };
        var arg = negated ? Arg<int>.IsNot(predicate) : Arg<int>.Is(predicate);
        calls.ShouldBe(0);
        arg.Matches(5).ShouldBe(!negated);
        result = false;
        arg.Matches(5).ShouldBe(negated);
        calls.ShouldBe(2);

        var exception = new InvalidOperationException("predicate failure");
        Func<int, bool> throwing = _ => throw exception;
        var failing = negated ? Arg<int>.IsNot(throwing) : Arg<int>.Is(throwing);
        Should.Throw<InvalidOperationException>(() => failing.Matches(5)).ShouldBeSameAs(exception);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GivenSetMatcher_WhenOriginalCollectionChanges_ShouldKeepItsSnapshot(bool negated)
    {
        var values = new List<int> { 1, 2, 3 };
        var arg = negated ? Arg<int>.IsNotIn(values) : Arg<int>.IsIn(values);
        values.Clear();
        values.Add(9);
        arg.Matches(1).ShouldBe(!negated);
        arg.Matches(9).ShouldBe(negated);
    }

    [Fact]
    public void GivenNullPredicateOrComparerOrSet_WhenConfigured_ShouldKeepArgumentValidation()
    {
        Should
            .Throw<ArgumentNullException>(() => Arg<int>.Is((Func<int, bool>)null!))
            .ParamName.ShouldBe("predicate");
        Should
            .Throw<ArgumentNullException>(() => Arg<int>.IsNot((Func<int, bool>)null!))
            .ParamName.ShouldBe("predicate");
        Should
            .Throw<ArgumentNullException>(() => Arg<int>.Is(5, null!))
            .ParamName.ShouldBe("comparer");
        Should
            .Throw<ArgumentNullException>(() => Arg<int>.IsNot(5, null!))
            .ParamName.ShouldBe("comparer");
        Should
            .Throw<ArgumentNullException>(() => Arg<int>.IsIn(null!, null!))
            .ParamName.ShouldBe("values");
        Should
            .Throw<ArgumentNullException>(() => Arg<int>.IsNotIn(null!, null!))
            .ParamName.ShouldBe("values");
        Should
            .Throw<ArgumentNullException>(() => Arg<int>.IsIn(new[] { 5 }, null!))
            .ParamName.ShouldBe("comparer");
        Should
            .Throw<ArgumentNullException>(() => Arg<int>.IsNotIn(new[] { 5 }, null!))
            .ParamName.ShouldBe("comparer");
    }

    private sealed class OrderedComparer : IEqualityComparer<string?>
    {
        public string? Actual { get; private set; }
        public string? Expected { get; private set; }
        public int Calls { get; private set; }

        public bool Equals(string? actual, string? expected)
        {
            Actual = actual;
            Expected = expected;
            Calls++;
            return actual == "candidate" && expected == "target";
        }

        public int GetHashCode(string? value) => throw new NotSupportedException();
    }

    private struct TestStruct
    {
        public int Value { get; }

        public TestStruct(int value) => Value = value;
    }
}
