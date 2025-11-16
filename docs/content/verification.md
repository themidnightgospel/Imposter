# Verification

How `Count` works when verifying calls.

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Verification/VerificationTests.cs#L6"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.IVerifyService))]

    public interface IVerifyService
    {
        void Increment(int v);
        int Combine(int a, int b);
    }
    ```

## Count basics

`Count` describes how many matching invocations are expected when you call `.Called(Count ...)`.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/Verification/VerificationTests.cs#L52"}
    var imposter = new IVerifyServiceImposter();
    var service = imposter.Instance();

    service.Increment(1);
    service.Increment(2);
    service.Increment(2);

    imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(3));
    imposter.Increment(2).Called(Count.Exactly(2));
    imposter.Increment(1).Called(Count.Once());
    imposter.Increment(999).Called(Count.Never());
    imposter.Increment(Arg<int>.Any()).Called(Count.Any);
    ```

## Available counts

- `Count.Exactly(n)` — require exactly `n` matching calls.
- `Count.AtLeast(n)` — require `>= n` matching calls.
- `Count.AtMost(n)` — require `<= n` matching calls.
- `Count.Once()` — shorthand for `Count.Exactly(1)`.
- `Count.Never()` — shorthand for `Count.Exactly(0)`.
- `Count.Any` — no constraint; always succeeds.

Counts always apply to *matching* invocations only (based on your `Arg<T>` / `OutArg<T>` matchers).

## Where to use Count

- Methods:
  - `imposter.Method(...).Called(Count.Exactly(n));`
- Properties:
  - `imposter.Property.Getter().Called(Count.AtLeast(1));`
  - `imposter.Property.Setter(Arg<T>.Any()).Called(Count.Once());`
- Indexers:
  - `imposter[Arg<int>.Is(i => i > 0)].Getter().Called(Count.Exactly(1));`
- Events:
  - `imposter.Event.Subscribed(Arg<Delegate>.Is(handler), Count.Once());`
  - `imposter.Event.HandlerInvoked(Arg<Delegate>.Is(handler), Count.AtLeast(1));`

See also:

- [Methods ▸ Verification](methods/verification.md) — method-focused examples.
- [Key API Reference](key-api-reference.md#verification-counts) — quick signature list.

