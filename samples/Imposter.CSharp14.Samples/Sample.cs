using Imposter.Abstractions;
using Imposter.CSharp14.Samples;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(ICalculator))]

namespace Imposter.CSharp14.Samples;

/// <summary>
/// Minimal sample showing how to impersonate a method with the C# 14 extensions.
/// </summary>
public static class Sample
{
    [Fact]
    public static void ConfigureCalculatorImposter()
    {
        var imposter = ICalculator.Imposter();

        imposter.Add(Arg<int>.Any(), Arg<int>.Any()).Returns(42);

        imposter.Instance().Add(1, 2).ShouldBe(42);
    }
}

public interface ICalculator
{
    int Add(int left, int right);
}
