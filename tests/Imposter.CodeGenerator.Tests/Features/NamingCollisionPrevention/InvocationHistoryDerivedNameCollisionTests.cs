using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

public class InvocationHistoryDerivedNameCollisionTests
{
    [Fact]
    public async Task GivenTypeParameterMatchingSnapshotOrderingControl_WhenGenerating_ShouldKeepNamesDistinct()
    {
        var source = """
            using System;
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.IHistory<>))]
            namespace Sample
            {
                public interface IHistory<orderBySequence>
                {
                    void Call();
                    int Value { get; set; }
                    int this[int key] { get; set; }
                    event Action Changed;
                }
            }
            """;
        var context = await GeneratorTestHelper.CreateContext(
            source,
            "Target.cs",
            "Snippet.cs",
            nameof(InvocationHistoryDerivedNameCollisionTests)
        );
        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }

    [Theory]
    [InlineData("int")]
    [InlineData("System.ReadOnlySpan<int>")]
    public async Task GivenCapturedFieldMatchingAnAdaptedTypeParameter_WhenGenerating_ShouldKeepNamesDistinct(
        string parameterType
    )
    {
        // These source names independently reproduce the collision between capturedItemsTarget
        // and the generator's adapted alias for capturedItems; they do not mirror a reserved list.
        var source = $$"""
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.ICollision))]
            namespace Sample
            {
                public interface ICollision
                {
                    bool Contains<capturedItems>({{parameterType}} ItemsTarget, capturedItems item);
                }
            }
            """;
        var context = await GeneratorTestHelper.CreateContext(
            source,
            "Target.cs",
            "Snippet.cs",
            nameof(InvocationHistoryDerivedNameCollisionTests)
        );
        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }
}
