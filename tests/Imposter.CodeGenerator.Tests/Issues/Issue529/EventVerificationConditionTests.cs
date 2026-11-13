using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue529;

public class EventVerificationConditionTests
{
    [Theory]
    [InlineData("System.Action")]
    [InlineData("System.Action<int, string>")]
    public async Task Given_Event_When_Generated_Should_NotEmitConstantVerificationConditions(
        string handlerType
    )
    {
        var source = $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IEventSource))]

            namespace Sample
            {
                public interface IEventSource
                {
                    event {{handlerType}} Changed;
                }
            }
            """;
        var context = await GeneratorTestHelper.CreateContext(
            source,
            baseSourceFileName: "EventSource.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(EventVerificationConditionTests)
        );

        var generated = context.RunGenerator().GeneratedSources.Single();
        var root = CSharpSyntaxTree.ParseText(generated.SourceText).GetRoot();

        root.DescendantNodes()
            .OfType<IfStatementSyntax>()
            .ShouldNotContain(statement =>
                statement.Condition.IsKind(SyntaxKind.TrueLiteralExpression)
            );
        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(""));
    }
}
