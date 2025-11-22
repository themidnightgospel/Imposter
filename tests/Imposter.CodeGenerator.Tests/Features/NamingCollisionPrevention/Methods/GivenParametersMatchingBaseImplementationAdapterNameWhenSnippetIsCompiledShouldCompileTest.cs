using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodParameterCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenParametersMatchingBaseImplementationAdapterName_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IMethodParameterBaseImplementationCollisionTargetImposter();
            var value = imposter.BaseImplementationName(baseImplementation: 42);
            _ = value;
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
