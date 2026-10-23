using System;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.CallAllocations
{
    public class ExceptionHistoryTests
    {
        private readonly ICallAllocationSutImposter _sut = new ICallAllocationSutImposter();

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenThrowingDelegate_WhenUnwinding_ShouldAlreadyHaveRecordedFailure(
            bool voidMethod
        )
        {
            var exception = new InvalidOperationException("failure");
            var recordedBeforeFinally = false;
            Action verify = voidMethod
                ? () => _sut.Log(Arg<string>.Any()).Called(Count.Once())
                : () => _sut.Square(Arg<int>.Any()).Called(Count.Once());
            Action throwing = () =>
            {
                try
                {
                    ThrowFromUserCode(exception);
                }
                finally
                {
                    // An exception filter records on the first pass, before user finally blocks run.
                    verify();
                    recordedBeforeFinally = true;
                }
            };
            Action invoke;
            if (voidMethod)
            {
                _sut.Log(Arg<string>.Any()).Callback(_ => throwing());
                invoke = () => _sut.Instance().Log("value");
            }
            else
            {
                _sut.Square(Arg<int>.Any())
                    .Returns(_ =>
                    {
                        throwing();
                        return 0;
                    });
                invoke = () => _sut.Instance().Square(1);
            }

            var caught = Should.Throw<InvalidOperationException>(invoke);
            caught.ShouldBeSameAs(exception);
            caught.StackTrace.ShouldNotBeNull();
            caught.StackTrace.ShouldContain(nameof(ThrowFromUserCode));
            recordedBeforeFinally.ShouldBeTrue();
            verify();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowFromUserCode(Exception exception) => throw exception;
    }
}
