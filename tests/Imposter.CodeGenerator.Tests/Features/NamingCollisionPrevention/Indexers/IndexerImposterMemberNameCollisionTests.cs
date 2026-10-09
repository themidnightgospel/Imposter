using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

// The indexer imposters take the indexer's parameters and refer to their own members by name.
public class IndexerImposterMemberNameCollisionTests
{
    private const string Usage =
        "imposter[Arg<int>.Any(), Arg<int>.Any()].Getter().Returns(1).Callback((a, b) => { }); "
        + "imposter[Arg<int>.Any(), Arg<int>.Any()].Setter().Callback((a, b, value) => { }); "
        + "var instance = imposter.Instance(); instance[1, 2] = instance[3, 4]; "
        + "imposter[Arg<int>.Any(), Arg<int>.Any()].Getter().Called(Count.Once());";

    [Fact]
    public async Task GivenParametersNamedLikeTheGetterAndSetterImposterFields_WhenIndexerIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[int _defaultBehaviour, int _invocationHistory] { get; set; } }",
            Usage,
            nameof(IndexerImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParametersNamedLikeTheInvocationBehaviorAndCallbacksFields_WhenIndexerIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[int _invocationBehavior, int _callbacks] { get; set; } }",
            Usage,
            nameof(IndexerImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParametersNamedLikeTheIndexerBuilderFields_WhenIndexerIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[int _getterImposter, int _setterImposter] { get; set; } }",
            Usage,
            nameof(IndexerImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParametersNamedLikeTheGetterImposterMethods_WhenIndexerIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[int EnsureGetterConfigured, int FindGetterInvocationImposter] { get; set; } }",
            Usage,
            nameof(IndexerImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParameterNamedLikeTheSetterImposterMethod_WhenIndexerIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[int EnsureSetterConfigured, int key] { get; set; } }",
            Usage,
            nameof(IndexerImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenClassIndexerParameterNamedLikeTheBaseCriteriaField_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { public virtual int this[int _baseCriteria, int key] { get => 0; set { } } }",
            "var imposter = new Sample.ServiceImposter(); imposter[Arg<int>.Any(), Arg<int>.Any()].Getter().UseBaseImplementation(); imposter[Arg<int>.Any(), Arg<int>.Any()].Setter().UseBaseImplementation(); var instance = imposter.Instance(); instance[1, 2] = instance[3, 4];",
            nameof(IndexerImposterMemberNameCollisionTests)
        );
    }
}
