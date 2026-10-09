using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventBuilderOperationCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenEventDelegateParametersMatchingRaiseLocals_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using System.Threading.Tasks;
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static async Task ExecuteAsync()
        {
            var imposter = new IEventRaiseLocalCollisionTargetImposter();
            var instance = imposter.Instance();
            instance.Happened += (callback, handler) => { };
            instance.HappenedAsync += (task, pendingTasks, callback, handler) => Task.CompletedTask;
            instance.HappenedValueTask += (task, pendingTasks, callback, handler) => default;

            imposter.Happened.Raise(1, 2);
            await imposter.HappenedAsync.RaiseAsync(1, 2, 3, 4);
            await imposter.HappenedValueTask.RaiseAsync(1, 2, 3, 4);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
