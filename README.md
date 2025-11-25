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

Use `[GenerateImposter]` attribute in your **tests** project, this will generate an imoster

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

Learn more: https://themidnightgospel.github.io/Imposter/

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

## ⏱️ Benchmark

We benchmarked the simple method-impersonation scenario: we set up a `Square` method to return `input * input` and ran it for 1, 10, 100, and 1000 iterations.

```csharp
public interface ICalculator
{
    int Square(int input);
}
```

Mean execution time 

| Method      | Iteration | Mean            |
|-------------|-----------|----------------:|
| Moq         | 1         | 69,346.1 ns |  
| NSubstitute | 1         | 1,976.2 ns |   
| FakItEasy   | 1         | 2,006.7 ns |   
| Imposter    | 1         | **194.3 ns** |     
| Moq         | 10        | 686,282.9 ns |  
| NSubstitute | 10        | 11,201.6 ns |  
| FakItEasy   | 10        | 12,399.0 ns |   
| Imposter    | 10        | **1,896.7 ns** |   
| Moq         | 100       | 6,804,897.3 ns |
| NSubstitute | 100       | 335,390.6 ns | 
| FakItEasy   | 100       | 258,220.2 ns |   
| Imposter    | 100       | **34,011.7 ns** |  
| Moq         | 1000      | 99,710,929.5 ns |
| NSubstitute | 1000      | 26,986,939.0 ns |
| FakItEasy   | 1000      | 18,997,374.5 ns  |   
| Imposter    | 1000      | **2,452,970.7 ns** |


Allocated Memory

| Method      | Iteration | Allocated   |
|-------------|-----------|------------:|
| Moq         | 1         | 13.05 KB |
| NSubstitute | 1         | 7.84 KB |
| FakeItEasy  | 1         | 5.84 KB |
| Imposter    | 1         | **2.4 KB** |
| Moq         | 10        | 115.73 KB |
| NSubstitute | 10        | 29.29 KB |
| FakeItEasy  | 10        |  38.81 KB |
| Imposter    | 10        | **22.37 KB** |
| Moq         | 100       | 1416.91 KB |
| NSubstitute | 100       | 247.26 KB |
| FakeItEasy  | 100       |  1033.38 KB |
| Imposter    | 100       | **222.05 KB** |
| Moq         | 1000      | 42275.19 KB |
| NSubstitute | 1000      | 2420.82 KB |
| FakeItEasy  | 1000      |  77101.74 KB |
| Imposter    | 1000      | **2218.93 KB** |


Benchmark Environment

```
BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.6899)
13th Gen Intel Core i9-13900HX 2.20GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.100
[Host]     : .NET 8.0.21 (8.0.21, 8.0.2125.47513), X64 RyuJIT x86-64-v3
DefaultJob : .NET 8.0.21 (8.0.21, 8.0.2125.47513), X64 RyuJIT x86-64-v3
```

See other benchmarks [benchmark](https://github.com/themidnightgospel/Imposter/blob/3172c333603fd2d76031b20be39753a9b62f31c3/benchmarks/Imposter.Benchmarks/ImposterVsMoqVsNSubstitute/SimpleMethodMockingBenchmarks.cs#L12)

## Docs
Docs: https://themidnightgospel.github.io/Imposter/

## License

Licensed under the MIT License. See LICENSE.txt for details.
