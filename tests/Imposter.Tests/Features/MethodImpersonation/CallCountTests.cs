using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class CallCountTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Fact]
        public void Given_NoCalls_When_ReadingCallCount_Should_ReturnZero()
        {
            _sut.VoidNoParams().CallCount().ShouldBe(0);
            _sut.IntSingleParam(Arg<int>.Any()).CallCount().ShouldBe(0);
        }

        [Fact]
        public void Given_PreviousCalls_When_CallingAgain_Should_ExposeAdditionalCallCount()
        {
            var verifier = _sut.VoidNoParams();
            _sut.Instance().VoidNoParams();
            var previousCount = verifier.CallCount();

            _sut.Instance().VoidNoParams();
            _sut.Instance().VoidNoParams();

            previousCount.ShouldBe(1);
            (verifier.CallCount() - previousCount).ShouldBe(2);
            verifier.CallCount().ShouldBe(3);
            verifier.Called(Count.Exactly(3));
        }

        [Fact]
        public void Given_DifferentArguments_When_ReadingCallCount_Should_CountOnlyMatchingCalls()
        {
            _sut.Instance().IntSingleParam(1);
            _sut.Instance().IntSingleParam(2);
            _sut.Instance().IntSingleParam(2);

            _sut.IntSingleParam(Arg<int>.Any()).CallCount().ShouldBe(3);
            _sut.IntSingleParam(2).CallCount().ShouldBe(2);
            _sut.IntSingleParam(Arg<int>.Is(value => value > 1)).CallCount().ShouldBe(2);
            _sut.IntSingleParam(3).CallCount().ShouldBe(0);
        }

        [Fact]
        public void Given_ExplicitVoidSetup_When_ReadingCallCount_Should_PreserveSetup()
        {
            var imposter = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            var verifier = imposter.VoidNoParams();
            verifier.CallCount().ShouldBe(0);

            imposter.Instance().VoidNoParams();
            verifier.CallCount().ShouldBe(1);
        }

        [Fact]
        public void Given_ParallelCalls_When_WorkCompletes_Should_CountAllInvocations()
        {
            var verifier = _sut.VoidNoParams();
            Parallel.For(0, 100, _ => _sut.Instance().VoidNoParams());

            verifier.CallCount().ShouldBe(100);
        }

        [Fact]
        public void Given_ClassWithBaseImplementation_When_ReadingCallCount_Should_PreserveBehavior()
        {
            var imposter = new MethodSetupFeatureClassSutImposter();
            var verifier = imposter.IntSingleParam(Arg<int>.Any());
            verifier.UseBaseImplementation();

            imposter.Instance().IntSingleParam(5).ShouldBe(10);
            verifier.CallCount().ShouldBe(1);
            imposter.Instance().IntSingleParam(5).ShouldBe(10);
            verifier.CallCount().ShouldBe(2);
        }

        [Fact]
        public void Given_ReturnSetup_When_ReadingCallCount_Should_PreserveConfiguredBehavior()
        {
            var verifier = _sut.IntNoParams();
            verifier.Returns(42);

            _sut.Instance().IntNoParams().ShouldBe(42);
            verifier.CallCount().ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(42);
            verifier.CallCount().ShouldBe(2);
        }

        [Fact]
        public void Given_SequencedSetup_When_ReadingCallCount_Should_PreserveSequenceAndCallbacks()
        {
            var callbackCount = 0;
            var verifier = _sut.IntNoParams();
            verifier.Returns(1).Callback(() => callbackCount++).Then().Returns(2);

            _sut.Instance().IntNoParams().ShouldBe(1);
            verifier.CallCount().ShouldBe(1);
            callbackCount.ShouldBe(1);
            _sut.Instance().IntNoParams().ShouldBe(2);
            verifier.CallCount().ShouldBe(2);
            verifier.Called(Count.Exactly(2));
        }

        [Fact]
        public void Given_GenericSetup_When_ReadingCallCount_Should_PreserveConfiguredBehavior()
        {
            var verifier = _sut.GenericReturnType<int>();
            verifier.Returns(42);

            _sut.Instance().GenericReturnType<int>().ShouldBe(42);
            verifier.CallCount().ShouldBe(1);
            _sut.Instance().GenericReturnType<string>();
            _sut.Instance().GenericReturnType<int>().ShouldBe(42);
            verifier.CallCount().ShouldBe(2);
            verifier.Called(Count.Exactly(2));
        }

        [Fact]
        public void Given_GenericCalls_When_ReadingCallCount_Should_MatchTypeAndArguments()
        {
            _sut.Instance().GenericSingleParam(42);
            _sut.Instance().GenericSingleParam("42");
            _sut.Instance().GenericSingleParam(43);
            _sut.Instance().GenericReturnType<int>();
            _sut.Instance().GenericReturnType<string>();
            _sut.Instance().GenericReturnType<string>();

            _sut.GenericSingleParam(Arg<int>.Any()).CallCount().ShouldBe(2);
            _sut.GenericSingleParam<int>(42).CallCount().ShouldBe(1);
            _sut.GenericSingleParam(Arg<string>.Any()).CallCount().ShouldBe(1);
            _sut.GenericReturnType<int>().CallCount().ShouldBe(1);
            _sut.GenericReturnType<string>().CallCount().ShouldBe(2);
            _sut.GenericReturnType<bool>().CallCount().ShouldBe(0);
        }

        [Fact]
        public void Given_RefAndOutParameters_When_ReadingCallCount_Should_MatchInvocationHistory()
        {
            var value = 10;
            _sut.Instance().IntRefParam(ref value);
            _sut.Instance().IntOutParam(out _);

            _sut.IntRefParam(10).CallCount().ShouldBe(1);
            _sut.IntRefParam(20).CallCount().ShouldBe(0);
            _sut.IntOutParam(OutArg<int>.Any()).CallCount().ShouldBe(1);
        }

        [Fact]
        public async Task Given_AsyncCalls_When_ReadingCallCount_Should_CountRecordedInvocations()
        {
            await _sut.Instance().AsyncTaskIntNoParams();
            await _sut.Instance().AsyncValueTaskIntNoParams();

            _sut.AsyncTaskIntNoParams().CallCount().ShouldBe(1);
            _sut.AsyncValueTaskIntNoParams().CallCount().ShouldBe(1);
        }

        [Fact]
        public void Given_ThrowingMethod_When_ReadingCallCount_Should_CountFailedInvocation()
        {
            var verifier = _sut.VoidNoParams();
            verifier.Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().VoidNoParams());

            verifier.CallCount().ShouldBe(1);
            Should.Throw<InvalidOperationException>(() => _sut.Instance().VoidNoParams());
            verifier.CallCount().ShouldBe(2);
        }
    }
}
