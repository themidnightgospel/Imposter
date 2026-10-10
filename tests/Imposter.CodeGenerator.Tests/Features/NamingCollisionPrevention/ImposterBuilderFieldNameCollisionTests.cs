using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// The imposter's setup members take the target's parameter names and use its builder fields by their bare names.
public class ImposterBuilderFieldNameCollisionTests
{
    [Fact]
    public async Task GivenMethodParametersNamedLikeTheMethodFields_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get(int _getMethodImposter, int _getMethodInvocationHistoryCollection); }",
            "imposter.Get(Arg<int>.Any(), Arg<int>.Any()).Returns(1); imposter.Instance().Get(1, 2); imposter.Get(Arg<int>.Any(), Arg<int>.Any()).Called(Count.Once());",
            nameof(ImposterBuilderFieldNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenMethodTypeParameterNamedLikeTheMethodField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get<_getMethodImposterCollection>(_getMethodImposterCollection key); }",
            "imposter.Get<int>(Arg<int>.Any()).Returns(1); imposter.Instance().Get(1);",
            nameof(ImposterBuilderFieldNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenIndexerParameterNamedLikeTheIndexerField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[int _IndexerIndexer] { get; set; } }",
            "imposter[Arg<int>.Any()].Getter().Returns(1); var value = imposter.Instance()[1];",
            nameof(ImposterBuilderFieldNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParameterNamedLikeThePropertyField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { string Name { get; set; } void Rename(string _NamePropertyBuilderField); }",
            "imposter.Name.Getter().Returns(\"name\"); imposter.Instance().Rename(imposter.Instance().Name);",
            nameof(ImposterBuilderFieldNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParameterNamedLikeTheEventField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { event System.EventHandler Changed; void Notify(int _Changed); }",
            "imposter.Instance().Changed += (sender, args) => { }; imposter.Changed.Raise(null, System.EventArgs.Empty); imposter.Instance().Notify(1);",
            nameof(ImposterBuilderFieldNameCollisionTests)
        );
    }

    // The builder fields share the imposter with the setup members, so they avoid the target's member names.
    [Fact]
    public async Task GivenPropertyNamedLikeTheEventField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { event System.EventHandler Changed; int _Changed { get; } }",
            "imposter._Changed.Getter().Returns(1); imposter.Changed.Raise(new object(), System.EventArgs.Empty); _ = imposter.Instance()._Changed;",
            nameof(ImposterBuilderFieldNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenPropertyNamedLikeTheIndexerField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[int key] { get; } int _IndexerIndexer { get; } }",
            "imposter._IndexerIndexer.Getter().Returns(1); imposter[Arg<int>.Any()].Getter().Returns(2); _ = imposter.Instance()[0] + imposter.Instance()._IndexerIndexer;",
            nameof(ImposterBuilderFieldNameCollisionTests)
        );
    }
}
