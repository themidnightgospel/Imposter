using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.IndexerImpersonation.IndexerImpersonationTestsShared;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

public sealed partial class IndexerFluentApiTests
{
    [Fact]
    public async Task GivenIndexerGetterSetup_WhenRepeatingReturnsWithoutThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Returns(1).Returns(2);
        }
    }
}
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }
}
