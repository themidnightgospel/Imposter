using System;
using System.Linq;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.IInvocationHistoryTarget))]
[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.ISpanHistoryTarget))]

namespace Imposter.Tests.Features.Verification
{
    public interface IInvocationHistoryTarget
    {
        string Hello(string name, int times);
        void Outer();
        void Outer(int value);
        void Inner();
        T Echo<T>(T value);
        bool TryRead(ref int input, out string output);
        ReadOnlySpan<int> Transform(Span<int> values);
        void Read(in int value);
        Task<int> Fetch();
        string Label { get; set; }
        string this[int key] { get; set; }
        event Action Changed;
        void Accept<T>(T value);
        void Produce<T>(ref int reference, out T output);
    }

    public interface ISpanHistoryTarget
    {
        Span<int> this[Span<int> key] { get; set; }
    }

    public class InvocationHistoryTests
    {
        private readonly IInvocationHistoryTargetImposter _sut = new();

        [Fact]
        public void GivenThrowingGenericValueReturn_WhenRecorded_ShouldRetainDefaultResult()
        {
            var failure = new InvalidOperationException("failed");
            _sut.Echo<int>(Arg<int>.Any()).Throws(failure);
            Should
                .Throw<InvalidOperationException>(() => _sut.Instance().Echo(42))
                .ShouldBeSameAs(failure);
            var invocation = _sut.Echo<int>(Arg<int>.Any()).Invocations.ShouldHaveSingleItem();
            invocation.Value.ShouldBe(42);
            invocation.Result.ShouldBe(0);
            invocation.Exception.ShouldBeSameAs(failure);
        }

        [Fact]
        public void GivenExceptionAsReturnedValue_WhenInspectingHistory_ShouldNotTreatItAsThrown()
        {
            var value = new InvalidOperationException("a value");
            _sut.Echo<Exception>(Arg<Exception>.Any()).Returns(value);
            _sut.Instance().Echo<Exception>(value).ShouldBeSameAs(value);
            var invocation = _sut.Echo<Exception>(Arg<Exception>.Any()).Invocations[0];
            invocation.Result.ShouldBeSameAs(value);
            invocation.Exception.ShouldBeNull();
        }

        [Fact]
        public void GivenRecordedCalls_WhenInspectingTypedInvocations_ShouldFilterAndOrderSnapshots()
        {
            _sut.Hello(Arg<string>.Any(), Arg<int>.Any()).Returns((name, times) => name);
            var instance = _sut.Instance();
            instance.Hello("Ann", 1);
            instance.Hello("Bob", 2);

            var snapshot = _sut.Hello(Arg<string>.Any(), Arg<int>.Any()).Invocations;
            snapshot.Count.ShouldBe(2);
            snapshot[0].Name.ShouldBe("Ann");
            snapshot[0].Times.ShouldBe(1);
            snapshot[1].Result.ShouldBe("Bob");
            snapshot[0].Sequence.ShouldBeLessThan(snapshot[1].Sequence);
            _sut.Hello("Bob", Arg<int>.Any()).Invocations.Count.ShouldBe(1);
            instance.Hello("Later", 3);
            snapshot.Count.ShouldBe(2);
        }

        [Fact]
        public void GivenNestedCalls_WhenInspectingInvocations_ShouldOrderByEntry()
        {
            _sut.Outer().Callback(() => _sut.Instance().Inner());
            _sut.Instance().Outer();

            _sut.Invocations()
                .Select(invocation => invocation.Member)
                .ToArray()
                .ShouldBe(new[] { "Outer", "Inner" });
        }

        [Fact]
        public void GivenSeparateImposters_WhenCalled_ShouldShareSequenceOrder()
        {
            var other = new IInvocationHistoryTargetImposter();
            _sut.Instance().Outer();
            other.Instance().Inner();
            _sut.Outer()
                .Invocations.Single()
                .Sequence.ShouldBeLessThan(other.Inner().Invocations.Single().Sequence);
        }

