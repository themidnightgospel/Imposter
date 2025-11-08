using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImposter
{
    public class ExceptionTests
    {
        private readonly IEventSetupSutImposter _sut = new IEventSetupSutImposter();

        [Fact]
        public void GivenSyncHandlerThrows_WhenRaise_ShouldPropagateAndStillRecord()
        {
            EventHandler h = (s, e) => throw new InvalidOperationException("boom");
            _sut.Instance().SomethingHappened += h;

            Should.Throw<InvalidOperationException>(() =>
                _sut.SomethingHappened.Raise(this, EventArgs.Empty));

            // Raised and invocation are recorded before throwing
            _sut.SomethingHappened.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), Count.Once());
            _sut.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Once());
        }
    }
}

