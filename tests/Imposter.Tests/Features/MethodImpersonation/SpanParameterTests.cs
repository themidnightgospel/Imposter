using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class SpanParameterTests
    {
        private readonly ISpanParameterSutImposter _sut = new ISpanParameterSutImposter();

        [Fact]
        public void GivenSetupForSpanElements_WhenInvokedWithThoseElements_ShouldReturnSetupValue()
        {
            _sut.Parse(SpanArg<char>.Is('4', '2'), Arg<int>.Any()).Returns(42);

            _sut.Instance().Parse("42".AsSpan(), 0).ShouldBe(42);
        }

        [Fact]
        public void GivenSetupForSpanElements_WhenInvokedWithOtherElements_ShouldReturnDefault()
        {
            _sut.Parse(SpanArg<char>.Is('4', '2'), Arg<int>.Any()).Returns(42);

            _sut.Instance().Parse("7".AsSpan(), 0).ShouldBe(0);
        }

        [Fact]
        public void GivenUntypedAnySetup_WhenInvokedWithAnyElements_ShouldReturnSetupValue()
        {
            _sut.Parse(Arg.Any, Arg.Any).Returns(5);

            _sut.Instance().Parse("xyz".AsSpan(), 0).ShouldBe(5);
        }

        [Fact]
        public void GivenReturnsDelegate_WhenInvoked_ShouldPassTheSpanToTheDelegate()
        {
            _sut.Parse(SpanArg<char>.Any(), Arg<int>.Any())
                .Returns((text, start) => text.Length + start);

            _sut.Instance().Parse("abc".AsSpan(), 10).ShouldBe(13);
        }

        [Fact]
        public void GivenCallback_WhenInvoked_ShouldPassTheSpanToTheCallback()
        {
            var written = Array.Empty<byte>();
            _sut.Write(SpanArg<byte>.Any()).Callback(buffer => written = buffer.ToArray());

            _sut.Instance().Write(new byte[] { 1, 2, 3 });

            written.ShouldBe(new byte[] { 1, 2, 3 });
        }

        [Fact]
        public void GivenCallbackThatWritesToTheSpan_WhenInvoked_ShouldWriteToTheCallersMemory()
        {
            _sut.Write(SpanArg<byte>.Any()).Callback(buffer => buffer[0] = 7);
            var buffer = new byte[1];

            _sut.Instance().Write(buffer);

            buffer[0].ShouldBe((byte)7);
        }

        [Fact]
        public void GivenGenericMethodCallbackThatWritesToASpanOfAFixedType_WhenInvoked_ShouldWriteToTheCallersMemory()
        {
            _sut.Fill<int>(SpanArg<byte>.Any(), Arg<int>.Any())
                .Callback((buffer, value) => buffer[0] = (byte)value);
            var buffer = new byte[1];

            _sut.Instance().Fill(buffer, 7);

            buffer[0].ShouldBe((byte)7);
        }

        [Fact]
        public void GivenInvocation_WhenVerifiedWithItsElements_ShouldCountIt()
        {
            _sut.Instance().Parse("abc".AsSpan(), 1);

            _sut.Parse(SpanArg<char>.Is('a', 'b', 'c'), Arg<int>.Is(1)).Called(Count.Once());
        }

        [Fact]
        public void GivenSpanChangedAfterTheCall_WhenVerified_ShouldMatchTheElementsAtCallTime()
        {
            var buffer = new byte[] { 1, 2 };
            _sut.Instance().Write(buffer);
            buffer[0] = 9;

            _sut.Write(SpanArg<byte>.Is(1, 2)).Called(Count.Once());
        }

        [Fact]
        public void GivenSpanPredicate_WhenInvoked_ShouldMatchByPredicate()
        {
            _sut.Parse(SpanArg<char>.Is(text => text.Length > 2), Arg<int>.Any()).Returns(1);

            _sut.Instance().Parse("abcd".AsSpan(), 0).ShouldBe(1);
        }

        [Fact]
        public void GivenGenericSpanSetup_WhenInvoked_ShouldReturnSetupValue()
        {
            _sut.Contains<int>(SpanArg<int>.Is(1, 2), Arg<int>.Is(2)).Returns(true);

            _sut.Instance().Contains(new[] { 1, 2 }.AsSpan(), 2).ShouldBeTrue();
        }

        [Fact]
        public async Task GivenAsyncMethodWithoutSetup_WhenInvoked_ShouldReturnDefault()
        {
            var count = await _sut.Instance().CountAsync(new byte[] { 1 });

            count.ShouldBe(0);
        }

        [Fact]
        public async Task GivenReturnsAsync_WhenAsyncMethodIsInvoked_ShouldReturnTheValue()
        {
            _sut.CountAsync(SpanArg<byte>.Any()).ReturnsAsync(5);

            var count = await _sut.Instance().CountAsync(new byte[] { 1 });

            count.ShouldBe(5);
        }

        [Fact]
        public async Task GivenThrowsAsync_WhenAsyncMethodIsInvoked_ShouldReturnAFaultedTask()
        {
            _sut.CountAsync(SpanArg<byte>.Any()).ThrowsAsync(new InvalidOperationException("boom"));

            var task = _sut.Instance().CountAsync(new byte[] { 1 });

            (await Should.ThrowAsync<InvalidOperationException>(task)).Message.ShouldBe("boom");
        }

        [Fact]
        public void GivenClassMethodWithUseBaseImplementation_WhenInvoked_ShouldPassTheSpanToTheBaseMethod()
        {
            var imposter = new SpanParameterClassImposter();
            imposter.Count(SpanArg<char>.Any()).UseBaseImplementation();

            imposter.Instance().Count("abcd".AsSpan()).ShouldBe(4);
        }
    }
}
