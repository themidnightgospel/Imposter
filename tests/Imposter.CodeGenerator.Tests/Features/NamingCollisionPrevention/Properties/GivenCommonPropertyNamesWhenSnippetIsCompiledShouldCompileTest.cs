using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public partial class PropertyNameCollisionPreventionTests
    : PropertyNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenCommonPropertyNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IPropertyCommonNameCollisionTargetImposter();
            imposter.Count.Getter().Returns(1);
            imposter.Count.Getter().Called(Count.AtLeast(1));
            imposter.Count.Setter(Arg<int>.Any()).Callback(_ => { });
            imposter.Default.Getter().Returns(2).Then().Returns(() => 3);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
