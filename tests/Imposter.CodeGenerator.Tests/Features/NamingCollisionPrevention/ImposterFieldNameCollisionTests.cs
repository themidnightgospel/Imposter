using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// The imposter instance reaches its imposter through a field named _imposter unless the target uses that name.
public class ImposterFieldNameCollisionTests
{
    [Fact]
    public async Task GivenInterfaceMethodParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public interface IService { int Get(int _imposter); }",
            "var imposter = new Sample.IServiceImposter(); imposter.Get(Arg<int>.Any()).Returns(1); imposter.Instance().Get(5);"
        );
    }

    [Fact]
    public async Task GivenInterfaceIndexerParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public interface IService { int this[int _imposter] { get; set; } }",
            "var imposter = new Sample.IServiceImposter(); var instance = imposter.Instance(); instance[1] = instance[2];"
        );
    }

    [Fact]
    public async Task GivenClassMethodParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public class Service { public virtual int Get(int _imposter) => _imposter; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Get(Arg<int>.Any()).UseBaseImplementation(); imposter.Instance().Get(5);"
        );
    }

    [Fact]
    public async Task GivenClassIndexerParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public class Service { public virtual int this[int _imposter] { get => _imposter; set { } } }",
            "var imposter = new Sample.ServiceImposter(); var instance = imposter.Instance(); instance[1] = instance[2];"
        );
    }

    [Fact]
    public async Task GivenMethodTypeParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public interface IService { void Accept<_imposter>(_imposter value); }",
            "var imposter = new Sample.IServiceImposter(); imposter.Instance().Accept(5);"
        );
    }

    [Fact]
    public async Task GivenTargetTypeParameterNamedLikeTheImposterField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "public interface IService<_imposter> { _imposter Get(); }",
            "var imposter = new Sample.IServiceImposter<int>(); imposter.Instance().Get();",
            "typeof(Sample.IService<>)"
        );
    }

    private static async Task AssertCompiles(
        string targetDeclaration,
        string usage,
        string targetType = "typeof(Sample.IService)"
    )
    {
        if (targetDeclaration.Contains("class Service"))
        {
            targetType = "typeof(Sample.Service)";
        }

        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter({{targetType}})]

            namespace Sample
            {
                {{targetDeclaration}}
            }
            """,
            baseSourceFileName: "ImposterFieldNameCollision.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(ImposterFieldNameCollisionTests)
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
                        {{usage}}
                    }
                }
                """
            )
        );
    }
}
