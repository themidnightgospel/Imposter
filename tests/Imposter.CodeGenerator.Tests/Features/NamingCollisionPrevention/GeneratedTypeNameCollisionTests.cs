using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// The imposter declares types named after each member, such as ICurrentPropertyBuilder or GetDelegate. A target member
// can be named like one of them, and two members can derive the same name, so a member whose types would clash takes
// another name for them.
public class GeneratedTypeNameCollisionTests
{
    [Fact]
    public async Task GivenMethodNamedLikeAPropertysBuilderInterface_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Current { get; set; } int ICurrentPropertyBuilder(); }",
            "imposter.Current.Getter().Returns(1); imposter.ICurrentPropertyBuilder().Returns(2); var service = imposter.Instance(); _ = service.Current + service.ICurrentPropertyBuilder();",
            nameof(GeneratedTypeNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenPropertyNamedLikeAMethodsDelegate_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get(int value); int GetDelegate { get; } }",
            "imposter.Get(Arg<int>.Any()).Returns(1); imposter.GetDelegate.Getter().Returns(2); var service = imposter.Instance(); _ = service.Get(0) + service.GetDelegate;",
            nameof(GeneratedTypeNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenMethodNamedLikeAnotherMethodsDelegate_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Create(); int CreateDelegate(); }",
            "imposter.Create().Returns(1); imposter.CreateDelegate().Returns(2); var service = imposter.Instance(); _ = service.Create() + service.CreateDelegate();",
            nameof(GeneratedTypeNameCollisionTests)
        );
    }

    // Create's types are renamed after Create_1, whose delegate Create_1Delegate is taken too, so they're renamed again.
    [Fact]
    public async Task GivenMethodNamedLikeTheRenamedDelegate_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Create(); int CreateDelegate(); int Create_1Delegate(); }",
            "imposter.Create().Returns(1); imposter.CreateDelegate().Returns(2); imposter.Create_1Delegate().Returns(3); var service = imposter.Instance(); _ = service.Create() + service.CreateDelegate() + service.Create_1Delegate();",
            nameof(GeneratedTypeNameCollisionTests)
        );
    }

    // Foo's callback delegate and FooCallback's delegate would both be FooCallbackDelegate.
    [Fact]
    public async Task GivenMethodsDerivingTheSameTypeName_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { void Foo(); void FooCallback(); }",
            "imposter.Foo().Callback(() => { }); imposter.FooCallback().Callback(() => { }); var service = imposter.Instance(); service.Foo(); service.FooCallback();",
            nameof(GeneratedTypeNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenMethodNamedLikeAnIndexersDelegate_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[int key] { get; } int IndexerIndexerDelegate(); }",
            "imposter[Arg<int>.Any()].Getter().Returns(1); imposter.IndexerIndexerDelegate().Returns(2); var service = imposter.Instance(); _ = service[0] + service.IndexerIndexerDelegate();",
            nameof(GeneratedTypeNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenPropertyNamedLikeAnEventsBuilderInterface_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { event System.EventHandler Changed; int IChangedEventImposterBuilder { get; } }",
            "imposter.Changed.Raise(new object(), System.EventArgs.Empty); imposter.IChangedEventImposterBuilder.Getter().Returns(1); _ = imposter.Instance().IChangedEventImposterBuilder;",
            nameof(GeneratedTypeNameCollisionTests)
        );
    }

    // CurrentProperty's delegate and the ref struct property Current's would both be CurrentPropertyDelegate.
    [Fact]
    public async Task GivenMethodDerivingARefStructPropertysDelegateName_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public ref struct Bookmark { public int Page; } public interface IService { Bookmark Current { get; } int CurrentProperty(); }",
            "imposter.Current.Getter().Returns(() => new Sample.Bookmark { Page = 1 }); imposter.CurrentProperty().Returns(2); var service = imposter.Instance(); _ = service.Current.Page + service.CurrentProperty();",
            nameof(GeneratedTypeNameCollisionTests)
        );
    }
}
