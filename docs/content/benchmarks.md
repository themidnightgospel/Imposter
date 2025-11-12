# Benchmarks

Overview of performance measurements using BenchmarkDotNet. The suite compares Imposter with popular mocking libraries for common setup and invocation patterns.

## What’s Measured

- Setup throughput: configuring return values for a simple method.
- Invocation throughput: calling the configured method.
- Parameterized iterations: 1, 10, 100, 1000 setups + calls.

Primary scenario (see `ImposterVsOthersBenchmark.cs`):

```csharp
public interface ICalculator { int Square(int input); }
// Benchmarks compare: Imposter vs Moq vs NSubstitute
```

Additional microbenchmarks cover generator internals (syntax builders) and struct behaviors.

## Benchmark Results

```text
BenchmarkDotNet v0.15.2, Windows 11 (10.0.26200.6899)
13th Gen Intel Core i9-13900HX 2.20GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2


| Method   | Iteration | Mean            | Error           | StdDev          | Gen0      | Gen1      | Gen2     | Allocated   |
|--------- |---------- |----------------:|----------------:|----------------:|----------:|----------:|---------:|------------:|
| Mock     | 1         |     57,888.4 ns |     1,152.97 ns |     1,861.83 ns |    0.6104 |    0.4883 |        - |    13.06 KB |
| NSub     | 1         |      1,792.1 ns |        35.42 ns |        48.48 ns |    0.4120 |         - |        - |     7.73 KB |
| Imposter | 1         |        187.8 ns |         3.41 ns |         3.02 ns |    0.1304 |    0.0010 |        - |      2.4 KB |
| Mock     | 10        |    561,381.0 ns |     9,870.99 ns |     8,750.38 ns |    5.8594 |    4.8828 |        - |    115.8 KB |
| NSub     | 10        |     11,091.9 ns |       220.34 ns |       506.26 ns |    1.5869 |         - |        - |    29.18 KB |
| Imposter | 10        |      1,897.5 ns |        25.95 ns |        23.01 ns |    1.2169 |    0.0877 |        - |    22.37 KB |
| Mock     | 100       |  5,659,959.7 ns |   106,879.17 ns |    89,248.95 ns |   62.5000 |   46.8750 |        - |  1417.69 KB |
| NSub     | 100       |    317,054.3 ns |     5,962.06 ns |     5,576.92 ns |   13.1836 |    4.3945 |        - |   247.15 KB |
| Imposter | 100       |     34,266.0 ns |       658.78 ns |       809.04 ns |   12.0239 |    5.6152 |        - |   222.05 KB |
| Mock     | 1000      | 87,595,090.5 ns | 1,733,629.98 ns | 1,536,817.82 ns | 2000.0000 | 1000.0000 | 666.6667 | 42282.98 KB |
| NSub     | 1000      | 25,822,022.4 ns |   513,318.30 ns |   630,401.02 ns |  125.0000 |   62.5000 |        - |  2420.71 KB |
| Imposter | 1000      |  2,635,074.0 ns |    51,717.64 ns |    61,566.17 ns |  117.1875 |  101.5625 |        - |  2218.93 KB |
```

## Run Locally

```bash
dotnet run -c Release -p benchmarks/Imposter.Benchmarks/Imposter.Benchmarks.csproj
```

BenchmarkDotNet will emit results under `benchmarks/Imposter.Benchmarks/BenchmarkDotNet.Artifacts/` (Markdown, CSV, and summary files).

!!! tip "Pro tip"
    - Run in `Release` without the debugger attached for consistent results.
    - Close background workloads and pin your .NET SDK version.
    - Compare mean time and allocated bytes; prefer allocation-free setups where possible.

## Contributing Results

- Run the suite, then add a brief summary of your findings here (date, runtime, CPU) if you want to share representative snapshots.
- For detailed tables, link to the generated Markdown in `BenchmarkDotNet.Artifacts/results` or paste selected rows.

See also: [Why Imposter?](why-imposter.md)
