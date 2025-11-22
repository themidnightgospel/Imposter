using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventBaseImplementationCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenRaiseAsyncEventsWithConflictingNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using System;
using System.Threading.Tasks;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static async Task ExecuteAsync()
        {
            var asyncImposter = new IAsyncRaiseAsyncEventCollisionTargetImposter();
            var sender = new object();

            await asyncImposter.RaiseAsync.RaiseAsync(sender, EventArgs.Empty);

            var syncImposter = new INonAsyncRaiseAsyncEventCollisionTargetImposter();
            syncImposter.RaiseAsync.Raise(sender, EventArgs.Empty);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
