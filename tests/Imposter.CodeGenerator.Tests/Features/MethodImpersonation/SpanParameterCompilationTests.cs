using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;
#if ROSLYN4_4_OR_GREATER
using Microsoft.CodeAnalysis.CSharp;
#endif

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// An imposter keeps a copy of each Span<T> or ReadOnlySpan<T> argument as an array and matches it with SpanArg<T>.
public class SpanParameterCompilationTests
{
    [Fact]
    public async Task GivenMethodsOverloadedOnSpanAndArray_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { void Write(System.ReadOnlySpan<byte> data); void Write(byte[] data); }",
            "imposter.Write(SpanArg<byte>.Is(1)).Called(Count.Never()); imposter.Write(new byte[] { 1 }).Called(Count.Never()); imposter.Instance().Write(new byte[1]); imposter.Instance().Write(new System.Span<byte>(new byte[1]));",
            nameof(SpanParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanParameterNextToOutParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { bool TryParse(System.ReadOnlySpan<char> text, out int value); }",
            "imposter.TryParse(SpanArg<char>.Any(), OutArg<int>.Any()).Returns(true); imposter.Instance().TryParse(System.MemoryExtensions.AsSpan(\"1\"), out var value);",
            nameof(SpanParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanParametersNextToRefParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Sum(System.ReadOnlySpan<int> left, System.Span<int> right, ref int total); }",
            "imposter.Sum(SpanArg<int>.Any(), SpanArg<int>.Any(), Arg<int>.Any()).Returns(1); var total = 0; imposter.Instance().Sum(new int[1], new int[1], ref total);",
            nameof(SpanParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodWithConstrainedSpanElement_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { T First<T>(System.ReadOnlySpan<T> items) where T : class; }",
            "imposter.First<string>(SpanArg<string>.Any()).Returns(\"a\"); imposter.Instance().First<string>(new[] { \"x\" });",
            nameof(SpanParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanOfTheTargetsTypeParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            "public interface IService<TItem> { int Count(System.ReadOnlySpan<TItem> items); }",
            "var imposter = new Sample.IServiceImposter<string>(); imposter.Count(SpanArg<string>.Is(\"a\")).Returns(1); imposter.Instance().Count(new[] { \"a\" });",
            nameof(SpanParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAwaitableMethodsWithSpanParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Threading.Tasks.Task<int> CountAsync(System.ReadOnlySpan<byte> data); System.Threading.Tasks.Task FlushAsync(System.Span<byte> buffer); System.Threading.Tasks.ValueTask<int> PeekAsync(System.ReadOnlySpan<byte> data); }",
            "imposter.CountAsync(SpanArg<byte>.Any()).ReturnsAsync(1); imposter.FlushAsync(SpanArg<byte>.Any()).ThrowsAsync(new System.Exception()); imposter.PeekAsync(SpanArg<byte>.Any()).ReturnsAsync(2); imposter.Instance().CountAsync(new byte[1]);",
            nameof(SpanParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAwaitableSpanMethodWithParameterNamedLikeTheAsyncResultFunction_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Threading.Tasks.Task<int> CountAsync(System.ReadOnlySpan<byte> data, int AsyncResult); }",
            "imposter.CountAsync(SpanArg<byte>.Any(), Arg<int>.Any()).ReturnsAsync(1); imposter.CountAsync(SpanArg<byte>.Any(), Arg<int>.Any()).ThrowsAsync(new System.Exception()); imposter.Instance().CountAsync(new byte[1], 0);",
            nameof(SpanParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAwaitableSpanMethodWithTypeParameterNamedLikeTheAsyncResultFunction_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Threading.Tasks.Task<AsyncResult> GetAsync<AsyncResult>(System.ReadOnlySpan<byte> data); }",
            "imposter.GetAsync<int>(SpanArg<byte>.Any()).ReturnsAsync(1); imposter.GetAsync<int>(SpanArg<byte>.Any()).ThrowsAsync(new System.Exception()); imposter.Instance().GetAsync<int>(new byte[1]);",
            nameof(SpanParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAbstractAndProtectedClassMethodsWithSpanParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public abstract class Service { public abstract int Get(System.ReadOnlySpan<char> text); protected virtual void Log(System.Span<char> buffer) { } public virtual int Count(System.ReadOnlySpan<char> text) => text.Length; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Get(SpanArg<char>.Any()).Returns(1); imposter.Count(SpanArg<char>.Any()).UseBaseImplementation(); imposter.Instance().Get(System.MemoryExtensions.AsSpan(\"a\"));",
            nameof(SpanParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanOfNullableElements_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get(System.ReadOnlySpan<string?> items); }",
            "imposter.Get(SpanArg<string?>.Is(new string?[] { null })).Returns(1); imposter.Instance().Get(new string?[] { null });",
            nameof(SpanParameterCompilationTests)
        );
    }

#if ROSLYN4_4_OR_GREATER
    [Fact]
    public async Task GivenScopedSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService",
            "public interface IService { int Count(scoped System.ReadOnlySpan<char> text); }",
            "var imposter = new Sample.IServiceImposter(); imposter.Count(SpanArg<char>.Any()).Returns(1); imposter.Instance().Count(System.MemoryExtensions.AsSpan(\"a\"));",
            nameof(SpanParameterCompilationTests),
            LanguageVersion.CSharp11
        );
    }
#endif

#if ROSLYN4_14_OR_GREATER
    [Fact]
    public async Task GivenParamsSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService",
            "public interface IService { int Sum(params System.ReadOnlySpan<int> values); }",
            "var imposter = new Sample.IServiceImposter(); imposter.Sum(SpanArg<int>.Is(1, 2)).Returns(3); imposter.Instance().Sum(1, 2);",
            nameof(SpanParameterCompilationTests),
            LanguageVersion.Preview
        );
    }
#endif
}
