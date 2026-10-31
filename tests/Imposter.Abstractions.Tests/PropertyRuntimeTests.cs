using System;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class PropertyRuntimeTests
{
    private readonly PropertyValue<int> _value = new();

    [Fact]
    public void GivenExplicitAccessors_WhenOnlyGetterCallbackConfigured_ShouldRejectAndNotRecord()
    {
        var getter = new Getter<int>(_value, ImposterMode.Explicit);
        var setter = new Setter<int>(_value, ImposterMode.Explicit);
        getter.OnRead(() => throw new InvalidOperationException());
        Should
            .Throw<MissingImposterException>(() => getter.Get())
            .Message.ShouldContain("Value (getter)");
        Should
            .Throw<MissingImposterException>(() => setter.Set(1))
            .Message.ShouldContain("Value (setter)");
        getter.Verify(Count.Never());
        setter.Called(Arg<int>.Any(), Count.Never());
        setter.Callback(Arg<int>.Is(1), _ => { });
        setter.Set(2); // A setup configures the setter even when its criteria do not match.
        setter.Called(Arg<int>.Is(2), Count.Once());
    }

    [Fact]
    public void GivenDefaultValue_WhenSetterRunsBeforeFirstRead_ShouldNotSeedFromBase()
    {
        var getter = new Getter<int>(_value);
        var setter = new Setter<int>(_value);
        setter.Set(42);
        getter.Get(() => throw new InvalidOperationException()).ShouldBe(42);
        setter.Called(Arg<int>.Is(42), Count.Once());
        getter.Verify(Count.Once());
    }

    [Fact]
    public void GivenBaseGetter_WhenReadingDefaultThenConfiguringSequence_ShouldSeedOnceAndRepeatLastOutcome()
    {
        var getter = new Getter<int>(_value);
        var baseCalls = 0;
        int ReadBase() => ++baseCalls;
        getter.Get(ReadBase).ShouldBe(1);
        getter.Get(ReadBase).ShouldBe(1);
        getter.Configure(10);
        getter.EnableBaseImplementation();
        getter.Configure(20);
        getter.Get(ReadBase).ShouldBe(10);
        getter.Get(ReadBase).ShouldBe(2);
        getter.Get(ReadBase).ShouldBe(20);
        getter.Get(ReadBase).ShouldBe(20);
        getter.Configure(30);
        getter.Get(ReadBase).ShouldBe(30);
        baseCalls.ShouldBe(2);
        getter.Verify(Count.Exactly(7));
    }

    [Fact]
    public void GivenBaseGetterReadingTheProperty_WhenReadingFirst_ShouldReturnDefaultToTheNestedRead()
    {
        var getter = new Getter<int>(_value);
        int ReadAgain() => getter.Get(ReadAgain) + 1;

        getter.Get(ReadAgain).ShouldBe(1);
        getter.Get(ReadAgain).ShouldBe(1);
    }

    [Fact]
    public void GivenThrowingBaseGetter_WhenReadingTwice_ShouldCallItOnlyOnce()
    {
        var getter = new Getter<int>(_value);
        var baseCalls = 0;
        int ThrowFromBase()
        {
            baseCalls++;
            throw new InvalidOperationException();
        }

        Should.Throw<InvalidOperationException>(() => getter.Get(ThrowFromBase));
        getter.Get(ThrowFromBase).ShouldBe(0);
        baseCalls.ShouldBe(1);
    }

    [Fact]
    public void GivenThrowingCallback_WhenReading_ShouldRecordButNotConsumeOutcome()
    {
        var getter = new Getter<int>(_value);
        var fail = true;
        getter.Configure(1);
        getter.Configure(2);
        getter.OnRead(() =>
        {
            if (fail)
                throw new InvalidOperationException();
        });
        Should.Throw<InvalidOperationException>(() => getter.Get());
        fail = false;
        getter.Get().ShouldBe(1);
        getter.Get().ShouldBe(2);
        getter.Verify(Count.Exactly(3));
    }

    [Fact]
    public void GivenBaseInitSetter_WhenAssigning_ShouldRunCallbacksAndRecordBeforeRequestingBaseAssignment()
    {
        var setter = new Setter<int>(_value);
        var callbackValue = 0;
        setter.Callback(Arg<int>.Any(), value => callbackValue = value);
        setter.UseBaseImplementation();
        setter.AssignInit(3).ShouldBeTrue();
        callbackValue.ShouldBe(3);
        setter.Called(Arg<int>.Is(3), Count.Once());
        new Getter<int>(_value).Get().ShouldBe(0);
    }

    [Fact]
    public void GivenThrowingSetterCallback_WhenAssigning_ShouldRecordWithoutUpdatingValue()
    {
        var setter = new Setter<int>(_value);
        setter.Callback(Arg<int>.Any(), _ => throw new InvalidOperationException());
        Should.Throw<InvalidOperationException>(() => setter.Set(7));
        setter.Called(Arg<int>.Is(7), Count.Once());
        new Getter<int>(_value).Get().ShouldBe(0);
    }

    [Fact]
    public void GivenSetterHistory_WhenVerificationFails_ShouldPreserveOrderAndNullFormatting()
    {
        var setter = new Setter<string?>(null);
        setter.Set("first");
        setter.Set(null);
        Should
            .Throw<VerificationFailedException>(() =>
                setter.Called(Arg<string?>.Any(), Count.Never())
            )
            .PerformedInvocations.ShouldBe(
                "set Value = <null>" + Environment.NewLine + "set Value = <first>"
            );
    }

    [Fact]
    public void GivenFirstSetupRacingWithReads_WhenPublishingOutcome_ShouldReturnDefaultOrConfiguredValue()
    {
        // Publication may overlap an implicit read; once setup completes every later read must see its outcome.
        // Cover both a value-type instantiation and the shared reference-type implementation.
        AssertFirstSetupPublication(42);
        AssertFirstSetupPublication("configured");
    }

    private static void AssertFirstSetupPublication<T>(T value)
    {
        const int calls = 64;
        for (var round = 0; round < 32; round++)
        {
            var getter = new Getter<T>(new PropertyValue<T>());
            var results = new T[calls];
            using var start = new Barrier(2);
            Parallel.Invoke(
                () =>
                {
                    start.SignalAndWait(TimeSpan.FromSeconds(10)).ShouldBeTrue();
                    getter.Configure(value);
                },
                () =>
                {
                    start.SignalAndWait(TimeSpan.FromSeconds(10)).ShouldBeTrue();
                    for (var call = 0; call < calls; call++)
                        results[call] = getter.Get();
                }
            );
            foreach (var result in results)
                result.ShouldBeOneOf(default(T), value);
            getter.Get().ShouldBe(value);
            getter.Verify(Count.Exactly(calls + 1));
        }
    }

    private sealed class Getter<T>(
        PropertyValue<T> value,
        ImposterMode mode = ImposterMode.Implicit
    ) : PropertyGetter<T>(value, mode, "Value")
    {
        internal void Configure(T value) => Returns(value);

        internal void OnRead(Action callback) => Callback(callback);

        internal void Verify(Count count) => Called(count);
    }

    private sealed class Setter<T>(
        PropertyValue<T>? value,
        ImposterMode mode = ImposterMode.Implicit
    ) : PropertySetter<T>(value, mode, "Value")
    {
        internal bool AssignInit(T value) => SetForInit(value);
    }
}
