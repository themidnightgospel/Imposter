using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class AwaitableByReferenceTests
    {
        private readonly IAwaitableByReferenceSutImposter _sut =
            new IAwaitableByReferenceSutImposter();

        [Fact]
        public async Task GivenReturnsAsync_WhenMethodWithOutParameterIsInvoked_ShouldReturnTheValue()
        {
            _sut.GetAsync(OutArg<int>.Any()).ReturnsAsync(3);

            (await _sut.Instance().GetAsync(out _)).ShouldBe(3);
        }

        [Fact]
        public void GivenReturnsAsync_WhenMethodWithOutParameterIsInvoked_ShouldAssignTheDefaultToIt()
        {
            _sut.GetAsync(OutArg<int>.Any()).ReturnsAsync(3);

            _sut.Instance().GetAsync(out var count);

            count.ShouldBe(0);
        }

        [Fact]
        public async Task GivenNoSetup_WhenMethodWithOutParameterIsInvoked_ShouldReturnTheDefault()
        {
            (await _sut.Instance().GetAsync(out _)).ShouldBe(0);
        }

        [Fact]
        public async Task GivenReturnsAsync_WhenMethodWithInParameterIsInvoked_ShouldReturnTheValue()
        {
            _sut.PeekAsync(Arg<int>.Is(7)).ReturnsAsync(4);
            var key = 7;

            (await _sut.Instance().PeekAsync(in key)).ShouldBe(4);
        }

        [Fact]
        public async Task GivenThrowsAsync_WhenMethodWithRefParameterIsInvoked_ShouldReturnAFaultedTask()
        {
            _sut.NextAsync(Arg<int>.Any()).ThrowsAsync(new InvalidOperationException("boom"));
            var position = 0;

            var task = _sut.Instance().NextAsync(ref position);

            (await Should.ThrowAsync<InvalidOperationException>(task)).Message.ShouldBe("boom");
        }
    }
}
