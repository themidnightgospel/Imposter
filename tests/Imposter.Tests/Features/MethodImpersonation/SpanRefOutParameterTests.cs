using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class SpanRefOutParameterTests
    {
        private readonly ISpanRefOutParameterSutImposter _sut =
            new ISpanRefOutParameterSutImposter();

        [Fact]
        public void GivenReturnsDelegateThatAssignsTheOutSpan_WhenInvoked_ShouldHandTheSpanToTheCaller()
        {
            _sut.TryRead(OutReadOnlySpanArg<byte>.Any())
                .Returns(
                    (out ReadOnlySpan<byte> data) =>
                    {
                        data = new byte[] { 1, 2 };
                        return true;
                    }
                );

            _sut.Instance().TryRead(out var data);

            data.ToArray().ShouldBe(new byte[] { 1, 2 });
        }

        [Fact]
        public void GivenNoSetup_WhenInvoked_ShouldAssignAnEmptyOutSpan()
        {
            _sut.Instance().TryRead(out var data);

            data.IsEmpty.ShouldBeTrue();
        }

        [Fact]
        public void GivenReturnsDelegateThatSlicesTheRefSpan_WhenInvoked_ShouldAdvanceTheCallersSpan()
        {
            _sut.Advance(ReadOnlySpanArg<byte>.Any())
                .Returns(
                    (ref ReadOnlySpan<byte> buffer) =>
                    {
                        buffer = buffer.Slice(1);
                        return 1;
                    }
                );
            var buffer = new ReadOnlySpan<byte>(new byte[] { 1, 2 });

            _sut.Instance().Advance(ref buffer);

            buffer.ToArray().ShouldBe(new byte[] { 2 });
        }

        [Fact]
        public void GivenSetupForTheRefSpansElements_WhenInvokedWithThoseElements_ShouldReturnSetupValue()
        {
            _sut.Advance(ReadOnlySpanArg<byte>.Is(1, 2)).Returns(7);
            var buffer = new ReadOnlySpan<byte>(new byte[] { 1, 2 });

            _sut.Instance().Advance(ref buffer).ShouldBe(7);
        }

        [Fact]
        public void GivenRefSpanReplacedDuringTheCall_WhenVerified_ShouldMatchTheElementsItArrivedWith()
        {
            _sut.Advance(ReadOnlySpanArg<byte>.Any())
                .Returns(
                    (ref ReadOnlySpan<byte> buffer) =>
                    {
                        buffer = buffer.Slice(1);
                        return 1;
                    }
                );
            var buffer = new ReadOnlySpan<byte>(new byte[] { 1, 2 });
            _sut.Instance().Advance(ref buffer);

            _sut.Advance(ReadOnlySpanArg<byte>.Is(1, 2)).Called(Count.Once());
        }

        [Fact]
        public void GivenCallbackThatWritesToTheRefSpan_WhenInvoked_ShouldWriteToTheCallersMemory()
        {
            _sut.Fill(SpanArg<byte>.Any()).Callback((ref Span<byte> buffer) => buffer[0] = 9);
            var memory = new byte[1];
            var buffer = new Span<byte>(memory);

            _sut.Instance().Fill(ref buffer);

            memory[0].ShouldBe((byte)9);
        }

        [Fact]
        public void GivenGenericReturnsDelegateThatAssignsTheOutSpan_WhenInvoked_ShouldHandTheElementsToTheCaller()
        {
            _sut.TryGet<int>(OutReadOnlySpanArg<int>.Any())
                .Returns(
                    (out ReadOnlySpan<int> items) =>
                    {
                        items = new[] { 4, 5 };
                        return true;
                    }
                );

            _sut.Instance().TryGet<int>(out var items);

            items.ToArray().ShouldBe(new[] { 4, 5 });
        }
    }
}
