using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue25;

public class InterfaceWithOptionalParametersCausesCs1737
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.Issue25.IServiceInterface))]
        [assembly: GenerateImposter(typeof(Sample.Issue25.INullableOutParameterService))]

        namespace Sample.Issue25
        {
            public interface IServiceInterface
            {
                void Process(string data, bool validate = true);

                bool Calculate(int value, string option = null, params int[] extras);

                void Execute(string command, bool validate = false, int timeout = 30);
            }

            public class Param1 { }
            public class Param2 { }
            public class Param3 { }

            public interface INullableOutParameterService
            {
                bool Func(Param1 definition, Param2? parent, out Param3? viewModel);
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
            assemblyName: nameof(InterfaceWithOptionalParametersCausesCs1737),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task CreateNewServiceInterfaceImposter_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.Issue25;
            using Imposter.Abstractions;

            namespace Sample.Issue25.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IServiceInterfaceImposter();
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task CreateNewNullableOutParameterServiceImposter_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.Issue25;
            using Imposter.Abstractions;

            namespace Sample.Issue25.Usage
            {
                public static class NullableOutScenario
                {
                    public static void Execute()
                    {
                        var imposter = new INullableOutParameterServiceImposter();
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    private static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }
}
