using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

// An event's raise methods and Raised criteria take the delegate's parameters with their nullable annotations, so a
// null argument warns only where the delegate's parameter is not nullable.
public class NullableEventParameterCompilationTests
{
    private const string Source = /*lang=csharp*/
        """
        #nullable enable
        using System;
        using System.Threading.Tasks;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IService))]

        namespace Sample
        {
            public delegate void MessageHandler(string? message, string channel);

            public delegate Task MessageAsyncHandler(string? message, string channel);

            public interface IService
            {
                event EventHandler Changed;

                event MessageHandler Received;

                event MessageAsyncHandler ReceivedAsync;

                event Func<object?, EventArgs, Task> ChangedAsync;
            }
        }
        """;

    private const string SnippetFileName = "NullableEventParameter.Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "NullableEventParameter.Source.cs",
            snippetFileName: SnippetFileName,
            assemblyName: nameof(NullableEventParameterCompilationTests)
        );

    [Fact]
    public async Task GivenEventHandlerEvent_WhenRaisedAndVerifiedWithNullSender_ShouldNotWarn()
    {
        var diagnostics = await CompileUsage(
            "imposter.Changed.Raise(null, EventArgs.Empty); imposter.Changed.Raised(Arg<object?>.IsDefault(), Arg<EventArgs>.Any(), Count.Once());"
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenNullableDelegateParameter_WhenRaisedAndVerifiedWithNull_ShouldNotWarn()
    {
        var diagnostics = await CompileUsage(
            "imposter.Received.Raise(null, \"news\"); imposter.Received.Raised(Arg<string?>.IsDefault(), Arg<string>.Any(), Count.Once());"
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenNullableAsyncDelegateParameter_WhenRaisedAsyncAndVerifiedWithNull_ShouldNotWarn()
    {
        var diagnostics = await CompileUsage(
            "_ = imposter.ReceivedAsync.RaiseAsync(null, \"news\"); imposter.ReceivedAsync.Raised(Arg<string?>.IsDefault(), Arg<string>.Any(), Count.Once());"
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenDelegateTypeArgumentAnnotatedNullable_WhenHandlerIsRaisedWithNull_ShouldNotWarn()
    {
        var diagnostics = await CompileUsage(
            "imposter.ChangedAsync.Callback((sender, e) => System.Threading.Tasks.Task.CompletedTask); imposter.Instance().ChangedAsync += (sender, e) => System.Threading.Tasks.Task.CompletedTask; _ = imposter.ChangedAsync.RaiseAsync(null, EventArgs.Empty); imposter.ChangedAsync.HandlerInvoked(Arg<Func<object?, EventArgs, System.Threading.Tasks.Task>>.Any(), Count.Once());"
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenNonNullableDelegateParameter_WhenRaisedWithNull_ShouldWarn()
    {
        var diagnostics = await CompileUsage("imposter.Received.Raise(\"hello\", null);");

        GeneratorTestHelper.AssertSingleDiagnostic(
            diagnostics,
            "CS8625",
            expectedLine: 11,
            SnippetFileName
        );
    }

    private static async Task<ImmutableArray<Diagnostic>> CompileUsage(string usage)
    {
        var context = await TestContextTask.ConfigureAwait(false);

        return context.CompileSnippet(
            /*lang=csharp*/
            $$"""
            #nullable enable
            using System;
            using Imposter.Abstractions;
            using Sample;

            public static class Usage
            {
                public static void Run()
                {
                    var imposter = new IServiceImposter();
                    {{usage}}
                }
            }
            """,
            DiagnosticSeverity.Warning
        );
    }
}
