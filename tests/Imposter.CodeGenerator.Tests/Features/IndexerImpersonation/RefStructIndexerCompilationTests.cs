using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

// An imposter can't keep a custom ref struct value, so it only passes an indexer's value between the instance and the
// delegates: the getter's Returns takes only the delegate that gets the keys, and without a setup the getter returns
// the base getter's value or the default.
public class RefStructIndexerCompilationTests
{
    private const string Bookmark = "public ref struct Bookmark { public int Page; } ";

    [Fact]
    public async Task GivenRefStructIndexer_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark + "public interface IService { Bookmark this[int index] { get; set; } }",
            "imposter[Arg<int>.Is(0)].Getter().Returns(index => new Sample.Bookmark { Page = index }).Callback(index => { }).Then().Throws<System.InvalidOperationException>(); imposter[Arg<int>.Any()].Setter().Callback((index, value) => { _ = value.Page; }).Then().Callback((index, value) => { }); imposter.Instance()[0] = imposter.Instance()[1]; imposter[Arg<int>.Any()].Getter().Called(Count.Once()); imposter[Arg<int>.Any()].Setter().Called(Count.Once());",
            nameof(RefStructIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGetterOnlyAndSetterOnlyRefStructIndexers_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark
                + "public interface IService { Bookmark this[int index] { get; } Bookmark this[string key] { set; } }",
            "imposter[Arg<int>.Any()].Getter().Returns(index => new Sample.Bookmark()); imposter[Arg<string>.Any()].Setter().Callback((key, value) => { }); _ = imposter.Instance()[0].Page; imposter.Instance()[\"a\"] = new Sample.Bookmark();",
            nameof(RefStructIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenRefStructIndexerWithSeveralKeys_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark
                + "public interface IService { Bookmark this[int row, string column] { get; set; } }",
            "imposter[Arg<int>.Any(), Arg<string>.Is(\"a\")].Getter().Returns((row, column) => new Sample.Bookmark { Page = row }); imposter[Arg<int>.Any(), Arg<string>.Any()].Setter().Called(Count.Never()); _ = imposter.Instance()[1, \"a\"].Page;",
            nameof(RefStructIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenRefStructIndexerWithSpanKey_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark
                + "public interface IService { Bookmark this[System.ReadOnlySpan<char> key] { get; set; } }",
            "imposter[ReadOnlySpanArg<char>.Is('a')].Getter().Returns(key => new Sample.Bookmark { Page = key.Length }); imposter[ReadOnlySpanArg<char>.Any()].Setter().Callback((key, value) => { }); imposter.Instance()[System.MemoryExtensions.AsSpan(\"a\")] = imposter.Instance()[System.MemoryExtensions.AsSpan(\"a\")];",
            nameof(RefStructIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenRefStructIndexerOfTheTargetsTypeParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            "public ref struct Wrapper<T> { public T Value; } public interface IService<T> { Wrapper<T> this[T key] { get; set; } }",
            "var imposter = new Sample.IServiceImposter<int>(); imposter[Arg<int>.Any()].Getter().Returns(key => new Sample.Wrapper<int> { Value = key }); imposter[Arg<int>.Any()].Setter().Callback((key, value) => { }); imposter.Instance()[1] = imposter.Instance()[2];",
            nameof(RefStructIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenRefStructIndexersImplementedExplicitly_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark
                + "public interface IFirst { Bookmark this[int index] { get; } } public interface ISecond { int this[int index] { get; } } public interface IService : IFirst, ISecond { }",
            "Sample.IFirst first = imposter.Instance(); Sample.ISecond second = imposter.Instance(); _ = first[0].Page + second[0];",
            nameof(RefStructIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenRefStructIndexer_WhenSetUpThroughTheView_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Bookmark
                + "public interface IBase { Bookmark this[int index] { get; } } public interface IService : IBase { }",
            "imposter.For(default(Sample.IBase))[Arg<int>.Any()].Getter().Returns(index => new Sample.Bookmark()); _ = imposter.Instance()[0].Page;",
            nameof(RefStructIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassRefStructIndexer_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Bookmark
                + "public class Service { private int _page; public virtual Bookmark this[int index] { get => new Bookmark { Page = _page + index }; set => _page = value.Page; } }",
            "var imposter = new Sample.ServiceImposter(); imposter[Arg<int>.Any()].Getter().UseBaseImplementation(); imposter[Arg<int>.Any()].Setter().UseBaseImplementation(); imposter[Arg<int>.Is(1)].Setter().Callback((index, value) => { }); imposter.Instance()[0] = imposter.Instance()[1];",
            nameof(RefStructIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAbstractClassRefStructIndexer_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Bookmark
                + "public abstract class Service { public abstract Bookmark this[int index] { get; set; } }",
            "var imposter = new Sample.ServiceImposter(); imposter[Arg<int>.Any()].Getter().Returns(index => new Sample.Bookmark()); imposter.Instance()[0] = imposter.Instance()[1];",
            nameof(RefStructIndexerCompilationTests)
        );
    }

    // Without a base getter, nothing reads the indexer through a copy of an in key, so the value is passed through.
    [Fact]
    public async Task GivenRefStructIndexersWithInKeyWithoutBaseGetter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Bookmark
                + "public abstract class Service { public abstract Bookmark this[in int index] { get; set; } public virtual Bookmark this[in long index] { set { } } }",
            "var imposter = new Sample.ServiceImposter(); imposter[Arg<int>.Any()].Getter().Returns(index => new Sample.Bookmark()); imposter[Arg<long>.Any()].Setter().UseBaseImplementation(); imposter.Instance()[1] = imposter.Instance()[1]; imposter.Instance()[1L] = new Sample.Bookmark();",
            nameof(RefStructIndexerCompilationTests)
        );
    }
}
