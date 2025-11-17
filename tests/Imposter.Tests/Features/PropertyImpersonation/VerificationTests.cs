using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class VerificationTests
    {
        private readonly IPropertySetupSutImposter _sut =
#if USE_CSHARP14
        IPropertySetupSut.Imposter();
#else
        new IPropertySetupSutImposter();
#endif

        [Fact]
        public void GivenPropertyInteractions_WhenVerifyingCorrectGetterCount_ShouldNotThrow()
        {
            _sut.Instance().Age.ShouldBe(0);
            _sut.Instance().Age.ShouldBe(0);

            Should.NotThrow(() => _sut.Age.Getter().Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenPropertyInteractions_WhenVerifyingIncorrectGetterCount_ShouldThrowException()
        {
            _sut.Instance().Age.ShouldBe(0);

            var exception = Should.Throw<Exception>(() =>
                _sut.Age.Getter().Called(Count.Exactly(2))
            );
            exception.Message.ShouldContain(
                "Invocation was expected to be performed exactly 2 time(s) but instead was performed 1 times."
            );
        }

        [Fact]
        public void GivenPropertySetterCalls_WhenVerifyingWithMatchingCriteria_ShouldVerifyCorrectCount()
        {
            _sut.Instance().Age = 10;
            _sut.Instance().Age = 20;
            _sut.Instance().Age = 10;

            Should.NotThrow(() =>
                _sut.Age.Setter(Arg<int>.Is(x => x == 20)).Called(Count.Exactly(1))
            );
            Should.NotThrow(() =>
                _sut.Age.Setter(Arg<int>.Is(x => x == 10)).Called(Count.Exactly(2))
            );
            Should.NotThrow(() => _sut.Age.Setter(Arg<int>.Any()).Called(Count.Exactly(3)));
        }

        [Fact]
        public void GivenPropertySetterCalls_WhenVerifyingWithNonMatchingCriteria_ShouldVerifyZeroCount()
        {
            _sut.Instance().Age = 10;
            _sut.Instance().Age = 20;

            Should.NotThrow(() => _sut.Age.Setter(Arg<int>.Is(x => x == 99)).Called(Count.Never()));
        }
    }
}
