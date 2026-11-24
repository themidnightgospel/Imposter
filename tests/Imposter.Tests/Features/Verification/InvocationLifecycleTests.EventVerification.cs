using System;
using System.Linq;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.Verification
{
    public partial class InvocationLifecycleTests
    {
        [Theory]
        [InlineData(0, false)]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(1, true)]
        [InlineData(3, false)]
        [InlineData(3, true)]
        public void GivenParameterlessEventRaises_WhenVerified_ShouldCountAndDescribeEveryRaise(
            int raises,
            bool withReason
        )
        {
            for (var i = 0; i < raises; i++)
            {
                _sut.Changed.Raise();
            }

            if (withReason)
            {
                _sut.Changed.Raised(Count.Exactly(raises), "the event should be recorded");
            }
            else
            {
                _sut.Changed.Raised(Count.Exactly(raises));
            }

            var exception = Should.Throw<VerificationFailedException>(() =>
            {
                if (withReason)
                {
                    _sut.Changed.Raised(Count.Exactly(raises + 1), "another raise was expected");
                }
                else
                {
                    _sut.Changed.Raised(Count.Exactly(raises + 1));
                }
            });

            exception.Message.ShouldContain($"performed {raises} times.");
            exception.PerformedInvocations.ShouldBe(
                string.Join(Environment.NewLine, Enumerable.Repeat("Changed raised", raises))
            );
            if (withReason)
            {
                exception.Message.ShouldContain("Because: another raise was expected");
            }
        }
    }
}
