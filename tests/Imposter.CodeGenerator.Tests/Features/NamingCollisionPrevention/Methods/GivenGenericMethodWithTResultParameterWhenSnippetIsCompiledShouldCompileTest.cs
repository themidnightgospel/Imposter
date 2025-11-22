using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodGenericParameterCollisionPreventionTests
    : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenGenericMethodWithTResultParameter_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using System;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodGenericResultTypeParameterCollisionTargetImposter();
            Func<int> defaultResultGenerator = () => 10;
            var valueSetup = imposter.ReturnsGeneric<int>(5);
            var otherSetup = imposter.ReturnsWithGenerator<int>(defaultResultGenerator);
            valueSetup.Returns(5);
            otherSetup.Returns(10);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
