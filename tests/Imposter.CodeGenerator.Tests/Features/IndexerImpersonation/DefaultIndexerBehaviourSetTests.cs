using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

// The setter imposter calls the base setter itself, so the default behaviour's Set only keeps the value.
public class DefaultIndexerBehaviourSetTests
{
    [Fact]
    public async Task GivenInterfaceIndexer_WhenImposterIsGenerated_ShouldGiveDefaultBehaviourSetTheArgumentsAndValueOnly()
    {
        var parameters = await DefaultBehaviourSetParameters(
            "public interface IService { int this[int key] { get; set; } }",
            "Sample.IService"
        );

        parameters.ShouldBe(["arguments", "value"]);
    }

    [Fact]
    public async Task GivenClassIndexerWithBaseSetter_WhenImposterIsGenerated_ShouldGiveDefaultBehaviourSetTheArgumentsAndValueOnly()
    {
        var parameters = await DefaultBehaviourSetParameters(
            "public class Service { public virtual int this[int key] { get => key; set { } } }",
            "Sample.Service"
        );

        parameters.ShouldBe(["arguments", "value"]);
    }

    private static async Task<string[]> DefaultBehaviourSetParameters(
        string targetDeclaration,
        string targetType
    )
    {
        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof({{targetType}}))]

            namespace Sample
            {
                {{targetDeclaration}}
            }
            """,
            baseSourceFileName: $"{nameof(DefaultIndexerBehaviourSetTests)}.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(DefaultIndexerBehaviourSetTests)
        );
        var set = context
            .RunGenerator()
            .GeneratedSources.SelectMany(source =>
                source.SyntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
            )
            .Where(type =>
                type.Identifier.Text.StartsWith("Default")
                && type.Identifier.Text.EndsWith("IndexerBehaviour")
            )
            .SelectMany(type => type.Members.OfType<MethodDeclarationSyntax>())
            .Single(method => method.Identifier.Text == "Set");

        return [.. set.ParameterList.Parameters.Select(parameter => parameter.Identifier.Text)];
    }
}
