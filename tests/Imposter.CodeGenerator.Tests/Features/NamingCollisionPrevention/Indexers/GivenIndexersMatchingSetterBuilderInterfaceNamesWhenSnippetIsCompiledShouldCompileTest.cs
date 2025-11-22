using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public partial class IndexerBuilderInterfaceCollisionPreventionTests
    : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenIndexersMatchingSetterBuilderInterfaceNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var setterBuilder = new IIndexerSetterBuilderCollisionTargetImposter();
            setterBuilder[Arg<int>.Any()].Setter().Callback((index, value) => { });

            var setterCallbackBuilder = new IIndexerSetterCallbackBuilderCollisionTargetImposter();
            setterCallbackBuilder[Arg<string>.Any()].Setter()
                .Callback((index, value) => { })
                .Then();

            var setterContinuationBuilder = new IIndexerSetterContinuationBuilderCollisionTargetImposter();
            setterContinuationBuilder[Arg<Guid>.Any()].Setter()
                .Callback((index, value) => { })
                .Then()
                .Callback((index, value) => { });

            var setterVerifier = new IIndexerSetterVerifierCollisionTargetImposter();
            setterVerifier[Arg<bool>.Any()].Setter().Called(Count.AtLeast(1));

            var setterFluentBuilder = new IIndexerSetterFluentBuilderCollisionTargetImposter();
            setterFluentBuilder[Arg<long>.Any()].Setter().Callback((index, value) => { });
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
