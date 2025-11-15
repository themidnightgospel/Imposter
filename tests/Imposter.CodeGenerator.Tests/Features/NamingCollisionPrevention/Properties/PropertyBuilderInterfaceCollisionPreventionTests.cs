using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public class PropertyBuilderInterfaceCollisionPreventionTests
    : PropertyNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenGetterBuilderInterfaceNameCollisions_WhenSnippetIsCompiled_ShouldCompile()
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
            var interfaceImposter = new IPropertyGetterBuilderNameCollisionTargetImposter();
            interfaceImposter.IWeirdPropertyGetterBuilder.Getter().Returns(1);
            interfaceImposter.IWeirdPropertyGetterOutcomeBuilder.Getter().Returns(2);
            interfaceImposter.IWeirdPropertyGetterContinuationBuilder.Getter().Returns(3).Then();
            interfaceImposter.IWeirdPropertyGetterCallbackBuilder.Getter().Returns(4).Callback(() => { });
            interfaceImposter.IWeirdPropertyGetterVerifier.Getter().Returns(5);
            interfaceImposter.IWeirdPropertyGetterVerifier.Getter().Called(Count.AtLeast(1));
            interfaceImposter.IWeirdPropertyGetterFluentBuilder.Getter().Returns(6).Then().Returns(() => 7);

            var classImposter = new PropertyGetterBuilderClassCollisionTargetImposter();
            classImposter.IWeirdPropertyGetterUseBaseImplementationBuilder.Getter().Returns(8).Then().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenSetterBuilderInterfaceNameCollisions_WhenSnippetIsCompiled_ShouldCompile()
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
            var interfaceImposter = new IPropertySetterBuilderNameCollisionTargetImposter();
            interfaceImposter.IWeirdPropertySetterBuilder.Setter(Arg<int>.Any()).Callback(_ => { });
            interfaceImposter.IWeirdPropertySetterBuilder.Setter(Arg<int>.Any()).Called(Count.AtLeast(0));
            interfaceImposter.IWeirdPropertySetterFluentBuilder.Setter(Arg<int>.Any()).Callback(_ => { });
            interfaceImposter.IWeirdPropertySetterCallbackBuilder.Setter(Arg<int>.Any()).Callback(_ => { });
            interfaceImposter.IWeirdPropertySetterContinuationBuilder.Setter(Arg<int>.Any()).Callback(_ => { }).Then();
            interfaceImposter.IWeirdPropertySetterVerifier.Setter(Arg<int>.Any()).Called(Count.AtLeast(1));

            var classImposter = new PropertySetterBuilderClassCollisionTargetImposter();
            classImposter.IWeirdPropertySetterUseBaseImplementationBuilder.Setter(Arg<int>.Any()).Then().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
