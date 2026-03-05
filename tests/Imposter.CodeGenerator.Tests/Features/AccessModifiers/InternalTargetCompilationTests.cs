using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.AccessModifiers;

public class InternalTargetCompilationTests
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;
        using System;
        using System.Threading.Tasks;

        [assembly: GenerateImposter(typeof(Sample.AccessModifiers.IInternalService))]

        namespace Sample.AccessModifiers
        {
            internal interface IInternalService
            {
                void MethodWithInternalParam(InternalModel model);
                InternalResult MethodWithInternalReturn();
                Task<InternalData> MethodWithInternalTaskReturn();
                InternalProp Property { get; set; }
                InternalValue this[int index] { get; }
                event EventHandler<InternalEventArgs> SomethingHappened;
            }

            internal class InternalModel { }
            internal class InternalResult { }
            internal class InternalData { }
            internal class InternalProp { }
            internal class InternalValue { }
            internal class InternalEventArgs : EventArgs { }
        }
        """;

    private const string BaseSourceFileName = "GeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(InternalTargetCompilationTests),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenInternalInterfaceWithInternalParam_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.AccessModifiers;

            namespace Sample.AccessModifiers.Usage
            {
                internal static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IInternalServiceImposter();
                        imposter.MethodWithInternalParam(Imposter.Abstractions.Arg<InternalModel>.Any());
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenInternalInterfaceWithInternalReturn_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.AccessModifiers;

            namespace Sample.AccessModifiers.Usage
            {
                internal static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IInternalServiceImposter();
                        imposter.MethodWithInternalReturn().Returns(new InternalResult());
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenInternalInterfaceWithInternalTaskReturn_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.AccessModifiers;
            using System.Threading.Tasks;

            namespace Sample.AccessModifiers.Usage
            {
                internal static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IInternalServiceImposter();
                        imposter.MethodWithInternalTaskReturn().Returns(Task.FromResult(new InternalData()));
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenInternalInterfaceWithInternalProperty_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.AccessModifiers;

            namespace Sample.AccessModifiers.Usage
            {
                internal static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IInternalServiceImposter();
                        imposter.Property.Getter().Returns(new InternalProp());
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenInternalInterfaceWithInternalIndexer_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.AccessModifiers;

            namespace Sample.AccessModifiers.Usage
            {
                internal static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IInternalServiceImposter();
                        imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Returns(new InternalValue());
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenInternalInterfaceWithInternalEvent_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.AccessModifiers;

            namespace Sample.AccessModifiers.Usage
            {
                internal static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IInternalServiceImposter();
                        imposter.SomethingHappened.Raise(null, new InternalEventArgs());
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
