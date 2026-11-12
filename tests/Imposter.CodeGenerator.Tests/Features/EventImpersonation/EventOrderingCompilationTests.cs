using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

public class EventOrderingCompilationTests
{
    [Fact]
    public async Task GivenGenericHandlerTypeParameterNamedInvocationStep_WhenCreatingOrderHandles_ShouldNotHideItsType()
    {
        var context = await GeneratorTestHelper.CreateContext(
            """
            using System;
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.IEvents<>))]
            namespace Sample
            {
                public interface IEvents<InvocationStep>
                {
                    event Action<InvocationStep> @event;
                }
            }
            """,
            baseSourceFileName: "EventOrdering.cs",
            snippetFileName: "Scenario.cs",
            assemblyName: nameof(EventOrderingCompilationTests)
        );

        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                """
                using System;
                using Imposter.Abstractions;
                namespace Sample
                {
                    public static class Scenario
                    {
                        public static void Execute()
                        {
                            var imposter = new IEventsImposter<int>();
                            Action<int> handler = value => { };
                            Imposters.InOrder()
                                .Then(imposter.@event.Subscribed(handler))
                                .Then(imposter.@event.Unsubscribed(handler));
                        }
                    }
                }
                """
            )
        );
    }

    [Fact]
    public async Task GivenAsyncEventAndVerificationLane_WhenSelectingEventSteps_ShouldCompile()
    {
        var diagnostics = await EventImpersonationTestsShared.CompileSnippet(
            """
            using System;
            using System.Threading.Tasks;
            using Imposter.Abstractions;
            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IEventSutImposter();
                        var criteria = Arg<Func<object?, EventArgs, Task>>.Any();
                        var verification = imposter.AsyncSomethingHappened.Subscribed(criteria, Count.Never());
                        Imposters.InOrder().Then(verification.Subscribed(criteria))
                            .Then(verification.Unsubscribed(criteria));
                    }
                }
            }
            """
        );
        EventImpersonationTestsShared.AssertNoDiagnostics(diagnostics);
    }
}
