using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// The imposter's setup members take the target's parameter names and use its builder fields by their bare names.
public class ImposterBuilderFieldNameCollisionTests
{
    [Fact]
    public async Task GivenMethodParametersNamedLikeTheMethodFields_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public interface IService { int Get(int _getMethodImposter, int _getMethodInvocationHistoryCollection); }",
            "imposter.Get(Arg<int>.Any(), Arg<int>.Any()).Returns(1); imposter.Instance().Get(1, 2); imposter.Get(Arg<int>.Any(), Arg<int>.Any()).Called(Count.Once());"
        );
    }

    [Fact]
    public async Task GivenMethodTypeParameterNamedLikeTheMethodField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public interface IService { int Get<_getMethodImposterCollection>(_getMethodImposterCollection key); }",
            "imposter.Get<int>(Arg<int>.Any()).Returns(1); imposter.Instance().Get(1);"
        );
    }

    [Fact]
    public async Task GivenIndexerParameterNamedLikeTheIndexerField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public interface IService { int this[int _IndexerIndexer] { get; set; } }",
            "imposter[Arg<int>.Any()].Getter().Returns(1); var value = imposter.Instance()[1];"
        );
    }

    [Fact]
    public async Task GivenParameterNamedLikeThePropertyField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public interface IService { string Name { get; set; } void Rename(string _NamePropertyBuilderField); }",
            "imposter.Name.Getter().Returns(\"name\"); imposter.Instance().Rename(imposter.Instance().Name);"
        );
    }

    [Fact]
    public async Task GivenParameterNamedLikeTheEventField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public interface IService { event System.EventHandler Changed; void Notify(int _Changed); }",
            "imposter.Instance().Changed += (sender, args) => { }; imposter.Changed.Raise(null, System.EventArgs.Empty); imposter.Instance().Notify(1);"
        );
    }

    private static async Task AssertCompiles(string targetDeclaration, string usage)
    {
        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IService))]

            namespace Sample
            {
                {{targetDeclaration}}
            }
            """,
            baseSourceFileName: "ImposterBuilderFieldNameCollision.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(ImposterBuilderFieldNameCollisionTests)
        );

        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                /*lang=csharp*/
                $$"""
                using Imposter.Abstractions;

                public static class Usage
                {
                    public static void Run()
                    {
                        var imposter = new Sample.IServiceImposter();
                        {{usage}}
                    }
                }
                """
            )
        );
    }
}
