using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class CountTests
{
    [Fact]
    public void GivenRangeConstraint_WhenEvaluatingMatches_ShouldEnforceInclusiveBounds()
    {
        var count = Count.Between(2, 4);

        count.Matches(1).ShouldBeFalse();
        count.Matches(2).ShouldBeTrue();
        count.Matches(3).ShouldBeTrue();
        count.Matches(4).ShouldBeTrue();
        count.Matches(5).ShouldBeFalse();
        count.ToString().ShouldBe("between 2 and 4 times");
    }

    [Fact]
    public void GivenRangeConstraint_WhenMinimumExceedsMaximum_ShouldThrowOutOfRange()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Count.Between(3, 2));
    }

    [Fact]
    public void GivenRangeConstraint_WhenMinimumIsNegative_ShouldThrowOutOfRange()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Count.Between(-1, 2));
    }

    [Fact]
    public void GivenRangeConstraint_WhenMaximumIsNegative_ShouldThrowOutOfRange()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Count.Between(0, -1));
    }

    [Fact]
    public void GivenExactConstraint_WhenActualMatches_ShouldReturnTrue()
    {
        var count = Count.Exactly(3);

        count.Matches(3).ShouldBeTrue();
    }

    [Fact]
    public void GivenExactConstraint_WhenActualDoesNotMatch_ShouldReturnFalse()
    {
        var count = Count.Exactly(3);

        count.Matches(2).ShouldBeFalse();
    }

    [Fact]
    public void GivenAtLeastConstraint_WhenActualBelowMinimum_ShouldReturnFalse()
    {
        var count = Count.AtLeast(2);

        count.Matches(1).ShouldBeFalse();
    }

    [Fact]
    public void GivenAtLeastConstraint_WhenActualMeetsMinimum_ShouldReturnTrue()
    {
        var count = Count.AtLeast(2);

        count.Matches(2).ShouldBeTrue();
        count.Matches(5).ShouldBeTrue();
    }

    [Fact]
    public void GivenAtMostConstraint_WhenActualExceedsMaximum_ShouldReturnFalse()
    {
        var count = Count.AtMost(2);

        count.Matches(3).ShouldBeFalse();
    }

    [Fact]
    public void GivenAtMostConstraint_WhenActualWithinMaximum_ShouldReturnTrue()
    {
        var count = Count.AtMost(2);

        count.Matches(1).ShouldBeTrue();
        count.Matches(2).ShouldBeTrue();
    }

    [Fact]
    public void GivenAnyConstraint_WhenMatching_ShouldAllowAnyCount()
    {
        Count.Any.Matches(0).ShouldBeTrue();
        Count.Any.Matches(10).ShouldBeTrue();
    }

    [Fact]
    public void GivenAtLeastOnceAlias_WhenEvaluated_ShouldMatchAtLeastOne()
    {
        Count.AtLeastOnce().ShouldBe(Count.AtLeast(1));
    }

    [Fact]
    public void GivenAtMostOnceAlias_WhenEvaluated_ShouldMatchAtMostOne()
    {
        Count.AtMostOnce().ShouldBe(Count.AtMost(1));
    }

    [Fact]
    public void GivenNeverAlias_WhenEvaluated_ShouldMatchExactlyZero()
    {
        var count = Count.Never();

        count.Matches(0).ShouldBeTrue();
        count.Matches(1).ShouldBeFalse();
        count.ToString().ShouldBe("exactly 0 time(s)");
    }

    [Fact]
    public void GivenOnceAlias_WhenEvaluated_ShouldMatchExactlyOne()
    {
        var count = Count.Once();

        count.Matches(1).ShouldBeTrue();
        count.Matches(0).ShouldBeFalse();
    }

    [Fact]
    public void GivenAnyConstraint_WhenFormatted_ShouldDescribeAnyCount()
    {
        Count.Any.ToString().ShouldBe("any number of times");
    }

    [Fact]
    public void GivenTwiceAlias_WhenEvaluated_ShouldMatchExactlyTwo()
    {
        Count.Twice().ShouldBe(Count.Exactly(2));
    }

    [Fact]
    public void GivenTimesAlias_WhenEvaluated_ShouldDelegateToExactly()
    {
        Count.Times(3).ShouldBe(Count.Exactly(3));

        Should.Throw<ArgumentOutOfRangeException>(() => Count.Times(-1));
    }

    [Fact]
    public void GivenExactConstraint_WhenNegative_ShouldThrowOutOfRange()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Count.Exactly(-1));
    }

    [Fact]
    public void GivenAtLeastConstraint_WhenNegative_ShouldThrowOutOfRange()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Count.AtLeast(-1));
    }

    [Fact]
    public void GivenAtMostConstraint_WhenNegative_ShouldThrowOutOfRange()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Count.AtMost(-1));
    }

    [Fact]
    public void GivenExactConstraint_WhenFormatted_ShouldDescribeExactCount()
    {
        Count.Exactly(5).ToString().ShouldBe("exactly 5 time(s)");
    }

    [Fact]
    public void GivenAtLeastConstraint_WhenFormatted_ShouldDescribeMinimum()
    {
        Count.AtLeast(2).ToString().ShouldBe("at least 2 time(s)");
    }

    [Fact]
    public void GivenAtMostConstraint_WhenFormatted_ShouldDescribeMaximum()
    {
        Count.AtMost(2).ToString().ShouldBe("at most 2 time(s)");
    }

    [Fact]
    public void GivenEquivalentCounts_WhenCompared_ShouldReportEquality()
    {
        var left = Count.Between(1, 3);
        var right = Count.Between(1, 3);
        var different = Count.AtLeast(1);

        left.ShouldBe(right);
        (left == right).ShouldBeTrue();
        (left != different).ShouldBeTrue();
        left.Equals(different).ShouldBeFalse();
        left.GetHashCode().ShouldBe(right.GetHashCode());
    }

    [Fact]
    public void GivenExactCounts_WhenComparingHashCodes_ShouldMatch()
    {
        Count.Exactly(2).GetHashCode().ShouldBe(Count.Exactly(2).GetHashCode());
    }

    [Fact]
    public void GivenSameInstance_WhenComparing_ShouldReturnTrue()
    {
        var count = Count.AtLeast(1);

        count.Equals(count).ShouldBeTrue();
    }

    [Fact]
    public void GivenNullOther_WhenComparing_ShouldReturnFalse()
    {
        Count.AtMost(1).Equals((Count?)null).ShouldBeFalse();
    }

    [Fact]
    public void GivenDifferentType_WhenComparing_ShouldReturnFalse()
    {
        Count.AtLeast(1).Equals(new object()).ShouldBeFalse();
    }

    [Fact]
    public void GivenNullOperands_WhenUsingEqualityOperators_ShouldBehaveAsExpected()
    {
        Count? left = null;
        Count? right = null;
        Count? something = Count.Any;

        (left == right).ShouldBeTrue();
        (left != right).ShouldBeFalse();
        (left == something).ShouldBeFalse();
        (left != something).ShouldBeTrue();
    }
}
