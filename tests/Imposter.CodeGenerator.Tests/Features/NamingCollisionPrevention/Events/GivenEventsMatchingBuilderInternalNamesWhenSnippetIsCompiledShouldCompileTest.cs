using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventBuilderOperationCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenEventsMatchingBuilderInternalNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static async Task ExecuteAsync()
        {
            var imposter = new IEventBuilderInternalNameCollisionTargetImposter();
            var sender = new object();

            imposter.RaiseInternal.Raise(sender, EventArgs.Empty);

            var asyncBuilder = imposter.RaiseCoreAsync;
            await asyncBuilder.RaiseAsync(sender, EventArgs.Empty);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
