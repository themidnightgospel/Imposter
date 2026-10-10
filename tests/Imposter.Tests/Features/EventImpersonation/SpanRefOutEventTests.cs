using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    public class SpanRefOutEventTests
    {
        private readonly ISpanRefOutEventSutImposter _sut = new ISpanRefOutEventSutImposter();

        [Fact]
        public void GivenHandlerThatSlicesTheRefSpan_WhenRaised_ShouldAdvanceTheRaisersSpan()
        {
            _sut.Instance().DataRead += (ref ReadOnlySpan<byte> data) => data = data.Slice(1);
            var data = new ReadOnlySpan<byte>(new byte[] { 1, 2 });

            _sut.DataRead.Raise(ref data);

            data.ToArray().ShouldBe(new byte[] { 2 });
        }

        [Fact]
        public void GivenRefSpanChangedByHandler_WhenRaisedIsVerified_ShouldMatchTheElementsItArrivedWith()
        {
            _sut.Instance().DataRead += (ref ReadOnlySpan<byte> data) => data = default;
            var data = new ReadOnlySpan<byte>(new byte[] { 1, 2 });

            _sut.DataRead.Raise(ref data);

            _sut.DataRead.Raised(ReadOnlySpanArg<byte>.Is(1, 2), Count.Once());
        }

        [Fact]
        public void GivenHandlerThatAssignsTheOutSpan_WhenRaised_ShouldHandItToTheRaiser()
        {
            var array = new byte[] { 7 };
            _sut.Instance().BufferRented += (out Span<byte> buffer) => buffer = array;

            _sut.BufferRented.Raise(out var buffer);

            buffer.ToArray().ShouldBe(new byte[] { 7 });
        }

        [Fact]
        public void GivenNoHandler_WhenRaisedWithOutSpan_ShouldAssignAnEmptySpan()
        {
            _sut.BufferRented.Raise(out var buffer);

            buffer.IsEmpty.ShouldBeTrue();
        }
    }
}
