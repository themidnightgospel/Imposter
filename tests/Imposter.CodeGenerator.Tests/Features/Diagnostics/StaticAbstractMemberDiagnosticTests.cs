#if ROSLYN4_4_OR_GREATER
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.Diagnostics;

// An interface whose static abstract member has no implementation can't be a type argument, which its imposter needs,
// so the target gets IMP012 instead of an imposter that doesn't compile.
public class StaticAbstractMemberDiagnosticTests
{
    private static readonly string StaticAbstractMemberId = DiagnosticDescriptors
        .ImposterTargetHasStaticAbstractMember
        .Id;

    [Fact]
    public async Task GivenInterfaceWithStaticAbstractMethod_WhenGeneratorRuns_ShouldReportIMP012()
    {
        var result = await RunGenerator(
            "public interface IService { static abstract int Create(); int Get(); }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(StaticAbstractMemberId);
    }

    [Fact]
    public async Task GivenInterfaceInheritingStaticAbstractMethod_WhenGeneratorRuns_ShouldReportIMP012()
    {
        var result = await RunGenerator(
            "public interface IBase { static abstract int Create(); } public interface IService : IBase { int Get(); }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(StaticAbstractMemberId);
    }

    [Fact]
    public async Task GivenInterfaceReabstractingInheritedStaticMethod_WhenGeneratorRuns_ShouldReportIMP012()
    {
        var result = await RunGenerator(
            "public interface IBase { static virtual int Create() => 1; } public interface IService : IBase { static abstract int IBase.Create(); int Get(); }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(StaticAbstractMemberId);
    }

    [Fact]
    public async Task GivenInterfaceWithStaticAbstractOperator_WhenGeneratorRuns_ShouldReportIMP012()
    {
        var result = await RunGenerator(
            "public interface IService { static abstract IService operator +(IService left, IService right); }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(StaticAbstractMemberId);
    }

    [Fact]
    public async Task GivenInterfaceWithStaticAbstractProperty_WhenGeneratorRuns_ShouldReportIMP012()
    {
        var result = await RunGenerator(
            "public interface IService { static abstract int Default { get; } }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(StaticAbstractMemberId);
    }

    [Fact]
    public async Task GivenInterfaceWithStaticAbstractMethod_WhenGeneratorRuns_ShouldNameTheMember()
    {
        var result = await RunGenerator(
            "public interface IService { static abstract int Create(); int Get(); }"
        );

        result
            .Diagnostics.ShouldHaveSingleItem()
            .GetMessage()
            .ShouldBe(
                "'Sample.IService' has the static abstract member 'Sample.IService.Create()', so it can't be the type argument its imposter needs"
            );
    }

    [Fact]
    public async Task GivenInterfaceWithStaticAbstractEvent_WhenGeneratorRuns_ShouldNameTheEvent()
    {
        var result = await RunGenerator(
            "public interface IService { static abstract event System.Action Changed; }"
        );

        result
            .Diagnostics.ShouldHaveSingleItem()
            .GetMessage()
            .ShouldBe(
                "'Sample.IService' has the static abstract member 'Sample.IService.Changed', so it can't be the type argument its imposter needs"
            );
    }

    [Fact]
    public async Task GivenInterfaceWithStaticAbstractMethod_WhenGeneratorRuns_ShouldNotGenerateTheImposter()
    {
        var result = await RunGenerator(
            "public interface IService { static abstract int Create(); int Get(); }"
        );

        result.GeneratedSources.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenInterfaceImplementingInheritedStaticAbstractMethod_WhenGeneratorRuns_ShouldNotReportDiagnostics()
    {
        var result = await RunGenerator(
            "public interface IBase { static abstract int Create(); } public interface IService : IBase { static int IBase.Create() => 1; int Get(); }"
        );

        result.Diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenInterfaceWithStaticVirtualMethod_WhenGeneratorRuns_ShouldNotReportDiagnostics()
    {
        var result = await RunGenerator(
            "public interface IService { static virtual int Create() => 1; int Get(); }"
        );

        result.Diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenClassImplementingInterfaceWithStaticAbstractMethod_WhenGeneratorRuns_ShouldNotReportDiagnostics()
    {
        var result = await RunGenerator(
            "public interface IBase { static abstract int Create(); } public class Service : IBase { public static int Create() => 1; public virtual int Get() => 0; }",
            "Sample.Service"
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
            nameof(StaticAbstractMemberDiagnosticTests),
            LanguageVersion.CSharp11
        );
}
#endif
