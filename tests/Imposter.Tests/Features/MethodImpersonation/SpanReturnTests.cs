using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class SpanReturnTests
    {
        private readonly ISpanReturnSutImposter _sut = new ISpanReturnSutImposter();

        [Fact]
        public void GivenReturnsArray_WhenInvoked_ShouldReturnASpanOverTheArray()
        {
            _sut.Rent(Arg<int>.Any()).Returns(new byte[] { 1, 2 });

            _sut.Instance().Rent(2).ToArray().ShouldBe(new byte[] { 1, 2 });
        }

        [Fact]
        public void GivenReturnsArray_WhenTheCallerWritesToTheSpan_ShouldWriteToTheArray()
        {
            var buffer = new byte[1];
            _sut.Rent(Arg<int>.Any()).Returns(buffer);

            _sut.Instance().Rent(1)[0] = 7;

            buffer[0].ShouldBe((byte)7);
        }

        [Fact]
        public void GivenReturnsDelegate_WhenInvoked_ShouldReturnTheDelegatesSpan()
        {
            _sut.Trim(ReadOnlySpanArg<char>.Any()).Returns(text => text.Trim());

            _sut.Instance().Trim("  a ".AsSpan()).ToString().ShouldBe("a");
        }

        [Fact]
        public void GivenNoSetup_WhenInvoked_ShouldReturnAnEmptySpan()
        {
            _sut.Instance().Rent(3).IsEmpty.ShouldBeTrue();
        }

        [Fact]
        public void GivenReturnsSequence_WhenInvokedTwice_ShouldReturnEachArrayInOrder()
        {
            _sut.Rent(Arg<int>.Any()).Returns(new byte[] { 1 }).Then().Returns(new byte[] { 2 });
            _sut.Instance().Rent(1);

            _sut.Instance().Rent(1).ToArray().ShouldBe(new byte[] { 2 });
        }

        [Fact]
        public void GivenThrows_WhenInvoked_ShouldThrow()
        {
            _sut.Rent(Arg<int>.Any()).Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().Rent(1));
        }

        [Fact]
        public void GivenInvocation_WhenVerified_ShouldCountIt()
        {
            _sut.Instance().Rent(4);

            _sut.Rent(Arg<int>.Is(4)).Called(Count.Once());
        }

        [Fact]
        public void GivenInvocation_WhenVerificationFails_ShouldThrowVerificationFailedException()
        {
            _sut.Rent(Arg<int>.Any()).Returns(new byte[] { 1 });
            _sut.Instance().Rent(1);

            Should.Throw<VerificationFailedException>(() =>
                _sut.Rent(Arg<int>.Any()).Called(Count.Never())
            );
        }

        [Fact]
        public void GivenGenericSpanReturn_WhenInvoked_ShouldReturnTheArrayElements()
        {
            _sut.Repeat<int>(Arg<int>.Any(), Arg<int>.Any()).Returns(new[] { 7, 7 });

            _sut.Instance().Repeat(7, 2).ToArray().ShouldBe(new[] { 7, 7 });
        }

        [Fact]
        public void GivenClassMethodWithUseBaseImplementation_WhenInvoked_ShouldReturnTheBaseSpan()
        {
            var imposter = new SpanReturnClassImposter();
            imposter.Name().UseBaseImplementation();

            imposter.Instance().Name().ToString().ShouldBe("base");
        }
    }
}
