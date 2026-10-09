using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class SpanElementsComparerTests
{
    private static readonly SpanElementsComparer<string?> Comparer =
        SpanElementsComparer<string?>.Default;

    [Fact]
    public void GivenArraysWithEqualElements_WhenCompared_ShouldBeEqual()
    {
        Comparer.Equals(["a", null], ["a", null]).ShouldBeTrue();
    }

    [Fact]
    public void GivenArraysWithElementsInAnotherOrder_WhenCompared_ShouldNotBeEqual()
    {
        Comparer.Equals(["a", "b"], ["b", "a"]).ShouldBeFalse();
    }

    [Fact]
    public void GivenArraysOfDifferentLengths_WhenCompared_ShouldNotBeEqual()
    {
        Comparer.Equals(["a"], ["a", "a"]).ShouldBeFalse();
    }

    [Fact]
    public void GivenTwoNullArrays_WhenCompared_ShouldBeEqual()
    {
        Comparer.Equals(null, null).ShouldBeTrue();
    }

    [Fact]
    public void GivenNullAndEmptyArrays_WhenCompared_ShouldNotBeEqual()
    {
        Comparer.Equals(null, []).ShouldBeFalse();
    }

    [Fact]
    public void GivenNullArray_WhenHashed_ShouldBeZero()
    {
        Comparer.GetHashCode(null!).ShouldBe(0);
    }

    [Fact]
    public void GivenArraysWithEqualElements_WhenHashed_ShouldHaveEqualHashCodes()
    {
        Comparer.GetHashCode(["a", null]).ShouldBe(Comparer.GetHashCode(["a", null]));
    }
}
