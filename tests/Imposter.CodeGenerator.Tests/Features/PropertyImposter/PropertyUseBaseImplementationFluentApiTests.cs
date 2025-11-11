
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
    public async Task Given_OverrideableProperty_WhenCallingUseBaseImplementation_ShouldCompile()
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
    public async Task Given_OverrideableGetterOnlyProperty_WhenCallingUseBaseImplementation_ShouldCompile()
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
    public async Task Given_OverrideableSetterOnlyProperty_WhenCallingUseBaseImplementation_ShouldCompile()
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
    public async Task Given_InterfaceProperty_WhenCallingUseBaseImplementation_ShouldFail()
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
    public async Task Given_AbstractProperty_WhenCallingUseBaseImplementation_ShouldFail()
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
    public async Task Given_NonVirtualProperty_WhenCallingUseBaseImplementation_ShouldFail()
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

    [Fact]
    public async Task Given_OverrideableProperty_WhenChainingUseBaseImplementationOnGetter_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new OverrideablePropertyServiceImposter();
            imposter.Age.UseBaseImplementation().Getter().Then().UseBaseImplementation();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_InterfaceProperty_WhenUsingPropertyLevelThenGetter_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new InterfaceTargetImposter();
            imposter.Age.UseBaseImplementation().Getter();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task Given_InterfaceProperty_WhenUsingPropertyLevelThenSetter_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new InterfaceTargetImposter();
            imposter.Age.UseBaseImplementation().Setter(Imposter.Abstractions.Arg<int>.Any());
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task Given_OverrideableProperty_WhenChainingUseBaseImplementationOnSetter_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new OverrideablePropertyServiceImposter();
            imposter.Age.UseBaseImplementation().Setter(Imposter.Abstractions.Arg<int>.Any()).Then().UseBaseImplementation();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_OverrideableProperty_WhenChainingGetterUseBaseImplementationAndReturning_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new OverrideablePropertyServiceImposter();
            imposter.Age.UseBaseImplementation().Getter().Then().UseBaseImplementation().Then().Returns(1);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_OverrideableProperty_WhenChainingSetterUseBaseImplementationAndCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new OverrideablePropertyServiceImposter();
            imposter.Age.UseBaseImplementation().Setter(Imposter.Abstractions.Arg<int>.Any()).Then().UseBaseImplementation().Then().Callback(value => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }
    [Fact]
    public async Task Given_GetterOnlyProperty_WhenChainingGetterUseBaseImplementation_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new GetterOnlyPropertyServiceImposter();
            imposter.Age.UseBaseImplementation().Getter().Then().UseBaseImplementation();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_GetterOnlyProperty_WhenChainingSetterUseBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new GetterOnlyPropertyServiceImposter();
            imposter.Age.UseBaseImplementation().Setter(Imposter.Abstractions.Arg<int>.Any()).Then().UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task Given_SetterOnlyProperty_WhenChainingSetterUseBaseImplementation_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SetterOnlyPropertyServiceImposter();
            imposter.Age.UseBaseImplementation().Setter(Imposter.Abstractions.Arg<int>.Any()).Then().UseBaseImplementation();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_SetterOnlyProperty_WhenChainingGetterUseBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SetterOnlyPropertyServiceImposter();
            imposter.Age.UseBaseImplementation().Getter().Then().UseBaseImplementation();
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
