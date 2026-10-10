using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    public class RefOutEventTests
    {
        private readonly IRefOutEventSutImposter _sut = new IRefOutEventSutImposter();

        [Fact]
        public void GivenHandlerThatChangesTheRefValue_WhenRaised_ShouldChangeTheRaisersValue()
        {
            _sut.Instance().Changed += (ref int value) => value *= 2;
            var value = 3;

            _sut.Changed.Raise(ref value);

            value.ShouldBe(6);
        }

        [Fact]
        public void GivenCallbackThatChangesTheRefValue_WhenRaised_ShouldChangeTheRaisersValue()
        {
            _sut.Changed.Callback((ref int value) => value += 1);
            var value = 3;

            _sut.Changed.Raise(ref value);

            value.ShouldBe(4);
        }

        [Fact]
        public void GivenRaisedWithRefValue_WhenVerified_ShouldMatchTheValueItArrivedWith()
        {
            _sut.Instance().Changed += (ref int value) => value = 0;
            var value = 3;

            _sut.Changed.Raise(ref value);

            _sut.Changed.Raised(Arg<int>.Is(3), Count.Once());
        }

        [Fact]
        public void GivenHandlerThatAssignsTheOutValue_WhenRaised_ShouldHandItToTheRaiser()
        {
            _sut.Instance().Requested += (out int value) => value = 7;

            _sut.Requested.Raise(out var value);

            value.ShouldBe(7);
        }

        [Fact]
        public void GivenNoHandler_WhenRaisedWithOutValue_ShouldAssignTheDefault()
        {
            var value = 1;

            _sut.Requested.Raise(out value);

            value.ShouldBe(0);
        }

        [Fact]
        public async Task GivenTaskHandler_WhenRaisedAsync_ShouldPassItTheValue()
        {
            var received = 0;
            _sut.Instance().ChangedAsync += (ref int value) =>
            {
                received = value;
                return Task.CompletedTask;
            };

            await _sut.ChangedAsync.RaiseAsync(5);

            received.ShouldBe(5);
        }

        [Fact]
        public void GivenClassEventHandlerThatChangesTheRefValue_WhenRaised_ShouldChangeTheRaisersValue()
        {
            var imposter = new RefEventClassImposter();
            imposter.Instance().Changed += (ref int value) => value = 9;
            var value = 1;

            imposter.Changed.Raise(ref value);

            value.ShouldBe(9);
        }
    }
}
