using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public partial class PropertyUseBaseImplementationFluentApiTests
{
    [Fact]
    public async Task GivenOverrideableProperty_WhenChainingUseBaseImplementationOnGetter_ShouldCompile()
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
            imposter.Age.UseBaseImplementation().Getter().Then().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
