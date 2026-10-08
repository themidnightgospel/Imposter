# Diagnostics

What each Imposter diagnostic means and how to resolve it. The help link of every diagnostic points to its section on this page.

| ID | Severity | Summary |
|---|---|---|
| [IMP002](#imp002) | Error | The target is not an interface or a non-sealed class |
| [IMP003](#imp003) | Error | The project's C# version is older than 9.0 |
| [IMP004](#imp004) | Error | The target class has no constructor the imposter can call |
| [IMP005](#imp005) | Error | The generator failed unexpectedly |
| [IMP006](#imp006) | Warning | A closed generic type is registered as a target |
| [IMPLOG001](#implog001) | Info | Generator log message |

## IMP002: Invalid imposter target { #imp002 }

`[GenerateImposter]` was applied to a type that cannot be impersonated, such as a sealed class, a struct, an enum or a delegate. No imposter is generated for it.

Register an interface or a non-sealed class instead. If you own the class, remove `sealed`, or extract an interface and register that.

!!! note
    Only virtual and abstract members of a class can be impersonated.

## IMP003: C# version not supported { #imp003 }

Generated imposters use C# 9.0 features. When the project compiles with an older language version, the generator reports IMP003 once and generates no imposters.

Set the language version to 9.0 or later in the project file, for example `<LangVersion>9.0</LangVersion>`. Projects that target .NET Framework, .NET Standard or .NET Core 3.x or earlier default to C# 7.3 or 8.0, so they need this setting.

## IMP004: No accessible constructor { #imp004 }

The generated imposter derives from the target class, so it must call one of the class's constructors. IMP004 means every constructor is private.

Add a public, internal or protected constructor to the class.

## IMP005: Generator crash { #imp005 }

The generator hit an unexpected exception. The message includes the exception, and no imposter is generated for the affected target.

This is a bug in Imposter. Please [open an issue](https://github.com/themidnightgospel/Imposter/issues/new?labels=bug&title=Generator%20crash:%20IMP005) with the full message and, if possible, the target type that triggers it.

## IMP006: Closed generic imposter target { #imp006 }

A closed generic type such as `typeof(IRepository<User>)` was registered. It generates the same generic imposter as the open type, `IRepositoryImposter<T>`, and the type arguments in the registration have no effect.

Register the open generic type instead, for example `typeof(IRepository<>)`, and choose the type arguments where you create the imposter. See [Generics](generics.md).

!!! warning
    With `TreatWarningsAsErrors`, IMP006 fails the build. Switch to the open registration, or suppress IMP006 with `#pragma warning disable IMP006` or `<NoWarn>`.

## IMPLOG001: Generator log { #implog001 }

Log messages from the generator, written only when the `IMPOSTER_LOG` MSBuild property is `true`. They show the language version, whether the static `Imposter()` extensions are generated, and each generated imposter with its file name.

`dotnet build` shows these messages only at detailed verbosity: `dotnet build -v:d -p:IMPOSTER_LOG=true`. See the tip on [Getting Started](index.md).

## Retired diagnostics

- **IMP001** (unsupported language) was removed. The generator only runs for C# projects, so it could never be reported.

!!! info "Next steps"
    - [Getting Started](index.md)
    - [Generics](generics.md)
    - [Key API Reference](key-api-reference.md)
    - [Limitations](limitations.md)
