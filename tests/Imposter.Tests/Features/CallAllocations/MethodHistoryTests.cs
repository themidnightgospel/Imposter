using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.CallAllocations;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(IHistoryCaptureSut))]

namespace Imposter.Tests.Features.CallAllocations
{
    public interface IHistoryCaptureSut
    {
        void Capture(ref int value, in int other);
        T Generic<T>(ref T value);
        T GenericIn<T>(in T value);
        void Span(Span<int> values);
        bool Match<T>(T value, ReadOnlySpan<int> values);
    }

    public class MethodHistoryTests
    {
        private readonly ICallAllocationSutImposter _sut = new ICallAllocationSutImposter();

        [Fact]
        public void GivenConcurrentCalls_WhenRecorded_ShouldKeepEveryInvocationExactlyOnce()
        {
            var instance = _sut.Instance();
            Parallel.For(
                0,
                2000,
                value =>
                {
                    instance.Square(value);
                    instance.Log(value.ToString());
                    instance.Echo(value);
                }
            );

            _sut.Square(Arg<int>.Any()).Called(Count.Exactly(2000));
            _sut.Log(Arg<string>.Any()).Called(Count.Exactly(2000));
            _sut.Echo<int>(Arg<int>.Any()).Called(Count.Exactly(2000));
            for (var value = 0; value < 2000; value++)
            {
                _sut.Square(value).Called(Count.Once());
                _sut.Echo<int>(value).Called(Count.Once());
            }
        }

        [Fact]
        public void GivenCallsAddedDuringVerification_WhenCounting_ShouldUseCapturedSnapshot()
        {
            var instance = _sut.Instance();
            instance.Square(1);
            instance.Square(2);
            var addCall = true;
            var verification = _sut.Square(
                Arg<int>.Is(value =>
                {
                    if (addCall)
                    {
                        addCall = false;
                        instance.Square(3);
                    }
                    return true;
                })
            );

            verification.Called(Count.Exactly(2));
            verification.Called(Count.Exactly(3));
        }

