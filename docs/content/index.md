# Getting Started

Imposter — Source-generated test doubles, zero runtime overhead.

!!! note
    Minimum supported C# version is 8.0

## Installation

Add the packages to your test or application project:

```bash
dotnet add package Imposter
```

This package includes both the source generator (analyzer) and the runtime abstractions.

!!! tip "Pro tip"
    To inspect generated sources locally, set `EmitCompilerGeneratedFiles` and `CompilerGeneratedFilesOutputPath` in your project. Remove these before committing.
    ```xml
    <!-- Source code generation settings-->
    <PropertyGroup>
        <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
        <CompilerGeneratedFilesOutputPath>GeneratedFiles</CompilerGeneratedFilesOutputPath>
    </PropertyGroup>

    <!-- Include but don't compile generated code. The system will already compile the code, but this will make it visible for reference.-->
    <ItemGroup>
        <Compile Remove="GeneratedFiles\**"/>
        <None Include="GeneratedFiles\**"/>
    </ItemGroup>

    ```

## Generate an Imposter

Annotate the target type with the assembly level attribute and build. The generator produces a `<TypeName>Imposter` you can new up in code.

```csharp
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(IMyService))]

public interface IMyService
{
    int GetNumber();
    int Increment(int value);
    event EventHandler SomethingHappened;
    int this[int key] { get; set; }
}
```

After a build, use the generated type:

=== "C# 14"

    ```csharp
    var imposter = IMyService.Imposter();
    var service = imposter.Instance();
    ```

=== "C# 8-13"

    Use the generated imposter type directly:

    ```csharp
    var imposter = new IMyServiceImposter(); // default: Implicit behavior
    var service = imposter.Instance();       // user-facing instance
    ```

!!! tip "Pro tip"
    C# 14+ enables static type extensions like `IMyService.Imposter()`. On C# 8–13, use the generated `IMyServiceImposter` type instead.

Example: Classes

```csharp
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(BaseService))]

public abstract class BaseService
{
    public virtual int GetNumber() => 0;
}
```

After a build, use the generated type:

=== "C# 14"

    ```csharp
    var imposter = BaseService.Imposter();
    var service = imposter.Instance();
    ```

=== "C# 8-13"

    ```csharp
    var imposter = new BaseServiceImposter();
    var service = imposter.Instance();
    ```

!!! note
    For classes, only virtual or abstract members can be intercepted.

Mock a method

```csharp
imposter.GetNumber().Returns(42);
service.GetNumber(); // 42

// Parameterized with a delegate
imposter.Increment(Arg<int>.Any()).Returns(v => v + 2);

// Match a specific value directly (implicit Arg<int> conversion)
imposter.Increment(20).Returns(200);
service.Increment(5); // 7
```

!!! tip "Pro tip"
    Exact values implicitly convert to `Arg<T>`

## Next Steps

- Explore Advanced Features:
  - Methods: [methods](methods/index.md)
  - Properties: [properties](properties/index.md)
  - Indexers: [indexers](indexers/index.md)
  - Events: [events](events/index.md)
- Quick lookup: [Key API Reference](key-api-reference.md)
- Check platform support: [Compatibility](compatibility.md)
- Understand the scope: [Limitations](limitations.md)
- Browse repository tests for real-world scenarios:
  - `tests/Imposter.Tests/Features/*`
  - `tests/Imposter.CodeGenerator.Tests/Features/*`
- Architecture and design notes: see site navigation for Architecture, Usage, Fluent API, and Troubleshooting.
