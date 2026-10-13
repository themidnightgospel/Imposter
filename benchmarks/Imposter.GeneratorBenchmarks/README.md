# Generator allocation benchmark

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
Use `/tmp/imposter-generator-after` for the second run, then check output identity:

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
