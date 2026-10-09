using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class SpanInParameterTests
    {
        private readonly ISpanInParameterSutImposter _sut = new ISpanInParameterSutImposter();

        [Fact]
        public void GivenSetupForSpanElements_WhenInvokedWithThoseElements_ShouldReturnSetupValue()
        {
            _sut.Count(ReadOnlySpanArg<byte>.Is(1, 2)).Returns(2);
            var data = new ReadOnlySpan<byte>(new byte[] { 1, 2 });

            _sut.Instance().Count(in data).ShouldBe(2);
        }

        [Fact]
        public void GivenReturnsDelegate_WhenInvoked_ShouldPassTheSpanToTheDelegate()
        {
            _sut.Count(ReadOnlySpanArg<byte>.Any())
                .Returns((in ReadOnlySpan<byte> data) => data[0]);
            var data = new ReadOnlySpan<byte>(new byte[] { 9 });

            _sut.Instance().Count(in data).ShouldBe(9);
        }

        [Fact]
        public void GivenInvocation_WhenVerifiedWithItsElements_ShouldCountIt()
        {
            var data = new ReadOnlySpan<byte>(new byte[] { 3 });
            _sut.Instance().Count(in data);

            _sut.Count(ReadOnlySpanArg<byte>.Is(3)).Called(Count.Once());
        }

        [Fact]
        public void GivenGenericSetupForSpanElements_WhenInvokedWithThoseElements_ShouldReturnSetupValue()
        {
            _sut.Sum<int>(ReadOnlySpanArg<int>.Is(1, 2)).Returns(3);
            var items = new ReadOnlySpan<int>(new[] { 1, 2 });

            _sut.Instance().Sum(in items).ShouldBe(3);
        }

#if USE_CSHARP14
        [Fact]
        public void GivenRefReadOnlySpanSetup_WhenInvokedWithThoseElements_ShouldReturnSetupValue()
        {
            var imposter = new ISpanRefReadOnlyParameterSutImposter();
            imposter.Count(ReadOnlySpanArg<byte>.Is(5)).Returns(1);
            var data = new ReadOnlySpan<byte>(new byte[] { 5 });

            imposter.Instance().Count(in data).ShouldBe(1);
        }
#endif
    }
}
