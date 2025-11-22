using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

public static class IndexerImpersonationTestsShared
{
    internal const string Source = /*lang=csharp*/
        """
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.IndexerService))]
[assembly: GenerateImposter(typeof(Sample.ReadonlyIndexerService))]
[assembly: GenerateImposter(typeof(Sample.GetterOnlyIndexerService))]
[assembly: GenerateImposter(typeof(Sample.SetterOnlyIndexerService))]
[assembly: GenerateImposter(typeof(Sample.AbstractGetterIndexerService))]
[assembly: GenerateImposter(typeof(Sample.AbstractSetterIndexerService))]
[assembly: GenerateImposter(typeof(Sample.IIndexerContract))]

namespace Sample
{
    public class IndexerService
    {
        public virtual int this[int key]
        {
            get => default;
            set { }
        }
    }

    public class ReadonlyIndexerService
    {
        public int this[int key]
        {
            get => default;
            set { }
        }
    }

    public class GetterOnlyIndexerService
    {
        public virtual int this[int key]
        {
            get => key;
        }
    }

    public class SetterOnlyIndexerService
    {
        public virtual int this[int key]
        {
            set { }
        }
    }

    public abstract class AbstractGetterIndexerService
    {
        public abstract int this[int key] { get; }
    }

    public abstract class AbstractSetterIndexerService
    {
        public abstract int this[int key] { set; }
    }

    public interface IIndexerContract
    {
        int this[int key] { get; set; }
    }
}
""";

    internal const string BaseSourceFileName = "IndexerFluentApiTests.Base.cs";
    internal const string SnippetFileName = "IndexerFluentApiTests.Snippet.cs";

    internal static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(IndexerFluentApiTests),
            languageVersion: LanguageVersion.CSharp9
        );

    internal static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }

    internal static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);

    internal static void AssertSingleDiagnostic(
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
