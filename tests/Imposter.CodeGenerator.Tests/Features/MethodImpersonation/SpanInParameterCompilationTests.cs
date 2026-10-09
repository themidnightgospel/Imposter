using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;
#if ROSLYN4_14_OR_GREATER
using Microsoft.CodeAnalysis.CSharp;
#endif

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// A span taken by `in` or `ref readonly` is read-only, so an imposter keeps a copy of its elements as it does for a span
// taken by value.
public class SpanInParameterCompilationTests
{
    [Fact]
    public async Task GivenInSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Count(in System.ReadOnlySpan<byte> data); }",
            "imposter.Count(ReadOnlySpanArg<byte>.Is(1)).Returns((in System.ReadOnlySpan<byte> data) => data.Length); var data = new System.ReadOnlySpan<byte>(new byte[1]); imposter.Instance().Count(in data);",
            nameof(SpanInParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodWithInSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Count<T>(in System.ReadOnlySpan<T> items); }",
            "imposter.Count<int>(ReadOnlySpanArg<int>.Any()).Returns(1); var items = new System.ReadOnlySpan<int>(new int[1]); imposter.Instance().Count(in items);",
            nameof(SpanInParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenInSpanParameterOnAwaitableMethod_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Threading.Tasks.Task<int> CountAsync(in System.ReadOnlySpan<byte> data); }",
            "imposter.CountAsync(ReadOnlySpanArg<byte>.Any()).ReturnsAsync(1); var data = new System.ReadOnlySpan<byte>(new byte[1]); imposter.Instance().CountAsync(in data);",
            nameof(SpanInParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassMethodWithInSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { public virtual int Count(in System.Span<byte> data) => data.Length; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Count(SpanArg<byte>.Any()).UseBaseImplementation(); var data = new System.Span<byte>(new byte[1]); imposter.Instance().Count(in data);",
            nameof(SpanInParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenInSpanParameterOnSpanReturningMethod_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.ReadOnlySpan<char> Trim(in System.ReadOnlySpan<char> text); }",
            "imposter.Trim(ReadOnlySpanArg<char>.Any()).Returns(new[] { 'a' }); var text = System.MemoryExtensions.AsSpan(\"a\"); imposter.Instance().Trim(in text);",
            nameof(SpanInParameterCompilationTests)
        );
    }

#if ROSLYN4_14_OR_GREATER
    [Fact]
    public async Task GivenRefReadOnlySpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService",
            "public interface IService { int Count(ref readonly System.ReadOnlySpan<byte> data); }",
            "var imposter = new Sample.IServiceImposter(); imposter.Count(ReadOnlySpanArg<byte>.Any()).Returns(1); var data = new System.ReadOnlySpan<byte>(new byte[1]); imposter.Instance().Count(in data);",
            nameof(SpanInParameterCompilationTests),
            LanguageVersion.CSharp12
        );
    }

    [Fact]
    public async Task GivenGenericMethodWithRefReadOnlySpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService",
            "public interface IService { int Count<T>(ref readonly System.ReadOnlySpan<T> items); }",
            "var imposter = new Sample.IServiceImposter(); imposter.Count<int>(ReadOnlySpanArg<int>.Any()).Returns(1); var items = new System.ReadOnlySpan<int>(new int[1]); imposter.Instance().Count(in items);",
            nameof(SpanInParameterCompilationTests),
            LanguageVersion.CSharp12
        );
    }
#endif
}
