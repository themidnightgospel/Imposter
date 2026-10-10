using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

// The raise passes a ref or out argument on with its ref kind, so a handler or a callback can change it for the raiser.
// An out argument starts as the default.
public class RefOutEventParameterCompilationTests
{
    [Fact]
    public async Task GivenEventOfHandlerWithRefParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate void Handler(object sender, ref int value); public interface IService { event Handler Changed; }",
            "imposter.Changed.Callback((object sender, ref int value) => value++); imposter.Instance().Changed += (object sender, ref int value) => value++; var value = 1; imposter.Changed.Raise(null, ref value); imposter.Changed.Raised(Arg<object>.Any(), Arg<int>.Is(1), Count.Once());",
            nameof(RefOutEventParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenEventOfHandlerWithOutParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate void Handler(out string value); public interface IService { event Handler Requested; }",
            "imposter.Requested.Callback((out string value) => value = \"a\"); imposter.Instance().Requested += (out string value) => value = \"b\"; imposter.Requested.Raise(out var value); imposter.Requested.Raised(Arg<string>.Any(), Count.Once());",
            nameof(RefOutEventParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenEventOfAsyncHandlerWithRefAndOutParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public delegate System.Threading.Tasks.Task Handler(ref int value, out int result); public interface IService { event Handler Changed; }",
            "imposter.Instance().Changed += (ref int value, out int result) => { result = value; return System.Threading.Tasks.Task.CompletedTask; }; imposter.Changed.RaiseAsync(1, 2).GetAwaiter().GetResult();",
            nameof(RefOutEventParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassEventOfHandlerWithRefParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public delegate void Handler(ref int value); public class Service { public virtual event Handler Changed; public void Change(ref int value) => Changed?.Invoke(ref value); }",
            "var imposter = new Sample.ServiceImposter(); imposter.Instance().Changed += (ref int value) => value++; var value = 1; imposter.Changed.Raise(ref value);",
            nameof(RefOutEventParameterCompilationTests)
        );
    }
}
