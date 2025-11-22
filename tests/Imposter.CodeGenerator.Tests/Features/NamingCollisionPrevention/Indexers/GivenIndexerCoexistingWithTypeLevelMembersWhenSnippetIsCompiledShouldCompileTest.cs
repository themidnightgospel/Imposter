using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public partial class IndexerMemberCollisionPreventionTests
    : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenIndexerCoexistingWithTypeLevelMembers_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using System;
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IIndexerMemberCollisionTargetImposter();
            _ = imposter.Indexer;
            _ = imposter._IndexerIndexer;
            _ = imposter._IndexerDefaultIndexerBehaviour;
            _ = imposter.Default;
            _ = imposter.Count;
            _ = imposter._invocationBehavior;
            _ = imposter.ImposterTargetInstance;
            _ = imposter._imposterInstance;
            _ = imposter.Instance();
            imposter.IndexerMethod();

            imposter[Arg<int>.Any()].Getter().Returns(7);
            imposter[Arg<string>.Any(), Arg<string>.Any()].Setter().Callback((first, second, value) => { });
            imposter[Arg<Guid>.Any()].Setter().Callback((index, value) => { });

        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
