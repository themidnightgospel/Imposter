using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public partial class PropertyNameCollisionPreventionTests
    : PropertyNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenReusedPropertyNamesAcrossAccessors_WhenSnippetIsCompiled_ShouldCompile()
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
            var getterOnly = new IPropertyDuplicateAccessorCollisionTargetImposter();
            getterOnly.Reused.Getter().Returns(1);

            var getterSetter = new IPropertyDuplicateAccessorWithSetterCollisionTargetImposter();
            getterSetter.Reused.Getter().Returns(2);
            getterSetter.Reused.Setter(Arg<int>.Any()).Callback(_ => { });
            getterSetter.Reused.Setter(Arg<int>.Any()).Called(Count.AtLeast(1));
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
