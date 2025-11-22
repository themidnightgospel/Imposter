using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public partial class PropertyNameCollisionPreventionTests
    : PropertyNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenPropertiesMatchingGeneratedFields_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IPropertyBackingFieldCollisionTargetImposter();
            imposter._defaultPropertyBehaviour.Getter().Returns(1);
            imposter._MyProperty.Getter().Returns(2);
            imposter.Prop.Getter().Returns(3);
            imposter.Prop.Setter(Arg<int>.Any()).Callback(_ => { });
            imposter._Prop.Getter().Returns(4);
            imposter._Prop.Setter(Arg<int>.Any()).Callback(_ => { });
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
