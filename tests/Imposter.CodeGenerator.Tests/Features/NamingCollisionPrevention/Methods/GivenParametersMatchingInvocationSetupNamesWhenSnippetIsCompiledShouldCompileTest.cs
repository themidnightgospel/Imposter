using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodParameterCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenParametersMatchingInvocationSetupNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using System;
using Sample.NamingCollision;
using Imposter.Abstractions;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodParameterInvocationSetupCollisionTargetImposter();
            var value = imposter.SetupNames(
                value: 1,
                resultGenerator: Arg<Func<int>>.Any(), 
                exception: new InvalidOperationException(),
                exceptionGenerator: Arg<Func<Exception>>.Any(), 
                callback: Arg<Action>.Any());

            _ = value;
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
