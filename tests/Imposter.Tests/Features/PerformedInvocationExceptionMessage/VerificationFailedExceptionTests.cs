using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;
using static Imposter.Tests.Features.PerformedInvocationExceptionMessage.PerformedInvocationTestHelper;

namespace Imposter.Tests.Features.PerformedInvocationExceptionMessage
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

        [Fact]
        public void GivenLegacyConstructorIsUsed_WhenExceptionIsCreated_ShouldKeepPerformedInvocationsNull()
        {
            var expectedCount = Count.Once();

            var exception = new VerificationFailedException(expectedCount, actualCount: 0);

            exception.PerformedInvocations.ShouldBeNull();
            exception.MessageShouldDescribeCounts(expectedCount, 0);
        }

        [Fact]
        public void GivenExtendedConstructorIsUsed_WhenExceptionIsCreated_ShouldIncludePerformedInvocationsInMessage()
        {
            var expectedCount = Count.AtLeast(2);
            var performedInvocations = string.Join(Environment.NewLine, new[] { "foo()", "bar()" });

            var exception = new VerificationFailedException(expectedCount, 1, performedInvocations);

            exception.PerformedInvocations.ShouldBe(performedInvocations);
            exception.MessageShouldDescribeCounts(expectedCount, 1);
            exception.Message.ShouldContain("Performed invocations:");
            exception.Message.ShouldContain("foo()");
            exception.Message.ShouldContain("bar()");
        }
    }
}
