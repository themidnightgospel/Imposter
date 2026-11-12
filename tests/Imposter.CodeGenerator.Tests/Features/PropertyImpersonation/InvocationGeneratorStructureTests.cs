using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public class InvocationGeneratorStructureTests
{
    [Theory]
    [InlineData("int", 1)]
    [InlineData("System.ReadOnlySpan<int>", 2)]
    public async Task GivenSetterCriteria_WhenGenerating_ShouldStoreOnlyRequiredRepresentations(
        string valueType,
        int expectedCriteriaFields
    )
    {
        var context = await GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.IService))]
            namespace Sample
            {
                public interface IService { {{valueType}} Value { set; } }
            }
            """,
            "SetterCriteria.cs",
            "Snippet.cs",
            nameof(InvocationGeneratorStructureTests)
        );
        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
        var root = context
            .RunGenerator()
            .GeneratedSources.ShouldHaveSingleItem()
            .SyntaxTree.GetRoot();
        var builder = root.DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Single(type =>
                type.Identifier.ValueText == "Builder"
                && type.Parent is ClassDeclarationSyntax parent
                && parent.Identifier.ValueText.EndsWith("SetterImposter")
            );
        builder
            .Members.OfType<FieldDeclarationSyntax>()
            .SelectMany(field => field.Declaration.Variables)
            .Count(variable => variable.Identifier.ValueText is "_criteria" or "_runtimeCriteria")
            .ShouldBe(expectedCriteriaFields);
    }

    [Theory]
    [InlineData("int")]
    [InlineData("System.Threading.Tasks.Task<int>")]
    public async Task GivenMethodHistory_WhenGenerating_ShouldUseConventionalConstructorParameterNames(
        string resultType
    )
    {
        var context = await GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.IService))]
            namespace Sample
            {
                public interface IService { {{resultType}} Read(int _result, out int _exception); }
            }
            """,
            "HistoryConstructor.cs",
            "Snippet.cs",
            nameof(InvocationGeneratorStructureTests)
        );
        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
        var root = context
            .RunGenerator()
            .GeneratedSources.ShouldHaveSingleItem()
            .SyntaxTree.GetRoot();
        var record = root.DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Single(type => type.Identifier.ValueText == "ReadInvocation");
        record
            .Members.OfType<ConstructorDeclarationSyntax>()
            .ShouldHaveSingleItem()
            .ParameterList.Parameters.ShouldAllBe(parameter =>
                !parameter.Identifier.ValueText.StartsWith("_")
            );
    }
}
