using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

public class ImposterGeneratorTargetTypeTests
{
    private const string Source =
        /*lang=csharp*/
        """
                using Imposter.Abstractions;

                [assembly: GenerateImposter(typeof(Sample.SealedClass))]

                namespace Sample;

                public sealed class SealedClass { }
            """;

    [Fact]
    public async Task GivenSealedClassTarget_WhenGeneratorRuns_ShouldReportIMP002()
    {
        var compilation = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            Source,
            "ImposterGeneratorTargetTypeTests"
        );
        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator());

        var runResult = driver.RunGenerators(compilation).GetRunResult();
        var diagnostic = runResult.Diagnostics.ShouldHaveSingleItem();

        diagnostic.Id.ShouldBe(DiagnosticDescriptors.InvalidImposterTarget.Id);
        diagnostic.GetMessage().ShouldContain("Sample.SealedClass");
    }
}
