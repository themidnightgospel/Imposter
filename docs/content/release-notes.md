# Release notes

## Unreleased: invocation history and verification

Typed invocation snapshots, clearing and reset, completeness checks, ordered verification, and richer failure messages add new APIs. The following existing behaviors also change:

- Generic verification now uses the caller's type arguments. If an `object` setup supplies the outcome for a `string` call through a compatible adapter, inspect and verify the `string` invocation. The compatible setup still counts as used.
- An Explicit-mode property getter records an attempted access before it throws for missing configuration. Count verification and completeness checks therefore see that attempt.
- Property setter history is published after its callbacks finish. Verification inside a setter callback does not yet see that assignment. Throwing method calls likewise become visible after callback `finally` blocks complete, preserving final `out` values.
- Successful count checks mark matching interactions covered. Matchers must be deterministic because a successful check may evaluate each recorded input twice. Failed checks leave coverage unchanged.
- Failure text now includes member names, matcher descriptions and, where available, other calls or configured outcomes. Existing assertions against exact message text may need updating; legacy exception constructors remain available.
- Event count-verification failures list all recorded actions for the verified member and action, including nonmatching entries. The reported count still includes only matcher matches. This message-detail change accompanies completeness checks; failure formatting does not re-evaluate matcher predicates.
- Generated count failures now name the member and matchers, list other interactions when that member has no calls, and append an optional `Because:` line. Legacy exception constructors keep their previous text.
- `MissingImposterException.Message` now reports actual arguments and a `Configured setups for <member>:` block with two-space indentation. Descriptions retain original outcomes after partial or full sequence consumption.
- Invocation text now brackets setter values, event handlers and exception values. Getter unused-setup findings include outcome labels such as ` -> Returns`. Matcher `ToString()` descriptions and safe `<unprintable TypeName>` fallbacks are now part of the diagnostic format.

!!! note "Performance is a tradeoff"
    Global entry ordering adds sequence metadata and an atomic increment. Segmented setter and event history reduces amortized allocation for longer histories but allocates a segment on the first interaction. Later completeness and diagnostic features also add setup and construction state. These APIs do not promise unchanged latency or first-call allocation. See [allocation measurements and limitations](invocation-contention.md) and the [history allocation ceilings](invocation-history.md#performance-costs).
