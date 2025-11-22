using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public partial class PropertySetterFluentApiTests
{
    [Fact]
    public async Task GivenSetterCallback_WhenCallingThen_ShouldCompile()
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
            imposter.Age.Setter(Imposter.Abstractions.Arg<int>.Any()).Callback(value => { }).Then();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
