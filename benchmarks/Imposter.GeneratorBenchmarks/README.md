# Generator and consumer compilation benchmark

This harness measures the generator on 20 interface targets with 10 ordinary methods,
10 generic methods, 10 async methods returning `Task<T>`, 10 properties, an event, and an
indexer each. It runs both plain targets and targets with `<TEntity, TKey>` parameters.

Cold runs use a fresh compilation and driver. Edit runs retain the driver and replace
an unrelated syntax tree, forcing semantic models to be reconsidered while generated
output can be reused. Parsing and compilation construction happen outside the measured
region. The reported values are medians of 15 runs after 3 warmups, using
`GC.GetAllocatedBytesForCurrentThread` and `Stopwatch`. MB means 1,000,000 bytes.
The symbol cache is not artificially cleared: fresh compilation symbols exercise the
same cache lifetime as normal generator runs.

Run from the repository root with the .NET 10 SDK:

```sh
dotnet run --project benchmarks/Imposter.GeneratorBenchmarks -c Release \
  -p:HUSKY=0 -p:ROSLYN_VERSION=5.0 -- /tmp/imposter-generator-before
```

Use a new output directory each time. The harness writes `measurements.csv` and all
generated sources under `sources/`, and fails on generator errors or compilation errors
in any output. To compare changes, run the same harness in the baseline and modified
checkouts (copy the harness's source and project files to the baseline checkout if it
does not yet contain them), keeping SDK, Roslyn version, and build options identical.
Use `/tmp/imposter-generator-after` for the second run, then inspect output differences
(identity is expected only for changes that preserve emitted code):

```sh
diff -rq /tmp/imposter-generator-before/sources /tmp/imposter-generator-after/sources
```

The output includes the plain and generic workloads before and after an unrelated edit,
plus these 14 named probe sets in `RenameProbes.cs`:

- `keyword-var`, `keyword-_`, `keyword-record`, `keyword-partial`
- `method-keywords`, `method-helper-members`, `suffix-collisions`
- `imposter-class-name`, `containing-imposter-name`, `nested-shadowing`
- `constructed-containing-type`, `inheritance-and-constraints`
- `arrays-tuples-nullability`, `class-constructor-and-method`

These probes exercise renaming, nullable annotations, constructed containing types,
constraints, and stable public names. Unit tests additionally compare formatting of
pointer and function-pointer types against display-part substitution. Existing runtime
snapshots and the full test matrix provide a wider output-identity check.

## Consumer compilation measurements

`compilation.csv` records generated characters and lines, parse time, bind+emit time,
assembly size, and method-body IL bytes for both workloads and for single-property and
single-method probes. The workload rows without generated code provide the corresponding
source-only compilation cost. Generation is measured separately in `measurements.csv`.

Compilation measurements use one warmup and the median of five runs. Each run reparses
the generated text and creates a fresh compilation with Release optimization before
emitting to a memory stream. Parsing is timed independently; bind+emit includes binding,
lowering, optimization and writing the PE. The original input syntax trees and metadata
references are prepared outside those timings. Assembly and IL sizes include the original
target declarations; IL bytes sum method bodies, excluding metadata and runtime helpers in
the separate Abstractions assembly. Source-only rows parse no generated text, so their
parse times are effectively zero. These are in-process warm-reference measurements, not
end-to-end MSBuild timings.

## Issue #407: first phase, ordinary properties

This phase follows the issue discussion's property-first spike. `PropertyGetter<T>` and
`PropertySetter<T>` move outcome factories, callbacks, strict-mode checks, invocation
counts/history, verification and base-accessor behavior into Abstractions. They reuse
`ReturnValueSequence<T>` and `CallbackCollection<T>` already on master. `PropertyValue<T>`
shares default auto-property storage. Generated internal accessor classes inherit these
types, avoiding another object per accessor, and keep the existing public fluent interfaces.
The runtime types are public only so consumer assemblies can reference them, hidden with
`EditorBrowsable(Never)`, and shipped with the version-matched generator. Storage stays
internal/private and fluent interfaces do not expose the runtime types.

Init-only base assignments still execute in the generated accessor, after the shared
setter records the invocation and runs callbacks. Span and other ref-struct properties
retain their specialized emitted implementation. Plain methods, generic methods, indexers
and events remain later phases; this change does not close the entire issue.

