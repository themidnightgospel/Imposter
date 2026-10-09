using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class OutSpanArgTests
{
    [Fact]
    public void GivenOutSpanArg_WhenAnyIsRequested_ShouldReturnAMatcher()
    {
        OutSpanArg<int>.Any().ShouldNotBeNull();
    }

    [Fact]
    public void GivenOutReadOnlySpanArg_WhenAnyIsRequested_ShouldReturnAMatcher()
    {
        OutReadOnlySpanArg<int>.Any().ShouldNotBeNull();
    }
}
