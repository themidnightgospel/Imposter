using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

public class IndexerInvocationStructureTests
{
    [Fact]
    public async Task GivenIndexerHistory_WhenGenerated_ShouldKeepArgumentsInternalAndRecordsImmutable()
    {
        var context = await GeneratorTestHelper.CreateContext(
            """
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.IService))]
            namespace Sample
            {
                public interface IService { int this[int key] { get; set; } }
            }
            """,
            baseSourceFileName: "IndexerInvocationStructure.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(IndexerInvocationStructureTests)
        );
        var root = context
            .RunGenerator()
            .GeneratedSources.ShouldHaveSingleItem()
            .SyntaxTree.GetRoot();
        var arguments = root.DescendantNodes()
            .OfType<StructDeclarationSyntax>()
            .Single(type => type.Identifier.ValueText.EndsWith("IndexerArguments"));
        arguments.Modifiers.Any(token => token.IsKind(SyntaxKind.InternalKeyword)).ShouldBeTrue();
        arguments.Modifiers.Any(token => token.IsKind(SyntaxKind.ReadOnlyKeyword)).ShouldBeTrue();
        arguments.BaseList.ShouldBeNull();
        var records = root.DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Where(type =>
                type.Identifier.ValueText.EndsWith("GetterInvocation")
                || type.Identifier.ValueText.EndsWith("SetterInvocation")
            )
            .ToArray();
        records.Length.ShouldBe(2);
        foreach (var record in records)
        {
            record
                .BaseList.ShouldNotBeNull()
                .Types.ShouldHaveSingleItem()
                .Type.ToString()
                .ShouldBe("global::Imposter.Abstractions.InvocationRecord");
            record
                .Members.OfType<FieldDeclarationSyntax>()
                .All(field =>
                    field.Modifiers.Any(token => token.IsKind(SyntaxKind.ReadOnlyKeyword))
                )
                .ShouldBeTrue();
            record
                .Members.OfType<MethodDeclarationSyntax>()
                .Any(method => method.Identifier.ValueText == "CompleteInvocation")
                .ShouldBeFalse();
        }
        root.DescendantNodes()
            .OfType<FinallyClauseSyntax>()
            .SelectMany(clause => clause.Block.Statements)
            .OfType<EmptyStatementSyntax>()
            .ShouldBeEmpty();
    }
}
