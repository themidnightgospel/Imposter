using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public partial class PropertySetterFluentApiTests
{
    [Fact]
    public async Task GivenSetter_WhenCallingReturnsValue_ShouldFail()
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
            imposter.Age.Setter(Imposter.Abstractions.Arg<int>.Any()).Returns(123);
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
