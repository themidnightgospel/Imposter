using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.OpenGenericImposter
{
    // The builders' generic Throws<...>() renames its type parameter when the target already uses TException, so it
    // doesn't shadow the target's.
    public class TExceptionTypeParameterTests
    {
        private readonly IHaveTExceptionTypeParameterImposter<string> _sut = new();

        [Fact]
        public void GivenPropertyGetterThrowsGenericException_WhenRead_ShouldThrowIt()
        {
            _sut.Current.Getter().Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().Current);
        }

        [Fact]
        public void GivenIndexerGetterThrowsGenericException_WhenRead_ShouldThrowIt()
        {
            _sut[Arg<int>.Any()].Getter().Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance()[1]);
        }

        [Fact]
        public void GivenMethodThrowsGenericException_WhenCalled_ShouldThrowIt()
        {
            _sut.Get().Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().Get());
        }
    }
}
