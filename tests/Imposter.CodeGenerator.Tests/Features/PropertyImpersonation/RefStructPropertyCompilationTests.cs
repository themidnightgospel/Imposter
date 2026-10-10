using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

// An imposter can't keep a custom ref struct value, so it only passes a property's value between the instance and the
// delegates: the getter's Returns takes only a delegate, the setter takes no value criteria, and without a setup the
// getter returns the default.
public class RefStructPropertyCompilationTests
{
    private const string Bookmark = "public ref struct Bookmark { public int Page; } ";

    [Fact]
    public async Task GivenRefStructProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark + "public interface IService { Bookmark Current { get; set; } }",
            "imposter.Current.Getter().Returns(() => new Sample.Bookmark { Page = 1 }).Callback(() => { }).Then().Throws<System.InvalidOperationException>(); imposter.Current.Setter().Callback(value => { _ = value.Page; }).Then().Callback(value => { }); imposter.Instance().Current = imposter.Instance().Current; imposter.Current.Getter().Called(Count.Once()); imposter.Current.Setter().Called(Count.Once());",
            nameof(RefStructPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGetterOnlyRefStructProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark + "public interface IService { Bookmark Current { get; } }",
            "imposter.Current.Getter().Returns(() => new Sample.Bookmark()); var page = imposter.Instance().Current.Page;",
            nameof(RefStructPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSetterOnlyRefStructProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark + "public interface IService { Bookmark Current { set; } }",
            "imposter.Current.Setter().Callback(value => { }); imposter.Instance().Current = new Sample.Bookmark(); imposter.Current.Setter().Called(Count.Once());",
            nameof(RefStructPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenInitOnlyRefStructProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark + "public interface IService { Bookmark Current { get; init; } }",
            "imposter.Current.Getter().Returns(() => new Sample.Bookmark()); imposter.Current.Setter().Called(Count.Never()); _ = imposter.Instance().Current.Page;",
            nameof(RefStructPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenRefStructPropertyOfTheTargetsTypeParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            "public ref struct Wrapper<T> { public T Value; } public interface IService<T> { Wrapper<T> Current { get; set; } }",
            "var imposter = new Sample.IServiceImposter<int>(); imposter.Current.Getter().Returns(() => new Sample.Wrapper<int> { Value = 1 }); imposter.Current.Setter().Callback(value => { }); imposter.Instance().Current = imposter.Instance().Current;",
            nameof(RefStructPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenRefStructPropertiesImplementedExplicitly_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark
                + "public interface IFirst { Bookmark Current { get; } } public interface ISecond { int Current { get; } } public interface IService : IFirst, ISecond { }",
            "Sample.IFirst first = imposter.Instance(); Sample.ISecond second = imposter.Instance(); _ = first.Current.Page + second.Current;",
            nameof(RefStructPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenRefStructProperty_WhenSetUpThroughTheView_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark + "public interface IService { Bookmark Current { get; set; } }",
            "var view = imposter.For(default(Sample.IService)); view.Current.Getter().Returns(() => new Sample.Bookmark()); view.Current.Setter().Called(Count.Never());",
            nameof(RefStructPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassRefStructProperty_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Bookmark
                + "public class Service { private int _page; public virtual Bookmark Current { get => new Bookmark { Page = _page }; set => _page = value.Page; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Current.Getter().UseBaseImplementation(); imposter.Current.Setter().Callback(value => { }).Then().UseBaseImplementation(); imposter.Instance().Current = imposter.Instance().Current; imposter.Current.UseBaseImplementation();",
            nameof(RefStructPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAbstractClassRefStructProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Bookmark
                + "public abstract class Service { public abstract Bookmark Current { get; set; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Current.Getter().Returns(() => new Sample.Bookmark()); imposter.Instance().Current = imposter.Instance().Current;",
            nameof(RefStructPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassInitOnlyRefStructProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Bookmark
                + "public class Service { private int _page; public virtual Bookmark Current { get => new Bookmark { Page = _page }; init => _page = value.Page; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Current.Setter().UseBaseImplementation(); imposter.Current.Setter().Called(Count.Never()); _ = imposter.Instance().Current.Page;",
            nameof(RefStructPropertyCompilationTests)
        );
    }
}
