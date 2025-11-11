using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public class IndexerBuilderOperationCollisionPreventionTests : IndexerNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task Given_IndexersMatchingBuilderOperationNames_ShouldCompile()
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
            var returnsIndexer = new IReturnsIndexerCollisionTargetImposter();
            returnsIndexer[Arg<int>.Any()].Getter().Returns(1);
            returnsIndexer[Arg<int>.Any()].Getter().Returns(() => 2);

            var throwsIndexer = new IThrowsIndexerCollisionTargetImposter();
            throwsIndexer[Arg<string>.Any()].Getter().Throws(new InvalidOperationException("fail"));
            throwsIndexer[Arg<string>.Any()].Getter().Throws<InvalidOperationException>();

            var callbackIndexer = new ICallbackIndexerCollisionTargetImposter();
            callbackIndexer[Arg<Guid>.Any()].Getter().Callback(value => { });
            callbackIndexer[Arg<Guid>.Any()].Setter().Callback((index, assigned) => { });

            var thenIndexer = new IThenIndexerCollisionTargetImposter();
            thenIndexer[Arg<bool>.Any()].Getter()
                .Returns(3)
                .Then()
                .Returns(() => 4);
            thenIndexer[Arg<bool>.Any()].Setter()
                .Callback((index, assigned) => { })
                .Then()
                .Callback((index, assigned) => { });

            var calledIndexer = new ICalledIndexerCollisionTargetImposter();
            calledIndexer[Arg<long>.Any()].Getter().Called(Count.AtLeast(1));
            calledIndexer[Arg<long>.Any()].Setter().Called(Count.AtLeast(2));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_VirtualIndexersMatchingBaseImplementationOperations_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
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
""");

        AssertNoDiagnostics(diagnostics);
    }
}
