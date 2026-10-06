using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// On netstandard2.0 the task types live in netstandard.dll, and ValueTask in System.Threading.Tasks.Extensions.dll
// (also the case on .NET Framework), rather than in System.Runtime/System.Private.CoreLib.
public class NetStandardAsyncMethodTests
{
    private const string Source = /*lang=csharp*/
        """
        using System.Threading.Tasks;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.NetStandardAsync.IAsyncService))]

        namespace Sample.NetStandardAsync
        {
            public interface IAsyncService
            {
                Task<int> GetAsync();

                ValueTask<int> GetValueAsync();

                Task SaveAsync();
            }
        }
        """;

    private static readonly ReferenceAssemblies NetStandard20WithValueTask =
        ReferenceAssemblies.NetStandard.NetStandard20.AddPackages(
            ImmutableArray.Create(new PackageIdentity("System.Threading.Tasks.Extensions", "4.5.4"))
        );

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "NetStandardAsyncMethod.Source.cs",
            snippetFileName: "NetStandardAsyncMethod.Snippet.cs",
            assemblyName: nameof(NetStandardAsyncMethodTests),
            referenceAssemblies: NetStandard20WithValueTask,
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenNetStandardTaskOfTMethod_WhenReturningAsyncValue_ShouldCompile()
    {
        var context = await TestContextTask;

        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            namespace Sample.NetStandardAsync.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        new Sample.NetStandardAsync.IAsyncServiceImposter().GetAsync().ReturnsAsync(1);
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenNetStandardValueTaskOfTMethod_WhenReturningAsyncValue_ShouldCompile()
    {
        var context = await TestContextTask;

        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            namespace Sample.NetStandardAsync.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        new Sample.NetStandardAsync.IAsyncServiceImposter().GetValueAsync().ReturnsAsync(1);
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenNetStandardTaskMethod_WhenThrowingAsync_ShouldCompile()
    {
        var context = await TestContextTask;

        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            namespace Sample.NetStandardAsync.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        new Sample.NetStandardAsync.IAsyncServiceImposter()
                            .SaveAsync()
                            .ThrowsAsync(new System.InvalidOperationException());
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }
}
