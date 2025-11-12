# Verification

Verify that methods were invoked the expected number of times with specific arguments. Use `Called(Count.*)` on the method builder.

## Basic counts

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_VerificationCodeSnippetsTests.cs#L23"}
service.Increment(1);
service.Increment(2);

imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(2));
imposter.Increment(2).Called(Count.Once());
```

## Count helpers

- `Count.Exactly(n)` — exactly n times
- `Count.AtLeast(n)` — n or more times
- `Count.AtMost(n)` — n or fewer times
- `Count.Once()` — exactly once
- `Count.Never()` — zero times
- `Count.Any` — any number of times

## Matching arguments

Verification respects the same argument matching rules used for arrangements:

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods_VerificationCodeSnippetsTests.cs#L40"}

See more examples on [GitHub](https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Features/MethodImposter/InvocationVerificationTests.cs).
imposter.Increment(Arg<int>.Is(x => x > 10)).Called(Count.Exactly(3));
imposter.Combine(Arg<int>.Is(x => x > 0), Arg<int>.Is(y => y < 10)).Called(Count.Once());
```

## Failures

When verification fails, `VerificationFailedException` is thrown with a descriptive message including expected vs actual counts.

## Tips

- Prefer verifying behavior at the boundary of your SUT rather than implementation details.
- Verification can be chained after prior setups via `Then()`, but is typically run after exercising your SUT.


