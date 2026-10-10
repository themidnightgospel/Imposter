using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    public class RefStructEventTests
    {
        private readonly IRefStructEventSutImposter _sut = new IRefStructEventSutImposter();

        [Fact]
        public void GivenSubscribedHandler_WhenRaised_ShouldPassItTheRefStruct()
        {
            var seen = 0;
            _sut.Instance().Moved += (id, position) => seen = id + position.Offset;

            _sut.Moved.Raise(1, new Position(2));

            seen.ShouldBe(3);
        }

        [Fact]
        public void GivenCallback_WhenRaised_ShouldPassItTheRefStruct()
        {
            var seen = 0;
            _sut.Moved.Callback((id, position) => seen = position.Offset);

            _sut.Moved.Raise(1, new Position(4));

            seen.ShouldBe(4);
        }

        [Fact]
        public void GivenRaises_WhenRaisedIsVerified_ShouldCountThemByTheOtherArguments()
        {
            _sut.Moved.Raise(1, new Position(1));
            _sut.Moved.Raise(1, new Position(2));
            _sut.Moved.Raise(2, new Position(1));

            _sut.Moved.Raised(Arg<int>.Is(1), Count.Exactly(2));
        }

        [Fact]
        public void GivenSubscribedHandler_WhenRaised_ShouldCountTheHandlerInvocation()
        {
            MovedHandler handler = (_, _) => { };
            _sut.Instance().Moved += handler;

            _sut.Moved.Raise(1, new Position(1));

            _sut.Moved.HandlerInvoked(Arg<MovedHandler>.Is(handler), Count.Once());
        }

        [Fact]
        public void GivenHandlerThatChangesTheRefStruct_WhenRaised_ShouldChangeTheRaisersValue()
        {
            _sut.Instance().Advanced += (ref Position position) => position.Offset++;
            var position = new Position(1);

            _sut.Advanced.Raise(ref position);

            position.Offset.ShouldBe(2);
            _sut.Advanced.Raised(Count.Once());
        }
    }
}
