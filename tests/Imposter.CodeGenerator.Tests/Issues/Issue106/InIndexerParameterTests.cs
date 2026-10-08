using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue106;

public class InIndexerParameterTests
{
    [Theory]
    [InlineData("IService", "public interface IService { int this[in int key] { get; set; } }")]
    [InlineData("IService", "public interface IService { int this[in int key] { get; } }")]
    [InlineData("IService", "public interface IService { int this[in int key] { set; } }")]
    [InlineData(
        "IService",
        "public interface IService { int this[in int key, string name] { get; set; } }"
    )]
    [InlineData(
        "Service",
        "public abstract class Service { public abstract int this[in int key] { get; set; } }"
    )]
    public async Task Given_IndexerWithInParameter_When_ImposterIsGenerated_Should_Compile(
        string targetName,
        string declaration
    )
    {
        var source = $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.{{targetName}}))]

            namespace Sample
            {
                {{declaration}}
            }
            """;

        var context = await GeneratorTestHelper.CreateContext(
            source,
            baseSourceFileName: "InIndexerParameter.Source.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(InIndexerParameterTests)
        );

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(""));
    }
}
