using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

public static class EventImpersonationTestsShared
{
    internal const string Source = /*lang=csharp*/
        """
        using System;
        using System.Threading.Tasks;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IEventSut))]

        namespace Sample
        {
            public interface IEventSut
            {
                event EventHandler SomethingHappened;

                event Action ActionOnly;

                event Action<object?> ActionWithArgument;

                event Func<object?, EventArgs, Task>? AsyncSomethingHappened;
            }
        }
        """;

    internal const string BaseSourceFileName = "GeneratorInput.cs";
    internal const string SnippetFileName = "Snippet.cs";

    internal static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(EventBuilderFluentApiTests)
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
        string expectedId
    )
    {
        var diagnostic = Assert.Single(diagnostics);
        Assert.Equal(expectedId, diagnostic.Id);
    }
}
