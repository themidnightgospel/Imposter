# Release notes

## Unreleased: invocation history and verification

Typed invocation snapshots, clearing and reset, completeness checks, and ordered verification add new APIs. The following existing behaviors also change:

- Generic verification now uses the caller's type arguments. If an `object` setup supplies the outcome for a `string` call through a compatible adapter, inspect and verify the `string` invocation. The compatible setup still counts as used.
- An Explicit-mode property getter records an attempted access before it throws for missing configuration. Count verification and completeness checks therefore see that attempt.
- Property setter history is published after its callbacks finish. Verification inside a setter callback does not yet see that assignment. Throwing method calls likewise become visible after callback `finally` blocks complete, preserving final `out` values.
- Successful count checks mark matching interactions covered. Matchers must be deterministic because a successful check may evaluate each recorded input twice. Failed checks leave coverage unchanged.
- Ordered verification failures identify the first unmatched step and list the involved imposters' interactions in entry order.

- Event count-verification failures list all recorded actions for the verified member and action, including nonmatching entries. The reported count still includes only matcher matches. This message-detail change accompanies completeness checks; failure formatting does not re-evaluate matcher predicates.

!!! note "Performance is a tradeoff"
    Global entry ordering adds sequence metadata and an atomic increment. Segmented setter and event history reduces amortized allocation for longer histories but allocates a segment on the first interaction. Completeness tracking also adds setup and construction state. These APIs do not promise unchanged latency or first-call allocation. See [allocation measurements and limitations](invocation-contention.md) and the [history allocation ceilings](invocation-history.md#performance-costs).
