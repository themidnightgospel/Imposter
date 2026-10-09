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
