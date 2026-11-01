using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    // The task an async method returns waits for the tasks its callbacks return.
    public class AsyncCallbackTests
    {
        private readonly IAsyncCallbackSutImposter _imposter = new();

        private IAsyncCallbackSut Instance => _imposter.Instance();

        [Fact]
        public async Task Given_TaskMethod_When_AsyncCallbackFails_Should_FaultReturnedTask()
        {
            _imposter.RunAsync(Arg<int>.Any()).Callback(FailAfterYield);

            await Should.ThrowAsync<InvalidOperationException>(() => Instance.RunAsync(1));
        }

        [Fact]
        public async Task Given_TaskOfTMethod_When_AsyncCallbackFails_Should_FaultReturnedTask()
        {
            _imposter.GetAsync(Arg<int>.Any()).ReturnsAsync(1).Callback(FailAfterYield);

            await Should.ThrowAsync<InvalidOperationException>(() => Instance.GetAsync(1));
        }

        [Fact]
        public async Task Given_ValueTaskMethod_When_AsyncCallbackFails_Should_FaultReturnedTask()
        {
            _imposter.RunValueAsync(Arg<int>.Any()).Callback(FailAfterYield);

            await Should.ThrowAsync<InvalidOperationException>(async () =>
                await Instance.RunValueAsync(1)
            );
        }

        [Fact]
        public async Task Given_ValueTaskOfTMethod_When_AsyncCallbackFails_Should_FaultReturnedTask()
        {
            _imposter.GetValueAsync(Arg<int>.Any()).ReturnsAsync(1).Callback(FailAfterYield);

            await Should.ThrowAsync<InvalidOperationException>(async () =>
                await Instance.GetValueAsync(1)
            );
        }

        [Fact]
        public async Task Given_CallbackReturningFaultedTask_When_Calling_Should_FaultReturnedTask()
        {
            _imposter
                .GetAsync(Arg<int>.Any())
                .ReturnsAsync(1)
                .Callback(_ => Task.FromException(new InvalidOperationException()));

            await Should.ThrowAsync<InvalidOperationException>(() => Instance.GetAsync(1));
        }

        [Fact]
        public async Task Given_PendingCallback_When_Calling_Should_CompleteOnlyAfterTheCallback()
        {
            var gate = new TaskCompletionSource<bool>();
            var callbackFinished = false;
            _imposter
                .GetAsync(Arg<int>.Any())
                .ReturnsAsync(7)
                .Callback(async _ =>
                {
                    await gate.Task;
                    callbackFinished = true;
                });

            var call = Instance.GetAsync(1);
            call.IsCompleted.ShouldBeFalse();
            gate.SetResult(true);

            (await call).ShouldBe(7);
            callbackFinished.ShouldBeTrue();
        }

        [Fact]
        public async Task Given_TwoPendingCallbacks_When_Calling_Should_WaitForBoth()
        {
            var first = new TaskCompletionSource<bool>();
            var second = new TaskCompletionSource<bool>();
            _imposter.RunAsync(Arg<int>.Any()).Callback(_ => first.Task).Callback(_ => second.Task);

            var call = Instance.RunAsync(1);
            second.SetResult(true);
            await Task.Yield();
            call.IsCompleted.ShouldBeFalse();
            first.SetResult(true);

            await call;
        }

        [Fact]
        public void Given_CallbacksThatCompleteSynchronously_When_Calling_Should_ReturnTheArrangedTask()
        {
            var arranged = Task.FromResult(5);
            _imposter
                .GetAsync(Arg<int>.Any())
                .Returns(_ => arranged)
                .Callback(_ => Task.CompletedTask)
                .Callback(_ => null!);

            Instance.GetAsync(1).ShouldBeSameAs(arranged);
        }

        [Fact]
        public async Task Given_FaultedResult_When_CallbackIsPending_Should_FailWithTheResultsException()
        {
            var gate = new TaskCompletionSource<bool>();
            _imposter
                .GetAsync(Arg<int>.Any())
                .ThrowsAsync(new ArgumentException())
                .Callback(_ => gate.Task);

            await Should.ThrowAsync<ArgumentException>(() => Instance.GetAsync(1));
        }

        [Fact]
        public async Task Given_SpanParameter_When_AsyncCallbackFails_Should_FaultReturnedTask()
        {
            _imposter
                .CountAsync(ReadOnlySpanArg<char>.Any())
                .ReturnsAsync(3)
                .Callback(_ => Task.FromException(new InvalidOperationException()));

            await Should.ThrowAsync<InvalidOperationException>(() =>
                Instance.CountAsync("abc".AsSpan())
            );
        }

        [Fact]
        public async Task Given_OutParameter_When_AsyncCallbackFails_Should_FaultReturnedTask()
        {
            var imposter = new IAwaitableByReferenceSutImposter();
            imposter
                .GetAsync(OutArg<int>.Any())
                .ReturnsAsync(1)
                .Callback(
                    (out int count) =>
                    {
                        count = 2;
                        return Task.FromException(new InvalidOperationException());
                    }
                );

            await Should.ThrowAsync<InvalidOperationException>(() =>
                imposter.Instance().GetAsync(out _)
            );
        }

        private static async Task FailAfterYield(int value)
        {
            await Task.Yield();
            throw new InvalidOperationException(value.ToString());
        }
    }
}
