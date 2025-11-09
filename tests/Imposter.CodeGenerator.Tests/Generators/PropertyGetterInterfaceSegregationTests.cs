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

public class PropertyGetterInterfaceSegregationTests
{
    private const string Source = """
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.SampleService))]

namespace Sample;

public class SampleService
{
    public virtual int Age { get; }
}
""";

    [Fact]
    public void OutcomeInterface_ExposesReturnsAndThrowsOnly()
    {
        var source = GetGeneratedSource(RunGenerator(), "SampleServiceImposter.g.cs");

        source.ShouldContain("public interface IAgePropertyGetterOutcomeBuilder");
        source.ShouldContain("IAgePropertyGetterContinuationBuilder Returns(int value);");
        source.ShouldContain("IAgePropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);");
        source.ShouldContain("IAgePropertyGetterContinuationBuilder Throws(System.Exception exception);");
        source.ShouldContain("IAgePropertyGetterContinuationBuilder Throws<TException>()");
    }

    [Fact]
    public void ContinuationInterface_AllowsOnlyCallbackAndThen()
    {
        var source = GetGeneratedSource(RunGenerator(), "SampleServiceImposter.g.cs");

        source.ShouldContain("public interface IAgePropertyGetterContinuationBuilder");
        source.ShouldContain("IAgePropertyGetterContinuationBuilder Callback(System.Action callback);");
        source.ShouldContain("IAgePropertyGetterBuilder Then();");
    }

    [Fact]
    public void BuilderInterface_InheritsVerifierAndSpecializedInterfaces()
    {
        var source = GetGeneratedSource(RunGenerator(), "SampleServiceImposter.g.cs");

        source.ShouldContain("public interface IAgePropertyGetterBuilder : IAgePropertyGetterOutcomeBuilder, IAgePropertyGetterContinuationBuilder, IAgePropertyGetterVerifier");
        source.ShouldContain("public interface IAgePropertyGetterVerifier");
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
            assemblyName: "PropertyGetterInterfaceSegregationTests",
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
