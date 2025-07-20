namespace Imposter;

public interface ICalculator
{
    // TODO what if it has default impl
    int Add(int left, int right);

    int Age { get; set; }
}