        [Fact]
        public void GivenNestedMixedMembers_WhenReadingSnapshots_ShouldPreserveMemberAndAggregateEntryOrder()
        {
            var instance = _sut.Instance();
            Action handler = () => { };
            _sut.Label.Setter(Arg<string>.Any())
                .Callback(value =>
                {
                    instance[1] = value;
                    _ = instance[1];
                    instance.Changed += handler;
                    instance.Inner();
                    instance.Changed -= handler;
                });
            _sut.Outer().Callback(() => instance.Label = "value");
            instance.Outer();
            instance.Outer();

            var members = new[]
            {
                _sut.Outer().Invocations.Select(call => call.Sequence).ToArray(),
                _sut.Inner().Invocations.Select(call => call.Sequence).ToArray(),
                _sut
                    .Label.Setter(Arg<string>.Any())
                    .Invocations.Select(call => call.Sequence)
                    .ToArray(),
                _sut[1].Setter().Invocations.Select(call => call.Sequence).ToArray(),
                _sut[1].Getter().Invocations.Select(call => call.Sequence).ToArray(),
                _sut.Changed.Subscriptions.Select(call => call.Sequence).ToArray(),
                _sut.Changed.Unsubscriptions.Select(call => call.Sequence).ToArray(),
            };
            foreach (var member in members)
            {
                member.Length.ShouldBe(2);
                member[0].ShouldBeLessThan(member[1]);
            }
            var aggregate = _sut.Invocations();
            aggregate
                .Select(call => call.Sequence)
                .ShouldBe(members.SelectMany(member => member).OrderBy(sequence => sequence));
            aggregate[0].Member.ShouldBe("Outer");

            instance.Outer();
            aggregate.Count.ShouldBe(14);
        }

        [Fact]
        public void GivenGenericCalls_WhenInspectingTypedInvocations_ShouldFilterByTypeArguments()
        {
            _sut.Instance().Echo(10);
            _sut.Instance().Echo("text");
            _sut.Echo(Arg<int>.Any()).Invocations.Single().Value.ShouldBe(10);
            _sut.Echo(Arg<string>.Any()).Invocations.Single().Value.ShouldBe("text");
            _sut.Invocations().Count.ShouldBe(2);
        }

        [Fact]
        public void GivenRefAndOutParameters_WhenCalled_ShouldRecordEntryAndExitValues()
        {
            _sut.TryRead(Arg<int>.Any(), OutArg<string>.Any())
                .Returns(
                    (ref int input, out string output) =>
                    {
                        input = 9;
                        output = "read";
                        return true;
                    }
                );
            var input = 2;
            _sut.Instance().TryRead(ref input, out _);
            var invocation = _sut.TryRead(2, OutArg<string>.Any()).Invocations.Single();
            invocation.Input.ShouldBe(2);
            invocation.Output.ShouldBe("read");
            invocation.Result.ShouldBe(true);
        }

        [Fact]
        public void GivenExplicitUnmatchedCall_WhenItThrows_ShouldRecordTheException()
        {
            var imposter = new IInvocationHistoryTargetImposter(ImposterMode.Explicit);
            var exception = Should.Throw<MissingImposterException>(() =>
                imposter.Instance().Outer()
            );
            imposter.Outer().Invocations.Single().Exception.ShouldBeSameAs(exception);
        }

        [Fact]
        public void GivenSpanArgumentsAndResults_WhenOriginalArraysChange_ShouldKeepRecordedCopies()
        {
            var result = new[] { 3, 4 };
            _sut.Transform(SpanArg<int>.Any()).Returns(result);
            var values = new[] { 1, 2 };
            _sut.Instance().Transform(values);
            values[0] = 99;
            result[0] = 99;
            var invocation = _sut.Transform(SpanArg<int>.Any()).Invocations.Single();
            invocation.Values.ToArray().ShouldBe(new[] { 1, 2 });
            invocation.Result.ToArray().ShouldBe(new[] { 3, 4 });
        }

        [Fact]
        public void GivenAccessorAndEventInteractions_WhenInspectingHistory_ShouldIncludeOnlyParticipatingMembers()
        {
            var instance = _sut.Instance();
            Action handler = () => { };
            instance.Label = "label";
            _ = instance.Label;
            instance[7] = "value";
            instance[7].ShouldBe("value");
            instance.Changed += handler;
            instance.Changed -= handler;

            _sut.Label.Setter("label").Invocations.Single().Value.ShouldBe("label");
            var setter = _sut[7].Setter().Invocations.Single();
            setter.Key.ShouldBe(7);
            setter.Value.ShouldBe("value");
            var getter = _sut[7].Getter().Invocations.Single();
            getter.Key.ShouldBe(7);
            getter.Result.ShouldBe("value");
            var history = _sut.Invocations();
            history.Count.ShouldBe(5);
            history
                .Select(invocation => invocation.Sequence)
                .ShouldBe(
                    history.Select(invocation => invocation.Sequence).OrderBy(sequence => sequence)
                );
            var subscriptions = history.OfType<EventSubscriptionInvocation<Action>>().ToArray();
            subscriptions.Length.ShouldBe(2);
            subscriptions[0].Handler.ShouldBeSameAs(handler);
            subscriptions[0].IsSubscription.ShouldBeTrue();
            subscriptions[1].IsSubscription.ShouldBeFalse();
            _sut.Changed.Subscriptions.Single().Handler.ShouldBeSameAs(handler);
            _sut.Changed.Unsubscriptions.Single().Handler.ShouldBeSameAs(handler);
        }

