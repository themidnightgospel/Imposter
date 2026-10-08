using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
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
}
