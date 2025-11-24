# Sequential Returns

Imposter supports sequencing multiple outcomes for the same method using `Then()`. This is useful for modeling stateful behavior across repeated calls.

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/SequentialReturns/SequentialReturnsTests.cs#L7"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.ISeqService))]

    public interface ISeqService
    {
        int GetNumber();
        System.Threading.Tasks.Task<int> GetNumberAsync();
    }
    ```

## Basic sequencing

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/SequentialReturns/SequentialReturnsTests.cs#L26"}
    imposter.GetNumber()
        .Returns(1)
        .Then().Returns(2)
        .Then().Returns(3);

    service.GetNumber(); // 1
    service.GetNumber(); // 2
    service.GetNumber(); // 3
    ```

## Interleaving exceptions

You can interleave `Throws` within a sequence. Separate each step with `Then()`.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/SequentialReturns/SequentialReturnsTests.cs#L71"}
    imposter.GetNumber()
        .Returns(1)
        .Then().Throws<InvalidOperationException>()
        .Then().Returns(2);

    service.GetNumber(); // 1
    service.GetNumber(); // throws InvalidOperationException
    service.GetNumber(); // 2
    ```

!!! tip "Pro tip"

    - Always use `Then()` between distinct outcomes.
    - Repeating `Returns` (or `Throws`) without `Then()` is invalid.
    - You can mix `Returns`, `ReturnsAsync`, `Throws`, and `UseBaseImplementation()` across steps for class targets.

## Concurrency note

Under concurrent calls, outcomes are consumed in order. When a sequence is exhausted, behavior falls back to the last applicable outcome or to default behavior (depending on the target and mode).
