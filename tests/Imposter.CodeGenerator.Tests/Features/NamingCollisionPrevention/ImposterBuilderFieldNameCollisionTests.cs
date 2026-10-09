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
        await AssertServiceCompiles(
            "public interface IService { int Get(int _getMethodImposter, int _getMethodInvocationHistoryCollection); }",
            "imposter.Get(Arg<int>.Any(), Arg<int>.Any()).Returns(1); imposter.Instance().Get(1, 2); imposter.Get(Arg<int>.Any(), Arg<int>.Any()).Called(Count.Once());"
        );
    }

    [Fact]
    public async Task GivenMethodTypeParameterNamedLikeTheMethodField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertServiceCompiles(
            "public interface IService { int Get<_getMethodImposterCollection>(_getMethodImposterCollection key); }",
            "imposter.Get<int>(Arg<int>.Any()).Returns(1); imposter.Instance().Get(1);"
        );
    }

    [Fact]
    public async Task GivenIndexerParameterNamedLikeTheIndexerField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertServiceCompiles(
            "public interface IService { int this[int _IndexerIndexer] { get; set; } }",
            "imposter[Arg<int>.Any()].Getter().Returns(1); var value = imposter.Instance()[1];"
        );
    }

    [Fact]
    public async Task GivenParameterNamedLikeThePropertyField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertServiceCompiles(
            "public interface IService { string Name { get; set; } void Rename(string _NamePropertyBuilderField); }",
            "imposter.Name.Getter().Returns(\"name\"); imposter.Instance().Rename(imposter.Instance().Name);"
        );
    }

    [Fact]
    public async Task GivenParameterNamedLikeTheEventField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertServiceCompiles(
            "public interface IService { event System.EventHandler Changed; void Notify(int _Changed); }",
            "imposter.Instance().Changed += (sender, args) => { }; imposter.Changed.Raise(null, System.EventArgs.Empty); imposter.Instance().Notify(1);"
        );
    }

    private static Task AssertServiceCompiles(string targetDeclaration, string usage) =>
        AssertCompiles(
            "Sample.IService",
            targetDeclaration,
            "var imposter = new Sample.IServiceImposter(); " + usage,
            nameof(ImposterBuilderFieldNameCollisionTests)
        );
}
