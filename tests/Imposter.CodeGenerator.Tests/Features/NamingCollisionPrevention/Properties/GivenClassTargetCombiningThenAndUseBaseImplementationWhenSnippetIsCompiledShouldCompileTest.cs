using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public partial class PropertyClassTargetCollisionPreventionTests : PropertyNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenClassTargetCombiningThenAndUseBaseImplementation_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new PropertyUseBaseImplementationCombinedCollisionTargetImposter();
            imposter.Then.Getter().Returns(1).Then().UseBaseImplementation();
            imposter.Then.Setter(Arg<int>.Any()).Then().UseBaseImplementation();

            imposter.UseBaseImplementation.Getter().Returns(2).Then().UseBaseImplementation();
            imposter.UseBaseImplementation.Setter(Arg<int>.Any()).Then().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}

