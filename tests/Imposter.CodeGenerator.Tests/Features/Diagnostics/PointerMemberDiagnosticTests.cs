using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.Diagnostics;

// A pointer or function pointer can't be a type argument, as an imposter's matchers and history need, so a target that
// impersonates a member whose signature uses one gets IMP013 instead of an imposter that doesn't compile.
public class PointerMemberDiagnosticTests
{
    private static readonly string PointerMemberId = DiagnosticDescriptors
        .ImposterTargetHasPointerMember
        .Id;

    [Fact]
    public async Task GivenMethodWithPointerParameter_WhenGeneratorRuns_ShouldReportIMP013()
    {
        var result = await RunGenerator(
            "public unsafe interface IService { void Use(int* value); }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(PointerMemberId);
    }

    [Fact]
    public async Task GivenMethodWithPointerParameterByReference_WhenGeneratorRuns_ShouldReportIMP013()
    {
        var result = await RunGenerator(
            "public unsafe interface IService { void Use(ref int* value); }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(PointerMemberId);
    }

    [Fact]
    public async Task GivenMethodReturningPointer_WhenGeneratorRuns_ShouldReportIMP013()
    {
        var result = await RunGenerator("public unsafe interface IService { int* Get(); }");

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(PointerMemberId);
    }

    [Fact]
    public async Task GivenMethodWithFunctionPointerParameter_WhenGeneratorRuns_ShouldReportIMP013()
    {
        var result = await RunGenerator(
            "public unsafe interface IService { void Use(delegate*<int, void> callback); }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(PointerMemberId);
    }

    [Fact]
    public async Task GivenMethodWithArrayOfPointersParameter_WhenGeneratorRuns_ShouldReportIMP013()
    {
        var result = await RunGenerator(
            "public unsafe interface IService { void Use(int*[] values); }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(PointerMemberId);
    }

    [Fact]
    public async Task GivenPropertyOfPointerType_WhenGeneratorRuns_ShouldReportIMP013()
    {
        var result = await RunGenerator(
            "public unsafe interface IService { int* Value { get; set; } }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(PointerMemberId);
    }

    [Fact]
    public async Task GivenIndexerWithPointerKey_WhenGeneratorRuns_ShouldReportIMP013()
    {
        var result = await RunGenerator(
            "public unsafe interface IService { int this[int* key] { get; } }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(PointerMemberId);
    }

    [Fact]
    public async Task GivenEventWhoseDelegateTakesPointer_WhenGeneratorRuns_ShouldReportIMP013()
    {
        var result = await RunGenerator(
            "public unsafe delegate void PointerHandler(int* value); public interface IService { event PointerHandler Raised; }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(PointerMemberId);
    }

    [Fact]
    public async Task GivenInheritedMethodWithPointerParameter_WhenGeneratorRuns_ShouldReportIMP013()
    {
        var result = await RunGenerator(
            "public unsafe interface IBase { void Use(int* value); } public interface IService : IBase { }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(PointerMemberId);
    }

    [Fact]
    public async Task GivenVirtualClassMethodWithPointerParameter_WhenGeneratorRuns_ShouldReportIMP013()
    {
        var result = await RunGenerator(
            "public unsafe class Service { public virtual void Use(int* value) { } }",
            "Sample.Service"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(PointerMemberId);
    }

    // Only virtual and abstract members of a class are impersonated.
    [Fact]
    public async Task GivenNonVirtualClassMethodWithPointerParameter_WhenGeneratorRuns_ShouldNotReportDiagnostics()
    {
        var result = await RunGenerator(
            "public unsafe class Service { public void Use(int* value) { } public virtual int Get() => 0; }",
            "Sample.Service"
        );

        result.Diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenMethodWithPointerParameter_WhenGeneratorRuns_ShouldNameTheMemberAndType()
    {
        var result = await RunGenerator(
            "public unsafe interface IService { void Use(int* value); }"
        );

        result
            .Diagnostics.ShouldHaveSingleItem()
            .GetMessage()
            .ShouldBe(
                "'Sample.IService' has the member 'Sample.IService.Use(int*)', whose signature uses the pointer type 'int*', which an imposter cannot store or match"
            );
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
            nameof(PointerMemberDiagnosticTests)
        );
}
