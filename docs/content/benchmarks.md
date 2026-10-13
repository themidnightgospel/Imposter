# Benchmarks

Overview of performance measurements using BenchmarkDotNet. The suite compares Imposter with popular impersonation libraries for common setup and invocation patterns.

## What’s Measured

- Setup throughput: configuring return values for a simple method.
- Invocation throughput: calling the configured method.
- Parameterized iterations: 1, 10, 100, 1000 setups + calls.

Primary scenario (see `benchmarks/Imposter.Benchmarks/ImposterVsAlternatives/SimpleMethodMockingBenchmarks.cs`), run with Moq, NSubstitute, FakeItEasy, Rocks and Imposter:

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/benchmarks/Imposter.Benchmarks/ImposterVsAlternatives/SimpleMethodMockingBenchmarks.cs#L109"}
public interface ICalculator { int Square(int input); }
```

Additional microbenchmarks cover generator internals (syntax builders) and struct behaviors.

## Benchmark Results

![Benchmark results for Moq, NSubstitute, FakeItEasy, Rocks and Imposter](https://raw.githubusercontent.com/themidnightgospel/Imposter/benchmark-results/simple-method.svg)

The [Benchmarks workflow](https://github.com/themidnightgospel/Imposter/actions/workflows/benchmarks.yml) reruns this comparison on a GitHub-hosted runner after every change to the code on master, so the table always shows the latest build. Shared runners are slower and noisier than a desktop machine: compare the libraries within the table rather than the absolute timings. Allocations are exact.

The same results are available as [Markdown](https://github.com/themidnightgospel/Imposter/blob/benchmark-results/simple-method.md) and as the [BenchmarkDotNet JSON report](https://github.com/themidnightgospel/Imposter/blob/benchmark-results/simple-method.json).

## Run Locally

```bash
dotnet run -c Release --project benchmarks/Imposter.Benchmarks/Imposter.Benchmarks.csproj -p:ROSLYN_VERSION=5.0
```

This runs the comparison above. Pass a filter to run another benchmark, for example `-- --filter *ComplexMethodMockingBenchmark*`. BenchmarkDotNet writes its reports (Markdown, CSV, HTML and JSON) to `BenchmarkDotNet.Artifacts/` in the directory you run it from.
