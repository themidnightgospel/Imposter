using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImposter
{
    public class InvalidSetups
    {
        private readonly IPropertySetupSutImposter _sut = new IPropertySetupSutImposter();

        [Fact]
        public void GivenNullFunction_WhenPropertyIsAccessed_ShouldThrowNullReferenceException()
        {
            _sut.Age.Getter().Returns((Func<int>)null!);

            Should.Throw<NullReferenceException>(() => _sut.Instance().Age);
        }

        [Fact]
        public void GivenNullCallback_WhenPropertyIsSet_ShouldThrowOnInvocation()
        {
            // This should not throw when setting up
            Should.NotThrow(() => _sut.Age.Setter(Arg<int>.Any()).Callback(null!));

            // But will throw when callback is invoked
            Should.Throw<NullReferenceException>(() => _sut.Instance().Age = 42);
        }
    }
}