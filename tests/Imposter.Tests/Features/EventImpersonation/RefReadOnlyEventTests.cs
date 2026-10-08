#if USE_CSHARP14
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    public class RefReadOnlyEventTests
    {
        private readonly IRefReadOnlyEventSutImposter _sut = new IRefReadOnlyEventSutImposter();

        [Fact]
        public void GivenHandler_WhenRaised_ShouldPassTheValueToTheHandler()
        {
            var received = 0;
            _sut.Instance().Happened += (ref readonly int value) => received = value;
            var value = 5;

            _sut.Happened.Raise(in value);

            received.ShouldBe(5);
        }

        [Fact]
        public void GivenCallback_WhenRaised_ShouldPassTheValueToTheCallback()
        {
            var received = 0;
            _sut.Happened.Callback((ref readonly int value) => received = value);
            var value = 5;

            _sut.Happened.Raise(in value);

            received.ShouldBe(5);
        }

        [Fact]
        public async Task GivenTaskHandler_WhenRaisedAsync_ShouldPassTheValueToTheHandler()
        {
            var received = 0;
            _sut.Instance().TaskHappened += (ref readonly int value) =>
            {
                received = value;
                return Task.CompletedTask;
            };

            await _sut.TaskHappened.RaiseAsync(5);

            received.ShouldBe(5);
        }
    }
}
#endif
