using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public class IndexerBuilderInterfaceCollisionPreventionTests : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenIndexersMatchingBuilderInterfaceNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
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
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexersMatchingGetterBuilderInterfaceNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
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
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexersMatchingSetterBuilderInterfaceNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
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
""");

        AssertNoDiagnostics(diagnostics);
    }
}


