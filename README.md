<p align="center">
  <img src="docs/imposter_logo.png" alt="Imposter logo" width="140" />
</p>

<p align="center">
    Mocking library with the perfect balance of Performance and Intuitive API
</p>

[![Build, Test, and Format verification](https://github.com/themidnightgospel/Imposter/actions/workflows/build-and-test.yml/badge.svg?branch=master)](https://github.com/themidnightgospel/Imposter/actions/workflows/build-and-test.yml)
[![Nuget](https://img.shields.io/nuget/v/Imposter.svg)](https://www.nuget.org/packages/Imposter)

## 🧘Balanced Performance and Developer Experience

Imposter delivers top-tier performance without sacrificing a natural, intuitive API. While being ~10× faster than NSubstitute and up to ~50× faster than Moq in common mocking scenarios, Imposter is not the fastest mocking library available. Instead, it focuses on achieving a balance between performance and API ease of use.

## 🚀 Quick Start

Add nuget package reference:

```bash
dotnet add package Imposter
```

Pick a target

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

Use the generated imposter

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


## 🛡️ Strongly typed all the way

Loosely typed callbacks can easily cause runtime exceptions. Imposter enforces strong typing across the entire mocking pipeline, eliminating type mismatch errors for good. See more [here](https://themidnightgospel.github.io/Imposter/latest/methods/callbacks/#method-callbacks)

## 🎭 Interfaces and classes

Not limited to interfaces — Imposter can impersonate non-sealed classes and their protected members. See more [here](https://themidnightgospel.github.io/Imposter/latest/methods/protected-members/)

## 🧬 Full generic support

Generics are fully supported. see more [here](https://themidnightgospel.github.io/Imposter/latest/generics/)

## 🧩 Mock any member

Imposter supports not only methods, but also properties, events and indexers.

## 🔀 Implicit and Explicit Modes

Choose how unmocked members behave. **Implicit** mode returns defaults silently — great for prototyping. **Explicit** mode throws on any call without a setup — ideal for unit tests that must be precise. See more [here](https://themidnightgospel.github.io/Imposter/latest/implicit-vs-explicit/)

## 🧵 Thread-safe by design

Imposter is built to work reliably in multi-threaded and parallel test environments.

## ⏱️ Benchmark

`Square` method is setup to return `input * input` and ran it for 1, 10, 100, and 1000 iterations. see [benchmarks](https://github.com/themidnightgospel/Imposter/tree/master/benchmarks/Imposter.Benchmarks) for more details

```csharp
public interface ICalculator
{
    int Square(int input);
}
```

| Method      | Iteration |                Mean |     Allocated |
|------------ |-----------|--------------------:|--------------:|
| Moq         | 1         |        78,776.14 ns |       13148 B |
| NSubstitute | 1         |         1,624.16 ns |        7664 B |
| FakeItEasy  | 1         |         1,939.11 ns |        5617 B |
| Rocks       | 1         |            37.91 ns |         304 B |
| Imposter    | 1         |       **188.72** ns |    **2408** B |
| Moq         | 10        |     1,153,399.52 ns |      117789 B |
| NSubstitute | 10        |        11,919.78 ns |       27897 B |
| FakeItEasy  | 10        |         9,464.72 ns |       35727 B |
| Rocks       | 10        |           627.42 ns |        1888 B |
| Imposter    | 10        |     **1,712.15** ns |   **22424** B |
| Moq         | 100       |     7,488,213.33 ns |     1445159 B |
| NSubstitute | 100       |       205,099.09 ns |      222676 B |
| FakeItEasy  | 100       |       172,337.20 ns |      732299 B |
| Rocks       | 100       |        25,495.06 ns |       17728 B |
| Imposter    | 100       |    **30,005.13** ns |  **222584** B |
| Moq         | 1000      |    98,890,235.56 ns |    43233637 B |
| NSubstitute | 1000      |    19,440,858.58 ns |     2174740 B |
| FakeItEasy  | 1000      |    15,170,287.96 ns |    46895901 B |
| Rocks       | 1000      |     1,868,905.85 ns |      176128 B |
| Imposter    | 1000      | **2,256,458.40** ns | **2224184** B |


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

| Method      | Iteration |     Execution Time | Allocated Memory | 
|-------------|-----------|-------------------:|-----------------:| 
| Moq         | 1         |        62,199.5 ns |         12.85 KB | 
| NSubstitute | 1         |         1,688.9 ns |          8.05 KB | 
| FakeItEasy  | 1         |         2,023.3 ns |          5.68 KB | 
| Rocks       | 1         |            46.2 ns |          0.30 KB | 
| Imposter    | 1         |       **250.8 ns** |      **2.73 KB** | 
| Moq         | 10        |       634,227.3 ns |        115.07 KB | 
| NSubstitute | 10        |         9,484.3 ns |         28.46 KB | 
| FakeItEasy  | 10        |         9,346.8 ns |         35.06 KB | 
| Rocks       | 10        |           587.9 ns |          1.84 KB | 
| Imposter    | 10        |     **2,419.2 ns** |     **25.65 KB** | 
| Moq         | 100       |     6,619,086.5 ns |       1412.43 KB | 
| NSubstitute | 100       |       224,316.0 ns |        236.98 KB | 
| FakeItEasy  | 100       |       228,507.4 ns |        715.71 KB | 
| Rocks       | 100       |        28,261.9 ns |         17.31 KB | 
| Imposter    | 100       |    **36,118.9 ns** |    **254.87 KB** | 
| Moq         | 1000      |    98,252,350.2 ns |      42221.29 KB | 
| NSubstitute | 1000      |    25,223,566.8 ns |       2312.93 KB | 
| FakeItEasy  | 1000      |    19,298,298.4 ns |      45797.90 KB | 
| Rocks       | 1000      |     2,574,515.3 ns |        172.00 KB | 
| Imposter    | 1000      | **4,011,942.7 ns** |   **2547.05 KB** | 


Benchmark Environment

```
BenchmarkDotNet v0.15.6, macOS Sequoia 15.7.3 (24G419) [Darwin 24.6.0]
Apple M2 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 10.0.101
[Host]     : .NET 10.0.1 (10.0.1, 10.0.125.57005), Arm64 RyuJIT armv8.0-a
DefaultJob : .NET 10.0.1 (10.0.1, 10.0.125.57005), Arm64 RyuJIT armv8.0-a
```
</details>

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

## 📜 MIT License

Free to use, modify, and distribute under the MIT License.