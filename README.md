<p align="center">
  <img src="docs/imposter_logo.png" alt="Imposter logo" width="140" />
</p>

<p align="center">
    Mocking library with the perfect balance of Performance and Intuitive API
</p>

[![Build, Test, and Format verification](https://github.com/themidnightgospel/Imposter/actions/workflows/build-and-test.yml/badge.svg?branch=master)](https://github.com/themidnightgospel/Imposter/actions/workflows/build-and-test.yml)
[![CodeQL](https://github.com/themidnightgospel/Imposter/actions/workflows/codeql.yml/badge.svg)](https://github.com/themidnightgospel/Imposter/actions/workflows/codeql.yml)
[![OpenSSF Scorecard](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fapi.scorecard.dev%2Fprojects%2Fgithub.com%2Fthemidnightgospel%2FImposter&query=%24.score&label=openssf%20scorecard)](https://scorecard.dev/viewer/?uri=github.com/themidnightgospel/Imposter)
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

Each library sets up `Square` to return `input * input` and calls it, for 1, 10, 100 and 1,000 iterations. See [benchmarks](https://github.com/themidnightgospel/Imposter/tree/master/benchmarks/Imposter.Benchmarks) for the code.

```csharp
public interface ICalculator
{
    int Square(int input);
}
```

![Benchmark results for Moq, NSubstitute, FakeItEasy, Rocks and Imposter](https://raw.githubusercontent.com/themidnightgospel/Imposter/benchmark-results/simple-method.svg)

The [Benchmarks workflow](https://github.com/themidnightgospel/Imposter/actions/workflows/benchmarks.yml) reruns this comparison on a GitHub-hosted runner after every change to the code on master, so the table always shows the latest build. Shared runners are slower and noisier than a desktop machine: compare the libraries within the table rather than the absolute timings. The same results are available as [Markdown](https://github.com/themidnightgospel/Imposter/blob/benchmark-results/simple-method.md).

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

- See [Docs](https://themidnightgospel.github.io/Imposter/latest/)

## 📜 MIT License

Free to use, modify, and distribute under the MIT License.