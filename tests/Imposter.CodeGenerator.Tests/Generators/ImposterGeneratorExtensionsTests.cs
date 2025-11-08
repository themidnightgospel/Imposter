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

public class ImposterGeneratorExtensionsTests
{
private const string Source = """
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.IOrderService))]

namespace Sample;

public interface IOrderService
{
    int Sum(int left, int right);
}
""";

    [Fact]
    public void Generator_EmitsExtension_WhenLanguageVersionSupportsTypeExtensions()
    {
        var runResult = RunGenerator(LanguageVersion.Preview);
        var generatedSource = string.Join(Environment.NewLine, runResult.GeneratedSources.Select(static source => source.SourceText.ToString()));

        generatedSource.ShouldContain("public static class IOrderServiceImposterExtensions");
        generatedSource.ShouldContain("extension (global::Sample.IOrderService imposter)");
    }

    [Fact]
    public void Generator_DoesNotEmitExtension_WhenLanguageVersionIsThirteen()
    {
        var runResult = RunGenerator(LanguageVersion.CSharp13);
        var generatedSource = string.Join(Environment.NewLine, runResult.GeneratedSources.Select(static source => source.SourceText.ToString()));

        generatedSource.ShouldNotContain("IOrderServiceImposterExtensions");
    }

    private static GeneratorRunResult RunGenerator(LanguageVersion languageVersion)
    {
        var compilation = CreateCompilation(languageVersion);
        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator());

        var runResult = driver.RunGenerators(compilation).GetRunResult();
        var generatorResult = runResult.Results.Single();

        runResult.Diagnostics.ShouldBeEmpty();
        generatorResult.Exception.ShouldBeNull();

        return generatorResult;
    }

    private static CSharpCompilation CreateCompilation(LanguageVersion languageVersion)
    {
        var parseOptions = new CSharpParseOptions(languageVersion);

        var references = ReferenceAssemblies.Net.Net90
            .ResolveAsync(null, CancellationToken.None)
            .GetAwaiter()
            .GetResult()
            .Concat(new[]
            {
                MetadataReference.CreateFromFile(typeof(GenerateImposterAttribute).Assembly.Location)
            });

        return CSharpCompilation.Create(
            assemblyName: "ImposterGeneratorExtensionsTests",
            syntaxTrees: new[] { CSharpSyntaxTree.ParseText(Source, parseOptions) },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }
}
