using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// The imposter instance reaches its imposter through a field named _imposter unless the target uses that name.
public class ImposterFieldNameCollisionTests
{
    private const string Interface = "Sample.IService";

    private const string Class = "Sample.Service";

    [Fact]
    public async Task GivenInterfaceMethodParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertTargetCompiles(
            Interface,
            "public interface IService { int Get(int _imposter); }",
            "var imposter = new Sample.IServiceImposter(); imposter.Get(Arg<int>.Any()).Returns(1); imposter.Instance().Get(5);"
        );
    }

    [Fact]
    public async Task GivenInterfaceIndexerParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertTargetCompiles(
            Interface,
            "public interface IService { int this[int _imposter] { get; set; } }",
            "var imposter = new Sample.IServiceImposter(); var instance = imposter.Instance(); instance[1] = instance[2];"
        );
    }

    [Fact]
    public async Task GivenClassMethodParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertTargetCompiles(
            Class,
            "public class Service { public virtual int Get(int _imposter) => _imposter; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Get(Arg<int>.Any()).UseBaseImplementation(); imposter.Instance().Get(5);"
        );
    }

    [Fact]
    public async Task GivenClassIndexerParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertTargetCompiles(
            Class,
            "public class Service { public virtual int this[int _imposter] { get => _imposter; set { } } }",
            "var imposter = new Sample.ServiceImposter(); var instance = imposter.Instance(); instance[1] = instance[2];"
        );
    }

    [Fact]
    public async Task GivenConstructorParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertTargetCompiles(
            Class,
            "public class Service { public Service(int _imposter) { } public virtual int Get() => 0; }",
            "var imposter = new Sample.ServiceImposter(1); imposter.Instance().Get();"
        );
    }

    [Fact]
    public async Task GivenMethodTypeParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertTargetCompiles(
            Interface,
            "public interface IService { void Accept<_imposter>(_imposter value); }",
            "var imposter = new Sample.IServiceImposter(); imposter.Instance().Accept(5);"
        );
    }

    [Fact]
    public async Task GivenTargetTypeParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertTargetCompiles(
            "Sample.IService<>",
            "public interface IService<_imposter> { _imposter Get(); }",
            "var imposter = new Sample.IServiceImposter<int>(); imposter.Instance().Get();"
        );
    }

    private static Task AssertTargetCompiles(
        string targetType,
        string targetDeclaration,
        string usage
    ) =>
        AssertCompiles(
            targetType,
            targetDeclaration,
            usage,
            nameof(ImposterFieldNameCollisionTests)
        );
}
