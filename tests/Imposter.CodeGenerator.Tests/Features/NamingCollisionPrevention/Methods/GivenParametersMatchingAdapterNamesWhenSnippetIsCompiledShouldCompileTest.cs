using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodParameterCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenParametersMatchingAdapterNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IMethodParameterAdapterCollisionTargetImposter();
            var value = imposter.AdapterNames(result: 1, arguments: "label", target: new object(), _target: new object());
            _ = value;
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
