using System;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

public class ImposterGeneratorUseBaseImplementationTests
{
    private const string Source = """
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.OverrideableService))]
[assembly: GenerateImposter(typeof(Sample.AbstractOnlyService))]
[assembly: GenerateImposter(typeof(Sample.IInterfaceService))]

namespace Sample;

public interface IInterfaceService
{
    void InterfaceMethod();
}

public abstract class AbstractOnlyService
{
    public abstract void AbstractMethod();
}

public class OverrideableService : AbstractOnlyService
{
    public override void AbstractMethod()
    {
    }

    public virtual void VirtualMethod()
    {
    }
}
""";

    [Fact]
    public void GivenOverrideableClassMethod_WhenGenerated_ThenIncludesUseBaseImplementation()
    {
        var runResult = RunGenerator();
        var generatedSource = GetGeneratedSource(runResult, "OverrideableServiceImposter.g.cs");

        generatedSource.ShouldContain("IVirtualMethodMethodInvocationImposterGroupContinuation UseBaseImplementation()");
    }

    [Fact]
    public void GivenAbstractMethod_WhenGenerated_ThenSkipsUseBaseImplementation()
    {
        var runResult = RunGenerator();
        var generatedSource = GetGeneratedSource(runResult, "AbstractOnlyServiceImposter.g.cs");

        generatedSource.ShouldNotContain("UseBaseImplementation");
    }

    [Fact]
    public void GivenInterfaceMethod_WhenGenerated_ThenSkipsUseBaseImplementation()
    {
        var runResult = RunGenerator();
        var generatedSource = GetGeneratedSource(runResult, "IInterfaceServiceImposter.g.cs");

        generatedSource.ShouldNotContain("UseBaseImplementation");
    }

    private static GeneratorRunResult RunGenerator()
    {
        var compilation = CreateCompilation(Source);
        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator());

        var runResult = driver.RunGenerators(compilation).GetRunResult();
        var generatorResult = runResult.Results.Single();

        runResult.Diagnostics.ShouldBeEmpty();
        generatorResult.Exception.ShouldBeNull();

        return generatorResult;
    }

    private static string GetGeneratedSource(GeneratorRunResult runResult, string hintNameSuffix) =>
        runResult.GeneratedSources
            .Single(source => source.HintName.EndsWith(hintNameSuffix, StringComparison.Ordinal))
            .SourceText
            .ToString();

    private static CSharpCompilation CreateCompilation(string source)
    {
        var parseOptions = new CSharpParseOptions(LanguageVersion.Preview);

        var references = ReferenceAssemblies.Net.Net90
            .ResolveAsync(null, CancellationToken.None)
            .GetAwaiter()
            .GetResult()
            .Concat(new[]
            {
                MetadataReference.CreateFromFile(typeof(GenerateImposterAttribute).Assembly.Location)
            });

        return CSharpCompilation.Create(
            assemblyName: "ImposterGeneratorUseBaseImplementationTests",
            syntaxTrees: new[] { CSharpSyntaxTree.ParseText(source, parseOptions) },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }
}
