using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// IAsyncEnumerable<T> is consumed with await foreach, not awaited, so it is impersonated like any other return type.
public class AsyncEnumerableMethodTests
{
    private const string Source = /*lang=csharp*/
        """
        using System.Collections.Generic;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.AsyncEnumerable.IStreamService))]

        namespace Sample.AsyncEnumerable
        {
            public interface IStreamService
            {
                IAsyncEnumerable<int> Stream();
            }
        }
        """;

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "AsyncEnumerableMethod.Source.cs",
            snippetFileName: "AsyncEnumerableMethod.Snippet.cs",
            assemblyName: nameof(AsyncEnumerableMethodTests),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenAsyncEnumerableMethod_WhenReturningEnumerable_ShouldCompile()
    {
        var context = await TestContextTask;

        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            namespace Sample.AsyncEnumerable.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        new Sample.AsyncEnumerable.IStreamServiceImposter()
                            .Stream()
                            .Returns((System.Collections.Generic.IAsyncEnumerable<int>)null);
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }
}
