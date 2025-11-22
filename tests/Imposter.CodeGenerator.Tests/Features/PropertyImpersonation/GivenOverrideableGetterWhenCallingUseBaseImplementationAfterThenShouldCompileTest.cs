using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImpersonation;

public partial class PropertyGetterFluentApiTests
{
    [Fact]
    public async Task GivenOverrideableGetter_WhenCallingUseBaseImplementationAfterThen_ShouldCompile()
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
                        imposter.Age.Getter().Then().UseBaseImplementation();
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
