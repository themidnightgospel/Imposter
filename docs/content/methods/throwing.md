# Throwing Exceptions

Arrange methods to throw exceptions instead of returning values. This is useful for testing error paths and retry logic.

## Ways to throw

- Generic type:
  ```csharp
  imposter.GetNumber().Throws<InvalidOperationException>();
  ```
- Specific instance:
  ```csharp
  imposter.GetNumber().Throws(new Exception("boom"));
  ```
- Factory delegate:
  ```csharp
  imposter.GetNumber().Throws(() => new Exception("deferred"));
  ```

## Sequencing with returns

Mix `Throws` with `Returns` using `Then()`:

```csharp
imposter.GetNumber()
    .Returns(1)
    .Then().Throws<InvalidOperationException>()
    .Then().Returns(2);
```

## Async methods

For Task/Task<T> or ValueTask/ValueTask<T>, `Throws` raises the exception when the method is invoked, just like synchronous methods. Use async-aware assertions in tests when appropriate.

```csharp
imposter.GetNumberAsync().Throws<TimeoutException>();
await Assert.ThrowsAsync<TimeoutException>(() => service.GetNumberAsync());
```

## Interactions with Explicit mode

In `ImposterInvocationBehavior.Explicit`, a missing setup already throws `MissingImposterException`. Prefer arranging `Throws` when you need a specific exception type or message rather than a generic missing-setup error.

## Troubleshooting

- Repeating `Throws` without `Then()` is invalid; separate distinct throwing steps with `Then()`.
- When combining callbacks, callbacks execute after the exception is produced.


