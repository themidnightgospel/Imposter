# Getting Started

Imposter is a mocking library that's using roslyn source generators to achieve high performance and low memory footprint.

## Installation

```
<ItemGroup>
    <PackageReference Include="Imposter" Version="*" PrivateAssets="all" />
</ItemGroup>
```

```
dotnet add package Imposter
```

```
Install-Package Imposter
```

This package includes both the source generator (analyzer) and the runtime abstractions.

Pro tip

To inspect generated sources locally, set `EmitCompilerGeneratedFiles` and `CompilerGeneratedFilesOutputPath` in your project. Remove these before committing.

```
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

## Generate an imposter for an interface

Annotate the target type with the assembly level attribute and build. The generator produces a `<TypeName>Imposter` you can new up in code.

Example

```
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(IMyService))]

public interface IMyService
{
    int Increment(int value);
}
```

Note

Imposter and the imposter target (type that you generate an impsoter for) **does not** have to live in the same assembly. You can generate imposters for types in referenced assemblies.

After a build, use the generated type:

```
var imposter = IMyService.Imposter();
imposter.Increment(Arg<int>.Any()).Returns(3);

var service = imposter.Instance();
service.Increment(1).ShouldBe(3);
```

Use the generated imposter type directly:

```
var imposter = new IMyServiceImposter();
imposter.Increment(Arg<int>.Any()).Returns(3);

var service = imposter.Instance();
service.Increment(1).ShouldBe(3);
```

Generate imposter for a class

Warning

Only non-sealed classes can be impersonated.

In the exact same fashion, you can generate imposter for classes.

Example

```
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(BaseService))]

public abstract class BaseService
{
    public virtual int GetNumber() => 0;
}
```

After a build, use the generated type:

```
var imposter = BaseService.Imposter();
imposter.GetNumber().Returns(42);

var service = imposter.Instance();
service.GetNumber().ShouldBe(42);
```

```
var imposter = new BaseServiceImposter();
imposter.GetNumber().Returns(42);

var service = imposter.Instance();
service.GetNumber().ShouldBe(42);
```

Note

For classes, only virtual or abstract members can be impersonated.

Warning

Minimum supported C# version is 9.0

Next steps

- [Methods](https://themidnightgospel.github.io/Imposter/0.1.9/methods/index.md)
- [Properties](https://themidnightgospel.github.io/Imposter/0.1.9/properties/index.md)
- [Indexers](https://themidnightgospel.github.io/Imposter/0.1.9/indexers/index.md)
- [Events](https://themidnightgospel.github.io/Imposter/0.1.9/events/index.md)
- [Key API Reference](https://themidnightgospel.github.io/Imposter/0.1.9/key-api-reference/index.md)
