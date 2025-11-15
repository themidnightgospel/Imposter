using Imposter.Abstractions;
using Imposter.CSharp14.Samples;
using Xunit;

[assembly: GenerateImposter(typeof(ICalculator))]

namespace Imposter.CSharp14.Samples;

/// <summary>
/// Minimal sample showing how to impersonate a method with the C# 14 extensions.
/// </summary>
public static class Sample
{
    [Fact]
    public static int ConfigureCalculatorImposter()
    {
        var imposter = new ICalculatorImposter();

        imposter.Add(Arg<int>.Any(), Arg<int>.Any()).Returns(42);

        return imposter.Instance().Add(20, 22);
    }
}

public interface ICalculator
{
    int Add(int left, int right);
}
