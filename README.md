<p style="text-align: center;">
  <img src="docs/imposter_logo.png" alt="Imposter logo" width="140" />
</p>

<p style="text-align: center;">
    The Fastest and Most Memory-Efficient Mocking Library
</p>

[![Build, Test, and Format verification](https://github.com/themidnightgospel/Imposter/actions/workflows/build-and-test.yml/badge.svg?branch=master)](https://github.com/themidnightgospel/Imposter/actions/workflows/build-and-test.yml)
[![Nuget](https://img.shields.io/nuget/v/Imposter.svg)](https://www.nuget.org/packages/Imposter)

Visit the https://themidnightgospel.github.io/Imposter/ for more information

## 🚀 Quick Start

Add nuget package reference:

```bash
dotnet add package Imposter
```

Includes the source generator (analyzer) and runtime abstractions in a single package.

Use `[GenerateImposter]` attribute to generate an imposter

```csharp
using System.Threading.Tasks;
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(ICalculator))]

public interface ICalculator
{
    int Add(int a, int b);
}


// c# 14
var imposter = ICalculator.Imposter();

// c# 8 - 13
// var imposter = new ICalculatorImposter();

imposter.Add(Arg<int>.Any(), Arg<int>.Any()).Returns(42);

imposter.Instance().Add(1, 2); // 42
```

Learn more: https://themidnightgospel.github.io/Imposter/methods/explicit-vs-implicit/

## ✨ Fully Featured:

- [Method Impersonation](https://themidnightgospel.github.io/Imposter/methods/) 
- [Property Impersonation](https://themidnightgospel.github.io/Imposter/properties/)
- [Indexer Impersonation](https://themidnightgospel.github.io/Imposter/indexers/)
- [Event Impersonation](https://themidnightgospel.github.io/Imposter/events/)
- [Implicit & Explicit Modes](https://themidnightgospel.github.io/Imposter/methods/explicit-vs-implicit/)
- [Use Base implementation](https://themidnightgospel.github.io/Imposter/methods/base-implementation/)

## ⚡ The Fastest and Most Memory-Efficient Mocking Library

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.6899)
13th Gen Intel Core i9-13900HX 2.20GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.100
[Host]     : .NET 8.0.21 (8.0.21, 8.0.2125.47513), X64 RyuJIT x86-64-v3
DefaultJob : .NET 8.0.21 (8.0.21, 8.0.2125.47513), X64 RyuJIT x86-64-v3


| Method      | Iteration | Mean            | Error           | StdDev          | Median           | Gen0      | Gen1      | Gen2     | Allocated   |
|-------------|---------- |----------------:|----------------:|----------------:|-----------------:|----------:|----------:|---------:|------------:|
| Moq         | 1         | **69,346.1 ns** |       366.12 ns |       324.56 ns |      69,265.5 ns |    0.6104 |    0.4883 |        - | **13.05 KB** |
| NSubstitute | 1         | **1,976.2 ns** |        39.39 ns |        90.51 ns |       1,939.5 ns |    0.4120 |         - |        - | **7.84 KB** |
| Imposter    | 1         | **194.3 ns** |         3.57 ns |         3.16 ns |         193.8 ns |    0.1304 |    0.0010 |        - | **2.4 KB** |
| Moq        | 10        | **686,282.9 ns** |    13,239.26 ns |    15,246.35 ns |     684,281.5 ns |    5.8594 |    4.8828 |        - | **115.73 KB** |
| NSubstitute | 10        | **11,201.6 ns** |       203.75 ns |       242.55 ns |      11,133.8 ns |    1.5869 |         - |        - | **29.29 KB** |
| Imposter    | 10        | **1,896.7 ns** |        37.89 ns |        47.91 ns |       1,891.9 ns |    1.2169 |    0.0877 |        - | **22.37 KB** |
| Moq        | 100       | **6,804,897.3 ns** |   131,630.72 ns |   192,942.62 ns |   6,795,509.4 ns |   62.5000 |   46.8750 |        - | **1416.91 KB** |
| NSubstitute | 100       | **335,390.6 ns** |     6,630.81 ns |     7,636.05 ns |     336,541.4 ns |   13.1836 |    3.9063 |        - | **247.26 KB** |
| Imposter    | 100       | **34,011.7 ns** |       676.96 ns |     1,500.09 ns |      33,901.4 ns |   12.0239 |    5.6152 |        - | **222.05 KB** |
| Moq        | 1000      | **99,710,929.5 ns** | 1,970,379.84 ns | 2,697,078.40 ns | 100,011,666.7 ns | 2000.0000 | 1000.0000 | 666.6667 | **42275.19 KB** |
| NSubstitute | 1000      | **26,986,939.0 ns** |   534,626.03 ns |   500,089.52 ns |  26,912,768.8 ns |  125.0000 |   62.5000 |        - | **2420.82 KB** |
| Imposter    | 1000      | **2,452,970.7 ns** |    28,770.60 ns |    25,504.39 ns |   2,453,516.0 ns |  117.1875 |  101.5625 |        - | **2218.93 KB** |

See the [benchmark](https://github.com/themidnightgospel/Imposter/blob/3172c333603fd2d76031b20be39753a9b62f31c3/benchmarks/Imposter.Benchmarks/ImposterVsMoqVsNSubstitute/SimpleMethodMockingBenchmarks.cs#L12)

## Docs
Docs: https://themidnightgospel.github.io/Imposter/

## License

Licensed under the MIT License. See LICENSE.txt for details.
