# Release notes

## Unreleased: invocation history

Typed invocation snapshots expose recorded method calls, property assignments, indexer accessors, and event subscriptions and unsubscriptions. The following existing behaviors also change:

- Generic verification follows the caller's type arguments. When an `object` setup supplies the outcome for a `string` call through an adapter, inspect and verify the `string` invocation.
- Unmatched Explicit-mode calls on recording members are retained with their synchronous exception, so subsequent count checks can observe those attempts.
- Property setter history is published after its callbacks finish. Verification inside a setter callback does not yet see that assignment. Throwing method calls become visible after user `finally` blocks complete so final `out` values are retained.

!!! note "Allocation and ordering costs"
    A sequence stamp and atomic increment provide global entry order. Setter and event history allocates its first storage segment on the first interaction; snapshot inspection materializes records. Span predicates may require a separate private entry copy. These changes do not promise unchanged latency or first-call allocation. See [history allocation ceilings](invocation-history.md#performance-costs) and [measured results and limitations](invocation-contention.md).
