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

public class MethodInvocationInterfaceSegregationTests
{
    private const string Source = """
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.SampleService))]

namespace Sample;

public class SampleService
{
    public virtual int Compute(int value) => value;
}
""";

    [Fact]
    public void StartInterface_ExposesOutcomesAndReturnsContinuation()
    {
        var source = GetGeneratedSource(RunGenerator(), "SampleServiceImposter.g.cs");

        source.ShouldContain("public interface IComputeMethodInvocationImposterGroup : IComputeMethodInvocationImposterGroupContinuation");
        source.ShouldContain("IComputeMethodInvocationImposterGroupContinuation Returns(ComputeDelegate resultGenerator);");
        source.ShouldContain("IComputeMethodInvocationImposterGroupContinuation Returns(int value);");
        source.ShouldContain("IComputeMethodInvocationImposterGroupContinuation Throws<TException>()");
        source.ShouldContain("IComputeMethodInvocationImposterGroupContinuation Throws(System.Exception exception);");
        source.ShouldContain("IComputeMethodInvocationImposterGroupContinuation UseBaseImplementation();");
    }

    [Fact]
    public void ContinuationInterface_AllowsOnlyCallbacksAndThen()
    {
        var source = GetGeneratedSource(RunGenerator(), "SampleServiceImposter.g.cs");

        source.ShouldContain("public interface IComputeMethodInvocationImposterGroupContinuation");
        source.ShouldContain("IComputeMethodInvocationImposterGroupContinuation Callback(ComputeCallbackDelegate callback);");
        source.ShouldContain("IComputeMethodInvocationImposterGroup Then();");
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
            assemblyName: "MethodInvocationInterfaceSegregationTests",
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