        [Fact]
        public void GivenConcurrentReadersAndWriters_WhenCounting_ShouldObserveMonotonicSnapshots()
        {
            var instance = _sut.Instance();
            var query = _sut.Square(Arg<int>.Any());
            Parallel.Invoke(
                () => Parallel.For(0, 2000, value => instance.Square(value)),
                () =>
                {
                    var previous = 0;
                    for (var i = 0; i < 2000; i++)
                    {
                        var current = query.CallCount();
                        current.ShouldBeGreaterThanOrEqualTo(previous);
                        current.ShouldBeLessThanOrEqualTo(2000);
                        previous = current;
                    }
                }
            );
            query.Called(Count.Exactly(2000));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenRefAndInArgumentsMutatedByCallback_WhenVerified_ShouldKeepOriginalValues(
            bool throws
        )
        {
            var sut = new IHistoryCaptureSutImposter();
            sut.Capture(Arg<int>.Any(), Arg<int>.Any())
                .Callback(
                    (ref int value, in int other) =>
                    {
                        value = 99;
                        if (throws)
                            throw new InvalidOperationException("failure");
                    }
                );
            var input = 7;
            // Aliased ref/in arguments must both be captured before the callback changes their shared storage.
            if (throws)
                Should.Throw<InvalidOperationException>(() =>
                    sut.Instance().Capture(ref input, in input)
                );
            else
                sut.Instance().Capture(ref input, in input);
            input.ShouldBe(99);
            sut.Capture(7, 7).Called(Count.Once());
            sut.Capture(99, 99).Called(Count.Never());
        }

        [Fact]
        public void GivenGenericRefArgumentMutatedByOutcome_WhenVerified_ShouldKeepOriginalValueAndType()
        {
            var sut = new IHistoryCaptureSutImposter();
            sut.Generic<string>(Arg<string>.Any()).Returns((ref string value) => value = "changed");
            var input = "original";
            sut.Instance().Generic(ref input).ShouldBe("changed");
            sut.Generic<string>("original").Called(Count.Once());
            sut.Generic<string>("changed").Called(Count.Never());
            sut.Generic<object>(Arg<object>.Any()).Called(Count.Never());
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenSpanMutatedByCallback_WhenVerified_ShouldKeepPreCallbackSnapshot(
            bool throws
        )
        {
            var sut = new IHistoryCaptureSutImposter();
            sut.Span(SpanArg<int>.Any())
                .Callback(
                    (Span<int> values) =>
                    {
                        values[0] = 99;
                        if (throws)
                            throw new InvalidOperationException("failure");
                    }
                );
            var values = new[] { 7, 8 };
            if (throws)
                Should.Throw<InvalidOperationException>(() => sut.Instance().Span(values));
            else
                sut.Instance().Span(values);
            values[0].ShouldBe(99);
            sut.Span(SpanArg<int>.Is(7, 8)).Called(Count.Once());
            sut.Span(SpanArg<int>.Is(99, 8)).Called(Count.Never());
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenGenericLookupMatcherMutatesCaller_WhenItRejectsCall_ShouldKeepOriginalRefOrInSnapshot(
            bool readOnly
        )
        {
            var sut = new IHistoryCaptureSutImposter();
            var value = 7;
            var observed = new List<int>();
            var observe = Arg<int>.Is(input =>
            {
                observed.Add(input);
                return false;
            });
            var mutate = Arg<int>.Is(input =>
            {
                value = 99;
                return false;
            });
            if (readOnly)
            {
                sut.GenericIn<int>(observe).Returns(1);
                sut.GenericIn<int>(mutate).Returns(2);
                sut.Instance().GenericIn<int>(in value);
            }
            else
            {
                sut.Generic<int>(observe).Returns(1);
                sut.Generic<int>(mutate).Returns(2);
                sut.Instance().Generic<int>(ref value);
            }
            value.ShouldBe(99);
            observed.ShouldBe(new[] { 7 });
        }

        [Fact]
        public void GivenAdaptedSpanMatcherMutatesItsCopy_WhenItRejectsCall_ShouldNotChangeNextCandidateSnapshot()
        {
            var sut = new IHistoryCaptureSutImposter();
            sut.Match<string>("value", ReadOnlySpanArg<int>.Is(1, 2)).Returns(true);
            sut.Match<object>(
                    Arg<object>.Any(),
                    ReadOnlySpanArg<int>.Is(values =>
                    {
                        values[0] = 99;
                        return false;
                    })
                )
                .Returns(false);
            var values = new[] { 1, 2 };

            sut.Instance().Match<string>("value", values).ShouldBeTrue();
            values.ShouldBe(new[] { 1, 2 });
            sut.Match<string>("value", ReadOnlySpanArg<int>.Is(1, 2)).Called(Count.Once());
            sut.Match<object>(Arg<object>.Any(), ReadOnlySpanArg<int>.Any()).Called(Count.Never());
        }

        [Fact]
        public void GivenHistory_WhenVerificationFails_ShouldDescribeNewestCallsFirst()
        {
            _sut.Instance().Square(1);
            _sut.Instance().Square(2);
            var error = Should.Throw<VerificationFailedException>(() =>
                _sut.Square(0).Called(Count.Once())
            );
            var newest = error.Message.IndexOf("value: <2>", StringComparison.Ordinal);
            var oldest = error.Message.IndexOf("value: <1>", StringComparison.Ordinal);
            newest.ShouldBeGreaterThanOrEqualTo(0);
            oldest.ShouldBeGreaterThan(newest);
        }

        [Fact]
        public void GivenNonGenericHistory_WhenVerifyingWithExistingBuilder_ShouldNotAllocate()
        {
            var verification = _sut.Square(Arg<int>.Any());
            var instance = _sut.Instance();
            for (var i = 0; i < 1000; i++)
                instance.Square(i);
            var count = Count.Exactly(1000);
            for (var i = 0; i < 100; i++)
                verification.Called(count);
            var before = GC.GetAllocatedBytesForCurrentThread();
            for (var i = 0; i < 100; i++)
                verification.Called(count);
            (GC.GetAllocatedBytesForCurrentThread() - before).ShouldBe(0);
        }
    }
}
