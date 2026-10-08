using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

public class GivenKeywordNamedTypeParametersWhenSnippetIsCompiledShouldCompileTest
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.KeywordTypeParameters.IKeywordGenericMethods))]
        [assembly: GenerateImposter(typeof(Sample.KeywordTypeParameters.IKeywordGenericTarget<>))]
        [assembly: GenerateImposter(typeof(Sample.KeywordTypeParameters.KeywordGenericMethodsClass))]

        namespace Sample.KeywordTypeParameters
        {
            public interface IKeywordGenericMethods
            {
                void Accept<@class>(@class value);

                @int Produce<@int>();

                @event Map<@event, @in>(@in source) where @in : @event;
            }

            public interface IKeywordGenericTarget<@class>
            {
                @class Value { get; set; }

                @class Get(@class key);

                event System.Action<@class> Changed;
            }

            public class KeywordGenericMethodsClass
            {
                public virtual @class Echo<@class>(@class value) => value;
            }
        }
        """;

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "KeywordTypeParametersGeneratorInput.cs",
            snippetFileName: "KeywordTypeParametersSnippet.cs",
            assemblyName: nameof(
                GivenKeywordNamedTypeParametersWhenSnippetIsCompiledShouldCompileTest
            )
        );

    [Fact]
    public async Task GivenKeywordNamedTypeParameters_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Imposter.Abstractions;
            using Sample.KeywordTypeParameters;

            namespace Sample.KeywordTypeParametersUsage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var methods = new IKeywordGenericMethodsImposter();
                        methods.Accept<string>(Arg<string>.Any()).Called(Count.Never());
                        methods.Produce<int>().Returns(42);
                        methods.Map<object, string>(Arg<string>.Any()).Returns("mapped");

                        var target = new IKeywordGenericTargetImposter<string>();
                        target.Get(Arg<string>.Any()).Returns("value");

                        var classTarget = new KeywordGenericMethodsClassImposter();
                        classTarget.Echo<int>(Arg<int>.Any()).UseBaseImplementation();
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    private static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }
}
