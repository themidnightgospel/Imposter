using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.GenericTargets;

public class ImposterGeneratorClosedGenericTargetTests
{
    private const string ClosedTargetSource =
        /*lang=csharp*/
        """
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IRepository<int>))]

            namespace Sample;

            public interface IRepository<TEntity> { TEntity Get(); }
            """;

    private const string OpenTargetSource =
        /*lang=csharp*/
        """
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IRepository<>))]

            namespace Sample;

            public interface IRepository<TEntity> { TEntity Get(); }
            """;

    [Fact]
    public async Task GivenClosedGenericTarget_WhenGeneratorRuns_ShouldReportIMP006()
    {
        var compilation = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            ClosedTargetSource,
            nameof(GivenClosedGenericTarget_WhenGeneratorRuns_ShouldReportIMP006)
        );

        var runResult = CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(compilation)
            .GetRunResult();

        runResult
            .Diagnostics.ShouldHaveSingleItem()
            .Id.ShouldBe(DiagnosticDescriptors.ClosedGenericImposterTarget.Id);
    }

    [Fact]
    public async Task GivenOpenGenericTarget_WhenGeneratorRuns_ShouldNotReportDiagnostics()
    {
        var compilation = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            OpenTargetSource,
            nameof(GivenOpenGenericTarget_WhenGeneratorRuns_ShouldNotReportDiagnostics)
        );

        var runResult = CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(compilation)
            .GetRunResult();

        runResult.Diagnostics.ShouldBeEmpty();
    }

#if ROSLYN4_14_OR_GREATER
    // Only the Roslyn 4.14+ builds generate the Imposter() extension, for the open type like the imposter.
    [Fact]
    public async Task GivenClosedGenericTarget_WhenImposterExtensionIsCalledWithAnotherTypeArgument_ShouldCompile()
    {
        var errors = await GeneratedCompilationErrors(
            LanguageVersion.Preview,
            ClosedTargetSource
                + /*lang=csharp*/
                """

                public static class Usage
                {
                    public static IRepositoryImposter<string> Create() => IRepository<string>.Imposter();
                }
                """,
            nameof(
                GivenClosedGenericTarget_WhenImposterExtensionIsCalledWithAnotherTypeArgument_ShouldCompile
            )
        );

        errors.ShouldBeEmpty();
    }
#endif

    // A closed registration generates the open type's imposter, which can be closed with any type argument.
    [Fact]
    public async Task GivenClosedGenericTarget_WhenImposterIsClosedWithAnotherTypeArgument_ShouldImpersonateThatType()
    {
        var errors = await GeneratedCompilationErrors(
            LanguageVersion.CSharp10,
            ClosedTargetSource
                + /*lang=csharp*/
                """

                public static class Usage
                {
                    public static IRepository<string> Create() => new IRepositoryImposter<string>().Instance();
                }
                """,
            nameof(
                GivenClosedGenericTarget_WhenImposterIsClosedWithAnotherTypeArgument_ShouldImpersonateThatType
            )
        );

        errors.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenTwoClosedRegistrationsOfOneGenericTarget_WhenGeneratorRuns_ShouldGenerateOneImposter()
    {
        var runResult = await RunGenerator(
            /*lang=csharp*/
            """
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IRepository<int>))]
            [assembly: GenerateImposter(typeof(Sample.IRepository<string>))]

            namespace Sample;

            public interface IRepository<TEntity> { TEntity Get(); }
            """,
            nameof(
                GivenTwoClosedRegistrationsOfOneGenericTarget_WhenGeneratorRuns_ShouldGenerateOneImposter
            )
        );

        runResult.GeneratedTrees.Length.ShouldBe(1);
    }

    [Fact]
    public async Task GivenClosedAndOpenRegistrationOfOneGenericTarget_WhenGeneratorRuns_ShouldGenerateOneImposter()
    {
        var runResult = await RunGenerator(
            /*lang=csharp*/
            """
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IRepository<int>))]
            [assembly: GenerateImposter(typeof(Sample.IRepository<>))]

            namespace Sample;

            public interface IRepository<TEntity> { TEntity Get(); }
            """,
            nameof(
                GivenClosedAndOpenRegistrationOfOneGenericTarget_WhenGeneratorRuns_ShouldGenerateOneImposter
            )
        );

        runResult.GeneratedTrees.Length.ShouldBe(1);
    }

    // The imposter declares only the target's own type parameters, so a containing type's type arguments stay.
    [Fact]
    public async Task GivenClosedRegistrationOfTypeNestedInGenericClass_WhenImposterIsUsed_ShouldCompile()
    {
        var errors = await GeneratedCompilationErrors(
            LanguageVersion.CSharp10,
            /*lang=csharp*/
            """
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.Outer<int>.Inner))]

            namespace Sample;

            public class Outer<T> { public interface Inner { T Get(); } }

            public static class Usage
            {
                public static Outer<int>.Inner Create() => new InnerImposter().Instance();
            }
            """,
            nameof(
                GivenClosedRegistrationOfTypeNestedInGenericClass_WhenImposterIsUsed_ShouldCompile
            )
        );

        errors.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenClosedGenericTargetNestedInGenericClass_WhenImposterIsClosedWithAnotherTypeArgument_ShouldImpersonateThatType()
    {
        var errors = await GeneratedCompilationErrors(
            LanguageVersion.CSharp10,
            /*lang=csharp*/
            """
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.Outer<int>.Inner<string>))]

            namespace Sample;

            public class Outer<T> { public interface Inner<U> { T Get(U value); } }

            public static class Usage
            {
                public static Outer<int>.Inner<long> Create() => new InnerImposter<long>().Instance();
            }
            """,
            nameof(
                GivenClosedGenericTargetNestedInGenericClass_WhenImposterIsClosedWithAnotherTypeArgument_ShouldImpersonateThatType
            )
        );

        errors.ShouldBeEmpty();
    }

    private static async Task<GeneratorDriverRunResult> RunGenerator(
        string source,
        string assemblyName
    )
    {
        var compilation = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            source,
            assemblyName
        );

        return CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(compilation)
            .GetRunResult();
    }

    private static async Task<Diagnostic[]> GeneratedCompilationErrors(
        LanguageVersion languageVersion,
        string source,
        string assemblyName
    )
    {
        var compilation = await CreateCompilationAsync(languageVersion, source, assemblyName);
        CSharpGeneratorDriver
            .Create(
                [new ImposterGenerator().AsSourceGenerator()],
                parseOptions: new CSharpParseOptions(languageVersion)
            )
            .RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out _);

        return
        [
            .. updatedCompilation
                .GetDiagnostics()
                .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error),
        ];
    }
}
