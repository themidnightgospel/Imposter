using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

public class InheritedIndexerCollisionTests
{
    [Fact]
    public async Task GivenIdenticalIndexersFromSeparateBaseInterfaces_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            /*lang=csharp*/
            """
            public interface IA { int this[int key] { get; } }
            public interface IB { int this[int key] { get; } }
            public interface IService : IA, IB { }
            """
        );
    }

    [Fact]
    public async Task GivenIndexersWithSameParametersAndDifferentReturnTypes_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            /*lang=csharp*/
            """
            public interface IA { int this[int key] { get; } }
            public interface IB { string this[int key] { get; } }
            public interface IService : IA, IB { }
            """
        );
    }

    [Fact]
    public async Task GivenIndexersWithSameParametersAndDifferentAccessors_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            /*lang=csharp*/
            """
            public interface IA { int this[int key] { get; set; } }
            public interface IB { int this[int key] { get; } }
            public interface IService : IA, IB { }
            """
        );
    }

    [Fact]
    public async Task GivenIndexerHiddenByDerivedInterface_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            /*lang=csharp*/
            """
            public interface IBase { int this[int key] { get; } }
            public interface IService : IBase { new string this[int key] { get; } }
            """
        );
    }

    [Fact]
    public async Task GivenIndexersWithDifferentParameterTypes_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            /*lang=csharp*/
            """
            public interface IA { int this[int key] { get; } }
            public interface IB { string this[string key] { get; } }
            public interface IService : IA, IB { }
            """
        );
    }

    [Fact]
    public async Task GivenSingleIndexerInheritedThroughDiamond_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            /*lang=csharp*/
            """
            public interface IBase { int this[int key] { get; } }
            public interface IA : IBase { }
            public interface IB : IBase { }
            public interface IService : IA, IB { }
            """
        );
    }

    private static async Task AssertImposterCompiles(string interfaces)
    {
        var context = await GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IService))]

            namespace Sample
            {
                {{interfaces}}
            }
            """,
            baseSourceFileName: "InheritedIndexers.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(InheritedIndexerCollisionTests)
        );

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }
}
