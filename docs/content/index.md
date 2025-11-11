# Getting Started

Imposter is a Roslyn incremental source generator that creates lightweight, source-generated imposters (mocks/stubs) for interfaces and classes.

> Do not edit generated `.g.cs` files. Make changes in your code or via the generator inputs and rebuild.

## Prerequisites

- C# 13 or later. Static type extensions (the `IMyService.Imposter()` form) require the Preview language version.
- Reference the generator at compile-time and the abstractions at runtime (see Installation).

## Installation

Add the packages to your test or application project:

```bash
# Runtime + fluent API surface
dotnet add package Imposter.Abstractions

# Source generator (compile-time only)
dotnet add package Imposter.CodeGenerator
```

Recommended csproj configuration (keeps the generator out of your runtime):

```xml
<ItemGroup>
  <PackageReference Include="Imposter.Abstractions" Version="x.y.z" />
  <PackageReference Include="Imposter.CodeGenerator" Version="x.y.z">
    <PrivateAssets>all</PrivateAssets>
    <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  </PackageReference>
  <!-- Optional: enable preview to use static type extensions -->
  <!-- <LangVersion>preview</LangVersion> -->
  <!-- Or per-Project PropertyGroup -->
</ItemGroup>
```

## Generate an Imposter

Annotate the target type at the assembly level and build. The generator produces a `<TypeName>Imposter` you can new up in code.

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

=== "C# Preview"

    ```csharp
    var imposter = IMyService.Imposter();
    var service = imposter.Instance();

    // You can also specify behavior explicitly
    var strictImposter = IMyService.Imposter(ImposterInvocationBehavior.Explicit);
    ```

=== "C# 13 and earlier (or without Preview)"

    Use the generated imposter type directly:

    ```csharp
    var imposter = new IMyServiceImposter(); // default: Implicit behavior
    var service = imposter.Instance();       // user-facing instance
    ```

Optionally, choose the invocation behavior:

```csharp
// Implicit: missing setups return defaults
var loose = new IMyServiceImposter(ImposterInvocationBehavior.Implicit);

// Explicit: missing setups throw MissingImposterException
var strict = new IMyServiceImposter(ImposterInvocationBehavior.Explicit);
```

Tip: `Instance()` is also available via the `ImposterExtensions.Instance` extension method for concise call sites.

## Method Mocking

Return values:

```csharp
imposter.GetNumber().Returns(42);
service.GetNumber(); // 42

// Parameterized with a delegate
imposter.Increment(Arg<int>.Any()).Returns(v => v + 2);
// Match a specific value directly (implicit Arg<int> conversion)
imposter.Increment(20).Returns(200);
service.Increment(5); // 7
```

Argument matchers (Arg<T>):

```csharp
imposter.Increment(Arg<int>.Is(x => x > 10)).Returns(100);
imposter.Increment(Arg<int>.IsIn(new[] { 1, 2, 3 })).Returns(1);
imposter.Increment(5).Returns(50);            // implicit conversion to Arg<int>
imposter.Increment(Arg<int>.Any()).Returns(0);
```

Sequencing with Then():

```csharp
imposter.GetNumber()
    .Returns(1)
    .Then().Returns(2)
    .Then().Throws<InvalidOperationException>();

service.GetNumber(); // 1
service.GetNumber(); // 2
service.GetNumber(); // throws InvalidOperationException
```

Callbacks and ordering:

```csharp
var stages = new List<string>();

imposter.GetNumber()
    .Returns(() => { stages.Add("return"); return 42; })
    .Callback(() => stages.Add("first"))
    .Callback(() => stages.Add("second"));

service.GetNumber();
// stages: ["return", "first", "second"]
```

Ref/out/in parameters:

```csharp
// Setup for a method with (out int o, ref string r, in double d, bool[] args)
imposter.GenericAllRefKind<int, string, double, bool, int>(
        OutArg<int>.Any(),   // out
        Arg<string>.Any(),   // ref
        Arg<double>.Any(),   // in
        Arg<bool[]>.Any())
    .Returns((out int o, ref string r, in double d, bool[] args) => { o = 5; return 99; })
    .Callback((out int o, ref string r, in double d, bool[] args) => { o = 5; /* side effects */ });

string refValue = "seed";
var result = service.GenericAllRefKind<int, string, double, bool, int>(out var outValue, ref refValue, in 5.0, new[] { true });
// result = 99, outValue = 5
```

