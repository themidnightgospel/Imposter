# Throwing Exceptions

Arrange methods to throw exceptions instead of returning values. This is useful for testing exception paths and retry logic.

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/ThrowingTests.cs#L7"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.IThrowService))]

    public interface IThrowService
    {
        int GetNumber();
        System.Threading.Tasks.Task<int> GetNumberAsync();
    }
    ```

## Ways to throw

- Generic type:
!!! example
      ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/ThrowingTests.cs#L26"}
      imposter.GetNumber().Throws<InvalidOperationException>();
      ```
- Specific instance:
!!! example
      ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/ThrowingTests.cs#L30"}
      imposter.GetNumber().Throws(new Exception("boom"));
      ```
- Factory delegate:
!!! example
      ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/ThrowingTests.cs#L35"}
      imposter.GetNumber().Throws(() => new Exception("deferred"));
      ```

## Sequencing with returns

Mix `Throws` with `Returns` using `Then()`:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/ThrowingTests.cs#L48"}
    imposter.GetNumber()
        .Returns(1)
        .Then().Throws<InvalidOperationException>()
        .Then().Returns(2);
    ```

## Async methods

For Task/Task<T> or ValueTask/ValueTask<T>, `Throws` raises the exception when the method is invoked, just like synchronous methods. Use async-aware assertions in tests when appropriate.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Methods/ThrowingTests.cs#L62"}
    imposter.GetNumberAsync().Throws<TimeoutException>();
    await Assert.ThrowsAsync<TimeoutException>(() => service.GetNumberAsync());
    ```
See more examples on [GitHub](https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ThrowTests.cs).
## Interactions with Explicit mode

In `ImposterMode.Explicit`, a missing setup already throws `MissingImposterException`. Prefer arranging `Throws` when you need a specific exception type or message rather than a generic missing-setup exception.

## Troubleshooting

- Repeating `Throws` without `Then()` is invalid; separate distinct throwing steps with `Then()`.
- When combining callbacks, callbacks execute after the exception is produced.


