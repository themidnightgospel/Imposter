using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;
#if ROSLYN4_4_OR_GREATER
using Microsoft.CodeAnalysis.CSharp;
#endif

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// An imposter keeps a copy of the elements a ref span arrives with, and matches an out span with OutSpanArg<T> or
// OutReadOnlySpanArg<T>. The delegates passed to Returns and Callback receive the span by reference.
public class SpanRefOutParameterCompilationTests
{
    [Fact]
    public async Task GivenOutReadOnlySpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { bool TryRead(out System.ReadOnlySpan<byte> data); }",
            "imposter.TryRead(OutReadOnlySpanArg<byte>.Any()).Returns((out System.ReadOnlySpan<byte> data) => { data = new byte[] { 1 }; return true; }); imposter.Instance().TryRead(out var data);",
            nameof(SpanRefOutParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenOutSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { void Rent(out System.Span<byte> buffer); }",
            "imposter.Rent(OutSpanArg<byte>.Any()).Callback((out System.Span<byte> buffer) => buffer = new byte[2]); imposter.Instance().Rent(out var buffer);",
            nameof(SpanRefOutParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenRefReadOnlySpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Advance(ref System.ReadOnlySpan<byte> buffer); }",
            "imposter.Advance(ReadOnlySpanArg<byte>.Is(1, 2)).Returns((ref System.ReadOnlySpan<byte> buffer) => { buffer = buffer.Slice(1); return 1; }); var buffer = new System.ReadOnlySpan<byte>(new byte[] { 1, 2 }); imposter.Instance().Advance(ref buffer);",
            nameof(SpanRefOutParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodsWithRefAndOutSpanParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { bool TryGet<T>(out System.ReadOnlySpan<T> items); int Advance<T>(ref System.Span<T> items); }",
            "imposter.TryGet<int>(OutReadOnlySpanArg<int>.Any()).Returns(true); imposter.Advance<int>(SpanArg<int>.Any()).Returns(1); var items = new System.Span<int>(new int[1]); imposter.Instance().TryGet<int>(out var read); imposter.Instance().Advance(ref items);",
            nameof(SpanRefOutParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodWithRefSpanOfAFixedTypeAndInParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Write<T>(ref System.Span<byte> buffer, in T value); }",
            "imposter.Write<int>(SpanArg<byte>.Any(), Arg<int>.Any()).Returns(1); var buffer = new System.Span<byte>(new byte[1]); var value = 1; imposter.Instance().Write(ref buffer, in value);",
            nameof(SpanRefOutParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAwaitableMethodWithOutSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Threading.Tasks.Task<bool> TryReadAsync(out System.ReadOnlySpan<byte> data); }",
            "imposter.TryReadAsync(OutReadOnlySpanArg<byte>.Any()).ReturnsAsync(true); imposter.Instance().TryReadAsync(out var data);",
            nameof(SpanRefOutParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassMethodWithRefSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { public virtual int Advance(ref System.ReadOnlySpan<char> text) { text = text.Slice(1); return 1; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Advance(ReadOnlySpanArg<char>.Any()).UseBaseImplementation(); var text = System.MemoryExtensions.AsSpan(\"ab\"); imposter.Instance().Advance(ref text);",
            nameof(SpanRefOutParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenOverloadsOnSpanByValueAndOut_WhenSetUpThroughTheView_ShouldKeepTheirNames()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Read(System.Span<byte> buffer); int Read(out System.Span<byte> buffer); }",
            "var view = imposter.For(default(Sample.IService)); view.Read(SpanArg<byte>.Any()).Returns(1); view.Read(OutSpanArg<byte>.Any()).Returns(2);",
            nameof(SpanRefOutParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanReturningMethodWithRefSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.ReadOnlySpan<byte> Take(ref System.ReadOnlySpan<byte> buffer, int count); }",
            "imposter.Take(ReadOnlySpanArg<byte>.Any(), Arg<int>.Any()).Returns(new byte[1]); var buffer = new System.ReadOnlySpan<byte>(new byte[2]); imposter.Instance().Take(ref buffer, 1);",
            nameof(SpanRefOutParameterCompilationTests)
        );
    }

#if ROSLYN4_4_OR_GREATER
    // The delegates take the scoped parameter as scoped too, so an explicitly typed lambda repeats the modifier.
    [Fact]
    public async Task GivenRefSpanParameterNextToScopedParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService",
            "public interface IService { int Copy(ref System.Span<byte> target, scoped System.ReadOnlySpan<byte> source); }",
            "var imposter = new Sample.IServiceImposter(); imposter.Copy(SpanArg<byte>.Any(), ReadOnlySpanArg<byte>.Any()).Returns((ref System.Span<byte> target, scoped System.ReadOnlySpan<byte> source) => { source.CopyTo(target); return source.Length; }); var target = new System.Span<byte>(new byte[1]); imposter.Instance().Copy(ref target, new byte[1]);",
            nameof(SpanRefOutParameterCompilationTests),
            LanguageVersion.CSharp11
        );
    }
#endif
}
