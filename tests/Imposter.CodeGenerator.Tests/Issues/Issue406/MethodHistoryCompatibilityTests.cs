using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue406;

public class MethodHistoryCompatibilityTests
{
    [Theory]
    [InlineData("count")]
    [InlineData("invocation")]
    [InlineData("Node")]
    [InlineData("Next")]
    [InlineData("MatchesCaptured")]
    [InlineData("capturedvalue")]
    public async Task GivenHistoryNames_WhenUsedAsTypeParametersAndParameters_ShouldCompile(
        string name
    )
    {
        var context = await GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.IService))]
            namespace Sample
            {
                public interface IService
                {
                    {{name}} Echo<{{name}}>({{name}} value);
                    int Capture(int value, int capturedvalue, int MatchesCaptured);
                    int Square(int {{name}});
                }
            }
            """,
            baseSourceFileName: "History.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(MethodHistoryCompatibilityTests),
            languageVersion: LanguageVersion.CSharp9
        );
        context.RunGenerator().Diagnostics.ShouldBeEmpty();
        context.CompileSnippet("", DiagnosticSeverity.Warning).ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenPublicHistoryPlumbing_WhenNamedByConsumer_ShouldRemainSourceCompatible()
    {
        var context = await GeneratorTestHelper.CreateContext(
            """
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.IService))]
            namespace Sample
            {
                public interface IService { int Square(int value); T Echo<T>(T value); }
            }
            """,
            baseSourceFileName: "History.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(MethodHistoryCompatibilityTests),
            languageVersion: LanguageVersion.CSharp9
        );
        context.RunGenerator().Diagnostics.ShouldBeEmpty();
        context
            .CompileSnippet(
                """
                using Imposter.Abstractions;
                namespace Sample
                {
                    public class Consumer : IServiceImposter.ISquareMethodInvocationHistory, IServiceImposter.IEchoMethodInvocationHistory
                    {
                        public bool Matches(IServiceImposter.SquareArgumentsCriteria criteria) =>
                            criteria.Matches(new IServiceImposter.SquareArguments(1));
                        public bool Matches<TTarget>(IServiceImposter.EchoArgumentsCriteria<TTarget> criteria) =>
                            criteria.Matches(new IServiceImposter.EchoArguments<TTarget>(default(TTarget)));
                        public void Use()
                        {
                            var args = new IServiceImposter.SquareArguments(1);
                            args.value = 2;
                            var criteria = new IServiceImposter.SquareArgumentsCriteria(Arg<int>.Any());
                            _ = criteria.value;
                            _ = criteria.Matches(args);
                            var generic = new IServiceImposter.EchoArguments<int>(1).As<object>();
                            var matchers = new IServiceImposter.EchoArgumentsCriteria<int>(Arg<int>.Any()).As<object>();
                            _ = matchers.Matches(generic);
                        }
                    }
                }
                """,
                DiagnosticSeverity.Warning
            )
            .ShouldBeEmpty();
    }
}