Targets for this phase are fewer than 270 lines for the complete C# 14 single-property file
(previously 390), at least 5% fewer generated characters on the mixed workload, and no
regression in the existing call-allocation budgets. Bind+emit should improve on the mixed
workload; timing is recorded rather than used as a flaky test threshold. The single-method
probe stays at 454 lines in this phase; a future plain-method extraction should target fewer
than 300 without changing its fluent interfaces or required signature-specific invocation
code. The generator regression test enforces a 260-line ceiling for C# 9, which omits the
C# 14 extension block, and compiles use of explicitly named fluent interfaces.

All four Roslyn snapshot sets are regenerated. Validation includes the full behavior and
generator suites, generated-only type visibility, allocation and concurrent collection
lifecycle tests, and an executable packed consumer. Final verification runs the net8.0
test projects on native ARM64 .NET 8.0.0, including Roslyn 4.0/4.4/4.14 behavior suites and
Roslyn 4.0/4.14 generator suites; Roslyn 5.0 behavior tests run on .NET 10.0.11. The packed
consumer executes on both runtimes and covers auto-properties, strict mode, sequences,
callbacks, exceptions, base accessors and generic nullable/reference/value/delegate types.

### Recorded results

Baseline: upstream master `9f2ff3af`. After: ordinary-property extraction in this change.
Both use the same updated harness, .NET 10.0.11 (SDK 10.0.400), Roslyn 5.0.0 and macOS
26.6.2 on Apple M3 Pro ARM64, Release without verification flags. Raw CSVs are in
[`results/issue-407`](results/issue-407). Timings are medians from one process per revision;
they vary with JIT, GC and machine load and are not confidence intervals.

| Workload | Lines before → after | Characters before → after | Bind+emit ms before → after | Assembly bytes before → after | Method IL bytes before → after |
| --- | ---: | ---: | ---: | ---: | ---: |
| plain | 353,660 → 327,860 | 14,391,220 → 13,325,420 | 5,510.91 → 5,263.18 | 7,624,704 → 7,009,280 | 1,495,349 → 1,299,949 |
| generic | 353,660 → 327,860 | 14,456,340 → 13,387,540 | 5,132.36 → 4,222.22 | 10,769,408 → 9,937,408 | 1,505,229 → 1,307,229 |
| single-property | 390 → 261 | 14,825 → 9,655 | 21.66 → 13.84 | 11,264 → 7,168 | 1,342 → 362 |
| single-method | 454 → 454 | 16,790 → 16,790 | 11.45 → 9.30 | 11,776 → 11,776 | 1,663 → 1,663 |

The mixed workloads lose 7.3% of their lines and 7.4% of their characters. Cold generator
allocations fall from 411.27 MB to 382.67 MB (plain) and 413.04 MB to 384.37 MB (generic).
Cold generation timings are 540.52 → 551.50 ms and 597.39 → 546.46 ms respectively, so this
run does not establish a consistent generator-time improvement. Unrelated-edit allocations
remain approximately unchanged. Parse timings are 414.70 → 329.71 ms (plain) and
307.21 → 360.07 ms (generic); do not infer a universal parse-time win from this run.

The single-property file loses 33.1% of its lines, 34.9% of its characters, and 73.0% of its
method IL. The method-only probe's source and IL sizes are unchanged. The shared runtime
assembly is excluded from these consumer assembly sizes: this is a reduction in repeated
consumer code, not removal of the behavior from the package.

The eight Imposter cases in `ImposterVsAlternatives` were also run before and after with
BenchmarkDotNet ShortRun (one launch, three warmups, three measured iterations):

```sh
IMPOSTER_BENCHMARK_JOB=short HUSKY=0 dotnet run \
  --project benchmarks/Imposter.Benchmarks -c Release -p:ROSLYN_VERSION=5.0 \
  -- --filter '*ImposterVsAlternatives*Imposter' --artifacts /tmp/imposter-runtime-results
```

`baseline-runtime.csv` and `properties-runtime.csv` retain means, standard deviations,
confidence margins and bytes per operation. Allocations are identical in all eight cases.
The timing intervals overlap, but this short run cannot exclude a small throughput
regression. The simple one-iteration mean is 90.31 → 101.91 ns; the other simple cases
change by -0.02% to +1.33%, and complex cases by +0.11% to +4.56%. These benchmarks use
methods, whose generated code is unchanged. They do not measure property-access latency.

