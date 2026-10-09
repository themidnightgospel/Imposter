using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    // The raise methods and Raised take the delegate's parameters with their nullable annotations, so an event whose
    // sender is object? is raised and verified with a null sender without a warning.
    public class NullableParameterTests
    {
        private readonly IEventSetupSutImposter _sut =
#if USE_CSHARP14
        IEventSetupSut.Imposter();
#else
        new IEventSetupSutImposter();
#endif

        [Fact]
        public void GivenEventHandler_WhenRaisedWithNullSender_ShouldPassNullToHandlerAndVerify()
        {
            object? receivedSender = this;
            _sut.Instance().SomethingHappened += (sender, e) => receivedSender = sender;

            _sut.SomethingHappened.Raise(null, EventArgs.Empty);

            receivedSender.ShouldBeNull();
            _sut.SomethingHappened.Raised(
                Arg<object?>.IsDefault(),
                Arg<EventArgs>.Any(),
                Count.Once()
            );
        }

        [Fact]
        public async Task GivenFuncEventWithNullableSender_WhenRaisedAsyncWithNull_ShouldPassNullToHandlerAndVerify()
        {
            object? receivedSender = this;
            Func<object?, EventArgs, Task> handler = (sender, e) =>
            {
                receivedSender = sender;
                return Task.CompletedTask;
            };
            _sut.Instance().AsyncSomethingHappened += handler;

            await _sut.AsyncSomethingHappened.RaiseAsync(null, EventArgs.Empty);

            receivedSender.ShouldBeNull();
            _sut.AsyncSomethingHappened.Raised(
                Arg<object?>.IsDefault(),
                Arg<EventArgs>.Any(),
                Count.Once()
            );
            _sut.AsyncSomethingHappened.HandlerInvoked(
                Arg<Func<object?, EventArgs, Task>>.Is(handler),
                Count.Once()
            );
        }
    }
}
