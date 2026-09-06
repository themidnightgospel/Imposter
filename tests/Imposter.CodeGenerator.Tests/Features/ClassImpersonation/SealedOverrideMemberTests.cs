using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

public class SealedOverrideMemberTests
{
    private const string Source = /*lang=csharp*/
        """
        using System;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.SealedOverrides))]
        [assembly: GenerateImposter(typeof(Sample.InheritedSealedOverrides))]

        namespace Sample
        {
            public class BaseMembers
            {
                public virtual int Value { get; set; }
                public virtual int this[int index] { get => index; set { } }
                public virtual event Action Changed { add { } remove { } }

                public virtual int OtherValue { get; set; }
                public virtual int this[string index] { get => index.Length; set { } }
                public virtual event Action OtherChanged { add { } remove { } }
                public virtual int UnchangedValue { get; set; }
            }

            public class SealedOverrides : BaseMembers
            {
                public sealed override int Value { get; set; }
                public sealed override int this[int index] { get => index; set { } }
                public sealed override event Action Changed { add { } remove { } }

                public override int OtherValue { get; set; }
                public override int this[string index] { get => index.Length; set { } }
                public override event Action OtherChanged { add { } remove { } }
            }

            public class InheritedSealedOverrides : SealedOverrides { }
        }
        """;

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "SealedOverrideMembers.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(SealedOverrideMemberTests)
        );

    [Fact]
    public async Task Given_SealedOverrides_When_UsingInstanceAndVirtualSetups_Should_Compile()
    {
        await AssertSupportedMembersCompile("SealedOverridesImposter");
    }

    [Fact]
    public async Task Given_InheritedSealedOverrides_When_UsingInstanceAndVirtualSetups_Should_Compile()
    {
        await AssertSupportedMembersCompile("InheritedSealedOverridesImposter");
    }

    [Fact]
    public async Task Given_SealedProperty_When_AccessingSetup_Should_ReportMissingMember()
    {
        await AssertSetupFails(
            "imposter.Value.Getter().Returns(1);",
            WellKnownCsCompilerErrorCodes.MemberNotFound
        );
    }

    [Fact]
    public async Task Given_SealedIndexer_When_AccessingSetup_Should_ReportInvalidArgument()
    {
        // The string indexer remains configurable, but the sealed int overload is absent.
        await AssertSetupFails("imposter[Arg<int>.Any()].Getter().Returns(1);", "CS1503");
    }

    [Fact]
    public async Task Given_SealedEvent_When_AccessingSetup_Should_ReportMissingMember()
    {
        await AssertSetupFails(
            "imposter.Changed.Raised(Count.Once());",
            WellKnownCsCompilerErrorCodes.MemberNotFound
        );
    }

    private static async Task AssertSupportedMembersCompile(string imposterType)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            $$"""
            using System;
            using Imposter.Abstractions;

            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new {{imposterType}}();
                        var instance = imposter.Instance();
                        instance.Value = instance.Value + 1;
                        instance[0] = instance[0] + 1;
                        Action handler = () => { };
                        instance.Changed += handler;
                        instance.Changed -= handler;

                        imposter.OtherValue.Getter().Returns(42);
                        imposter.OtherValue.Setter(Arg<int>.Any()).Called(Count.Never());
                        imposter[Arg<string>.Any()].Getter().Returns(42);
                        imposter[Arg<string>.Any()].Setter().Called(Count.Never());
                        imposter.OtherChanged.Raised(Count.Never());
                        imposter.UnchangedValue.Getter().Returns(42);
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    private static async Task AssertSetupFails(string setup, string expectedId)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        var diagnosticsByType = new[]
        {
            "SealedOverridesImposter",
            "InheritedSealedOverridesImposter",
        }.Select(imposterType =>
            context.CompileSnippet( /*lang=csharp*/
                $$"""
                using Imposter.Abstractions;
                namespace Sample
                {
                    public static class Scenario
                    {
                        public static void Execute()
                        {
                            var imposter = new {{imposterType}}();
                            {{setup}}
                        }
                    }
                }
                """
            )
        );

        foreach (var diagnostics in diagnosticsByType)
        {
            GeneratorTestHelper.AssertSingleDiagnostic(diagnostics, expectedId, expectedLine: 9);
        }
    }
}
