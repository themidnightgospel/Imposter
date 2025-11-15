# Compatibility Matrix

Imposter ships a single NuGet package (`Imposter`) containing the source generator (analyzer) and the runtime abstractions (`netstandard2.0`).

Minimum supported C# language version is 8.0 for all target frameworks. Static type extensions (e.g., `IMyService.Imposter()`) are emitted only when compiling with C# 14 or later.

!!! tip "Pro tip"
    Enable static type extensions by setting `<LangVersion>14</LangVersion>` (or `latest`) in your project. Otherwise, use the generated `FooImposter` type directly.

## Matrix

| TFM                     | Minimum C# | Static type extensions | Notes |
|-------------------------|------------|------------------------|-------|
| net9.0                  | 8.0        | Yes (C# 14+), otherwise No | Works out-of-the-box with SDK projects and analyzers. |
| net8.0                  | 8.0        | Yes (C# 14+), otherwise No | Same capabilities as net9.0; set `<LangVersion>14</LangVersion>` (or `latest`) to enable static type extensions. |
| net6.0                  | 8.0        | Yes (C# 14+), otherwise No | Ensure SDK-style project to load analyzers. |
| Libraries consuming netstandard2.0 | 8.0 | Yes (C# 14+), otherwise No | Runtime ships as `netstandard2.0`; analyzer runs at build time. |

## Notes

- Static type extensions are a compile-time feature. If your project compiles with C# 9–13, use the generated `FooImposter` type directly. With C# 14+, you can use `Foo.Imposter()`.
- The generator requires SDK-style projects so analyzers load during build.
- The runtime library (`Imposter.Abstractions`) targets `netstandard2.0`, enabling broad runtime compatibility.
- For older tooling or non-SDK projects, migrate to SDK-style to use source generators.

See also: [Limitations](limitations.md).
