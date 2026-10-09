using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// Returns takes the T[] a Span<T> or ReadOnlySpan<T> result spans, and the invocation history keeps a copy of each result.
public class SpanReturnCompilationTests
{
    [Fact]
    public async Task GivenMethodReturningASliceOfItsSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.ReadOnlySpan<char> Slice(System.ReadOnlySpan<char> text, int start); }",
            "imposter.Slice(ReadOnlySpanArg<char>.Any(), Arg<int>.Any()).Returns((text, start) => text.Slice(start)); imposter.Instance().Slice(System.MemoryExtensions.AsSpan(\"ab\"), 1);",
            nameof(SpanReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodReturningASpanOfAFixedType_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Span<byte> Get<T>(T key); }",
            "imposter.Get<string>(Arg<string>.Any()).Returns(new byte[1]); imposter.Instance().Get(\"a\");",
            nameof(SpanReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodReturningASpanOfItsTypeParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Span<T> Get<T>() where T : struct; }",
            "imposter.Get<int>().Returns(new int[1]); imposter.Instance().Get<int>();",
            nameof(SpanReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenMethodReturningASpanOfNullableElements_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.ReadOnlySpan<string?> Names(); }",
            "imposter.Names().Returns(new string?[] { null }); imposter.Instance().Names();",
            nameof(SpanReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanReturningMethodWithParameterNamedLikeTheReturnsValue_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Span<byte> Get(int value); }",
            "imposter.Get(Arg<int>.Any()).Returns(new byte[1]); imposter.Instance().Get(0);",
            nameof(SpanReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAbstractAndProtectedClassMethodsReturningSpans_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public abstract class Service { public abstract System.ReadOnlySpan<char> Name(); protected virtual System.Span<byte> Buffer() => default; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Name().Returns(new[] { 'a' }); imposter.Buffer().UseBaseImplementation(); imposter.Instance().Name();",
            nameof(SpanReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanReturningMethodWithRefAndOutParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.ReadOnlySpan<byte> Get(ref int position, out int length); }",
            "imposter.Get(Arg<int>.Any(), OutArg<int>.Any()).Returns(new byte[1]); var position = 0; imposter.Instance().Get(ref position, out var length);",
            nameof(SpanReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericSpanReturningMethodWithRefParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.ReadOnlySpan<byte> Get<T>(ref T value); }",
            "imposter.Get<int>(Arg<int>.Any()).Returns(new byte[1]); var value = 0; imposter.Instance().Get(ref value);",
            nameof(SpanReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericSpanReturningMethodWithSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.ReadOnlySpan<T> Skip<T>(System.ReadOnlySpan<T> items, int count); }",
            "imposter.Skip<int>(ReadOnlySpanArg<int>.Any(), Arg<int>.Any()).Returns((items, count) => items.Slice(count)); imposter.Instance().Skip<int>(new[] { 1, 2 }, 1);",
            nameof(SpanReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenOverloadsReturningSpans_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Span<byte> Read(int count); System.ReadOnlySpan<byte> Read(long count); }",
            "imposter.Read(Arg<int>.Any()).Returns(new byte[1]); imposter.Read(Arg<long>.Any()).Returns(new byte[2]); imposter.Instance().Read(1); imposter.Instance().Read(1L);",
            nameof(SpanReturnCompilationTests)
        );
    }
}
