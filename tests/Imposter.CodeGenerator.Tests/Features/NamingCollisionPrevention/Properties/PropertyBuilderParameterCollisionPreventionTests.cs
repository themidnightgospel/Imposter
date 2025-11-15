using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public class PropertyBuilderParameterCollisionPreventionTests
    : PropertyNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenBuilderParameterNamedProperties_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using System;
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IPropertyBuilderParameterCollisionTargetImposter();

            var value = 1;
            Func<int> valueGenerator = () => 2;
            var exception = new InvalidOperationException();
            Action callback = () => { };

            imposter.value.Getter().Returns(value).Then().Returns(valueGenerator);
            imposter.valueGenerator.Getter().Returns(3);
            imposter.valueGenerator.Getter().Returns(valueGenerator);
            imposter.exception.Getter().Returns(4);
            imposter.exception.Getter().Throws(exception);
            imposter.callback.Getter().Callback(callback);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