### Property runtime verification

`PropertyRuntimeBenchmarks` measures the accessors changed in this phase. Review caught
an avoidable generic fallback-delegate cache lookup on configured reads. The getter now
uses a null sequence fallback and returns `default` directly when publication of the first
setup is still in progress. A regression test races first setup with reads for value and
reference types, checks every result, and verifies the final outcome and invocation count.
This optimization changes the shared runtime only; the emitted-source measurements above
are unchanged.

Run the same benchmark source on baseline `9f2ff3af` and the modified checkout:

```sh
IMPOSTER_BENCHMARK_JOB=short HUSKY=0 dotnet run \
  --project benchmarks/Imposter.Benchmarks -c Release -p:ROSLYN_VERSION=5.0 \
  -- --filter '*PropertyRuntimeBenchmarks*' --warmupCount 8 --iterationCount 15 \
  --artifacts /tmp/imposter-property-runtime
```

These results use one launch, eight warmups and fifteen measured iterations on the same
.NET 10.0.11 / Apple M3 Pro environment as above. Setter methods create one imposter per
1,000-assignment batch so retained history stays bounded and BenchmarkDotNet can collect
long steady-state iterations. Construction and callback registration are included and
amortized over each batch. Getter setup is excluded. The allocation column is BDN's rounded
bytes per operation; separate allocation tests still enforce zero bytes per getter call and
at most 32 bytes per setter call after setup.

| Method | Baseline mean (ns) | Final mean (ns) | Allocated bytes, before / after |
| --- | ---: | ---: | ---: |
| DefaultGetter | 1.185 | 1.358 | 0 / 0 |
| ConfiguredGetter | 1.801 | 1.472 | 0 / 0 |
| ConfiguredReferenceGetter | 1.779 | 1.468 | 0 / 0 |
| GetterWithCallback | 2.598 | 1.925 | 0 / 0 |
| Setter | 7.434 | 6.488 | 32 / 32 |
| SetterWithCallback | 8.923 | 7.536 | 33 / 33 |

Configured getter and setter means improve in this run. The default-getter mean is higher
by 0.173 ns; its confidence intervals overlap (baseline ±0.214 ns, final ±0.182 ns), so that
case is inconclusive. Allocation counts do not increase. These are single-machine results,
not a universal throughput guarantee. `baseline-property-runtime.csv` and
`properties-property-runtime.csv` retain sample counts, standard deviations and confidence
margins, including cases where BDN removed outliers.

## Baseline with async methods

Master `af8b195e`, the first workload with async methods (#374). Windows 11 x64, Intel Core
i9-13900HX, .NET 10.0.12, Roslyn 5.0.0, in Release without verification-build flags. Adding
the async methods raised each scenario by 30% to 32% on the same machine, so compare a change
only with numbers from the same workload, not with the #370 table below.

| Scenario | Bytes |
| --- | ---: |
| Plain targets, unrelated edit | 5,314,520 |
| Generic targets, unrelated edit | 5,589,960 |
| Plain targets, cold generation | 406,254,952 |
| Generic targets, cold generation | 407,958,808 |

## Issue #370 measurements

Measured before #374 added the async methods to the workload.

Baseline: master `887e5089`. After: weak symbol-name cache and unchanged-name formatting
fast path. Both used the harness above, .NET 10.0.11 (SDK 10.0.400), Roslyn 5.0.0,
and Apple M3 Pro ARM64 on macOS 26.6.2, in Release without verification-build flags.

| Scenario | Before (bytes) | After (bytes) | Reduction |
| --- | ---: | ---: | ---: |
| Plain targets, unrelated edit | 5,031,976 | 4,026,008 | 20.0% |
| Generic targets, unrelated edit | 8,469,976 | 4,243,272 | 49.9% |
| Plain targets, cold generation | 313,693,872 | 312,811,320 | 0.3% |
| Generic targets, cold generation | 317,840,520 | 313,766,176 | 1.3% |

All 94 generated files matched byte for byte across the two runs. Timings and allocation
counts can vary slightly with runtime caches and GC bookkeeping; the table reports
medians, not a timing-performance claim. These numbers describe this committed workload,
which is not the original uncommitted benchmark from the issue.
