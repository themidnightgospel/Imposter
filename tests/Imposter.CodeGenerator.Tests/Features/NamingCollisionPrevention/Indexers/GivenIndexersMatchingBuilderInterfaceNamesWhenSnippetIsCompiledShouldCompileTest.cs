using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public partial class IndexerBuilderInterfaceCollisionPreventionTests
    : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenIndexersMatchingBuilderInterfaceNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var interfaceBuilder = new IIndexerBuilderInterfaceCollisionTargetImposter();
            interfaceBuilder[Arg<int>.Any()].Getter().Returns(10);

            var getterEntry = new IIndexerGetterEntryCollisionTargetImposter();
            getterEntry[Arg<string>.Any()].Getter().Returns(() => 11);

            var setterEntry = new IIndexerSetterEntryCollisionTargetImposter();
            setterEntry[Arg<Guid>.Any()].Setter().Callback((index, value) => { });
            setterEntry[Arg<Guid>.Any()].Setter().Called(Count.AtLeast(1));
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
