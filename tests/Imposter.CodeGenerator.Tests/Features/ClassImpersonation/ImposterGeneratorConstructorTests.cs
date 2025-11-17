using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

public class ImposterGeneratorConstructorTests
{
    private const string Source =
        /*lang=csharp*/
        """
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.PrivateOnlyClass))]

            namespace Sample;

            public class PrivateOnlyClass
            {
                private PrivateOnlyClass() { }
                private PrivateOnlyClass(int value) { }
            }

            """;

    [Fact]
    public void GivenClassWithOnlyPrivateConstructors_WhenGeneratorRuns_ShouldReportIMP004()
    {
        var compilation = CreateCompilation(LanguageVersion.CSharp9);
        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator());

        var runResult = driver.RunGenerators(compilation).GetRunResult();
        var diagnostic = runResult.Diagnostics.ShouldHaveSingleItem();

        diagnostic.Id.ShouldBe(
            DiagnosticDescriptors.ImposterTargetMustHaveAccessibleConstructor.Id
        );
        diagnostic.GetMessage().ShouldContain("Sample.PrivateOnlyClass");
    }

    private static CSharpCompilation CreateCompilation(LanguageVersion languageVersion)
    {
        var parseOptions = new CSharpParseOptions(languageVersion);
        var references = ReferenceAssemblies
            .Net.Net90.ResolveAsync(null, CancellationToken.None)
            .GetAwaiter()
            .GetResult()
            .Concat([
                MetadataReference.CreateFromFile(
                    typeof(GenerateImposterAttribute).Assembly.Location
                ),
            ]);

        return CSharpCompilation.Create(
            assemblyName: "ImposterGeneratorConstructorTests",
            syntaxTrees: [CSharpSyntaxTree.ParseText(Source, parseOptions)],
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
    }
}
