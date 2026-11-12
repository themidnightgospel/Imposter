using Imposter.Abstractions;
using Shouldly;
using Xunit;
using static Imposter.Tests.Features.Verification.PerformedInvocationTestHelper;

namespace Imposter.Tests.Features.Verification
{
    public class VerificationFailedExceptionTests
    {
        [Fact]
        public void GivenVerificationFails_WhenExceptionIsCreated_ShouldExposeCountsAndPerformedInvocations()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var result = sut.Instance().IntNoParams();

            var expectedCount = Count.Exactly(2);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.IntNoParams().Called(expectedCount)
            );
            exception.MessageShouldDescribeCounts(expectedCount, 1);
            exception.PerformedInvocations.ShouldNotBeNull();
            exception.ReadEntries().ShouldBe(new[] { $"IntNoParams() => {FormatValue(result)}" });
        }
    }
}
