using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public partial class IndexerBuilderOperationCollisionPreventionTests
    : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenVirtualIndexersMatchingBaseImplementationOperations_WhenSnippetIsCompiled_ShouldCompile()
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
            var baseImplementationIndexer = new UseBaseImplementationIndexerCollisionTargetImposter();
            baseImplementationIndexer[Arg<int>.Any()].Getter().UseBaseImplementation();
            baseImplementationIndexer[Arg<int>.Any()].Setter().UseBaseImplementation();

            var virtualThenIndexer = new ThenVirtualIndexerCollisionTargetImposter();
            virtualThenIndexer[Arg<string>.Any()].Getter()
                .Returns(1)
                .Then()
                .Returns(() => 2);
            virtualThenIndexer[Arg<string>.Any()].Setter()
                .Callback((index, value) => { })
                .Then();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
