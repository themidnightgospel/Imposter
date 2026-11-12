# Invocation sequence contention

The process-wide sequence supports ordering across imposter instances. It adds a shared atomic increment, which can contend when tests invoke many independent imposters concurrently.

## Measure contention

[InvocationContentionBenchmarks](https://github.com/themidnightgospel/Imposter/blob/master/benchmarks/Imposter.Benchmarks/InvocationContentionBenchmarks.cs) compares local and global sequence counters with identical records, scheduling, and publication. It also measures generated method calls with 1, 4 and 8 workers sharing one imposter or using independent imposters. Each batch retains a bounded history of 32,768 calls.

The synthetic local/global comparison isolates sequence-counter ownership. Generated calls also include lookup, outcome execution, and result/exception storage, so comparing them with the synthetic recorder is not a before/after implementation comparison.

Run an initial check with `IMPOSTER_BENCHMARK_JOB=short`:

```sh
dotnet run -c Release --project benchmarks/Imposter.Benchmarks/Imposter.Benchmarks.csproj -p:ROSLYN_VERSION=5.0 -- --filter '*InvocationContentionBenchmarks*'
```

This is a **manual benchmark**, built with the regular benchmark project and excluded from the comparative CI filter. Use an otherwise idle, representative machine and a longer run when interpreting latency or throughput. Record both revision hashes, SDK/runtime versions, hardware, benchmark settings and confidence intervals with results. This page does not publish measurements from a superseded implementation or claim unchanged latency.

## Compare revisions

The regular benchmark project contains `BeforeAfterSetupBenchmarks`, `FirstHistoryVerificationBenchmarks`, `EventAccessorComparisonBenchmarks`, and `EventAccessorPairComparisonBenchmarks`. These fixtures use APIs available on baseline `55dca6b2`. Run identical fixture copies in separate baseline and candidate checkouts, with the same SDK/runtime and sequential runs on an idle host:

```sh
dotnet run -c Release --project benchmarks/Imposter.Benchmarks -p:ROSLYN_VERSION=5.0 -- --filter '*BeforeAfterSetupBenchmarks*' '*FirstHistoryVerificationBenchmarks*' '*EventAccessorComparisonBenchmarks*' '*EventAccessorPairComparisonBenchmarks*' '*MethodHistoryCallBenchmarks*' '*MethodHistoryVerificationBenchmarks*' '*PropertyRuntimeBenchmarks*' '*CallAllocationBenchmarks.CallbackOnlyIndexer*'
```

Set `IMPOSTER_BENCHMARK_JOB=short` for an initial check. First-verification histories are prepared outside timing and verified once; repeated verification checks the same history again. The event-pair fixture retains at most one handler, while the subscription-only fixture deliberately measures a growing handler list. Inspect allocation and latency separately; a lower amortized allocation does not establish better throughput.

## First-interaction allocation limits

Compact setter and event histories allocate their first segment on demand. Warm averages do not describe a fresh imposter's first interaction. Snapshot materialization allocates separately. The [history performance section](invocation-history.md#performance-costs) documents the warm ceilings for this stage.

[ColdInvocationAllocationTests](https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/CallAllocations/ColdInvocationAllocationTests.cs) pins these x64 Release ceilings, including construction, optional setup, `Instance()` and the first interaction:

| Fixture | Ceiling per fresh imposter |
| --- | ---: |
| Property setter | 1,136 bytes |
| Property setter with callback | 1,352 bytes |
| Event removal | 1,000 bytes |
| Cached-handler subscription/removal pair | 1,872 bytes |

These are regression limits for the stated fixtures, not measured costs for every imposter. Warm-up uses separate fresh imposters. Every measured imposter includes its first segment. The test takes the smallest total from five batches of 100 fresh imposters and compares it with 100 times the ceiling, without rounding per-imposter allocations. A fixed per-imposter allocation increase affects every batch.

```sh
dotnet test tests/Imposter.Tests/Imposter.Tests.csproj -c Release -p:ROSLYN_VERSION=5.0 --filter FullyQualifiedName~ColdInvocationAllocationTests
```

!!! info "Next steps"
    See [Invocation history](invocation-history.md) for captured values, snapshot semantics and warm allocation ceilings.
