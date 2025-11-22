using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public partial class PropertySetterFluentApiTests
{
    private const string Source = /*lang=csharp*/
        """
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.SampleService))]
[assembly: GenerateImposter(typeof(Sample.SetterInterfaceTarget))]
[assembly: GenerateImposter(typeof(Sample.AbstractSetterTarget))]

namespace Sample
{
    public class SampleService
    {
        public virtual int Age { get; set; }
    }

    public interface SetterInterfaceTarget
    {
        int Age { get; set; }
    }

    public abstract class AbstractSetterTarget
    {
        public abstract int Age { get; set; }
    }
}
""";

    private const string BaseSourceFileName = "GeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(PropertySetterFluentApiTests)
        );

    private static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }

    private static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);

    private static void AssertSingleDiagnostic(
        ImmutableArray<Diagnostic> diagnostics,
        string expectedId,
        int expectedLine
    ) =>
        GeneratorTestHelper.AssertSingleDiagnostic(
            diagnostics,
            expectedId,
            expectedLine,
            SnippetFileName
        );
}
