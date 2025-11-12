# Imposter

Imposter — Source-generated test doubles, zero runtime overhead.

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

[assembly: GenerateImposter(typeof(IMyService))]

public interface IMyService
{
    int GetNumber();
    Task<int> GetNumberAsync();
}


var imposter = new IMyServiceImposter();

imposter.GetNumber().Returns(42);
imposter.Instance().GetNumber(); // 42

imposter.GetNumberAsync().ReturnsAsync(7);
await imposter.Instance().GetNumberAsync(); // 7
```

Example: classes (virtual/abstract members)

```csharp
using System.Threading.Tasks;
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(BaseService))]

public abstract class BaseService
{
    public virtual int GetNumber() => 0;
    public virtual Task<int> GetNumberAsync() => Task.FromResult(0);
}

var classImposter = new BaseServiceImposter();
classImposter.GetNumber().Returns(42);
classImposter.Instance().GetNumber(); // 42
```

Note: For classes, only virtual/abstract members can be intercepted.

## Accessing the generated imposter

If you're using C# 14 or later then accessing the imposter is more simpler and refactoring-friendly

```csharp
var imposter = IMyService.Imposter();
```

## Behavior modes

- Implicit (loose, default): Missing setups return default values; calls succeed.
- Explicit (strict): Missing setups throw `MissingImposterException`.

Learn more: https://themidnightgospel.github.io/Imposter/methods/explicit-vs-implicit/

## Docs
Docs: https://themidnightgospel.github.io/Imposter/

## License

Licensed under the MIT License. See LICENSE.txt for details.
