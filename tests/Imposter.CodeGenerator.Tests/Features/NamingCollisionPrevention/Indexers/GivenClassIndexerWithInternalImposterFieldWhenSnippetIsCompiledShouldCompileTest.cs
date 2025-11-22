using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public partial class IndexerMemberCollisionPreventionTests
    : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenClassIndexerWithInternalImposterField_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerImposterFieldCollisionTargetImposter();
            imposter[Arg<int>.Any()].Getter().Returns(42);
            imposter[Arg<int>.Any()].Setter().Callback((index, value) => { });
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
