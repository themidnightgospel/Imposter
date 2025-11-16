using Imposter.Abstractions;
using Shouldly;
using Xunit;
using static Imposter.Tests.Features.PerformedInvocationExceptionMessage.PerformedInvocationTestHelper;

namespace Imposter.Tests.Features.PerformedInvocationExceptionMessage
{
    public class PropertyPerformedInvocationExceptionMessageTests
    {
        [Fact]
        public void GivenPropertySetterCalledTooFewTimes_WhenVerificationFails_ShouldDescribePerformedInvocations()
        {
            var sut = PerformedInvocationSutFactory.CreatePropertySut();
            sut.Instance().Age = 30;

            var setter = sut.Age.Setter(Arg<int>.Any());
            var expectedCount = Count.Exactly(2);
            var exception = Should.Throw<VerificationFailedException>(() =>
                setter.Called(expectedCount)
            );

            var entries = exception.ReadEntries();
            entries
                .ShouldHaveSingleItem()
                .ShouldBe(
                    "set Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.Age = <30>"
                );
            exception.MessageShouldDescribeCounts(expectedCount, 1);
        }

        [Fact]
        public void GivenMultiplePropertySetterCalls_WhenVerificationFails_ShouldKeepOrdering()
        {
            var sut = PerformedInvocationSutFactory.CreatePropertySut();
            sut.Instance().Age = 10;
            sut.Instance().Age = 20;
            sut.Instance().Age = 30;

            var setter = sut.Age.Setter(Arg<int>.Any());
            var expectedCount = Count.AtLeast(4);
            var exception = Should.Throw<VerificationFailedException>(() =>
                setter.Called(expectedCount)
            );

            exception
                .ReadEntries()
                .ShouldBe(
                    new[]
                    {
                        "set Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.Age = <30>",
                        "set Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.Age = <20>",
                        "set Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.Age = <10>",
                    }
                );
            exception.MessageShouldDescribeCounts(expectedCount, 3);
        }

        [Fact]
        public void GivenPropertySetterPredicateFails_WhenVerificationFails_ShouldListAllSetterInvocations()
        {
            var sut = PerformedInvocationSutFactory.CreatePropertySut();
            sut.Instance().Age = 5;
            sut.Instance().Age = 15;
            sut.Instance().Age = 25;

            var setter = sut.Age.Setter(Arg<int>.Is(v => v > 10));
            var expectedCount = Count.AtLeast(3);
            var exception = Should.Throw<VerificationFailedException>(() =>
                setter.Called(expectedCount)
            );
            exception
                .ReadEntries()
                .ShouldBe(
                    new[]
                    {
                        "set Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.Age = <25>",
                        "set Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.Age = <15>",
                        "set Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.Age = <5>",
                    }
                );
            exception.MessageShouldDescribeCounts(expectedCount, 2);
        }

        [Fact]
        public void GivenGetterVerificationFails_WhenVerificationFails_ShouldNotPopulatePerformedInvocations()
        {
            var sut = PerformedInvocationSutFactory.CreatePropertySut();
            _ = sut.Instance().Age;

            var expectedCount = Count.Exactly(2);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.Age.Getter().Called(expectedCount)
            );

            exception.MessageShouldDescribeCounts(expectedCount, 1);
            exception.PerformedInvocations.ShouldBeNull();
        }
    }
}
