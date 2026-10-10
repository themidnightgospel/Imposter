#if ROSLYN4_14_OR_GREATER
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// A value of a method's type parameter that allows ref structs may be a ref struct, so the imposter passes it through
// as it does another ref struct, and repeats the anti-constraint on every generic type it declares for the method. Its
// setups apply to calls with the same type arguments only.
public class AllowsRefStructCompilationTests
{
    private const string Slot = "public ref struct Slot { public int Value; } ";

    [Fact]
    public async Task GivenMethodTakingValueOfTypeParameterThatAllowsRefStructs_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Slot
                + "public interface IService { int Use<T>(T value, int count) where T : allows ref struct; }",
            "imposter.Use<Sample.Slot>(Arg<int>.Is(1)).Returns((value, count) => value.Value + count).Callback((value, count) => { }).Then().Throws((value, count) => new System.Exception()).Then().Returns(3); imposter.Use<int>(Arg<int>.Any()).Returns((value, count) => value); _ = imposter.Instance().Use(new Sample.Slot(), 1) + imposter.Instance().Use(2, 1); imposter.Use<Sample.Slot>(Arg<int>.Any()).Called(Count.Once());",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }

    [Fact]
    public async Task GivenMethodReturningValueOfTypeParameterThatAllowsRefStructs_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Slot
                + "public interface IService { T Echo<T>(T value) where T : allows ref struct; T Create<T>() where T : allows ref struct; }",
            "imposter.Echo<Sample.Slot>().Returns(value => value); imposter.Create<Sample.Slot>().Returns(() => new Sample.Slot { Value = 1 }).Then().Throws(new System.Exception()); _ = imposter.Instance().Echo(new Sample.Slot()).Value + imposter.Instance().Create<Sample.Slot>().Value;",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }

    [Fact]
    public async Task GivenValuesOfTypeParameterThatAllowsRefStructsOfEveryRefKind_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Slot
                + "public interface IService { void Fill<T>(out T value, ref T other, in T input, scoped T last) where T : allows ref struct; }",
            "imposter.Fill<Sample.Slot>().Callback((out Sample.Slot value, ref Sample.Slot other, in Sample.Slot input, scoped Sample.Slot last) => { value = input; }); var slot = new Sample.Slot(); imposter.Instance().Fill(out Sample.Slot filled, ref slot, in slot, slot);",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }

    // The other type parameter, and the values of types that only use the one allowing ref structs, are matched.
    [Fact]
    public async Task GivenMethodWithOtherTypeParameterAndValues_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Slot
                + "public ref struct Wrapper<T> where T : allows ref struct { public T Value; } public interface IService { void Mix<T, U>(T value, U other) where T : allows ref struct; int Wrap<T>(Wrapper<T> wrapper, int n) where T : allows ref struct; int Call<T>(System.Func<T> factory) where T : allows ref struct; Wrapper<T> MakeWrapper<T>(int n) where T : allows ref struct; }",
            "imposter.Mix<Sample.Slot, string>(Arg<string>.Is(\"a\")).Callback((value, other) => { }); imposter.Wrap<Sample.Slot>(Arg<int>.Any()).Returns((wrapper, n) => wrapper.Value.Value); imposter.Call<Sample.Slot>(Arg<System.Func<Sample.Slot>>.Any()).Returns(factory => factory().Value); imposter.MakeWrapper<Sample.Slot>(Arg<int>.Any()).Returns(n => new Sample.Wrapper<Sample.Slot>()); var service = imposter.Instance(); service.Mix(new Sample.Slot(), \"a\"); _ = service.Wrap(new Sample.Wrapper<Sample.Slot>(), 1) + service.Call(() => new Sample.Slot()); _ = service.MakeWrapper<Sample.Slot>(1);",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }

    [Fact]
    public async Task GivenTypeParameterWithOtherConstraintsThatAllowsRefStructs_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public ref struct Slot : System.IDisposable { public void Dispose() { } } public interface IService { void A<T>(T value) where T : struct, allows ref struct; void B<T>(T value) where T : System.IDisposable, allows ref struct; T C<T>() where T : new(), allows ref struct; }",
            "imposter.A<Sample.Slot>().Callback(value => value.Dispose()); imposter.B<Sample.Slot>().Callback(value => value.Dispose()); imposter.C<Sample.Slot>().Returns(() => new Sample.Slot()); var service = imposter.Instance(); service.A(new Sample.Slot()); service.B(new Sample.Slot()); _ = service.C<Sample.Slot>();",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }

    [Fact]
    public async Task GivenClassMethodsWhoseTypeParameterAllowsRefStructs_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Slot
                + "public abstract class Service { public virtual int Use<T>(T value, int count) where T : allows ref struct => count; public abstract T Echo<T>(T value) where T : allows ref struct; protected virtual T Make<T>() where T : allows ref struct => default!; public int CallMake() => Make<Slot>().Value; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Use<Sample.Slot>(Arg<int>.Any()).UseBaseImplementation().Then().Returns((value, count) => 1); imposter.Echo<Sample.Slot>().Returns(value => value); imposter.Make<Sample.Slot>().UseBaseImplementation(); var service = imposter.Instance(); _ = service.Use(new Sample.Slot(), 2) + service.Echo(new Sample.Slot()).Value + service.CallMake();",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }

    [Fact]
    public async Task GivenInheritedMethodWhoseTypeParameterAllowsRefStructs_WhenSetUpThroughTheView_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Slot
                + "public interface IBase { int Use<T>(T value) where T : allows ref struct; } public interface IService : IBase { new int Use<T>(T value) where T : allows ref struct; }",
            "imposter.Use<Sample.Slot>().Returns(value => 1); imposter.For(default(Sample.IBase)).Use<Sample.Slot>().Returns(value => 2); _ = imposter.Instance().Use(new Sample.Slot()) + ((Sample.IBase)imposter.Instance()).Use(new Sample.Slot());",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }

    [Fact]
    public async Task GivenOpenGenericTargetWithMethodWhoseTypeParameterAllowsRefStructs_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            Slot
                + "public interface IService<TOuter> { TOuter Use<T>(T value, TOuter outer) where T : allows ref struct; }",
            "var imposter = new Sample.IServiceImposter<string>(); imposter.Use<Sample.Slot>(Arg<string>.Any()).Returns((value, outer) => outer); _ = imposter.Instance().Use(new Sample.Slot(), \"a\");",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }

    [Fact]
    public async Task GivenAwaitableMethodWhoseTypeParameterAllowsRefStructs_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Slot
                + "public interface IService { System.Threading.Tasks.Task<int> UseAsync<T>(T value, int count) where T : allows ref struct; System.Threading.Tasks.Task RunAsync<T>(T value) where T : allows ref struct; }",
            "imposter.UseAsync<Sample.Slot>(Arg<int>.Is(1)).ReturnsAsync(3); imposter.UseAsync<Sample.Slot>(Arg<int>.Any()).Returns((value, count) => System.Threading.Tasks.Task.FromResult(value.Value)); imposter.RunAsync<Sample.Slot>().Callback(value => System.Threading.Tasks.Task.CompletedTask); _ = imposter.Instance().UseAsync(new Sample.Slot(), 1); _ = imposter.Instance().RunAsync(new Sample.Slot());",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }

    // Overloads whose setups take the same matchers are set up by their unique names.
    [Fact]
    public async Task GivenOverloadsWhoseTypeParameterAllowsRefStructs_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Slot
                + "public interface IService { void Use<T>(T value) where T : allows ref struct; void Use<T>(T value, Slot extra) where T : allows ref struct; }",
            "imposter.Use<Sample.Slot>().Callback(value => { }); imposter.Use_1<Sample.Slot>().Callback((value, extra) => { }); imposter.Instance().Use(new Sample.Slot()); imposter.Instance().Use(new Sample.Slot(), new Sample.Slot());",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }

    // Each overload's own declaration says which of its parameters its setup matches, whatever the type parameters it's
    // compared in.
    [Fact]
    public async Task GivenOverloadThatDiffersOnlyInAValueOfItsTypeParameter_WhenSetUpThroughTheView_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Slot
                + "public interface IService { int Use<T>(T value, int x) where T : allows ref struct; int Use<T>(int x); }",
            "imposter.Use<Sample.Slot>(Arg<int>.Any()).Returns((value, x) => x); imposter.Use_1<int>(Arg<int>.Any()).Returns(x => x); var view = imposter.For(default(Sample.IService)); view.Use<Sample.Slot>(Arg<int>.Any()).Returns((value, x) => x); view.Use_1<int>(Arg<int>.Any()).Returns(x => x); _ = imposter.Instance().Use(new Sample.Slot(), 1) + imposter.Instance().Use<int>(1);",
            nameof(AllowsRefStructCompilationTests),
            LanguageVersion.CSharp13
        );
    }
}
#endif
