# Failure messages

Describe a matcher when its meaning matters in a failing test. A reason on `Called(count, because)` explains the expectation. Null or empty reasons are omitted.

!!! note "Minimum C# version"
    These examples require C# 8 or later; C# 14 additionally supports the `Target.Imposter()` factory.

!!! example "Explain a failed expectation"
    === "C# 14"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/FailureMessages/FailureMessagesTests.cs#L70"}
        var imposter = IDiagnosticMessages.Imposter();
        var failure = Should.Throw<VerificationFailedException>(() =>
            imposter
                .Hello(Arg<string>.Any(), Arg<int>.Is(x => x > 10).Describe("greater than 10"))
                .Called(Count.Once(), "a greeting should be repeated")
        );
        failure.Expected.ShouldContain("greater than 10");
        failure.Message.ShouldContain("a greeting should be repeated");
        ```

    === "C# 8-13"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/FailureMessages/FailureMessagesTests.cs#L72"}
        var imposter = new IDiagnosticMessagesImposter();
        var failure = Should.Throw<VerificationFailedException>(() =>
            imposter
                .Hello(Arg<string>.Any(), Arg<int>.Is(x => x > 10).Describe("greater than 10"))
                .Called(Count.Once(), "a greeting should be repeated")
        );
        failure.Expected.ShouldContain("greater than 10");
        failure.Message.ShouldContain("a greeting should be repeated");
        ```

!!! tip "Descriptions do not change matching"
    `Describe` returns a new matcher without mutating a shared `Any()` matcher. `SpanArg<T>` and `ReadOnlySpanArg<T>` also support descriptions. Describing a matcher does not evaluate its predicate or comparer.

Default descriptions include `any`, `<value>`, `<predicate>`, `not <description>` and membership lists such as `in [<1>, <2>]`. Span equality matchers display captured elements.

## Missing setup

`MissingImposterException.ConfiguredSetups` lists original configured outcomes, including partly or fully consumed sequences. Bare verification handles are not setups.

!!! example "Actual arguments beside configured outcomes"
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/FailureMessages/FailureMessagesTests.cs#L14"}
    var sut = new IGreeterImposter(ImposterMode.Explicit);
    sut.Hello("Ann", Arg<int>.Any()).Throws(new InvalidOperationException());
    sut.Hello(
            Arg<string>.Any(),
            Arg<int>.Is(value => value > 10).Describe("greater than 10")
        )
        .Returns("first")
        .Then()
        .Throws<InvalidOperationException>();
    var failure = Should.Throw<MissingImposterException>(() =>
        sut.Instance().Hello("Bob", 2)
    );
    ```

    ```text
    No setup matched Hello(name: <Bob>, times: <2>).
    Configured setups for Hello:
      Hello(name: any, times: greater than 10) -> Returns, Throws<InvalidOperationException>
      Hello(name: <Ann>, times: any) -> Throws
    ```

## Failed count

`VerificationFailedException.Expected` gives the member and matcher expectation. `PerformedInvocations` retains the relevant history. When this member has no calls, other recorded interactions appear in entry order; when it has calls with different arguments, the message stays scoped to its own history.

!!! example "An expectation with no calls to its member"
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/FailureMessages/FailureMessagesTests.cs#L43"}
    var sut = new IGreeterImposter();
    sut.Instance().Goodbye("Ann");
    var failure = Should.Throw<VerificationFailedException>(() =>
        sut.Hello(
                Arg<string>.Any(),
                Arg<int>.Is(value => value > 10).Describe("greater than 10")
            )
            .Called(Count.Once())
    );
    ```

    ```text
    Expected Hello(name: any, times: greater than 10) exactly 1 time(s), performed 0 times.
    Performed invocations of Hello: none.
    Other interactions on IGreeterImposter:
      Goodbye(name: <Ann>)
    ```

Event verification failures list all recorded entries for the member/action, including nonmatching entries, while the actual count stays matcher-filtered. Formatting does not re-evaluate the predicate.

Text is built on failure. Formatting does not run configured factories, callbacks or matcher predicates. Original outcome descriptions retain outcome kinds and exception names without capturing delegates or returned values. User `ToString()` failures produce `<unprintable TypeName>` while preserving the intended verification/missing-setup exception.

!!! warning "Exact message assertions change"
    Generated verifiers use the new member/matcher message format. Legacy exception constructors remain available with their original count text. See [Release notes](release-notes.md) for all changed message details.

!!! info "Next steps"
    Read [Verification](verification.md), [Verification completeness](verification-completeness.md), or [Ordered verification](ordered-verification.md).
