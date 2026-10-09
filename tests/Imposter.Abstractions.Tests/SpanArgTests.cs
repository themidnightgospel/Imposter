using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class SpanArgTests
{
    [Fact]
    public void GivenAnyMatcher_WhenEvaluatingElements_ShouldMatch()
    {
        var arg = SpanArg<int>.Any();

        arg.Matches([1, 2]).ShouldBeTrue();
    }

    [Fact]
    public void GivenAnyMatcher_WhenEvaluatingNoElements_ShouldMatch()
    {
        var arg = SpanArg<int>.Any();

        arg.Matches([]).ShouldBeTrue();
    }

    [Fact]
    public void GivenIsMatcherWithElements_WhenElementsAreEqualInOrder_ShouldMatch()
    {
        var arg = SpanArg<int>.Is(1, 2);

        arg.Matches([1, 2]).ShouldBeTrue();
    }

    [Fact]
    public void GivenIsMatcherWithElements_WhenElementsDifferInOrder_ShouldNotMatch()
    {
        var arg = SpanArg<int>.Is(1, 2);

        arg.Matches([2, 1]).ShouldBeFalse();
    }

    [Fact]
    public void GivenIsMatcherWithElements_WhenElementsDifferInLength_ShouldNotMatch()
    {
        var arg = SpanArg<int>.Is(1, 2);

        arg.Matches([1, 2, 3]).ShouldBeFalse();
    }

    [Fact]
    public void GivenIsMatcherWithElements_WhenTheExpectedArrayChangesLater_ShouldKeepTheOriginalElements()
    {
        var expected = new[] { 1, 2 };
        var arg = SpanArg<int>.Is(expected);

        expected[0] = 9;

        arg.Matches([1, 2]).ShouldBeTrue();
    }

    [Fact]
    public void GivenIsMatcherWithComparer_WhenElementsAreEqualByTheComparer_ShouldMatch()
    {
        var arg = SpanArg<string>.Is(["A", "b"], StringComparer.OrdinalIgnoreCase);

        arg.Matches(["a", "B"]).ShouldBeTrue();
    }

    [Fact]
    public void GivenIsMatcherWithPredicate_WhenThePredicateHolds_ShouldMatch()
    {
        var arg = SpanArg<char>.Is(elements => elements.Length > 2);

        arg.Matches(['a', 'b', 'c']).ShouldBeTrue();
    }

    [Fact]
    public void GivenIsMatcherWithPredicate_WhenThePredicateFails_ShouldNotMatch()
    {
        var arg = SpanArg<char>.Is(elements => elements.Length > 2);

        arg.Matches(['a']).ShouldBeFalse();
    }

    [Fact]
    public void GivenNullExpectedElements_WhenCreatingMatcher_ShouldThrow()
    {
        Should.Throw<ArgumentNullException>(() => SpanArg<int>.Is((int[])null!));
    }

    [Fact]
    public void GivenNullComparer_WhenCreatingMatcher_ShouldThrow()
    {
        Should.Throw<ArgumentNullException>(() =>
            SpanArg<int>.Is([1], (IEqualityComparer<int>)null!)
        );
    }

    [Fact]
    public void GivenNullPredicate_WhenCreatingMatcher_ShouldThrow()
    {
        Should.Throw<ArgumentNullException>(() => SpanArg<int>.Is((Func<int[], bool>)null!));
    }

    [Fact]
    public void GivenAnyArgMarker_WhenConvertedToSpanArg_ShouldMatchAnyElements()
    {
        SpanArg<int> arg = Arg.Any;

        arg.Matches([7]).ShouldBeTrue();
    }
}
