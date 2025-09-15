namespace Imposter.Ideation;

public static class CalculatorImposterExtensions
{
    public static CalculatorImposter.CalculatorImposterInstance ImposterInstance(this ICalculatorImposter calculatorImposter)
        => calculatorImposter.ImposterInstance;

    public static CalculatorImposterVerifier Verify(this ICalculatorImposter calculatorImposter)
        => calculatorImposter.Verify();
}