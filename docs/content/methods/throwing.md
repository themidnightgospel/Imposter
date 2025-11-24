# Throwing Exceptions

Arrange methods to throw exceptions instead of returning values. This is useful for testing exception paths and retry logic.

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Throw/ThrowingTests.cs#L7"}
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
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Throw/ThrowingTests.cs#L26"}
    imposter.GetNumber().Throws<InvalidOperationException>();

    service.GetNumber(); // throws InvalidOperationException
    ```
- Specific instance:
!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Throw/ThrowingTests.cs#L32"}
    imposter.GetNumber().Throws(new Exception("boom"));

    service.GetNumber(); // throws Exception("boom")
    ```
- Factory delegate:
!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Throw/ThrowingTests.cs#L40"}
    imposter.GetNumber().Throws(() => new Exception("deferred"));

    service.GetNumber(); // throws Exception("deferred")
    ```

## Sequencing with returns

Mix `Throws` with `Returns` using `Then()`:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Throw/ThrowingTests.cs#L52"}
    imposter.GetNumber()
        .Returns(1)
        .Then().Throws<InvalidOperationException>()
        .Then().Returns(2);

    service.GetNumber(); // 1
    service.GetNumber(); // throws InvalidOperationException
    service.GetNumber(); // 2
    ```

## Async methods

For Task/Task<T> or ValueTask/ValueTask<T>, `Throws` raises the exception when the method is invoked, just like synchronous methods. Use async-aware assertions in tests when appropriate.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Throw/ThrowingTests.cs#L62"}
    imposter.GetNumberAsync().Throws<TimeoutException>();
    await Assert.ThrowsAsync<TimeoutException>(() => service.GetNumberAsync());
    ```
See more examples on [GitHub](https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImpersonation/ThrowTests.cs).

!!! note
    - Repeating `Throws` without `Then()` is invalid; separate distinct throwing steps with `Then()`.