        [Fact]
        public void GivenUnmatchedExplicitIndexers_WhenInvoked_ShouldRecordFailureAndKeys()
        {
            var imposter = new IInvocationHistoryTargetImposter(ImposterMode.Explicit);
            var getterException = Should.Throw<MissingImposterException>(() =>
                _ = imposter.Instance()[3]
            );
            var setterException = Should.Throw<MissingImposterException>(() =>
                imposter.Instance()[4] = "value"
            );
            imposter[3].Getter().Invocations.Single().Exception.ShouldBeSameAs(getterException);
            var setter = imposter[4].Setter().Invocations.Single();
            setter.Exception.ShouldBeSameAs(setterException);
            setter.Value.ShouldBe("value");
        }

        [Fact]
        public void GivenInParameterAndTaskResult_WhenCalled_ShouldCaptureValueAndTaskIdentity()
        {
            var source = new TaskCompletionSource<int>();
            _sut.Fetch().Returns(source.Task);
            var value = 5;
            _sut.Instance().Read(in value);
            value = 8;
            _sut.Instance().Read(in value);
            _sut.Read(8).Invocations.Single().Value.ShouldBe(8);
            _sut.Instance().Fetch().ShouldBeSameAs(source.Task);
            _sut.Read(5).Invocations.Single().Value.ShouldBe(5);
            _sut.Fetch().Invocations.Single().Result.ShouldBeSameAs(source.Task);
            source.SetResult(9);
        }

        [Fact]
        public void GivenConcurrentCalls_WhenReadingSnapshot_ShouldKeepEveryCompletedCallInEntryOrder()
        {
            Parallel.For(0, 100, value => _sut.Instance().Echo(value));
            var snapshot = _sut.Echo(Arg<int>.Any()).Invocations;
            snapshot.Count.ShouldBe(100);
            snapshot
                .Select(invocation => invocation.Value)
                .OrderBy(value => value)
                .ShouldBe(Enumerable.Range(0, 100));
            snapshot.Select(invocation => invocation.Sequence).Distinct().Count().ShouldBe(100);
            snapshot
                .Select(invocation => invocation.Sequence)
                .ShouldBe(
                    snapshot.Select(invocation => invocation.Sequence).OrderBy(sequence => sequence)
                );
        }

        [Fact]
        public void GivenSpanIndexerCallbackMutation_WhenInspectingHistory_ShouldKeepEntryKeysAndValue()
        {
            var imposter = new ISpanHistoryTargetImposter();
            imposter[SpanArg<int>.Any()]
                .Setter()
                .Callback(
                    (key, value) =>
                    {
                        key[0] = 99;
                        value[0] = 99;
                    }
                );
            imposter.Instance()[new[] { 1 }.AsSpan()] = new[] { 2 };
            var setter = imposter[SpanArg<int>.Is(1)].Setter().Invocations.Single();
            setter.Key.ToArray().ShouldBe(new[] { 1 });
            setter.Value.ToArray().ShouldBe(new[] { 2 });

            var result = new[] { 3 };
            imposter[SpanArg<int>.Any()].Getter().Returns(result);
            _ = imposter.Instance()[new[] { 1 }.AsSpan()];
            result[0] = 99;
            imposter[SpanArg<int>.Is(1)]
                .Getter()
                .Invocations.Single()
                .Result.ToArray()
                .ShouldBe(new[] { 3 });
        }

        [Fact]
        public void GivenCompatibleGenericSetup_WhenCalledWithAnotherType_ShouldRecordTheCallerType()
        {
            object? observed = null;
            _sut.Accept<object>(Arg<object>.Any()).Callback(value => observed = value);
            _sut.Instance().Accept<string>("value");
            observed.ShouldBe("value");
            _sut.Accept<string>(Arg<string>.Any()).Invocations.Single().Value.ShouldBe("value");
            _sut.Accept<string>(Arg<string>.Any()).Called(Count.Once());
            _sut.Accept<object>(Arg<object>.Any()).Invocations.ShouldBeEmpty();
            _sut.Invocations().Count.ShouldBe(1);
        }

