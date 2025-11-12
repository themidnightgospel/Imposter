using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImposter
{
    public class AsyncTests
    {
        private readonly IEventSetupSutImposter _sut =
#if USE_CSHARP14
            IEventSetupSut.Imposter();
#else
            new IEventSetupSutImposter();
#endif

        [Fact]
        public async Task GivenMultipleAsyncHandlersAndCallback_WhenRaiseAsync_ShouldAwaitAll()
        {
            _sut.AsyncSomethingHappened.Callback(async (s, e) => await Task.Delay(50));
            _sut.Instance().AsyncSomethingHappened += async (s, e) => await Task.Delay(50);

            await _sut.AsyncSomethingHappened.RaiseAsync(this, EventArgs.Empty);
        }

        [Fact]
        public async Task GivenAsyncCallbackReturnsNullTask_WhenRaiseAsync_ShouldNotThrow()
        {
            // Delegate returns null; generator handles null by skipping await
            _sut.AsyncSomethingHappened.Callback((s, e) => null);

            await _sut.AsyncSomethingHappened.RaiseAsync(this, EventArgs.Empty);

            _sut.AsyncSomethingHappened.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), Count.Once());
        }

        [Fact]
        public async Task GivenAsyncHandlerThrows_WhenRaiseAsync_ShouldPropagateException()
        {
            _sut.Instance().AsyncSomethingHappened += async (s, e) => { throw new InvalidOperationException("boom"); };

            await Should.ThrowAsync<InvalidOperationException>(async () =>
                await _sut.AsyncSomethingHappened.RaiseAsync(this, EventArgs.Empty).WaitAsync(TimeSpan.FromSeconds(1)));
        }
    }
}