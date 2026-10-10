using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;
#if ROSLYN4_14_OR_GREATER
using Microsoft.CodeAnalysis.CSharp;
#endif

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

// An event whose delegate takes a Span<T> or ReadOnlySpan<T> is raised with the span, and its history keeps the
// span's elements as an array, so Raised matches them with SpanArg<T> or ReadOnlySpanArg<T>.
public class SpanEventCompilationTests
{
    [Fact]
    public async Task GivenEventOfReadOnlySpanHandler_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate void TextHandler(System.ReadOnlySpan<char> text); public interface IService { event TextHandler Received; }",
            "char[] seen = null; imposter.Received.Callback(text => { seen = text.ToArray(); }); imposter.Instance().Received += text => { }; imposter.Received.Raise(System.MemoryExtensions.AsSpan(\"ab\")); imposter.Received.Raised(ReadOnlySpanArg<char>.Is('a', 'b'), Count.Once());",
            nameof(SpanEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenEventOfHandlerWithSpanAndOtherParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate void DataHandler(object sender, System.Span<byte> data); public interface IService { event DataHandler Received; }",
            "imposter.Instance().Received += (sender, data) => data[0] = 1; imposter.Received.Raise(null, new byte[2]); imposter.Received.Raised(Arg<object>.Any(), SpanArg<byte>.Any(), Count.Once()); imposter.Received.HandlerInvoked(Arg<Sample.DataHandler>.Any(), Count.Once());",
            nameof(SpanEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenEventOfHandlerWithInReadOnlySpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate void TextHandler(in System.ReadOnlySpan<char> text); public interface IService { event TextHandler Received; }",
            "imposter.Received.Callback((in System.ReadOnlySpan<char> text) => { }); imposter.Received.Raise(System.MemoryExtensions.AsSpan(\"a\")); imposter.Received.Raised(ReadOnlySpanArg<char>.Any(), Count.Once());",
            nameof(SpanEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericEventOfSpanHandler_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            "public delegate void ItemsHandler<T>(System.ReadOnlySpan<T> items); public interface IService<T> { event ItemsHandler<T> Received; }",
            "var imposter = new Sample.IServiceImposter<int>(); imposter.Received.Raise(new int[] { 1 }); imposter.Received.Raised(ReadOnlySpanArg<int>.Is(1), Count.Once());",
            nameof(SpanEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassEventOfSpanHandler_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public delegate void TextHandler(System.ReadOnlySpan<char> text); public class Service { public virtual event TextHandler Received; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Received.UseBaseImplementation(); imposter.Instance().Received += text => { }; imposter.Received.Raise(System.MemoryExtensions.AsSpan(\"a\"));",
            nameof(SpanEventCompilationTests)
        );
    }

    // Raise passes a ref or out span on by reference, and the history keeps the elements it arrives with.
    [Fact]
    public async Task GivenEventOfHandlerWithRefSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate void ReadHandler(object sender, ref System.ReadOnlySpan<byte> data); public interface IService { event ReadHandler Read; }",
            "imposter.Read.Callback((object sender, ref System.ReadOnlySpan<byte> data) => data = data.Slice(1)); imposter.Instance().Read += (object sender, ref System.ReadOnlySpan<byte> data) => { }; var data = new System.ReadOnlySpan<byte>(new byte[2]); imposter.Read.Raise(null, ref data); imposter.Read.Raised(Arg<object>.Any(), ReadOnlySpanArg<byte>.Any(), Count.Once());",
            nameof(SpanEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenEventOfHandlerWithOutSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate void FillHandler(out System.Span<byte> buffer); public interface IService { event FillHandler Filled; }",
            "imposter.Instance().Filled += (out System.Span<byte> buffer) => buffer = new byte[1]; imposter.Filled.Raise(out var buffer); imposter.Filled.Raised(SpanArg<byte>.Any(), Count.Once());",
            nameof(SpanEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassEventOfHandlerWithRefSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public delegate void ReadHandler(ref System.ReadOnlySpan<char> text); public class Service { public virtual event ReadHandler Read; public void Advance(ref System.ReadOnlySpan<char> text) => Read?.Invoke(ref text); }",
            "var imposter = new Sample.ServiceImposter(); imposter.Instance().Read += (ref System.ReadOnlySpan<char> text) => text = text.Slice(1); var text = System.MemoryExtensions.AsSpan(\"ab\"); imposter.Read.Raise(ref text);",
            nameof(SpanEventCompilationTests)
        );
    }

    // An async method can't take a span (CS4012), so RaiseAsync takes the array the span covers, and the callbacks and
    // handlers get a span over it.
    [Fact]
    public async Task GivenEventOfAsyncSpanHandler_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate System.Threading.Tasks.Task DataHandler(object sender, System.ReadOnlySpan<byte> data); public interface IService { event DataHandler Received; }",
            "imposter.Received.Callback((sender, data) => System.Threading.Tasks.Task.CompletedTask); imposter.Instance().Received += (sender, data) => System.Threading.Tasks.Task.CompletedTask; imposter.Received.RaiseAsync(null, new byte[] { 1 }).GetAwaiter().GetResult(); imposter.Received.Raised(Arg<object>.Any(), ReadOnlySpanArg<byte>.Is(1), Count.Once());",
            nameof(SpanEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenEventOfValueTaskSpanHandler_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate System.Threading.Tasks.ValueTask FillHandler(System.Span<byte> buffer); public interface IService { event FillHandler Filled; }",
            "imposter.Instance().Filled += buffer => { buffer[0] = 1; return default; }; var buffer = new byte[1]; imposter.Filled.RaiseAsync(buffer).GetAwaiter().GetResult(); imposter.Filled.HandlerInvoked(Arg<Sample.FillHandler>.Any(), Count.Once());",
            nameof(SpanEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenEventOfAsyncHandlerWithInSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate System.Threading.Tasks.Task TextHandler(in System.ReadOnlySpan<char> text); public interface IService { event TextHandler Received; }",
            "imposter.Received.Callback((in System.ReadOnlySpan<char> text) => System.Threading.Tasks.Task.CompletedTask); imposter.Received.RaiseAsync(new[] { 'a' }).GetAwaiter().GetResult(); imposter.Received.Raised(ReadOnlySpanArg<char>.Is('a'), Count.Once());",
            nameof(SpanEventCompilationTests)
        );
    }

#if ROSLYN4_14_OR_GREATER
    // `ref readonly` parameters need C# 12, which the Roslyn 4.0 and 4.4 builds of these tests don't know.
    [Fact]
    public async Task GivenEventOfHandlerWithRefReadOnlySpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService",
            "public delegate void TextHandler(ref readonly System.ReadOnlySpan<char> text); public interface IService { event TextHandler Received; }",
            "var imposter = new Sample.IServiceImposter(); var text = System.MemoryExtensions.AsSpan(\"a\"); imposter.Received.Raise(in text); imposter.Received.Raised(ReadOnlySpanArg<char>.Any(), Count.Once());",
            nameof(SpanEventCompilationTests),
            LanguageVersion.CSharp12
        );
    }
#endif
}
