# Diagnostics

What each Imposter diagnostic means and how to resolve it. The help link of every diagnostic points to its section on this page.

| ID | Severity | Summary |
|---|---|---|
| [IMP002](#imp002) | Error | The target is not an interface or a non-sealed class |
| [IMP003](#imp003) | Error | The project's C# version is older than 9.0 |
| [IMP004](#imp004) | Error | The target class has no constructor the imposter can call |
| [IMP005](#imp005) | Error | The generator failed unexpectedly |
| [IMP006](#imp006) | Warning | A closed generic type is registered as a target |
| [IMP007](#imp007) | Error | Two targets would generate the same imposter type |
| [IMP008](#imp008) | Error | The target class has abstract members your project cannot override |
| [IMP009](#imp009) | Error | The target has a member whose signature uses a ref-like type |
| [IMP010](#imp010) | Error | The target class has required members, and `SetsRequiredMembersAttribute` is missing |
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

The generated imposter derives from the target class, so it must call one of the class's constructors. IMP004 means none of them is accessible from your project: every constructor is private, or the class is in another assembly and its constructors are internal or private protected without `InternalsVisibleTo` for your project.

Add a public or protected constructor to the class, or an internal one if the class is in your project or its assembly grants your project `InternalsVisibleTo`.

## IMP005: Generator crash { #imp005 }

The generator hit an unexpected exception. The message includes the exception, and no imposter is generated for the affected target.

This is a bug in Imposter. Please [open an issue](https://github.com/themidnightgospel/Imposter/issues/new?labels=bug&title=Generator%20crash:%20IMP005) with the full message and, if possible, the target type that triggers it.

## IMP006: Closed generic imposter target { #imp006 }

A closed generic type such as `typeof(IRepository<User>)` was registered. It generates the same generic imposter as the open type, `IRepositoryImposter<T>`, and the type arguments in the registration have no effect.

Register the open generic type instead, for example `typeof(IRepository<>)`, and choose the type arguments where you create the imposter. See [Generics](generics.md).

!!! warning
    With `TreatWarningsAsErrors`, IMP006 fails the build. Switch to the open registration, or suppress IMP006 with `#pragma warning disable IMP006` or `<NoWarn>`.

## IMP007: Imposter type name collision { #imp007 }

An imposter is named after its target, `IServiceImposter` for `IService`, and by default goes into the target's namespace. Two registered targets that share a name and namespace, such as interfaces nested in different classes, would therefore get the same imposter type. The generator reports IMP007 for both and generates neither.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.CodeGenerator.Tests/Generators/ImposterTypeNameCollisionTests.cs#L150"}
    [assembly: GenerateImposter(typeof(Sample.A.IService))]
    [assembly: GenerateImposter(typeof(Sample.B.IService), putInTheSameNamespace: false)]
    ```

Register one of them, or both, with `putInTheSameNamespace: false`. Its imposter then goes into a namespace of its own, such as `Imposters.Sample.B.IService`. Two closed registrations of one generic type, such as `IRepository<int>` and `IRepository<string>`, collide too; register the open type instead (see [IMP006](#imp006)).

## IMP008: Abstract members the project cannot override { #imp008 }

The generated imposter derives from the target class, so it must override every abstract member the class leaves abstract. IMP008 means one of them is not accessible from your project: the class is in another assembly, and the member, or one of its property accessors, is `internal` or `private protected` without `InternalsVisibleTo` for your project. Only its own assembly, or one it grants `InternalsVisibleTo`, can derive from such a class, so no imposter is generated.

Register an interface the class implements instead, or ask the class's owner to grant your project `InternalsVisibleTo`.

## IMP009: Member with a ref-like type { #imp009 }

An imposter records every argument and result of the members it impersonates, and matches arguments with `Arg<T>`. It keeps them in fields, delegates and matchers, and none of these can hold a ref-like value: `Span<T>`, `ReadOnlySpan<T>` or another `ref struct`. The exception is a method's `Span<T>` or `ReadOnlySpan<T>` parameter, or a span it returns by value. The imposter copies the elements a span argument arrives with into an array and matches them with `SpanArg<T>` or `ReadOnlySpanArg<T>`, or `OutSpanArg<T>` or `OutReadOnlySpanArg<T>` for an `out` span (see [Span parameters](arguments-matching.md#span-parameters)), and `Returns` takes the array a returned span covers (see [Methods](methods/index.md#setup-return-values)).

IMP009 means a member the imposter would impersonate uses a ref-like type anywhere else in its signature: as a method parameter or return type of another `ref struct` type, a span returned by reference, a span a method with a `scoped` parameter returns or takes by `ref` or `out` (a `params` span parameter is scoped implicitly), a property or indexer type, an indexer parameter, or a parameter or return type of an event's delegate. The diagnostic names the first such member and type, and no imposter is generated.

Change the member to take or return a type the imposter can store, such as `ReadOnlyMemory<T>`, `Memory<T>` or an array, or register an interface without the member. On a class target, only virtual and abstract members are impersonated, so a non-virtual member with a ref-like type doesn't cause IMP009.

## IMP010: Required members without SetsRequiredMembersAttribute { #imp010 }

The imposter creates its instance without setting the target's C# 11 `required` members. Its constructors allow that with `System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute`, which is built into .NET 7 and later. IMP010 means the target class has required members, and your project can't use that attribute: it targets an older framework, such as .NET Standard, .NET Framework or .NET 6, and declares only the attributes the compiler needs for required members. No imposter is generated.

Target .NET 7 or later, or declare the attribute in your project, as polyfill packages do:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.CodeGenerator.Tests/Features/ClassImpersonation/RequiredMemberTests.cs#L100"}
    namespace System.Diagnostics.CodeAnalysis
    {
        [AttributeUsage(AttributeTargets.Constructor)]
        internal sealed class SetsRequiredMembersAttribute : Attribute { }
    }
    ```

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
