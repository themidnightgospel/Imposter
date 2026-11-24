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
    [InlineData("System.Action", 0)]
    [InlineData("System.Action<int, string>", 2)]
    public async Task Given_Event_When_Generated_Should_NotEmitConstantVerificationConditions(
        string handlerType,
        int expectedMatcherCount
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

        var raisedVerifiers = root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(method => method.Identifier.ValueText == "Raised" && method.Body is not null)
            .ToArray();
        raisedVerifiers.Length.ShouldBe(2);
        foreach (var verifier in raisedVerifiers)
        {
            var countLoop = verifier.DescendantNodes().OfType<ForEachStatementSyntax>().First();
            var conditions = countLoop
                .Statement.DescendantNodes()
                .OfType<IfStatementSyntax>()
                .ToArray();
            if (expectedMatcherCount == 0)
            {
                conditions.ShouldBeEmpty();
            }
            else
            {
                var condition = conditions.ShouldHaveSingleItem().Condition;
                condition
                    .DescendantNodesAndSelf()
                    .OfType<InvocationExpressionSyntax>()
                    .Count(invocation =>
                        invocation.Expression is MemberAccessExpressionSyntax memberAccess
                        && memberAccess.Name.Identifier.ValueText == "Matches"
                    )
                    .ShouldBe(expectedMatcherCount);
            }
        }

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(""));
    }
}
