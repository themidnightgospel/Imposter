using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public partial class IndexerDelegateCollisionPreventionTests
    : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenIndexersMatchingDelegateNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var valueDelegate = new IIndexerValueDelegateCollisionTargetImposter();
            valueDelegate[Arg<int>.Any()].Getter().Returns(() => 10);

            var getterCallback = new IIndexerGetterCallbackDelegateCollisionTargetImposter();
            getterCallback[Arg<string>.Any()].Getter().Callback(value => { });

            var setterCallback = new IIndexerSetterCallbackDelegateCollisionTargetImposter();
            setterCallback[Arg<Guid>.Any()].Setter().Callback((index, value) => { });

            var exceptionGenerator = new IIndexerExceptionGeneratorDelegateCollisionTargetImposter();
            exceptionGenerator[Arg<bool>.Any()].Getter().Throws(flag => new InvalidOperationException(flag.ToString()));
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
