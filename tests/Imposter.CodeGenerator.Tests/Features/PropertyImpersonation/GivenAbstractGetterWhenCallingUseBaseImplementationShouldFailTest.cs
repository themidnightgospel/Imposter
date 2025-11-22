using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public partial class PropertyGetterFluentApiTests
{
    [Fact]
    public async Task GivenAbstractGetter_WhenCallingUseBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new AbstractGetterTargetImposter();
            imposter.Age.Getter().Then().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }
}
