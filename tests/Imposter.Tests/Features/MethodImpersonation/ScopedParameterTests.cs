#if USE_CSHARP14
using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class ScopedParameterTests
    {
        private readonly IScopedParameterSutImposter _sut = new IScopedParameterSutImposter();

        [Fact]
        public void GivenReturnsArrayForScopedSpanElements_WhenInvokedWithThem_ShouldReturnASpanOverTheArray()
        {
            _sut.Name(ReadOnlySpanArg<char>.Is('a')).Returns(new[] { 'b' });

            _sut.Instance().Name("a".AsSpan()).ToString().ShouldBe("b");
        }

        [Fact]
        public void GivenReturnsDelegate_WhenInvoked_ShouldPassItTheScopedSpan()
        {
            _sut.Name(ReadOnlySpanArg<char>.Any())
                .Returns(
                    (scoped ReadOnlySpan<char> text) =>
                        text.ToString().ToUpperInvariant().ToCharArray()
                );

            _sut.Instance().Name("ab".AsSpan()).ToString().ShouldBe("AB");
        }

        [Fact]
        public void GivenInvokedWithScopedSpan_WhenVerifiedForItsElements_ShouldCountIt()
        {
            _sut.Instance().Name("a".AsSpan());

            _sut.Name(ReadOnlySpanArg<char>.Is('a')).Called(Count.Once());
        }

        [Fact]
        public void GivenReturnsArrayForScopedRefArgument_WhenInvoked_ShouldReturnASpanOverTheArray()
        {
            _sut.Slice(Arg<int>.Is(1)).Returns(new[] { 2, 3 });
            var start = 1;

            _sut.Instance().Slice(ref start).ToArray().ShouldBe(new[] { 2, 3 });
        }

        [Fact]
        public void GivenReturnsDelegateTakingTheScopedSpan_WhenInvoked_ShouldWriteToTheRefSpan()
        {
            _sut.Copy(SpanArg<byte>.Any(), ReadOnlySpanArg<byte>.Any())
                .Returns(
                    (ref Span<byte> target, scoped ReadOnlySpan<byte> source) =>
                    {
                        source.CopyTo(target);
                        return source.Length;
                    }
                );
            var target = new Span<byte>(new byte[2]);

            var copied = _sut.Instance().Copy(ref target, new byte[] { 1, 2 });

            copied.ShouldBe(2);
            target.ToArray().ShouldBe(new byte[] { 1, 2 });
        }

        [Fact]
        public void GivenReturnsArrayForParamsSpanElements_WhenInvokedWithThem_ShouldReturnASpanOverTheArray()
        {
            _sut.First(ReadOnlySpanArg<int>.Is(1, 2)).Returns(new[] { 1 });

            _sut.Instance().First(1, 2).ToArray().ShouldBe(new[] { 1 });
        }

        [Fact]
        public void GivenUseBaseImplementation_WhenClassMethodWithScopedSpanIsInvoked_ShouldReturnTheBaseResult()
        {
            var imposter = new ScopedParameterClassImposter();
            imposter.Name(ReadOnlySpanArg<char>.Any()).UseBaseImplementation();

            imposter.Instance().Name("a".AsSpan()).ToString().ShouldBe("base");
        }
    }
}
#endif
