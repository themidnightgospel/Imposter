using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue130;

public class InitOnlyAccessorTests
{
    [Theory]
    [InlineData("public interface Service { int Value { get; init; } }")]
    [InlineData("public class Service { public virtual int Value { get; init; } }")]
    [InlineData("public abstract class Service { public abstract int Value { get; init; } }")]
    [InlineData("public interface Service { int Value { get; set; } }")]
    [InlineData("public class Service { public virtual int Value { get; set; } }")]
    [InlineData("public abstract class Service { public abstract int Value { get; set; } }")]
    [InlineData("public interface Service { int Value { init; } }")]
    [InlineData("public class Service { public virtual int Value { init { } } }")]
    [InlineData("public abstract class Service { public abstract int Value { init; } }")]
    [InlineData(
        "public interface IBase { int Value { get; init; } } public interface Service : IBase { }"
    )]
    [InlineData(
        "public class Base { public virtual int Value { get; init; } } public class Service : Base { }"
    )]
    [InlineData(
        "public interface IFirst { int Value { get; init; } } public interface ISecond { string Value { get; init; } } public interface Service : IFirst, ISecond { }"
    )]
    public async Task Given_PropertyAccessor_When_GeneratingImposter_Should_Compile(
        string targetDeclaration
    )
    {
        var context = await GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.Service))]
            namespace Sample
            {
                {{targetDeclaration}}
            }
            """,
            baseSourceFileName: "InitOnlyAccessors.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(InitOnlyAccessorTests)
        );

        context
            .Compilation.GetDiagnostics()
            .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
            .ShouldBeEmpty();
        context.RunGenerator().GeneratedSources.Length.ShouldBe(1);
        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                "public static class Scenario { public static object Create() => new Sample.ServiceImposter(); }"
            )
        );
    }
}
