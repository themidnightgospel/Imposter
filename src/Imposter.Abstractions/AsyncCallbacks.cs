using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>
/// Makes the task an impersonated async method returns wait for the tasks its callbacks return, so a callback's
/// failure reaches the caller and the call completes only after its callbacks have.
/// </summary>
/// <remarks>
/// Callbacks still start when the call is made, in registration order, because they can take <c>ref</c>, <c>out</c>
/// and span parameters that a continuation couldn't carry. It is public for generated imposters only and may change
/// with the generator in the same package.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class AsyncCallbacks
{
    /// <summary>
    /// Keeps a callback's task for <see cref="CompleteAfter(Task, List{Task})"/> unless it already ran to completion,
    /// so callbacks that finish synchronously cost nothing.
    /// </summary>
    /// <param name="callbackTasks">The tasks kept so far; created on the first one kept.</param>
    /// <param name="callbackTask">The task a callback returned. A null one counts as completed.</param>
    public static void Track(ref List<Task>? callbackTasks, Task? callbackTask)
    {
        if (callbackTask is null || callbackTask.Status == TaskStatus.RanToCompletion)
        {
            return;
        }

        (callbackTasks ??= []).Add(callbackTask);
    }

    /// <summary>
    /// A task that completes after <paramref name="result"/> and then each callback task in order, and fails with the
    /// first failure among them.
    /// </summary>
    /// <param name="result">The task the arranged outcome produced.</param>
    /// <param name="callbackTasks">The callback tasks <see cref="Track"/> kept.</param>
    public static async Task CompleteAfter(Task result, List<Task> callbackTasks)
    {
        await result.ConfigureAwait(false);
        await AwaitInOrder(callbackTasks).ConfigureAwait(false);
    }

    /// <inheritdoc cref="CompleteAfter(Task, List{Task})"/>
    /// <typeparam name="T">The method's result type.</typeparam>
    public static async Task<T> CompleteAfter<T>(Task<T> result, List<Task> callbackTasks)
    {
        var value = await result.ConfigureAwait(false);
        await AwaitInOrder(callbackTasks).ConfigureAwait(false);
        return value;
    }

    private static async Task AwaitInOrder(List<Task> callbackTasks)
    {
        foreach (var callbackTask in callbackTasks)
        {
            await callbackTask.ConfigureAwait(false);
        }
    }
}
