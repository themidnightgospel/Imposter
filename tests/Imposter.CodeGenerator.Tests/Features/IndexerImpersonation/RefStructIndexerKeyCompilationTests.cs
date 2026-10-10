using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

// An imposter can't keep or match a custom ref struct key, so setups and verification match the other keys only, and
// the delegates and the base accessors get the key itself. An indexer without other keys to match, or whose other keys
// match another indexer's, is set up by a method named after it.
public class RefStructIndexerKeyCompilationTests
{
    private const string Cursor = "public ref struct Cursor { public int Position; } ";

    [Fact]
    public async Task GivenIndexerWithRefStructAndOtherKeys_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor + "public interface IService { int this[int row, Cursor cursor] { get; set; } }",
            "imposter[Arg<int>.Is(1)].Getter().Returns((row, cursor) => row + cursor.Position).Callback((row, cursor) => { }).Then().Returns(2).Then().Throws((row, cursor) => new System.Exception()); imposter[Arg<int>.Any()].Setter().Callback((row, cursor, value) => { _ = cursor.Position; }); imposter.Instance()[1, new Sample.Cursor()] = imposter.Instance()[1, new Sample.Cursor()]; imposter[Arg<int>.Any()].Getter().Called(Count.Once()); imposter[Arg<int>.Any()].Setter().Called(Count.Once());",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenIndexerWithOnlyRefStructKeys_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor + "public interface IService { int this[Cursor cursor] { get; set; } }",
            "imposter.Indexer().Getter().Returns(cursor => cursor.Position).Then().Returns(() => 1); imposter.Indexer().Setter().Callback((cursor, value) => { }); imposter.Instance()[new Sample.Cursor()] = imposter.Instance()[new Sample.Cursor()]; imposter.Indexer().Getter().Called(Count.Once());",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenIndexerWithRefStructKeyAndValue_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor
                + "public ref struct Bookmark { public int Page; } public interface IService { Bookmark this[Cursor cursor] { get; set; } }",
            "imposter.Indexer().Getter().Returns(cursor => new Sample.Bookmark { Page = cursor.Position }); imposter.Indexer().Setter().Callback((cursor, value) => { }); imposter.Instance()[new Sample.Cursor()] = imposter.Instance()[new Sample.Cursor()];",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    // Both indexers would take this[Arg<int>], so each is set up by a method named after it.
    [Fact]
    public async Task GivenIndexersMatchingTheSameKeys_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor
                + "public interface IService { int this[int row] { get; } int this[int row, Cursor cursor] { get; } }",
            "imposter.Indexer(Arg<int>.Any()).Getter().Returns(1); imposter.Indexer_1(Arg<int>.Any()).Getter().Returns((row, cursor) => 2); _ = imposter.Instance()[0] + imposter.Instance()[0, new Sample.Cursor()];",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenIndexerWithRefStructKeyOfTheTargetsTypeParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            "public ref struct Wrapper<T> { public T Value; } public interface IService<T> { T this[int index, Wrapper<T> key] { get; set; } }",
            "var imposter = new Sample.IServiceImposter<string>(); imposter[Arg<int>.Any()].Getter().Returns((index, key) => key.Value); imposter[Arg<int>.Any()].Setter().Callback((index, key, value) => { }); imposter.Instance()[0, new Sample.Wrapper<string>()] = imposter.Instance()[0, new Sample.Wrapper<string>()];",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenIndexerWithRefStructKey_WhenSetUpThroughTheView_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor
                + "public interface IBase { int this[Cursor cursor] { get; } string this[int row, Cursor cursor] { get; } } public interface IService : IBase { }",
            "var view = imposter.For(default(Sample.IBase)); view.Indexer().Getter().Returns(cursor => cursor.Position); view[Arg<int>.Any()].Getter().Returns((row, cursor) => \"a\"); _ = imposter.Instance()[new Sample.Cursor()];",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenIndexersWithRefStructKeysImplementedExplicitly_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor
                + "public interface IFirst { int this[Cursor cursor] { get; } } public interface ISecond { string this[Cursor cursor] { get; } } public interface IService : IFirst, ISecond { }",
            "imposter.For(default(Sample.IFirst)).Indexer().Getter().Returns(cursor => 1); Sample.IFirst first = imposter.Instance(); Sample.ISecond second = imposter.Instance(); _ = first[new Sample.Cursor()] + second[new Sample.Cursor()];",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassIndexerWithRefStructKey_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Cursor
                + "public class Service { public virtual int this[int row, Cursor cursor] { get => row + cursor.Position; set { } } public virtual int this[Cursor cursor] { get => cursor.Position; set { } } }",
            "var imposter = new Sample.ServiceImposter(); imposter[Arg<int>.Any()].Getter().UseBaseImplementation(); imposter[Arg<int>.Any()].Setter().UseBaseImplementation(); imposter.Indexer_1().Getter().UseBaseImplementation(); imposter.Instance()[1, new Sample.Cursor()] = imposter.Instance()[new Sample.Cursor()];",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassIndexerWithRefStructKeyAndValue_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Cursor
                + "public ref struct Bookmark { public int Page; } public class Service { public virtual Bookmark this[Cursor cursor] { get => new Bookmark { Page = cursor.Position }; set { } } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Indexer().Getter().UseBaseImplementation(); imposter.Indexer().Setter().UseBaseImplementation(); imposter.Instance()[new Sample.Cursor()] = imposter.Instance()[new Sample.Cursor()];",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    // The getter's setup methods and handlers take the keys passed through under names that don't hide their own.
    [Fact]
    public async Task GivenRefStructKeysNamedLikeTheGettersOwnNames_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor
                + "public interface IService { long this[Cursor value] { get; } int this[int arguments, Cursor generator] { get; set; } string this[Cursor callback, string exceptionGenerator, Cursor valueGenerator] { get; } }",
            "imposter.Indexer().Getter().Returns(2L).Then().Returns(() => 3L).Then().Returns(value => 4L); imposter[Arg<int>.Any()].Getter().Returns(1).Then().Throws(new System.Exception()).Then().Throws((arguments, generator) => new System.Exception()).Callback((arguments, generator) => { }); imposter[Arg<int>.Any()].Setter().Callback((arguments, generator, value) => { }); imposter[Arg<string>.Any()].Getter().Returns(\"a\").Then().Returns((callback, exceptionGenerator, valueGenerator) => exceptionGenerator); _ = imposter.Instance()[new Sample.Cursor()];",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenIndexerWithRefStructAndSpanKeys_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor
                + "public interface IService { int this[System.ReadOnlySpan<char> name, Cursor cursor] { get; set; } }",
            "imposter[ReadOnlySpanArg<char>.Is('a')].Getter().Returns((name, cursor) => name.Length + cursor.Position); imposter[ReadOnlySpanArg<char>.Any()].Setter().Callback((name, cursor, value) => { }); imposter.Instance()[System.MemoryExtensions.AsSpan(\"a\"), new Sample.Cursor()] = imposter.Instance()[System.MemoryExtensions.AsSpan(\"a\"), new Sample.Cursor()];",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }

    // The view declares the setup methods of indexers matching the same keys, with those keys' matchers.
    [Fact]
    public async Task GivenIndexersMatchingTheSameKeys_WhenSetUpThroughTheView_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor
                + "public interface IBase { int this[int row] { get; } string this[int row, Cursor cursor] { get; } } public interface IService : IBase { }",
            "var view = imposter.For(default(Sample.IBase)); view.Indexer(Arg<int>.Any()).Getter().Returns(1); view.Indexer_1(Arg<int>.Is(2)).Getter().Returns((row, cursor) => \"a\"); _ = imposter.Instance()[0] + imposter.Instance()[2, new Sample.Cursor()];",
            nameof(RefStructIndexerKeyCompilationTests)
        );
    }
}
