using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.InterfaceImpersonation;

// A static member is called through the interface, never through the imposter's instance, so the imposter has no setup
// for it.
public class StaticInterfaceMemberTests
{
    private const string Target = /*lang=csharp*/
        """
        using System;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IService))]

        namespace Sample
        {
            public interface IBase
            {
                static event Action? Raised;
            }

            public interface IService : IBase
            {
                static int Shared() => 1;
                static int Count { get; set; }
                static event EventHandler? Changed;
                new event Action Raised;
                int Get();
            }
        }
        """;

    [Fact]
    public async Task GivenStaticMethod_WhenItsSetupIsUsed_ShouldNotCompile()
    {
        var diagnostics = await CompileUsage("imposter.Shared();");

        GeneratorTestHelper.AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 7
        );
    }

    [Fact]
    public async Task GivenStaticProperty_WhenItsSetupIsUsed_ShouldNotCompile()
    {
        var diagnostics = await CompileUsage("var count = imposter.Count;");

        GeneratorTestHelper.AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 7
        );
    }

    [Fact]
    public async Task GivenStaticEvent_WhenItsSetupIsUsed_ShouldNotCompile()
    {
        var diagnostics = await CompileUsage("var changed = imposter.Changed;");

        GeneratorTestHelper.AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 7
        );
    }

    [Fact]
    public async Task GivenStaticEventHiddenByInstanceEvent_WhenBaseInterfaceViewIsUsed_ShouldNotOfferIt()
    {
        var diagnostics = await CompileUsage("imposter.For(default(Sample.IBase)).Raised.Raise();");

        GeneratorTestHelper.AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 7
        );
    }

    [Fact]
    public async Task GivenStaticSpanProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { static System.Span<byte> Buffer => default; int Get(); }",
            "imposter.Get().Returns(1); imposter.Instance().Get();",
            nameof(StaticInterfaceMemberTests)
        );
    }

    private static async Task<ImmutableArray<Diagnostic>> CompileUsage(string usage)
    {
        var context = await GeneratorTestHelper.CreateContext(
            Target,
            baseSourceFileName: "StaticInterfaceMembers.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(StaticInterfaceMemberTests)
        );

        return context.CompileSnippet(
            /*lang=csharp*/
            $$"""
            public static class Usage
            {
                public static void Run()
                {
                    var imposter = new Sample.IServiceImposter();

                    {{usage}}
                }
            }
            """
        );
    }
}
