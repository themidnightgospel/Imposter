# Getting Started

Imposter — The Fastest and Most Memory-Efficient Mocking Library

## Installation

Add the packages to your test or application project:

=== "PackageReference"

    ```xml
    <ItemGroup>
        <PackageReference Include="Imposter" Version="*" ExcludeAssets="runtime" PrivateAssets="all" />
    </ItemGroup>
    ```

=== ".NET CLI"

    ```bash
    dotnet add package Imposter
    ```

=== "Package Manager"

    ```powershell
    Install-Package Imposter
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

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/GettingStarted/GettingStartedTests.cs#L12"}
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

    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/GettingStarted/GettingStartedTests.cs#L66"}
    var imposter = IMyService.Imposter();
    imposter.GetNumber().Returns(42);

    var service = imposter.Instance();
    service.GetNumber().ShouldBe(42);
    ```

=== "C# 8-13"

    Use the generated imposter type directly:

    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/GettingStarted/GettingStartedTests.cs#L31"}
    var imposter = new IMyServiceImposter();
    imposter.GetNumber().Returns(42);

    var service = imposter.Instance();
    service.GetNumber().ShouldBe(42);
    ```

Generate imposter for classes

!!! warning
    Only non-sealed classes can be impersonated.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/GettingStarted/GettingStartedTests.cs#L20"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(BaseService))]

    public abstract class BaseService
    {
        public virtual int GetNumber() => 0;
    }
    ```

After a build, use the generated type:

=== "C# 14"

    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/GettingStarted/GettingStartedTests.cs#L69"}
    var imposter = BaseService.Imposter();
    imposter.GetNumber().Returns(42);

    var service = imposter.Instance();
    service.GetNumber().ShouldBe(42);
    ```

=== "C# 8-13"

    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/GettingStarted/GettingStartedTests.cs#L51"}
    var imposter = new BaseServiceImposter();
    imposter.GetNumber().Returns(42);

    var service = imposter.Instance();
    service.GetNumber().ShouldBe(42);
    ```

!!! note
    For classes, only virtual or abstract members can be impersonated (mocked).

!!! warning
    Minimum supported C# version is 8.0

!!! info "Next steps"

      - [Methods](methods/index.md)
      - [Properties](properties/index.md)
      - [Indexers](indexers/index.md)
      - [Events](events/index.md)
      - [Key API Reference](key-api-reference.md)
      - [Compatibility](compatibility.md)
      - [Limitations](limitations.md)
