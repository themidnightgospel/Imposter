using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

var source = @"using Imposter.Abstractions;
[assembly: GenerateImposterAttribute(typeof(CompatSut.ICalc))]
namespace CompatSut {
    public interface ICalc { int Add(int a, int b); }
}
";

var syntaxTree = CSharpSyntaxTree.ParseText(source, CSharpParseOptions.Default);

var references = new List<MetadataReference>
{
    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
    MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
    MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
    MetadataReference.CreateFromFile(typeof(GenerateImposterAttribute).Assembly.Location),
};

var compilation = CSharpCompilation.Create(
    assemblyName: "CompatHost",
    syntaxTrees: new[] { syntaxTree },
    references: references,
    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

var compileDiagnostics = compilation.GetDiagnostics();
Console.WriteLine($"Compilation diagnostics: {compileDiagnostics.Length}");
foreach (var diag in compileDiagnostics)
{
    Console.WriteLine(diag);
}

Console.WriteLine($"Assembly attributes: {compilation.Assembly.GetAttributes().Length}");
foreach (var attr in compilation.Assembly.GetAttributes())
{
    Console.WriteLine($"Attribute: {attr.AttributeClass?.ToDisplayString()} args={attr.ConstructorArguments.Length}");
}

var generator = new ImposterGenerator();
GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostics);

Console.WriteLine($"Driver diagnostics: {diagnostics.Length}");
foreach (var d in diagnostics)
{
    Console.WriteLine(d);
}

var runResult = driver.GetRunResult();
Console.WriteLine($"Results count: {runResult.Results.Length}");
foreach (var result in runResult.Results)
{
    Console.WriteLine($"Generator: {result.Generator.GetType().Name}, Sources: {result.GeneratedSources.Length}, Exception: {result.Exception}");
    foreach (var diag in result.Diagnostics)
    {
        Console.WriteLine($"  Result diag: {diag}");
    }
    foreach (var sourceText in result.GeneratedSources)
    {
        Console.WriteLine($"Hint: {sourceText.HintName}, Length: {sourceText.SourceText.Length}");
    }
}
