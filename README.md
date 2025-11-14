# Imposter

<p align="center">
  <img src="docs/imposter_logo.png" alt="Imposter logo" width="140" />
</p>

Imposter — Source-generated test doubles/mocks/imposters with zero runtime overhead.

[![Build, Test, and Format verification](https://github.com/themidnightgospel/Imposter/actions/workflows/build-and-test.yml/badge.svg?branch=master)](https://github.com/themidnightgospel/Imposter/actions/workflows/build-and-test.yml)
[![Nuget](https://img.shields.io/nuget/v/Imposter.svg)](https://www.nuget.org/packages/Imposter)

Docs: https://themidnightgospel.github.io/Imposter/

## Quick Start

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

## Docs
Docs: https://themidnightgospel.github.io/Imposter/

## License

Licensed under the MIT License. See LICENSE.txt for details.
