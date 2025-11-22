using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public partial class PropertyNameCollisionPreventionTests
    : PropertyNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenImposterMemberNamedProperties_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IPropertyImposterMemberCollisionTargetImposter();
            var instance = ((IHaveImposterInstance<IPropertyImposterMemberCollisionTarget>)imposter).Instance();
            _ = instance;

            imposter.Instance.Getter().Returns(new object());
            imposter.ImposterTargetInstance.Getter().Returns(new object());
            imposter._imposterInstance.Getter().Returns(new object());
            imposter._invocationBehavior.Getter().Returns("custom");
            imposter.InitializeOutParametersWithDefaultValues.Getter().Returns(42);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
