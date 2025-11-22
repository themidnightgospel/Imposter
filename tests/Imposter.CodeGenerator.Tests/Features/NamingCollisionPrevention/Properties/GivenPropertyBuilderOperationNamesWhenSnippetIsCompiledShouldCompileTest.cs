using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public partial class PropertyNameCollisionPreventionTests
    : PropertyNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenPropertyBuilderOperationNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var interfaceImposter = new IPropertyBuilderOperationCollisionTargetImposter();
            interfaceImposter.Returns.Getter().Returns(1).Then().Returns(() => 2);

            interfaceImposter.Throws.Getter().Throws<InvalidOperationException>();
            interfaceImposter.Throws.Getter().Throws(new InvalidOperationException());

            interfaceImposter.Callback.Getter().Callback(() => { });

            interfaceImposter.Then.Getter().Returns(5).Then();
            interfaceImposter.Then.Setter(Arg<int>.Any()).Callback(_ => { }).Then();

            interfaceImposter.Called.Getter().Called(Count.AtLeast(1));
            interfaceImposter.Called.Setter(Arg<int>.Any()).Called(Count.AtLeast(1));

            var classImposter = new PropertyBuilderOperationClassCollisionTargetImposter();
            classImposter.UseBaseImplementation.Getter().Returns(10).Then().UseBaseImplementation();
            classImposter.UseBaseImplementation.Setter(Arg<int>.Any()).Then().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
