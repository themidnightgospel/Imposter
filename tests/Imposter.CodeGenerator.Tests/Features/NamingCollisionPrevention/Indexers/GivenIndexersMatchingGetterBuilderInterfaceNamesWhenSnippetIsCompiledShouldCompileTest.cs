using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public partial class IndexerBuilderInterfaceCollisionPreventionTests
    : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenIndexersMatchingGetterBuilderInterfaceNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var getterBuilder = new IIndexerGetterBuilderCollisionTargetImposter();
            getterBuilder[Arg<int>.Any()].Getter().Returns(1);

            var getterOutcomeBuilder = new IIndexerGetterOutcomeBuilderCollisionTargetImposter();
            getterOutcomeBuilder[Arg<string>.Any()].Getter().Returns(2).Then();

            var getterContinuationBuilder = new IIndexerGetterContinuationBuilderCollisionTargetImposter();
            getterContinuationBuilder[Arg<Guid>.Any()].Getter().Returns(3).Then().Returns(() => 4);

            var getterCallbackBuilder = new IIndexerGetterCallbackBuilderCollisionTargetImposter();
            getterCallbackBuilder[Arg<bool>.Any()].Getter()
                .Returns(5)
                .Then()
                .Callback(value => { });

            var getterVerifier = new IIndexerGetterVerifierCollisionTargetImposter();
            getterVerifier[Arg<long>.Any()].Getter().Called(Count.AtLeast(1));

            var getterFluentBuilder = new IIndexerGetterFluentBuilderCollisionTargetImposter();
            getterFluentBuilder[Arg<double>.Any()].Getter()
                .Returns(7)
                .Then()
                .Returns(() => 8)
                .Then();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
