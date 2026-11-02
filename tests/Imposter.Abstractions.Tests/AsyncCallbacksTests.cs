using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class AsyncCallbacksTests
{
    [Fact]
    public void GivenNullOrCompletedTasks_WhenTracking_ShouldKeepNone()
    {
        List<Task>? callbackTasks = null;

        AsyncCallbacks.Track(ref callbackTasks, null);
        AsyncCallbacks.Track(ref callbackTasks, Task.CompletedTask);

        callbackTasks.ShouldBeNull();
    }

    [Fact]
    public void GivenPendingAndFaultedTasks_WhenTracking_ShouldKeepThemInOrder()
    {
        List<Task>? callbackTasks = null;
        var pending = new TaskCompletionSource<bool>().Task;
        var faulted = Task.FromException(new InvalidOperationException());

        AsyncCallbacks.Track(ref callbackTasks, pending);
        AsyncCallbacks.Track(ref callbackTasks, faulted);

        callbackTasks.ShouldBe(new[] { pending, faulted });
    }

    [Fact]
    public async Task GivenPendingCallbackTask_WhenCompletingAfterIt_ShouldReturnTheResultOnceItCompletes()
    {
        var callback = new TaskCompletionSource<bool>();

        var completion = AsyncCallbacks.CompleteAfter(Task.FromResult(3), [callback.Task]);
        completion.IsCompleted.ShouldBeFalse();
        callback.SetResult(true);

        (await completion).ShouldBe(3);
    }

    [Fact]
    public async Task GivenTwoFailedCallbackTasks_WhenCompletingAfterThem_ShouldFailWithTheFirst()
    {
        var completion = AsyncCallbacks.CompleteAfter(
            Task.CompletedTask,
            [
                Task.FromException(new ArgumentException()),
                Task.FromException(new InvalidOperationException()),
            ]
        );

        await Should.ThrowAsync<ArgumentException>(() => completion);
    }

    [Fact]
    public async Task GivenFailedResult_WhenCompletingAfterCallbacks_ShouldFailWithTheResultsException()
    {
        var completion = AsyncCallbacks.CompleteAfter(
            Task.FromException<int>(new ArgumentException()),
            [Task.FromException(new InvalidOperationException())]
        );

        await Should.ThrowAsync<ArgumentException>(() => completion);
    }
}
