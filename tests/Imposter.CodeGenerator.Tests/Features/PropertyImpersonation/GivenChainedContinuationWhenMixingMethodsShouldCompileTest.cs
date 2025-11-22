using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public partial class PropertyGetterFluentApiTests
{
    [Fact]
    public async Task GivenChainedContinuation_WhenMixingMethods_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Then().Callback(() => { }).Then().Returns(() => 2);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
