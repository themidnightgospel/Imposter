using System;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.CallAllocations
{
    public class CachedAsyncOutcomeTests
    {
        private readonly IAsyncAllocationSutImposter _sut = new IAsyncAllocationSutImposter();

        [Fact]
        public async Task GivenReturnsAsync_WhenCalledRepeatedly_ShouldReuseTaskAndPreserveSequence()
        {
            _sut.GetAsync(Arg<int>.Any()).ReturnsAsync(12345).Then().ReturnsAsync(67890);
            var instance = _sut.Instance();
            (await instance.GetAsync(1)).ShouldBe(12345);
            var second = instance.GetAsync(1);
            instance.GetAsync(2).ShouldBeSameAs(second);
            (await second).ShouldBe(67890);
            _sut.GetAsync(Arg<int>.Any()).Called(Count.Exactly(3));
        }

        [Fact]
        public async Task GivenByReferenceReturnsAsync_WhenCalledRepeatedly_ShouldReuseTaskAndInitializeOutParameter()
        {
            _sut.GetByReferenceAsync(Arg<int>.Any(), OutArg<int>.Any(), Arg<int>.Any())
                .ReturnsAsync(12345);
            var instance = _sut.Instance();
            var key = 7;
            var extra = 9;
            var first = instance.GetByReferenceAsync(ref key, out var count, in extra);
            count.ShouldBe(0);
            var second = instance.GetByReferenceAsync(ref key, out count, in extra);
            second.ShouldBeSameAs(first);
            (await second).ShouldBe(12345);
            count.ShouldBe(0);
            key.ShouldBe(7);
            extra.ShouldBe(9);
            var history = _sut.GetByReferenceAsync(
                Arg<int>.Any(),
                OutArg<int>.Any(),
                Arg<int>.Any()
            ).Invocations;
            history[0].Result.ShouldBeSameAs(first);
            history[0].Exception.ShouldBeNull();
            history[0].Key.ShouldBe(7);
            history[0].Count.ShouldBe(0);
            history[0].Extra.ShouldBe(9);
        }

        [Fact]
        public void GivenSynchronousTaskFailure_WhenInspectingHistory_ShouldPreserveExceptionAndDefaultResult()
        {
            var failure = new InvalidOperationException("synchronous");
            _sut.GetAsync(Arg<int>.Any()).Throws(failure);
            Should
                .Throw<InvalidOperationException>(() => _sut.Instance().GetAsync(7))
                .ShouldBeSameAs(failure);
            var invocation = _sut.GetAsync(Arg<int>.Any()).Invocations[0];
            invocation.Exception.ShouldBeSameAs(failure);
            invocation.Result.ShouldBeNull();
        }

        [Fact]
        public void GivenNullTaskResult_WhenInspectingHistory_ShouldKeepNullWithoutException()
        {
            Task<int> result = null!;
            _sut.GetAsync(Arg<int>.Any()).Returns(result);
            _sut.Instance().GetAsync(7).ShouldBeNull();
            var invocation = _sut.GetAsync(Arg<int>.Any()).Invocations[0];
            invocation.Result.ShouldBeNull();
            invocation.Exception.ShouldBeNull();
        }

        [Fact]
        public async Task GivenDynamicTaskValue_WhenReturnsAsync_ShouldReturnItAsAValue()
        {
            object value = Task.FromResult<object>("nested");
            _sut.GetDynamicAsync().ReturnsAsync(value);
            object result = await _sut.Instance().GetDynamicAsync();
            result.ShouldBeSameAs(value);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GivenThrowsAsync_WhenCalledRepeatedly_ShouldPreserveExceptionAndCancellation(
            bool cancellation
        )
        {
            using var source = new CancellationTokenSource();
            source.Cancel();
            Exception exception = cancellation
                ? new OperationCanceledException("canceled", source.Token)
                : new InvalidOperationException("failure");
            _sut.GetAsync(Arg<int>.Any()).ThrowsAsync(exception);
            _sut.PeekAsync(Arg<int>.Any()).ThrowsAsync(exception);
            _sut.RunAsync().ThrowsAsync(exception);
            _sut.RunValueAsync().ThrowsAsync(exception);
            _sut.GetByReferenceAsync(Arg<int>.Any(), OutArg<int>.Any(), Arg<int>.Any())
                .ThrowsAsync(exception);
            var instance = _sut.Instance();
            var key = 7;
            var extra = 9;
            var first = instance.GetAsync(1);
            instance.GetAsync(2).ShouldBeSameAs(first);
            var tasks = new Task[]
            {
                first,
                instance.PeekAsync(1).AsTask(),
                instance.PeekAsync(2).AsTask(),
                instance.RunAsync(),
                instance.RunValueAsync().AsTask(),
                instance.GetByReferenceAsync(ref key, out var count, in extra),
            };
            count.ShouldBe(0);
            key.ShouldBe(7);
            foreach (var task in tasks)
            {
                task.IsCanceled.ShouldBe(cancellation);
                task.IsFaulted.ShouldBe(!cancellation);
                var caught = await Record.ExceptionAsync(() => task);
                caught.ShouldBeSameAs(exception);
            }
        }

        [Fact]
        public async Task GivenCancellationWithoutCanceledToken_WhenThrowsAsync_ShouldStillReturnCanceledTask()
        {
            var exception = new OperationCanceledException();
            _sut.GetAsync(Arg<int>.Any()).ThrowsAsync(exception);
            var task = _sut.Instance().GetAsync(1);
            task.IsCanceled.ShouldBeTrue();
            (await Record.ExceptionAsync(() => task)).ShouldBeSameAs(exception);
        }

        [Fact]
        public async Task GivenNullException_WhenThrowsAsync_ShouldFailOnAwait()
        {
            _sut.GetAsync(Arg<int>.Any()).ThrowsAsync(null!);
            var task = _sut.Instance().GetAsync(1);
            await Should.ThrowAsync<NullReferenceException>(task);
        }
    }
}
