using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public partial class IndexerMemberCollisionPreventionTests
    : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenMultipleIndexersAndCommonMemberNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IMultiIndexerCollisionTargetImposter();
            _ = imposter.Default;
            _ = imposter.Count;
            imposter.Indexer();

            imposter[Arg<int>.Any()].Getter().Returns(11);
            imposter[Arg<int>.Any(), Arg<string>.Any()].Setter().Callback((first, second, value) => { });
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
