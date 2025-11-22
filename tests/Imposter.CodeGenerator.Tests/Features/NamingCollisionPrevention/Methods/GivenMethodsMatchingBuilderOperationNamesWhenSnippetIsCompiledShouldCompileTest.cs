using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodNameCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenMethodsMatchingBuilderOperationNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodOperationNameCollisionTargetImposter();
            _ = imposter.Returns();
            _ = imposter.ReturnsAsync();
            _ = imposter.Throws();
            _ = imposter.ThrowsAsync();
            _ = imposter.Callback();
            _ = imposter.Then();
            _ = imposter.UseBaseImplementation();
            _ = imposter.DefaultResultGenerator();
            _ = imposter.Default();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
