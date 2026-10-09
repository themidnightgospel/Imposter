using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.Diagnostics;

// GeneratorTestHelper expects a generator run without errors, so tests of the diagnostics that stop generation run
// the generator directly.
internal static class TargetGeneratorRun
{
    internal static async Task<GeneratorRunResult> RunAsync(
        string source,
        string assemblyName,
        LanguageVersion languageVersion = LanguageVersion.CSharp9
    )
    {
        var compilation = await CreateCompilationAsync(languageVersion, source, assemblyName);

        return CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(compilation)
            .GetRunResult()
            .Results.Single();
    }
}
