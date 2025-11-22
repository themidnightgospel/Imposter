using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodParameterCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenParametersMatchingInvocationHistoryNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IMethodParameterHistoryCollisionTargetImposter();
            imposter.HistoryNames(Arg<int>.Any(), OutArg<int>.Any(), OutArg<Exception>.Any());
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
