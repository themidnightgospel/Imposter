using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Generators;

public class ImposterTypeNameCollisionTests
{
    private const string NestedTargets = /*lang=csharp*/
        """
        namespace Sample
        {
            public class A { public interface IService { int Get(); } }
            public class B { public interface IService { int Get(); } }
        }
        """;

    [Fact]
    public async Task GivenSameNamedNestedTargets_WhenGeneratorRuns_ShouldReportIMP007ForEachTarget()
    {
        var runResult = await RunGenerator(
            /*lang=csharp*/
            """
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.A.IService))]
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.B.IService))]
            """
        );

        runResult
            .Diagnostics.Select(diagnostic => diagnostic.Id)
            .ShouldBe([
                DiagnosticDescriptors.ImposterTypeNameCollision.Id,
                DiagnosticDescriptors.ImposterTypeNameCollision.Id,
            ]);
    }

    [Fact]
    public async Task GivenSameNamedNestedTargets_WhenGeneratorRuns_ShouldNameTheOtherTargetAndTheImposterType()
    {
        var runResult = await RunGenerator(
            /*lang=csharp*/
            """
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.A.IService))]
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.B.IService))]
            """
        );

        runResult
            .Diagnostics.Select(diagnostic => diagnostic.GetMessage())
            .ShouldBe([
                "The imposters of 'Sample.A.IService' and 'Sample.B.IService' would both be 'Sample.IServiceImposter'; register one of them with putInTheSameNamespace: false to generate it in its own namespace",
                "The imposters of 'Sample.B.IService' and 'Sample.A.IService' would both be 'Sample.IServiceImposter'; register one of them with putInTheSameNamespace: false to generate it in its own namespace",
            ]);
    }

    [Fact]
    public async Task GivenSameNamedNestedTargets_WhenGeneratorRuns_ShouldGenerateNeitherImposter()
    {
        var runResult = await RunGenerator(
            /*lang=csharp*/
            """
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.A.IService))]
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.B.IService))]
            """
        );

        runResult.Results.Single().GeneratedSources.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenTwoClosedRegistrationsOfOneGenericType_WhenGeneratorRuns_ShouldReportIMP007ForEach()
    {
        var compilation = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            /*lang=csharp*/
            """
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.IRepository<int>))]
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.IRepository<string>))]

            namespace Sample { public interface IRepository<T> { T Get(); } }
            """,
            nameof(ImposterTypeNameCollisionTests)
        );

        CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(compilation)
            .GetRunResult()
            .Diagnostics.Select(diagnostic => diagnostic.Id)
            .ShouldBe([
                DiagnosticDescriptors.ImposterTypeNameCollision.Id,
                DiagnosticDescriptors.ImposterTypeNameCollision.Id,
            ]);
    }

    [Fact]
    public async Task GivenNestedGenericTargetsWithDifferentTypeParameterNames_WhenGeneratorRuns_ShouldReportIMP007ForEach()
    {
        var compilation = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            /*lang=csharp*/
            """
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.A.IService<>))]
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.B.IService<>))]

            namespace Sample
            {
                public class A { public interface IService<T> { T Get(); } }
                public class B { public interface IService<TKey> { TKey Get(); } }
            }
            """,
            nameof(ImposterTypeNameCollisionTests)
        );

        CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(compilation)
            .GetRunResult()
            .Diagnostics.Select(diagnostic => diagnostic.Id)
            .ShouldBe([
                DiagnosticDescriptors.ImposterTypeNameCollision.Id,
                DiagnosticDescriptors.ImposterTypeNameCollision.Id,
            ]);
    }

    [Fact]
    public async Task GivenSameNamedNestedTargetsInDedicatedNamespaces_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImpostersCompile(
            /*lang=csharp*/
            """
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.A.IService), putInTheSameNamespace: false)]
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.B.IService), putInTheSameNamespace: false)]
            """ + NestedTargets
        );
    }

    [Fact]
    public async Task GivenOneOfSameNamedNestedTargetsInDedicatedNamespace_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImpostersCompile(
            /*lang=csharp*/
            """
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.A.IService))]
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.B.IService), putInTheSameNamespace: false)]
            """ + NestedTargets
        );
    }

    [Fact]
    public async Task GivenSameNamedTargetsInDifferentNamespaces_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImpostersCompile(
            /*lang=csharp*/
            """
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(First.IService))]
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Second.IService))]

            namespace First { public interface IService { int Get(); } }
            namespace Second { public interface IService { int Get(); } }
            """
        );
    }

    private static async Task<Microsoft.CodeAnalysis.GeneratorDriverRunResult> RunGenerator(
        string registrations
    )
    {
        var compilation = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            registrations + NestedTargets,
            nameof(ImposterTypeNameCollisionTests)
        );

        return CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(compilation)
            .GetRunResult();
    }

    private static async Task AssertImpostersCompile(string source)
    {
        var context = await GeneratorTestHelper.CreateContext(
            source,
            baseSourceFileName: "ImposterTypeNameCollision.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(ImposterTypeNameCollisionTests)
        );

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }
}
