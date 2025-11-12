# Throwing Exceptions

Arrange methods to throw exceptions instead of returning values. This is useful for testing exception paths and retry logic.

## Ways to throw

- Generic type:
  ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_ThrowingCodeSnippetsTests.cs#L26"}
  imposter.GetNumber().Throws<InvalidOperationException>();
  ```
- Specific instance:
  ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_ThrowingCodeSnippetsTests.cs#L30"}
  imposter.GetNumber().Throws(new Exception("boom"));
  ```
- Factory delegate:
  ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_ThrowingCodeSnippetsTests.cs#L35"}
  imposter.GetNumber().Throws(() => new Exception("deferred"));
  ```

## Sequencing with returns

Mix `Throws` with `Returns` using `Then()`:

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_ThrowingCodeSnippetsTests.cs#L46"}
imposter.GetNumber()
    .Returns(1)
    .Then().Throws<InvalidOperationException>()
    .Then().Returns(2);
```

## Async methods

For Task/Task<T> or ValueTask/ValueTask<T>, `Throws` raises the exception when the method is invoked, just like synchronous methods. Use async-aware assertions in tests when appropriate.

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_ThrowingCodeSnippetsTests.cs#L62"}
imposter.GetNumberAsync().Throws<TimeoutException>();
await Assert.ThrowsAsync<TimeoutException>(() => service.GetNumberAsync());
```
See more examples on [GitHub](https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Features/MethodImposter/ThrowTests.cs).
## Interactions with Explicit mode

In `ImposterMode.Explicit`, a missing setup already throws `MissingImposterException`. Prefer arranging `Throws` when you need a specific exception type or message rather than a generic missing-setup exception.

## Troubleshooting

- Repeating `Throws` without `Then()` is invalid; separate distinct throwing steps with `Then()`.
- When combining callbacks, callbacks execute after the exception is produced.


