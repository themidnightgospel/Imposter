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

public class PropertySetterInterfaceSegregationTests
{
    private const string Source = """
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.SampleService))]

namespace Sample;

public class SampleService
{
    public virtual int Age { get; set; }
}
""";

    [Fact]
    public void FluentInterface_ExposesCallbackAndThenOnly()
    {
        var source = GetGeneratedSource(RunGenerator(), "SampleServiceImposter.g.cs");

        source.ShouldContain("public interface IAgePropertySetterFluentBuilder");
        source.ShouldContain("IAgePropertySetterFluentBuilder Callback(System.Action<int> callback);");
        source.ShouldContain("IAgePropertySetterFluentBuilder Then();");
    }

    [Fact]
    public void BuilderInterface_InheritsFluentAndVerifier()
    {
        var source = GetGeneratedSource(RunGenerator(), "SampleServiceImposter.g.cs");

        source.ShouldContain("public interface IAgePropertySetterBuilder : IAgePropertySetterFluentBuilder, IAgePropertySetterVerifier");
        source.ShouldContain("public interface IAgePropertySetterVerifier");
        source.ShouldContain("void Called(Imposter.Abstractions.Count count);");
    }

    private static GeneratorRunResult RunGenerator()
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

        var compilation = CSharpCompilation.Create(
            assemblyName: "PropertySetterInterfaceSegregationTests",
            syntaxTrees: new[] { CSharpSyntaxTree.ParseText(Source, parseOptions) },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator());
        var runResult = driver.RunGenerators(compilation).GetRunResult();
        var generatorResult = runResult.Results.Single();

        runResult.Diagnostics.ShouldBeEmpty();
        generatorResult.Exception.ShouldBeNull();

        return generatorResult;
    }

    private static string GetGeneratedSource(GeneratorRunResult result, string hintNameSuffix) =>
        result.GeneratedSources
            .Single(source => source.HintName.EndsWith(hintNameSuffix, StringComparison.Ordinal))
            .SourceText
            .ToString();
}
