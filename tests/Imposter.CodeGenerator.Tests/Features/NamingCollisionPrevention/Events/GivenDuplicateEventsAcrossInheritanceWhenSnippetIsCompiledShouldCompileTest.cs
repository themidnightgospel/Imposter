using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventBaseImplementationCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenDuplicateEventsAcrossInheritance_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using System;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var sender = new object();
            var args = EventArgs.Empty;

            var child = new IEventDuplicateChildCollisionTargetImposter();
            child.Raise.Raise(sender, args);

            var parent = new IEventDuplicateBaseCollisionTargetImposter();
            parent.Raise.Raise(sender, args);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
