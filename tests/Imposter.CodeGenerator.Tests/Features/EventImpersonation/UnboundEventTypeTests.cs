using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

// An event whose type doesn't bind is a compile error the user already sees, as in code being edited. The imposter
// leaves the event out instead of failing.
public class UnboundEventTypeTests
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IService))]

        namespace Sample
        {
            public class System { }

            public interface IService
            {
                event System.Action Changed;
                int Get();
            }
        }
        """;

    [Fact]
    public async Task GivenEventWhoseTypeDoesNotBind_WhenGeneratorRuns_ShouldNotReportACrash()
    {
        var runResult = await RunGenerator(
            nameof(GivenEventWhoseTypeDoesNotBind_WhenGeneratorRuns_ShouldNotReportACrash)
        );

        runResult.Diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenEventWhoseTypeDoesNotBind_WhenGeneratorRuns_ShouldGenerateTheImposter()
    {
        var runResult = await RunGenerator(
            nameof(GivenEventWhoseTypeDoesNotBind_WhenGeneratorRuns_ShouldGenerateTheImposter)
        );

        runResult.GeneratedTrees.Length.ShouldBe(1);
    }

    private static async Task<GeneratorDriverRunResult> RunGenerator(string assemblyName)
    {
        var compilation = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            Source,
            assemblyName
        );

        return CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(compilation)
            .GetRunResult();
    }
}
