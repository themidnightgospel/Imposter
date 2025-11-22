using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public partial class IndexerDelegateCollisionPreventionTests : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenIndexersMatchingArgumentAndParameterNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var arguments = new IIndexerArgumentsCollisionTargetImposter();
            arguments[Arg<int>.Any(), Arg<int>.Any()].Getter().Returns(1);

            var argumentsCriteria = new IIndexerArgumentsCriteriaCollisionTargetImposter();
            argumentsCriteria[Arg<int>.Any(), Arg<string>.Any()].Getter().Returns(() => 2);

            var parameters = new IIndexerParameterCollisionTargetImposter();
            parameters[Arg<int>.Any()].Getter().Returns(() => 3);
            parameters[Arg<string>.Any()].Getter().Throws(value => new InvalidOperationException(value));
            parameters[Arg<Guid>.Any()].Setter().Callback((index, value) => { });
            parameters[Arg<long>.Any()].Setter().Called(Count.AtLeast(1));
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}

