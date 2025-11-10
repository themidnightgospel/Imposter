
using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImposter;

public class PropertyUseBaseImplementationFluentApiTests
{
    private const string Source = /*lang=csharp*/"""
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.OverrideablePropertyService))]
[assembly: GenerateImposter(typeof(Sample.GetterOnlyPropertyService))]
[assembly: GenerateImposter(typeof(Sample.SetterOnlyPropertyService))]
[assembly: GenerateImposter(typeof(Sample.InterfaceTarget))]
[assembly: GenerateImposter(typeof(Sample.AbstractPropertyService))]
[assembly: GenerateImposter(typeof(Sample.NonVirtualPropertyService))]

namespace Sample
{
    public class OverrideablePropertyService
    {
        public virtual int Age { get; set; }
    }

    public class GetterOnlyPropertyService
    {
        public virtual int Age { get; }
    }

    public class SetterOnlyPropertyService
    {
        public virtual int Age
        {
            set { }
        }
    }

    public interface InterfaceTarget
    {
        int Age { get; set; }
    }

    public abstract class AbstractPropertyService
    {
        public abstract int Age { get; set; }
    }

    public class NonVirtualPropertyService
    {
        public int Age { get; set; }
    }
}
""";

    [Fact]
    public async Task GivenOverrideableProperty_WhenCallingUseBaseImplementation_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new OverrideablePropertyServiceImposter();
            imposter.Age.UseBaseImplementation();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenOverrideableGetterOnlyProperty_WhenCallingUseBaseImplementation_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new GetterOnlyPropertyServiceImposter();
            imposter.Age.UseBaseImplementation();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenOverrideableSetterOnlyProperty_WhenCallingUseBaseImplementation_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SetterOnlyPropertyServiceImposter();
            imposter.Age.UseBaseImplementation();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenInterfaceProperty_WhenCallingUseBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new InterfaceTargetImposter();
            imposter.Age.UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task GivenAbstractProperty_WhenCallingUseBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new AbstractPropertyServiceImposter();
            imposter.Age.UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task GivenNonVirtualProperty_WhenCallingUseBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new NonVirtualPropertyServiceImposter();
            imposter.Age.UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    private const string BaseSourceFileName = "GeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(PropertyUseBaseImplementationFluentApiTests));

    private static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }

    private static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);

    private static void AssertSingleDiagnostic(ImmutableArray<Diagnostic> diagnostics, string expectedId, int expectedLine) =>
        GeneratorTestHelper.AssertSingleDiagnostic(diagnostics, expectedId, expectedLine, SnippetFileName);
}
