using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.Diagnostics;

// An imposter returns the results it is set up with by value, so a target that impersonates a member returning by
// reference gets IMP011 instead of an imposter that doesn't compile.
public class RefReturnDiagnosticTests
{
    private static readonly string RefReturnId = DiagnosticDescriptors
        .ImposterTargetHasRefReturningMember
        .Id;

    [Fact]
    public async Task GivenMethodReturningByReference_WhenGeneratorRuns_ShouldReportIMP011()
    {
        var result = await RunGenerator("public interface IService { ref int Get(); }");

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefReturnId);
    }

    [Fact]
    public async Task GivenMethodReturningByReadonlyReference_WhenGeneratorRuns_ShouldReportIMP011()
    {
        var result = await RunGenerator("public interface IService { ref readonly int Peek(); }");

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefReturnId);
    }

    [Fact]
    public async Task GivenGenericMethodReturningByReference_WhenGeneratorRuns_ShouldReportIMP011()
    {
        var result = await RunGenerator("public interface IService { ref T Get<T>(); }");

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefReturnId);
    }

    [Fact]
    public async Task GivenInheritedMethodReturningByReference_WhenGeneratorRuns_ShouldReportIMP011()
    {
        var result = await RunGenerator(
            "public interface IBase { ref int Get(); } public interface IService : IBase { }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefReturnId);
    }

    [Fact]
    public async Task GivenPropertyReturningByReference_WhenGeneratorRuns_ShouldReportIMP011()
    {
        var result = await RunGenerator("public interface IService { ref int Value { get; } }");

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefReturnId);
    }

    [Fact]
    public async Task GivenIndexerReturningByReadonlyReference_WhenGeneratorRuns_ShouldReportIMP011()
    {
        var result = await RunGenerator(
            "public interface IService { ref readonly int this[int index] { get; } }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefReturnId);
    }

    [Fact]
    public async Task GivenVirtualClassMethodReturningByReference_WhenGeneratorRuns_ShouldReportIMP011()
    {
        var result = await RunGenerator(
            "public class Service { private int _value; public virtual ref int Get() => ref _value; }",
            "Sample.Service"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefReturnId);
    }

    [Fact]
    public async Task GivenAbstractClassPropertyReturningByReference_WhenGeneratorRuns_ShouldReportIMP011()
    {
        var result = await RunGenerator(
            "public abstract class Service { public abstract ref int Value { get; } }",
            "Sample.Service"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefReturnId);
    }

    [Fact]
    public async Task GivenMethodReturningByReference_WhenGeneratorRuns_ShouldNameTheMember()
    {
        var result = await RunGenerator("public interface IService { ref int Get(); }");

        result
            .Diagnostics.ShouldHaveSingleItem()
            .GetMessage()
            .ShouldBe(
                "'Sample.IService' has the member 'Sample.IService.Get()', which returns by reference, so an imposter cannot impersonate it"
            );
    }

    [Fact]
    public async Task GivenMethodReturningByReference_WhenGeneratorRuns_ShouldNotGenerateTheImposter()
    {
        var result = await RunGenerator("public interface IService { ref int Get(); }");

        result.GeneratedSources.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenNonVirtualClassMethodReturningByReference_WhenGeneratorRuns_ShouldNotReportDiagnostics()
    {
        var result = await RunGenerator(
            "public class Service { private int _value; public ref int Get() => ref _value; public virtual int Count() => 0; }",
            "Sample.Service"
        );

        result.Diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenStaticInterfaceMethodReturningByReference_WhenGeneratorRuns_ShouldNotReportDiagnostics()
    {
        var result = await RunGenerator(
            "public interface IService { private static int s_value; static ref int Shared() => ref s_value; int Get(); }"
        );

        result.Diagnostics.ShouldBeEmpty();
    }

    // Calls reach the default body, which the imposter leaves in place.
    [Fact]
    public async Task GivenInterfaceMethodReturningByReferenceWithDefaultBody_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Plain(); ref int Get() => throw null!; }",
            "imposter.Plain().Returns(1); imposter.Instance().Plain();",
            nameof(RefReturnDiagnosticTests)
        );
    }

    [Fact]
    public async Task GivenInterfacePropertyReturningByReferenceWithDefaultBody_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Plain { get; } ref readonly int Value => throw null!; }",
            "imposter.Plain.Getter().Returns(1); _ = imposter.Instance().Plain;",
            nameof(RefReturnDiagnosticTests)
        );
    }

    [Fact]
    public async Task GivenEventWhoseDelegateReturnsByReference_WhenGeneratorRuns_ShouldNotReportDiagnostics()
    {
        var result = await RunGenerator(
            "public delegate ref int RefHandler(); public interface IService { event RefHandler Raised; }"
        );

        result.Diagnostics.ShouldBeEmpty();
    }

    private static Task<GeneratorRunResult> RunGenerator(
        string targetDeclaration,
        string targetType = "Sample.IService"
    ) =>
        TargetGeneratorRun.RunAsync(
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof({{targetType}}))]

            namespace Sample
            {
                {{targetDeclaration}}
            }
            """,
            nameof(RefReturnDiagnosticTests)
        );
}
