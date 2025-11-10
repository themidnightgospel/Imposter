using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using Xunit.Abstractions;

namespace Imposter.CompatibilityTests;

public class ImposterGenerator_RoslynCompatibility
{
    public ImposterGenerator_RoslynCompatibility(ITestOutputHelper testOutput)
    {
        var assembly = typeof(CSharpSyntaxTree).Assembly;
        var version = assembly.GetName().Version;
        var location = assembly.Location;

        testOutput.WriteLine($"Microsoft.CodeAnalysis.CSharp version: {version}");
        testOutput.WriteLine($"Assembly location: {location}");
    }
    
    [Fact]
    public void Runs_and_generates_code_without_errors()
    {
        // Arrange: a minimal target interface and assembly-level attribute
        const string source = @"using Imposter.Abstractions;
namespace CompatSut {
    public interface ICalc { int Add(int a, int b); }
}
[assembly: GenerateImposterAttribute(typeof(CompatSut.ICalc))]
";

        var parseOptions = CSharpParseOptions.Default;
        var syntaxTree = CSharpSyntaxTree.ParseText(source, parseOptions);

        var references = new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
            // Bring in the attribute assembly so the generator can find GenerateImposterAttribute
            MetadataReference.CreateFromFile(typeof(GenerateImposterAttribute).Assembly.Location),
        };

        var compilation = CSharpCompilation.Create(
            assemblyName: "CompatHost",
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var generator = new ImposterGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Act
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostics);
        var runResult = driver.GetRunResult();

        // Assert: generator didn’t crash, produced code, and the updated compilation has no errors
        Assert.Empty(diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));
        Assert.True(runResult.Results.Any(r => r.GeneratedSources.Length > 0), "Expected at least one generated source file.");

        var compileDiagnostics = updatedCompilation.GetDiagnostics();
        var errors = compileDiagnostics.Where(d => d.Severity == DiagnosticSeverity.Error).ToArray();
        if (errors.Length > 0)
        {
            var message = string.Join(Environment.NewLine, errors.Select(e => e.ToString()));
            Assert.False(true, "Updated compilation contains errors:\n" + message);
        }
    }
}
