using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue108;

public class NamespaceTypeCollisionTests
{
    [Theory]
    [InlineData("Environment")]
    [InlineData("Count")]
    [InlineData("TypeCaster")]
    [InlineData("Exception")]
    [InlineData("NotImplementedException")]
    [InlineData("ArgumentNullException")]
    [InlineData(
        "Environment,Count,TypeCaster,Exception,NotImplementedException,ArgumentNullException,System,Imposter"
    )]
    public async Task Given_SameNamedTypesInTargetNamespace_When_ImposterIsGenerated_Should_Compile(
        string typeNames
    )
    {
        var declarations = string.Join(
            "\n",
            typeNames.Split(',').Select(name => $"public class {name} {{ }}")
        );
        var source = $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IService))]

            namespace Sample
            {
                {{declarations}}

                public interface IService
                {
                    void Do(int value);
                    T Echo<T>(T value);
                    int Value { get; set; }
                    int this[int key] { get; set; }
                    event global::System.Action Changed;
                }
            }
            """;

        var context = await GeneratorTestHelper.CreateContext(
            source,
            baseSourceFileName: "NamespaceTypeCollision.Source.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(NamespaceTypeCollisionTests)
        );

        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            using Imposter.Abstractions;

            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IServiceImposter();
                        imposter.Echo<int>(Arg<int>.Any()).Returns(value => value);
                        imposter.Value.Getter().Throws<global::System.InvalidOperationException>();
                        imposter[Arg<int>.Any()].Getter().Throws<global::System.InvalidOperationException>();

                        var instance = imposter.Instance();
                        instance.Do(1);
                        instance.Echo(2);
                        imposter.Do(1).Called(global::Imposter.Abstractions.Count.Once());
                        imposter.Echo<int>(2).Called(global::Imposter.Abstractions.Count.Once());

                        global::System.Action handler = () => { };
                        instance.Changed += handler;
                        imposter.Changed.Raise();
                        instance.Changed -= handler;
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }
}
