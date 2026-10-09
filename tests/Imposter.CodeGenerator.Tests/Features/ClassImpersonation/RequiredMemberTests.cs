#if ROSLYN4_4_OR_GREATER
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

// The imposter creates its instance with new and overrides virtual members, so required members need
// [SetsRequiredMembers] on the instance's constructors and the required modifier on the overrides.
public class RequiredMemberTests
{
    [Fact]
    public async Task GivenClassWithRequiredProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Service { public required string Name { get; set; } public virtual int Get() => 0; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Get().Returns(1); imposter.Instance().Get();"
        );
    }

    [Fact]
    public async Task GivenClassWithRequiredField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Service { public required int Id; public virtual int Get() => 0; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Instance().Get();"
        );
    }

    [Fact]
    public async Task GivenClassWithVirtualRequiredProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Service { public required virtual string Name { get; set; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Name.Getter().Returns(\"a\"); _ = imposter.Instance().Name;"
        );
    }

    [Fact]
    public async Task GivenClassWithAbstractRequiredProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public abstract class Service { public required abstract string Name { get; set; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Name.Getter().Returns(\"a\"); _ = imposter.Instance().Name;"
        );
    }

    [Fact]
    public async Task GivenClassWithVirtualRequiredInitOnlyProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Service { public required virtual string Name { get; init; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Name.Getter().Returns(\"a\"); _ = imposter.Instance().Name;"
        );
    }

    [Fact]
    public async Task GivenClassInheritingRequiredProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Entity { public required string Id { get; set; } } public class Service : Entity { public virtual int Get() => 0; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Instance().Get();"
        );
    }

    [Fact]
    public async Task GivenClassWithRequiredPropertyAndConstructorParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Service { public Service(int seed) { } public required string Name { get; set; } public virtual int Get() => 0; }",
            "var imposter = new Sample.ServiceImposter(1); imposter.Instance().Get();"
        );
    }

    // Before .NET 7, a project that uses required members declares the two attributes the compiler needs for them, and
    // SetsRequiredMembersAttribute only when it wants it.
    private const string RequiredMemberAttributes = /*lang=csharp*/
        """
        namespace System.Runtime.CompilerServices
        {
            internal sealed class RequiredMemberAttribute : Attribute { }

            internal sealed class CompilerFeatureRequiredAttribute : Attribute
            {
                public CompilerFeatureRequiredAttribute(string featureName) { }
            }
        }
        """;

    private const string SetsRequiredMembersAttribute = /*lang=csharp*/
        """
        namespace System.Diagnostics.CodeAnalysis
        {
            [AttributeUsage(AttributeTargets.Constructor)]
            internal sealed class SetsRequiredMembersAttribute : Attribute { }
        }
        """;

    [Fact]
    public async Task GivenNetStandardProjectWithoutSetsRequiredMembersAttribute_WhenGeneratorRuns_ShouldReportIMP010()
    {
        var context = await CreateNetStandardContext(RequiredMemberAttributes);

        var result = CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(context.Compilation)
            .GetRunResult()
            .Results.Single();

        result
            .Diagnostics.ShouldHaveSingleItem()
            .Id.ShouldBe(
                DiagnosticDescriptors.ImposterTargetRequiredMembersNeedSetsRequiredMembers.Id
            );
    }

    // Generators don't see each other's output, so the imposter's generator sees none of these attributes.
    [Fact]
    public async Task GivenNetStandardProjectWhoseAttributesComeFromAnotherGenerator_WhenImpostersAreGenerated_ShouldCompile()
    {
        var context = await CreateNetStandardContext(polyfills: string.Empty);

        CSharpGeneratorDriver
            .Create(
                [
                    new ImposterGenerator().AsSourceGenerator(),
                    new PolyfillGenerator().AsSourceGenerator(),
                ],
                parseOptions: (CSharpParseOptions)context.Compilation.SyntaxTrees.Single().Options
            )
            .RunGeneratorsAndUpdateCompilation(
                context.Compilation,
                out var compilation,
                out var generatorDiagnostics
            );

        compilation
            .GetDiagnostics()
            .Concat(generatorDiagnostics)
            .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
            .ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenNetStandardProjectDeclaringSetsRequiredMembersAttribute_WhenImposterIsUsed_ShouldCompile()
    {
        var context = await CreateNetStandardContext(
            RequiredMemberAttributes + SetsRequiredMembersAttribute
        );

        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                /*lang=csharp*/
                """
                using Imposter.Abstractions;

                public static class Usage
                {
                    public static void Run()
                    {
                        var imposter = new Sample.ServiceImposter();
                        imposter.Get().Returns(1);
                        imposter.Instance().Get();
                    }
                }
                """
            )
        );
    }

    private static Task<GeneratorTestContext> CreateNetStandardContext(string polyfills) =>
        GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.Service))]

            namespace Sample
            {
                public class Service
                {
                    public required string Name { get; set; }

                    public virtual int Get() => 0;
                }
            }

            {{polyfills}}
            """,
            baseSourceFileName: "RequiredMembers.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(RequiredMemberTests),
            ReferenceAssemblies.NetStandard.NetStandard20,
            LanguageVersion.CSharp11
        );

    private static Task AssertRequiredTargetCompiles(string targetDeclaration, string usage) =>
        AssertCompiles(
            "Sample.Service",
            targetDeclaration,
            usage,
            nameof(RequiredMemberTests),
            LanguageVersion.CSharp11
        );

    // Adds the polyfills as its output, as PolySharp does.
    private sealed class PolyfillGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context) =>
            context.RegisterSourceOutput(
                context.CompilationProvider,
                static (output, _) =>
                {
                    output.AddSource("RequiredMemberAttributes.g.cs", RequiredMemberAttributes);
                    output.AddSource(
                        "SetsRequiredMembersAttribute.g.cs",
                        SetsRequiredMembersAttribute
                    );
                }
            );
    }
}
#endif