Async methods:

```csharp
imposter.GetNumberAsync().ReturnsAsync(42);
await service.GetNumberAsync(); // 42

// Task-returning (no result)
imposter.DoWorkAsync().Returns(Task.CompletedTask);
await service.DoWorkAsync();
```

Exceptions:

```csharp
imposter.GetNumber().Throws(new Exception("boom"));
imposter.GetNumber().Throws<InvalidOperationException>();
imposter.GetNumber().Throws(() => new Exception("deferred"));
```

Verification with Count:

```csharp
service.Increment(1);
service.Increment(2);

imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(2));
imposter.Increment(2).Called(Count.Once());
```

## Property Mocking

Getter and sequencing:

```csharp
imposter.Age.Getter().Returns(33);
service.Age; // 33

imposter.Age.Getter().Returns(10).Then().Returns(20);
```

Setter callbacks and verification:

```csharp
imposter.Age.Setter(Arg<int>.Any()).Callback(v => { /* observe/set side-effects */ });
imposter.Age.Setter(Arg<int>.Any()).Called(Count.Exactly(1));
```

Base implementation forwarding:

```csharp
imposter.Age.Getter().UseBaseImplementation();
imposter.Age.Setter(Arg<int>.Any()).UseBaseImplementation();
```

Default behavior and initializers:

- In Implicit mode, missing getter setups return defaults. For class targets with property initializers, the first read mirrors the initialized base value.

## Indexer Mocking

```csharp
// Getter
imposter[Arg<int>.Is(k => k > 0)].Getter().Returns(10);
var value = service[123]; // 10

// Setter
imposter[Arg<int>.Any()].Setter().Callback((index, v) => { /* track writes */ });

// Base
imposter[Arg<int>.Any()].Getter().UseBaseImplementation();
imposter[Arg<int>.Any()].Setter().UseBaseImplementation();
```

## Event Mocking

Subscribe/unsubscribe verification, raising, and interceptors:

```csharp
EventHandler h = (s, e) => { };

service.SomethingHappened += h;
service.SomethingHappened -= h;

imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.Once());
imposter.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h), Count.Once());

// Raise in-order for current subscribers
imposter.SomethingHappened.Raise(this, EventArgs.Empty);

// Observe subscriptions/unsubscriptions
imposter.SomethingHappened.OnSubscribe(handler => { /* inspect */ });
imposter.SomethingHappened.OnUnsubscribe(handler => { /* inspect */ });

// Verify handler invocation count
imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(2));
```

## Base Implementations (UseBaseImplementation)

For overridable class members, you may forward to the base implementation:

```csharp
imposter.DoWork(Arg<int>.Any()).UseBaseImplementation();
imposter.Age.Getter().UseBaseImplementation();
imposter[Arg<int>.Any()].Getter().UseBaseImplementation();
imposter.SomethingHappened.UseBaseImplementation(); // when supported on the target member
```

If base is unavailable and Explicit mode is enabled, a `MissingImposterException` is thrown when no setup applies.

## Tips and Notes

- Implicit vs Explicit modes control behavior for missing setups.
- `Arg<T>` provides rich matching: `Any`, `Is`, `IsNot`, `IsIn`, `IsNotIn`, `IsDefault`, plus implicit value conversion.
- Use `OutArg<T>.Any()` for `out` parameters (they always match).
- `Count` helpers: `Exactly`, `AtLeast`, `AtMost`, `Once`, `Never`, `Any`.
- Thread-safety: imposters are stress-tested under concurrency; sequencing preserves ordering (see thread-safety tests).
- Static type extensions (e.g., `IMyService.Imposter()`) are emitted only when the project compiles with `LangVersion` set to `preview`.

## Next Steps

- Explore advanced usage:
  - Methods: see Methods overview and subpages.
  - Properties: property-specific behaviors and verification.
  - Indexers and Events: dedicated pages and examples.
- Browse repository tests for real-world scenarios:
  - `tests/Imposter.Tests/Features/*`
  - `tests/Imposter.CodeGenerator.Tests/Features/*`
- Architecture and design notes: see site navigation for Architecture, Usage, Fluent API, and Troubleshooting.
