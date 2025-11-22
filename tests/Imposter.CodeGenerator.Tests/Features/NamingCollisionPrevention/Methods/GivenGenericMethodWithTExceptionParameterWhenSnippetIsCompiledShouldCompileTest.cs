using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodGenericParameterCollisionPreventionTests
    : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenGenericMethodWithTExceptionParameter_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IMethodGenericExceptionTypeParameterCollisionTargetImposter();
            imposter.ThrowsGeneric<System.InvalidOperationException>();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
