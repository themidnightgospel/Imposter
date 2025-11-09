using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.FluentApi;

public class PropertyGetterFluentApiTests
{
    private const string Source = /*lang=csharp*/"""
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.SampleService))]

namespace Sample
{
    public class SampleService
    {
        public virtual int Age { get; }
    }
}
""";

    [Fact]
    public void GivenGetterSetup_WhenRepeatingReturnsWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Returns(2);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterContinuation_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterThen_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Then().Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterCallback_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Callback(() => { }).Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterThrows_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws(new System.Exception("fail")).Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterThrowsGeneric_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws<System.InvalidOperationException>().Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenTopLevelBuilder_WhenCallingCalled_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public void GivenContinuation_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Then().Callback(() => { });
        }
    }
}
""");

        diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public void GivenContinuation_WhenSwitchingToReturns_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(() => 1).Then().Returns(2);
        }
    }
}
""");

        diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public void GivenChainedContinuation_WhenMixingMethods_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Then().Callback(() => { }).Then().Returns(() => 2);
        }
    }
}
""");

        diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public void GivenContinuation_WhenThrowingThenReturning_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws(new System.Exception("fail")).Then().Returns(1);
        }
    }
}
""");

        diagnostics.ShouldBeEmpty();
    }

    private static GeneratorRunResult RunGenerator()
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: "PropertyGetterInterfaceSegregationTests",
            syntaxTrees: new[] { CSharpSyntaxTree.ParseText(Source, ParseOptions, path: BaseSourceFileName) },
            references: References,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator());
        var runResult = driver.RunGenerators(compilation).GetRunResult();
        var generatorResult = runResult.Results.Single();

        runResult.Diagnostics.ShouldBeEmpty();
        generatorResult.Exception.ShouldBeNull();

        return generatorResult;
    }

    private static ImmutableArray<Diagnostic> CompileSnippet(string snippet)
    {
        var generatorResult = RunGenerator();
        var baseTree = CSharpSyntaxTree.ParseText(Source, ParseOptions, path: BaseSourceFileName);
        var snippetTree = CSharpSyntaxTree.ParseText(snippet, ParseOptions, path: SnippetFileName);
        var generatedTrees = generatorResult.GeneratedSources
            .Select(gs => CSharpSyntaxTree.ParseText(gs.SourceText.ToString(), ParseOptions, path: gs.HintName));

        var compilation = CSharpCompilation.Create(
            assemblyName: "PropertyGetterInterfaceSegregationTests.Snippet",
            syntaxTrees: new[] { baseTree, snippetTree }.Concat(generatedTrees),
            references: References,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return compilation
            .GetDiagnostics()
            .Where(d => d.Severity == DiagnosticSeverity.Error)
            .ToImmutableArray();
    }

    private static readonly CSharpParseOptions ParseOptions = new CSharpParseOptions(LanguageVersion.CSharp8);
    private const string BaseSourceFileName = "GeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";
    
    private static readonly Lazy<MetadataReference[]> CachedReferences = new(() =>
        ReferenceAssemblies.Net.Net90
            .ResolveAsync(null, CancellationToken.None)
            .GetAwaiter()
            .GetResult()
            .Concat(new[]
            {
                MetadataReference.CreateFromFile(typeof(GenerateImposterAttribute).Assembly.Location)
            })
            .ToArray());

    private static MetadataReference[] References => CachedReferences.Value;

    private static void AssertSingleDiagnostic(ImmutableArray<Diagnostic> diagnostics, string expectedId, int expectedLine)
    {
        diagnostics.Length.ShouldBe(1);
        var diagnostic = diagnostics[0];
        diagnostic.Id.ShouldBe(expectedId);
        var span = diagnostic.Location.GetLineSpan();
        span.Path.ShouldBe(SnippetFileName);
        (span.StartLinePosition.Line + 1).ShouldBe(expectedLine);
    }
}
