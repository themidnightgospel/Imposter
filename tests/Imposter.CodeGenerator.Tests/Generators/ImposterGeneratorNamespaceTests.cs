using System;
using System.Linq;
using System.Text;
using System.Threading;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

public class ImposterGeneratorNamespaceTests
{
    private const string Source = @"using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.IGeneric<int, string>), false)]

namespace Sample;

public interface IGeneric<TFirst, TSecond>
{
    TFirst Convert(TSecond value);
}
";

    [Fact]
    public void Generator_Uses_Sanitized_Components_Namespace_For_Generic_Targets()
    {
        var (result, compilation) = RunGenerator(LanguageVersion.CSharp13);

        var imposterSource = result.GeneratedSources.Single(source => source.HintName == "IGenericImposter.g.cs");
        var namespaceLine = imposterSource.SourceText
            .ToString()
            .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .First(line => line.TrimStart().StartsWith("namespace ", StringComparison.Ordinal))
            .Trim();

        var expectedNamespace = $"namespace Imposters.{BuildSanitizedSuffix(compilation)}";

        namespaceLine.ShouldBe(expectedNamespace);
        namespaceLine.ShouldNotContain("<");
        namespaceLine.ShouldNotContain(">");
    }

    private static (GeneratorRunResult Result, CSharpCompilation Compilation) RunGenerator(LanguageVersion languageVersion)
    {
        var compilation = CreateCompilation(languageVersion);
        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator());

        var runResult = driver.RunGenerators(compilation).GetRunResult();
        var generatorResult = runResult.Results.Single();

        runResult.Diagnostics.ShouldBeEmpty();
        generatorResult.Exception.ShouldBeNull();

        return (generatorResult, compilation);
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
            assemblyName: "ImposterGeneratorNamespaceTests",
            syntaxTrees: new[] { CSharpSyntaxTree.ParseText(Source, parseOptions) },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    private static string BuildSanitizedSuffix(CSharpCompilation compilation)
    {
        var interfaceSymbol = compilation.GetTypeByMetadataName("Sample.IGeneric`2")!;
        var constructed = interfaceSymbol.Construct(
            compilation.GetSpecialType(SpecialType.System_Int32),
            compilation.GetSpecialType(SpecialType.System_String));

        var display = constructed.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        const string globalPrefix = "global::";
        if (display.StartsWith(globalPrefix, StringComparison.Ordinal))
        {
            display = display[globalPrefix.Length..];
        }

        var builder = new StringBuilder(display.Length);
        foreach (var ch in display)
        {
            builder.Append(char.IsLetterOrDigit(ch) || ch == '_' || ch == '.' ? ch : '_');
        }

        return builder.ToString();
    }
}
