using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ThrowingFailuresFluentApiTests
{
    [Fact]
    public async Task GivenMethodThrowsGeneric_WhenUsingNonExceptionType_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IMethodInterfaceImposter();
                        imposter.GetNumber().Throws<int>();
                    }
                }
            }
            """
        );

        AssertSingleDiagnostic(diagnostics, "CS0315", expectedLine: 8);
    }
}
