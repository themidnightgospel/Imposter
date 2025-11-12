# Sequential Returns

Imposter supports sequencing multiple outcomes for the same method using `Then()`. This is useful for modeling stateful behavior across repeated calls.

## Basic sequencing

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_SequentialReturnsCodeSnippetsTests.cs#L24"}
imposter.GetNumber()
    .Returns(1)
    .Then().Returns(2)
    .Then().Returns(3);

service.GetNumber(); // 1
service.GetNumber(); // 2
service.GetNumber(); // 3
```

## Mixing delegates

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_SequentialReturnsCodeSnippetsTests.cs#L40"}
imposter.GetNumber()
    .Returns(() => 1)
    .Then().Returns(() => 2);
```

## Async sequences

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_SequentialReturnsCodeSnippetsTests.cs#L54"}
imposter.GetNumberAsync()
    .ReturnsAsync(1)
    .Then().Returns(() => Task.FromResult(2));
```

## Interleaving exceptions

You can interleave `Throws` within a sequence. Separate each step with `Then()`.

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_SequentialReturnsCodeSnippetsTests.cs#L64"}

See more examples on [GitHub](https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs).
imposter.GetNumber()
    .Returns(1)
    .Then().Throws<InvalidOperationException>()
    .Then().Returns(2);
```

!!! tip "Pro tip"

    - Always use `Then()` between distinct outcomes.
    - Repeating `Returns` (or `Throws`) without `Then()` is invalid.
    - You can mix `Returns`, `ReturnsAsync`, `Throws`, and `UseBaseImplementation()` across steps for class targets.

## Concurrency note

Under concurrent calls, outcomes are consumed in order. When a sequence is exhausted, behavior falls back to the last applicable outcome or to default behavior (depending on the target and mode).


