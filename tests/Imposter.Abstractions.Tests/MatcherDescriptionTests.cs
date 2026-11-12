using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class MatcherDescriptionTests
{
    [Fact]
    public void GivenArgumentMatchers_WhenDescribed_ShouldIdentifyTheExpectedConstraint()
    {
        MatcherDescription.Format(Arg<int>.Any()).ShouldBe("any");
        MatcherDescription.Format(Arg<int>.Is(42)).ShouldBe("<42>");
        MatcherDescription.Format(Arg<string?>.Is((string?)null)).ShouldBe("<null>");
        MatcherDescription.Format(Arg<int>.IsNot(42)).ShouldBe("not <42>");
    }

    [Fact]
    public void GivenSpanMatchers_WhenDescribed_ShouldUseTheirStoredExpectations()
    {
        MatcherDescription.Format(SpanArg<int>.Any()).ShouldBe("any");
        MatcherDescription.Format(ReadOnlySpanArg<int>.Any()).ShouldBe("any");
        MatcherDescription.Format(SpanArg<int>.Is(1, 2)).ShouldBe("[<1>, <2>]");
        MatcherDescription.Format(ReadOnlySpanArg<int>.Is(1, 2)).ShouldBe("[<1>, <2>]");
    }

    [Fact]
    public void GivenPredicates_WhenDescribed_ShouldNotEvaluateThem()
    {
        MatcherDescription
            .Format(Arg<int>.Is(_ => throw new InvalidOperationException()))
            .ShouldBe("<predicate>");
        MatcherDescription
            .Format(SpanArg<int>.Is(_ => throw new InvalidOperationException()))
            .ShouldBe("<predicate>");
        MatcherDescription
            .Format(ReadOnlySpanArg<int>.Is(_ => throw new InvalidOperationException()))
            .ShouldBe("<predicate>");
    }

    [Fact]
    public void GivenThrowingValueFormatter_WhenDescribed_ShouldPreserveTheOrderedFailure()
    {
        MatcherDescription
            .Format(Arg<Unprintable>.Is(new Unprintable()))
            .ShouldBe("<unprintable Unprintable>");
    }

    private sealed class Unprintable
    {
        public override string ToString() => throw new InvalidOperationException();
    }
}
