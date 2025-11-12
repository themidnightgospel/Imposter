# Tests Module

This document summarizes conventions and practical guidance for tests in the Imposter repository.

## Structure
- Tests live under `tests/Imposter.Tests/Features/...`, grouped by feature area.
- Generated test artifacts are placed under `tests/Imposter.Tests/GeneratedFiles/...`; do not edit `.g.cs` files directly.

## Concurrency Tests
- Avoid creating large bursts of `Task.Run(...)` gated by `ManualResetEventSlim`/`CountdownEvent`. This pattern can cause thread-pool starvation and slowdowns.
- Prefer async-friendly fan-out patterns:
  - Issue async work directly and await with `Task.WhenAll(...)`.
  - If a start barrier is needed, gate with `TaskCompletionSource` using `TaskCreationOptions.RunContinuationsAsynchronously`.
- Example (fan-out without extra thread-pool work items):
  ```csharp
  var tasks = Enumerable.Range(0, N)
      .Select(_ => builder.AsyncEvent.RaiseAsync(this, EventArgs.Empty))
      .ToArray();
  await Task.WhenAll(tasks);
  ```

## Logging and Artifacts
- For any temporary test outputs (e.g., diagnostics, logs), write to the OS temp directory.

## Do Not Edit Generated Files
- Any file ending with `.g.cs` is generated and should not be modified by hand. Update the generator instead.

