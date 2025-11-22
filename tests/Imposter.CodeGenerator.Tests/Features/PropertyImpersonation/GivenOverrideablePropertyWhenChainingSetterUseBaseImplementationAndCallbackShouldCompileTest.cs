using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public partial class PropertyUseBaseImplementationFluentApiTests
{
    [Fact]
    public async Task GivenOverrideableProperty_WhenChainingSetterUseBaseImplementationAndCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new OverrideablePropertyServiceImposter();
            imposter.Age.UseBaseImplementation().Setter(Imposter.Abstractions.Arg<int>.Any()).Then().UseBaseImplementation().Then().Callback(value => { });
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
