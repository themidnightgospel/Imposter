<p align="center">
  <img src="docs/imposter_logo.png" alt="Imposter logo" width="140" />
</p>

<p align="center">
    Fast and Memory-Efficient Mocking Library
</p>

[![Build, Test, and Format verification](https://github.com/themidnightgospel/Imposter/actions/workflows/build-and-test.yml/badge.svg?branch=master)](https://github.com/themidnightgospel/Imposter/actions/workflows/build-and-test.yml)
[![Nuget](https://img.shields.io/nuget/v/Imposter.svg)](https://www.nuget.org/packages/Imposter)

Imposter is a mocking library that's using roslyn source generators to achieve high performance and low memory footprint.

Visit the [Docs](https://themidnightgospel.github.io/Imposter/) for more information

## 🚀 Quick Start

Add nuget package reference:

```bash
dotnet add package Imposter
```

Pick an interface or non-sealed class that you would like to generate an imposter for.

Say we have a below interface

```csharp

namespace Application.Domain;

public interface ICalculator
{
    int Add(int a, int b);
}
```

Use `[GenerateImposter]` attribute in your **tests** project, this will generate an imposter

```csharp
[assembly: GenerateImposter(typeof(Application.Domain.ICalculator))]
```

Then use can use the generated imposter in your tests

```csharp
using System.Threading.Tasks;
using Imposter.Abstractions;

// c# 14
var imposter = ICalculator.Imposter();

// c# 9 - 13
// var imposter = new ICalculatorImposter();

imposter.Add(Arg<int>.Any(), Arg<int>.Any())
    .Returns(1)
    .Then()
    .Returns(2);

var calculator = imposter.Instance();

calculator.Add(1, 2); // 1
calculator.Add(1, 2); // 2
```

## ⚡ Built for speed

Benchmarks show Imposter running ~10× faster than NSubstitute and up to ~200× faster than Moq in common mocking scenarios. See benchmarks below.

## 🛡️ Strongly typed all the way

Loosely typed callbacks often lead to runtime exceptions. **Imposter** enforces strong typing across the entire mocking pipeline, ensuring errors are caught at compile time. see more [here](https://themidnightgospel.github.io/Imposter/latest/methods/callbacks/#method-callbacks)

## 🧵 Thread-safe by design

Imposter is built to work reliably in multi-threaded and parallel test environments.

## 🎭 Interfaces and classes

Not limited to interfaces — Imposter can impersonate non-sealed classes and their protected members.

## 🧬 Full generic support

Generics are fully supported.

## 🧩 Mock any member

Imposter supports not only methods, but also properties, events and indexers.

## ⏱️ Benchmark

We benchmarked the simple method-impersonation scenario: we set up a `Square` method to return `input * input` and ran it for 1, 10, 100, and 1000 iterations.

```csharp
public interface ICalculator
{
    int Square(int input);
}
```

| Method      | Iteration |     Execution Time | Allocated Memory |
|-------------|-----------|-------------------:|-----------------:|
| Moq         | 1         |        69,346.1 ns |         13.05 KB |
| NSubstitute | 1         |         1,976.2 ns |          7.84 KB |
| FakItEasy   | 1         |         2,006.7 ns |          5.84 KB |
| Imposter    | 1         |       **194.3 ns** |       **2.4 KB** |
| Moq         | 10        |       686,282.9 ns |        115.73 KB |
| NSubstitute | 10        |        11,201.6 ns |         29.29 KB |
| FakItEasy   | 10        |        12,399.0 ns |         38.81 KB |
| Imposter    | 10        |     **1,896.7 ns** |     **22.37 KB** |
| Moq         | 100       |     6,804,897.3 ns |       1416.91 KB |
| NSubstitute | 100       |       335,390.6 ns |        247.26 KB |
| FakItEasy   | 100       |       258,220.2 ns |       1033.38 KB |
| Imposter    | 100       |    **34,011.7 ns** |    **222.05 KB** |
| Moq         | 1000      |    99,710,929.5 ns |      42275.19 KB |
| NSubstitute | 1000      |    26,986,939.0 ns |       2420.82 KB |
| FakItEasy   | 1000      |    18,997,374.5 ns |      77101.74 KB |
| Imposter    | 1000      | **2,452,970.7 ns** |   **2218.93 KB** |


Benchmark Environment

```
BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.6899)
13th Gen Intel Core i9-13900HX 2.20GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.100
[Host]     : .NET 8.0.21 (8.0.21, 8.0.2125.47513), X64 RyuJIT x86-64-v3
DefaultJob : .NET 8.0.21 (8.0.21, 8.0.2125.47513), X64 RyuJIT x86-64-v3
```

<details>
<summary>Benchmarks on macOS (Apple M2 Pro)</summary>

Mean execution time

| Method      | Iteration | Mean              |
|-------------|-----------|------------------:|
| Moq         | 1         | 62,199.5 ns       |
| NSubstitute | 1         | 1,688.9 ns        |
| FakeItEasy  | 1         | 2,023.3 ns        |
| Rocks       | 1         | 46.2 ns           |
| Imposter    | 1         | **250.8 ns**      |
| Moq         | 10        | 634,227.3 ns      |
| NSubstitute | 10        | 9,484.3 ns        |
| FakeItEasy  | 10        | 9,346.8 ns        |
| Rocks       | 10        | 587.9 ns          |
| Imposter    | 10        | **2,419.2 ns**    |
| Moq         | 100       | 6,619,086.5 ns    |
| NSubstitute | 100       | 224,316.0 ns      |
| FakeItEasy  | 100       | 228,507.4 ns      |
| Rocks       | 100       | 28,261.9 ns       |
| Imposter    | 100       | **36,118.9 ns**   |
| Moq         | 1000      | 98,252,350.2 ns   |
| NSubstitute | 1000      | 25,223,566.8 ns   |
| FakeItEasy  | 1000      | 19,298,298.4 ns   |
| Rocks       | 1000      | 2,574,515.3 ns    |
| Imposter    | 1000      | **4,011,942.7 ns**|


Allocated Memory

| Method      | Iteration | Allocated    |
|-------------|-----------|-------------:|
| Moq         | 1         | 12.85 KB     |
| NSubstitute | 1         | 8.05 KB      |
| FakeItEasy  | 1         | 5.68 KB      |
| Rocks       | 1         | 0.30 KB      |
| Imposter    | 1         | **2.73 KB**  |
| Moq         | 10        | 115.07 KB    |
| NSubstitute | 10        | 28.46 KB     |
| FakeItEasy  | 10        | 35.06 KB     |
| Rocks       | 10        | 1.84 KB      |
| Imposter    | 10        | **25.65 KB** |
| Moq         | 100       | 1412.43 KB   |
| NSubstitute | 100       | 236.98 KB    |
| FakeItEasy  | 100       | 715.71 KB    |
| Rocks       | 100       | 17.31 KB     |
| Imposter    | 100       | **254.87 KB**|
| Moq         | 1000      | 42221.29 KB  |
| NSubstitute | 1000      | 2312.93 KB   |
| FakeItEasy  | 1000      | 45797.90 KB  |
| Rocks       | 1000      | 172.00 KB    |
| Imposter    | 1000      | **2547.05 KB**|


Benchmark Environment

```
BenchmarkDotNet v0.15.6, macOS Sequoia 15.7.3 (24G419) [Darwin 24.6.0]
Apple M2 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 10.0.101
[Host]     : .NET 10.0.1 (10.0.1, 10.0.125.57005), Arm64 RyuJIT armv8.0-a
DefaultJob : .NET 10.0.1 (10.0.1, 10.0.125.57005), Arm64 RyuJIT armv8.0-a
```
</details>


See other benchmarks [benchmark](https://github.com/themidnightgospel/Imposter/blob/3172c333603fd2d76031b20be39753a9b62f31c3/benchmarks/Imposter.Benchmarks/ImposterVsMoqVsNSubstitute/SimpleMethodMockingBenchmarks.cs#L12)

## ✨ Feature-Rich

- [Method Impersonation](https://themidnightgospel.github.io/Imposter/latest/methods/)
- [Property Impersonation](https://themidnightgospel.github.io/Imposter/latest/properties/)
- [Indexer Impersonation](https://themidnightgospel.github.io/Imposter/latest/indexers/)
- [Event Impersonation](https://themidnightgospel.github.io/Imposter/latest/events/)
- [Class Impersonation](https://themidnightgospel.github.io/Imposter/latest/base-implementation/)
- [Generics](https://themidnightgospel.github.io/Imposter/latest/generics/)
- [Implicit & Explicit Modes](https://themidnightgospel.github.io/Imposter/latest/implicit-vs-explicit/)
- [Use Base implementation](https://themidnightgospel.github.io/Imposter/latest/base-implementation/)
- [Async Support](https://themidnightgospel.github.io/Imposter/latest/methods/#async-methods)
- [Protected members Impersonation](https://themidnightgospel.github.io/Imposter/latest/methods/protected-members/)


## Docs
Docs: https://themidnightgospel.github.io/Imposter/

## 📜 MIT License

Free to use, modify, and distribute under the MIT License.