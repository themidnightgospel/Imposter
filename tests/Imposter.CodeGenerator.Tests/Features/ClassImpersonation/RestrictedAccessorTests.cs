using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

public class RestrictedAccessorTests
{
    [Fact]
    public async Task GivenPropertyWithProtectedSetter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles("public virtual int Value { get; protected set; }");
    }

    [Fact]
    public async Task GivenPropertyWithPrivateSetter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles("public virtual int Value { get; private set; }");
    }

    [Fact]
    public async Task GivenPropertyWithProtectedGetter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles("public virtual int Value { protected get; set; }");
    }

    [Fact]
    public async Task GivenPropertyWithPrivateGetter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles("public virtual int Value { private get; set; }");
    }

    [Fact]
    public async Task GivenPropertyWithInternalSetter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles("public virtual int Value { get; internal set; }");
    }

    [Fact]
    public async Task GivenIndexerWithProtectedSetter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual int this[int key] { get => key; protected set { } }"
        );
    }

    [Fact]
    public async Task GivenIndexerWithPrivateSetter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual int this[int key] { get => key; private set { } }"
        );
    }

    [Fact]
    public async Task GivenIndexerWithProtectedGetter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual int this[int key] { protected get => key; set { } }"
        );
    }

    [Fact]
    public async Task GivenIndexerWithPrivateGetter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual int this[int key] { private get => key; set { } }"
        );
    }

    [Fact]
    public async Task GivenAbstractPropertyWithProtectedSetter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public abstract int Value { get; protected set; }",
            isAbstract: true
        );
    }

    private static async Task AssertClassImposterCompiles(
        string memberDeclaration,
        bool isAbstract = false
    )
    {
        var classModifier = isAbstract ? "abstract " : "";
        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.Service))]

            namespace Sample
            {
                public {{classModifier}}class Service
                {
                    {{memberDeclaration}}
                }
            }
            """,
            baseSourceFileName: "RestrictedAccessors.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(RestrictedAccessorTests)
        );

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }
}
