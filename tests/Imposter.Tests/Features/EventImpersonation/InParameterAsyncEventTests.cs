using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    public class InParameterAsyncEventTests
    {
        private readonly IInParameterAsyncEventSutImposter _sut =
            new IInParameterAsyncEventSutImposter();

        [Fact]
        public async Task GivenTaskHandler_WhenRaisedAsync_ShouldPassTheValueToTheHandler()
        {
            var received = 0;
            _sut.Instance().TaskHappened += (in int value) =>
            {
                received = value;
                return Task.CompletedTask;
            };

            await _sut.TaskHappened.RaiseAsync(5);

            received.ShouldBe(5);
        }

        [Fact]
        public async Task GivenTaskCallback_WhenRaisedAsync_ShouldPassTheValueToTheCallback()
        {
            var received = 0;
            _sut.TaskHappened.Callback(
                (in int value) =>
                {
                    received = value;
                    return Task.CompletedTask;
                }
            );

            await _sut.TaskHappened.RaiseAsync(5);

            received.ShouldBe(5);
        }

        [Fact]
        public async Task GivenValueTaskHandler_WhenRaisedAsync_ShouldPassTheValueToTheHandler()
        {
            var received = 0;
            _sut.Instance().ValueTaskHappened += (in int value) =>
            {
                received = value;
                return default;
            };

            await _sut.ValueTaskHappened.RaiseAsync(5);

            received.ShouldBe(5);
        }

        [Fact]
        public async Task GivenRaisedAsync_WhenVerified_ShouldCountTheRaiseWithItsValue()
        {
            await _sut.TaskHappened.RaiseAsync(5);

            _sut.TaskHappened.Raised(Arg<int>.Is(5), Count.Once());
        }
    }
}
