namespace Imposter.Ideation;

public interface ICalculator
{
    // TODO what if it has default impl
    int Add(int left, int right);

    int Multiply(int left, int right);

    int Age { get; set; }
}