using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

// Raise passes a custom ref struct argument on to the callbacks and the handlers, but an imposter can't keep or match
// it, so the raise and handler-invocation histories and Raised leave it out.
public class RefStructEventCompilationTests
{
    private const string Cursor = "public ref struct Cursor { public int Position; } ";

    [Fact]
    public async Task GivenEventOfHandlerWithRefStructParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor
                + "public delegate void MovedHandler(int id, Cursor cursor); public interface IService { event MovedHandler Moved; }",
            "imposter.Moved.Callback((id, cursor) => { }); imposter.Instance().Moved += (id, cursor) => { }; imposter.Moved.Raise(1, new Sample.Cursor()); imposter.Moved.Raised(Arg<int>.Is(1), Count.Once()); imposter.Moved.HandlerInvoked(Arg<Sample.MovedHandler>.Any(), Count.Once());",
            nameof(RefStructEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenEventOfHandlerWhoseOnlyParameterIsARefStruct_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor
                + "public delegate void MovedHandler(Cursor cursor); public interface IService { event MovedHandler Moved; }",
            "imposter.Instance().Moved += cursor => { }; imposter.Moved.Raise(new Sample.Cursor()); imposter.Moved.Raised(Count.Once()); imposter.Moved.HandlerInvoked(Arg<Sample.MovedHandler>.Any(), Count.Once());",
            nameof(RefStructEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenEventOfHandlerWithRefAndOutRefStructParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Cursor
                + "public delegate void MovedHandler(object sender, ref Cursor from, out Cursor to); public interface IService { event MovedHandler Moved; }",
            "imposter.Instance().Moved += (object sender, ref Sample.Cursor from, out Sample.Cursor to) => { to = from; }; var from = new Sample.Cursor(); imposter.Moved.Raise(null, ref from, out var to); imposter.Moved.Raised(Arg<object>.Any(), Count.Once());",
            nameof(RefStructEventCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassEventOfHandlerWithRefStructParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Cursor
                + "public delegate void MovedHandler(Cursor cursor); public class Service { public virtual event MovedHandler Moved; public void Move(Cursor cursor) => Moved?.Invoke(cursor); }",
            "var imposter = new Sample.ServiceImposter(); imposter.Instance().Moved += cursor => { }; imposter.Moved.Raise(new Sample.Cursor());",
            nameof(RefStructEventCompilationTests)
        );
    }
}
