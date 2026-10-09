using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

// An indexer's span keys and span value are kept as arrays of their elements, so its setups match keys with SpanArg<T>
// or ReadOnlySpanArg<T>, and its delegates take and return arrays.
public class SpanIndexerCompilationTests
{
    [Fact]
    public async Task GivenIndexerOfSpanValue_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Span<byte> this[int index] { get; set; } }",
            "imposter[Arg<int>.Is(0)].Getter().Returns(new byte[2]); imposter[Arg<int>.Any()].Setter().Callback((index, value) => { byte[] copy = value; }); imposter.Instance()[0] = new byte[1]; System.Span<byte> buffer = imposter.Instance()[0];",
            nameof(SpanIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenIndexerWithReadOnlySpanKey_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[System.ReadOnlySpan<char> key] { get; set; } }",
            "imposter[ReadOnlySpanArg<char>.Is('a')].Getter().Returns(1); imposter[ReadOnlySpanArg<char>.Any()].Getter().Returns(key => key.Length); imposter.Instance()[System.MemoryExtensions.AsSpan(\"a\")] = 2; _ = imposter.Instance()[System.MemoryExtensions.AsSpan(\"a\")];",
            nameof(SpanIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenIndexerWithSpanKeyAndSpanValue_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Span<byte> this[System.Span<byte> key] { get; set; } }",
            "imposter[SpanArg<byte>.Any()].Getter().Returns(key => key); imposter[SpanArg<byte>.Is(1)].Setter().Called(Count.Never()); imposter.Instance()[new byte[] { 1 }] = imposter.Instance()[new byte[] { 2 }];",
            nameof(SpanIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGetterOnlyAndSetterOnlySpanIndexers_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.ReadOnlySpan<char> this[int index] { get; } int this[System.Span<byte> key] { set; } }",
            "imposter[Arg<int>.Any()].Getter().Returns(new[] { 'a' }); imposter[SpanArg<byte>.Any()].Setter().Callback((key, value) => { }); _ = imposter.Instance()[0]; imposter.Instance()[new byte[1]] = 1;",
            nameof(SpanIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericSpanIndexer_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            "public interface IService<T> { System.Span<T> this[System.ReadOnlySpan<T> key] { get; set; } }",
            "var imposter = new Sample.IServiceImposter<int>(); imposter[ReadOnlySpanArg<int>.Is(1)].Getter().Returns(new int[1]); imposter.Instance()[new int[] { 1 }] = new int[1];",
            nameof(SpanIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanKeyIndexerInInterfaceSetupView_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IBase { int this[System.ReadOnlySpan<char> key] { get; } } public interface IService : IBase { }",
            "imposter.For(default(Sample.IBase))[ReadOnlySpanArg<char>.Any()].Getter().Returns(3); _ = imposter.Instance()[System.MemoryExtensions.AsSpan(\"a\")];",
            nameof(SpanIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassIndexerWithSpanKey_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { public virtual int this[System.ReadOnlySpan<char> key] { get => key.Length; set { } } }",
            "var imposter = new Sample.ServiceImposter(); imposter[ReadOnlySpanArg<char>.Any()].Getter().UseBaseImplementation(); imposter[ReadOnlySpanArg<char>.Any()].Setter().UseBaseImplementation(); imposter.Instance()[System.MemoryExtensions.AsSpan(\"ab\")] = imposter.Instance()[System.MemoryExtensions.AsSpan(\"a\")];",
            nameof(SpanIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassIndexerOfSpanValue_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { private byte[] _buffer = new byte[1]; public virtual System.Span<byte> this[int index] { get => _buffer; set => _buffer = value.ToArray(); } }",
            "var imposter = new Sample.ServiceImposter(); imposter[Arg<int>.Any()].Getter().UseBaseImplementation(); imposter[Arg<int>.Any()].Setter().UseBaseImplementation(); imposter.Instance()[0] = imposter.Instance()[1];",
            nameof(SpanIndexerCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassIndexerWithInSpanKey_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { public virtual int this[in System.ReadOnlySpan<char> key] { get => key.Length; set { } } }",
            "var imposter = new Sample.ServiceImposter(); imposter[ReadOnlySpanArg<char>.Any()].Getter().UseBaseImplementation(); imposter[ReadOnlySpanArg<char>.Any()].Setter().UseBaseImplementation(); var key = System.MemoryExtensions.AsSpan(\"a\"); imposter.Instance()[in key] = imposter.Instance()[in key];",
            nameof(SpanIndexerCompilationTests)
        );
    }
}