        [Fact]
        public void GivenGenericAdapterCallbackThrow_WhenRefAndOutChanged_ShouldPropagateAndRecordCallerValues()
        {
            var failure = new InvalidOperationException("callback failed");
            _sut.Produce<string>(Arg<int>.Any(), OutArg<string>.Any())
                .Callback(
                    (ref int reference, out string output) =>
                    {
                        reference = 9;
                        output = "written";
                        throw failure;
                    }
                );
            var reference = 2;
            object output = "before";
            Should
                .Throw<InvalidOperationException>(() =>
                    _sut.Instance().Produce<object>(ref reference, out output)
                )
                .ShouldBeSameAs(failure);
            reference.ShouldBe(9);
            output.ShouldBe("written");
            var invocation = _sut.Produce<object>(2, OutArg<object>.Any()).Invocations.Single();
            invocation.Reference.ShouldBe(2);
            invocation.Output.ShouldBe("written");
            invocation.Exception.ShouldBeSameAs(failure);
            _sut.Invocations().Count.ShouldBe(1);
        }

        [Fact]
        public async Task GivenAsyncFailure_WhenReturnedTaskFaults_ShouldRecordTheTaskWithoutASynchronousException()
        {
            _sut.Fetch().ThrowsAsync(new InvalidOperationException());
            var task = _sut.Instance().Fetch();
            await Should.ThrowAsync<InvalidOperationException>(task);
            var invocation = _sut.Fetch().Invocations.Single();
            invocation.Result.ShouldBeSameAs(task);
            invocation.Exception.ShouldBeNull();
        }

        [Fact]
        public void GivenOverloads_WhenInspectingTypedSnapshots_ShouldKeepEachSignatureSeparate()
        {
            _sut.Instance().Outer();
            _sut.Instance().Outer(7);
            _sut.Outer().Invocations.Count.ShouldBe(1);
            _sut.Outer(Arg<int>.Any()).Invocations.Single().Value.ShouldBe(7);
            _sut.Invocations().Count.ShouldBe(2);
        }

        [Fact]
        public void GivenGenericLookupPredicateThrowsAfterNestedCall_WhenInspectingHistory_ShouldKeepEntryOrderAndValues()
        {
            var reference = 2;
            var failure = new InvalidOperationException("matcher failed");
            _sut.Produce<object>(
                    Arg<int>.Is(value =>
                    {
                        _sut.Instance().Inner();
                        reference = 99;
                        throw failure;
                    }),
                    OutArg<object>.Any()
                )
                .Callback((ref int input, out object output) => output = "unused");

            object output = "before";
            Should
                .Throw<InvalidOperationException>(() =>
                    _sut.Instance().Produce<object>(ref reference, out output)
                )
                .ShouldBeSameAs(failure);
            var invocation = _sut.Produce<object>(Arg<int>.Any(), OutArg<object>.Any())
                .Invocations.Single();
            invocation.Reference.ShouldBe(2);
            invocation.Output.ShouldBeNull();
            invocation.Exception.ShouldBeSameAs(failure);
            _sut.Invocations().Select(call => call.Member).ShouldBe(new[] { "Produce", "Inner" });
        }

        [Fact]
        public void GivenThrowingCallbackFinallyChangesOutValue_WhenRecordingFailure_ShouldKeepFinalOutValue()
        {
            var failure = new InvalidOperationException("callback failed");
            _sut.TryRead(Arg<int>.Any(), OutArg<string>.Any())
                .Returns(
                    (ref int input, out string output) =>
                    {
                        output = "before";
                        try
                        {
                            throw failure;
                        }
                        finally
                        {
                            output = "after";
                        }
                    }
                );
            var input = 2;
            string output = "unassigned";
            Should
                .Throw<InvalidOperationException>(() =>
                    _sut.Instance().TryRead(ref input, out output)
                )
                .ShouldBeSameAs(failure);
            output.ShouldBe("after");
            var invocation = _sut.TryRead(2, OutArg<string>.Any()).Invocations.Single();
            invocation.Output.ShouldBe("after");
            invocation.Exception.ShouldBeSameAs(failure);
        }
    }
}
