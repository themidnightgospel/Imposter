using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class ArgAllocationTests
{
    [Fact]
    public void GivenIsValue_WhenCreated_ShouldOnlyAllocateTheMatcher()
    {
        AllocatedBytesPerCall(() => Arg<int>.Is(5)).ShouldBeLessThanOrEqualTo(40);
        AllocatedBytesPerCall(() => Arg<string>.Is("value")).ShouldBeLessThanOrEqualTo(48);
    }

    [Fact]
    public void GivenIsNotValue_WhenCreated_ShouldOnlyAllocateTheMatcher()
    {
        AllocatedBytesPerCall(() => Arg<int>.IsNot(5)).ShouldBeLessThanOrEqualTo(40);
        AllocatedBytesPerCall(() => Arg<string>.IsNot("value")).ShouldBeLessThanOrEqualTo(48);
    }

    [Fact]
    public void GivenImplicitConversion_WhenCreated_ShouldOnlyAllocateTheMatcher()
    {
        AllocatedBytesPerCall<int>(() => 5).ShouldBeLessThanOrEqualTo(40);
        AllocatedBytesPerCall<string>(() => "value").ShouldBeLessThanOrEqualTo(48);
    }

    [Fact]
    public void GivenCustomComparer_WhenCreated_ShouldOnlyAllocateTheMatcher()
    {
        var comparer = StringComparer.OrdinalIgnoreCase;
        AllocatedBytesPerCall(() => Arg<string>.Is("value", comparer))
            .ShouldBeLessThanOrEqualTo(48);
        AllocatedBytesPerCall(() => Arg<string>.IsNot("value", comparer))
            .ShouldBeLessThanOrEqualTo(48);
    }

    [Fact]
    public void GivenExistingPredicate_WhenNegated_ShouldOnlyAllocateTheMatcher()
    {
        Func<int, bool> predicate = value => value > 0;
        AllocatedBytesPerCall(() => Arg<int>.IsNot(predicate)).ShouldBeLessThanOrEqualTo(40);
    }

    [Fact]
    public void GivenSetMembership_WhenNegated_ShouldNotAllocateAnExtraClosure()
    {
        var values = new[] { 1, 2, 3 };
        AllocatedBytesPerCall(() => Arg<int>.IsNotIn(values))
            .ShouldBe(AllocatedBytesPerCall(() => Arg<int>.IsIn(values)));
    }

    [Fact]
    public void GivenAny_WhenRequestedRepeatedly_ShouldNotAllocate()
    {
        AllocatedBytesPerCall(() => Arg<int>.Any()).ShouldBe(0);
    }

    [Fact]
    public void GivenEqualityMatchers_WhenMatching_ShouldNotAllocate()
    {
        var equal = Arg<int>.Is(5);
        var different = Arg<int>.IsNot(5);
        var text = Arg<string>.Is("value");
        bool Match() => equal.Matches(5) && different.Matches(6) && text.Matches("value");
        for (var i = 0; i < 100; i++)
            Match();
        var before = GC.GetAllocatedBytesForCurrentThread();
        var matches = 0;
        for (var i = 0; i < 100; i++)
            if (Match())
                matches++;
        var allocated = GC.GetAllocatedBytesForCurrentThread() - before;
        matches.ShouldBe(100);
        allocated.ShouldBe(0);
    }

    // Warm static initialization and delegates before measuring. On a 64-bit runtime an int matcher
    // is 40 bytes; a reference-type matcher is 48 bytes. The caller supplies any predicate/comparer.
    private static long AllocatedBytesPerCall<T>(Func<Arg<T>> create)
    {
        const int calls = 100;
        for (var i = 0; i < calls; i++)
            GC.KeepAlive(create());
        var before = GC.GetAllocatedBytesForCurrentThread();
        for (var i = 0; i < calls; i++)
            GC.KeepAlive(create());
        return (GC.GetAllocatedBytesForCurrentThread() - before) / calls;
    }
}
