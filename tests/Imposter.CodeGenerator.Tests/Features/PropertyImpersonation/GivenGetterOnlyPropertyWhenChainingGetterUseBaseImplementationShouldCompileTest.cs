using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public partial class PropertyUseBaseImplementationFluentApiTests
{
    [Fact]
    public async Task GivenGetterOnlyProperty_WhenChainingGetterUseBaseImplementation_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new GetterOnlyPropertyServiceImposter();
            imposter.Age.UseBaseImplementation().Getter().Then().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
