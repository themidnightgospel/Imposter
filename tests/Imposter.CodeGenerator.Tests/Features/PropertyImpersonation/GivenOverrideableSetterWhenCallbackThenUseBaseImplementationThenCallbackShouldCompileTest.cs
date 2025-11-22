using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public partial class PropertySetterFluentApiTests
{
    [Fact]
    public async Task GivenOverrideableSetter_WhenCallbackThenUseBaseImplementationThenCallback_ShouldCompile()
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
            imposter.Age
                .Setter(Imposter.Abstractions.Arg<int>.Is(v => v < 5))
                .Callback(value => { })
                .Then()
                .UseBaseImplementation()
                .Then()
                .Callback(value => { });
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
