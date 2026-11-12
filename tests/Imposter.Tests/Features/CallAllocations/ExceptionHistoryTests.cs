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
        public void GivenThrowingDelegate_WhenUnwinding_ShouldRecordAfterUserFinallyCompletes(
            bool voidMethod
        )
        {
            var exception = new InvalidOperationException("failure");
            var completedFinally = false;
            Action<Count> verify = voidMethod
                ? count => _sut.Log(Arg<string>.Any()).Called(count)
                : count => _sut.Square(Arg<int>.Any()).Called(count);
            Action throwing = () =>
            {
                try
                {
                    ThrowFromUserCode(exception);
                }
                finally
                {
                    // The call is still in progress; final out values are not available until this block completes.
                    verify(Count.Never());
                    completedFinally = true;
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
            completedFinally.ShouldBeTrue();
            verify(Count.Once());
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowFromUserCode(Exception exception) => throw exception;
    }
}
