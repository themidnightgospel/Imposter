using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

public class ImposterGeneratorConstructorTests
{
    private const string Source =
        /*lang=csharp*/
        """
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.PrivateOnlyClass))]

            namespace Sample;

            public class PrivateOnlyClass
            {
                private PrivateOnlyClass() { }
                private PrivateOnlyClass(int value) { }
            }

            """;

    [Fact]
    public async Task GivenClassWithOnlyPrivateConstructors_WhenGeneratorRuns_ShouldReportIMP004()
    {
        var compilation = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            Source,
            "ImposterGeneratorConstructorTests"
        );
        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator());

        var runResult = driver.RunGenerators(compilation).GetRunResult();
        var diagnostic = runResult.Diagnostics.ShouldHaveSingleItem();

        diagnostic.Id.ShouldBe(
            DiagnosticDescriptors.ImposterTargetMustHaveAccessibleConstructor.Id
        );
        diagnostic.GetMessage().ShouldContain("Sample.PrivateOnlyClass");
    }
}
