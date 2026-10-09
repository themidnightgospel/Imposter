using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

// A span property's imposter keeps the elements it gets or returns in an array, so its setups take arrays and match
// with SpanArg<T> or ReadOnlySpanArg<T>.
public class SpanPropertyCompilationTests
{
    [Fact]
    public async Task GivenGetterOnlySpanProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Span<byte> Buffer { get; } }",
            "imposter.Buffer.Getter().Returns(new byte[2]); System.Span<byte> buffer = imposter.Instance().Buffer;",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanPropertyWithSetter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Span<byte> Buffer { get; set; } }",
            "imposter.Buffer.Setter(SpanArg<byte>.Is(1, 2)).Callback(value => { }); imposter.Instance().Buffer = new byte[] { 1, 2 }; imposter.Buffer.Setter(SpanArg<byte>.Any()).Called(Count.Once());",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenReadOnlySpanProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.ReadOnlySpan<char> Name { get; set; } }",
            "imposter.Name.Getter().Returns(() => new[] { 'a' }); imposter.Name.Setter(ReadOnlySpanArg<char>.Is(text => text.Length == 1)).Called(Count.Never()); System.ReadOnlySpan<char> name = imposter.Instance().Name;",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSetterOnlySpanProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Span<byte> Buffer { set; } }",
            "imposter.Instance().Buffer = new byte[1]; imposter.Buffer.Setter(SpanArg<byte>.Any()).Called(Count.Once());",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenInitOnlySpanProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Span<byte> Buffer { get; init; } }",
            "imposter.Buffer.Getter().Returns(new byte[1]); imposter.Buffer.Setter(SpanArg<byte>.Any()).Called(Count.Never()); _ = imposter.Instance().Buffer;",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericSpanProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            "public interface IService<T> { System.Span<T> Items { get; set; } }",
            "var imposter = new Sample.IServiceImposter<int>(); imposter.Items.Getter().Returns(new int[1]); imposter.Items.Setter(SpanArg<int>.Is(1)).Callback(items => { }); imposter.Instance().Items = new int[] { 1 };",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanPropertyOfNullableElements_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.ReadOnlySpan<string?> Names { get; set; } }",
            "imposter.Names.Getter().Returns(new string?[] { null }); imposter.Names.Setter(ReadOnlySpanArg<string?>.Any()).Callback(names => { }); _ = imposter.Instance().Names;",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenSpanPropertiesImplementedExplicitly_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IFirst { System.Span<byte> Buffer { get; } } public interface ISecond { System.ReadOnlySpan<byte> Buffer { get; } } public interface IService : IFirst, ISecond { }",
            "Sample.IFirst first = imposter.Instance(); Sample.ISecond second = imposter.Instance(); _ = first.Buffer.Length + second.Buffer.Length;",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassSpanProperty_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { private byte[] _buffer = new byte[1]; public virtual System.Span<byte> Buffer { get => _buffer; set => _buffer = value.ToArray(); } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Buffer.Getter().UseBaseImplementation(); imposter.Buffer.Setter(SpanArg<byte>.Any()).UseBaseImplementation(); imposter.Instance().Buffer = imposter.Instance().Buffer;",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAbstractClassSpanProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public abstract class Service { public abstract System.ReadOnlySpan<char> Name { get; set; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Name.Getter().Returns(new[] { 'a' }); imposter.Instance().Name = imposter.Instance().Name;",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenProtectedClassSpanProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { protected virtual System.Span<byte> Buffer { get => default; set { } } public int Length => Buffer.Length; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Buffer.Getter().Returns(new byte[3]); _ = imposter.Instance().Length;",
            nameof(SpanPropertyCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassInitOnlySpanProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { private byte[] _buffer = new byte[0]; public virtual System.Span<byte> Buffer { get => _buffer; init => _buffer = value.ToArray(); } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Buffer.Setter(SpanArg<byte>.Any()).Called(Count.Never()); _ = imposter.Instance().Buffer;",
            nameof(SpanPropertyCompilationTests)
        );
    }
}
