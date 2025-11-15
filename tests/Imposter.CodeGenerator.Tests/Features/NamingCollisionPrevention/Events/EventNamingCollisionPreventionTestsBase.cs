using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public abstract class EventNamingCollisionPreventionTestsBase
{
    internal const string Source = /*lang=csharp*/
        """
        using System;
        using System.Threading.Tasks;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IEventBuilderOperationCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IEventBuilderInternalNameCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IEventBuilderInterfaceCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IEventImposterMemberCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IEventBuilderFieldCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.EventBuilderParameterCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.EventBaseImplementationCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IAsyncRaiseAsyncEventCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.INonAsyncRaiseAsyncEventCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IEventDuplicateBaseCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IEventDuplicateChildCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IEventOperationClusterCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.ICrossMemberEventCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IEventTypeNameCollisionTarget))]
        [assembly: GenerateImposter(typeof(Sample.NamingCollision.IEventCaseSensitivityCollisionTarget))]

        namespace Sample.NamingCollision
        {
            public interface IEventBuilderOperationCollisionTarget
            {
                event EventHandler Raise;
                event Func<object?, EventArgs, Task> RaiseAsync;
                event EventHandler Subscribe;
                event EventHandler Unsubscribe;
                event EventHandler Callback;
                event EventHandler OnSubscribe;
                event EventHandler OnUnsubscribe;
                event EventHandler Subscribed;
                event EventHandler Unsubscribed;
                event EventHandler HandlerInvoked;
                event EventHandler CountMatches;
                event EventHandler EnsureCountMatches;
                event EventHandler Raised;
            }

            public interface IEventBuilderInternalNameCollisionTarget
            {
                event EventHandler RaiseInternal;
                event Func<object?, EventArgs, Task> RaiseCoreAsync;
                event EventHandler EnumerateActiveHandlers;
            }

            public interface IEventBuilderInterfaceCollisionTarget
            {
                event EventHandler IUniqueEventImposterBuilder;
                event EventHandler IUniqueEventImposterSetupBuilder;
                event EventHandler IUniqueEventImposterVerificationBuilder;
            }

            public interface IEventImposterMemberCollisionTarget
            {
                event EventHandler Instance;
                event EventHandler ImposterTargetInstance;
                event EventHandler _imposterInstance;
                event EventHandler _invocationBehavior;
                event EventHandler Default;
                event EventHandler Count;
            }

            public interface IEventBuilderFieldCollisionTarget
            {
                event EventHandler _MyEvent;
                event EventHandler _handlerOrder;
                event EventHandler _handlerCounts;
                event EventHandler _handlerInvocations;
            }

            public class EventBuilderParameterCollisionTarget
            {
                public virtual event EventHandler? handler;
                public virtual event EventHandler? interceptor;
                public virtual event EventHandler? criteria;
                public virtual event EventHandler? handlerCriteria;
                public virtual event EventHandler? count;
                public virtual event EventHandler? callback;
                public virtual event EventHandler? baseImplementation;
            }

            public class EventBaseImplementationCollisionTarget
            {
                public virtual event EventHandler? UseBaseImplementation;
                public virtual event EventHandler? Subscribe;
                public virtual event EventHandler? Unsubscribe;
                public virtual event EventHandler? Then;
            }

            public interface IAsyncRaiseAsyncEventCollisionTarget
            {
                event Func<object?, EventArgs, Task> RaiseAsync;
            }

            public interface INonAsyncRaiseAsyncEventCollisionTarget
            {
                event EventHandler RaiseAsync;
            }

            public interface IEventDuplicateBaseCollisionTarget
            {
                event EventHandler Raise;
            }

            public interface IEventDuplicateChildCollisionTarget : IEventDuplicateBaseCollisionTarget
            {
                new event EventHandler Raise;
            }

            public interface IEventOperationClusterCollisionTarget
            {
                event EventHandler Raise;
                event EventHandler Subscribe;
                event EventHandler Callback;
                event EventHandler Raised;
            }

            public interface ICrossMemberEventCollisionTarget
            {
                event EventHandler Instance;
                event EventHandler IChaosEventImposterBuilder;
                event EventHandler IChaosEventImposterSetupBuilder;
                event EventHandler IChaosEventImposterVerificationBuilder;
                event EventHandler Raise;
                event EventHandler Subscribe;
                event EventHandler _imposterInstance;
                event EventHandler _invocationBehavior;
                event EventHandler _handlerOrder;
                event EventHandler Default;
                event EventHandler Count;
                event EventHandler HandlerInvoked;
                event Func<object?, EventArgs, Task> RaiseAsync;
            }

            public interface IEventTypeNameCollisionTarget
            {
                event EventHandler Imposter;
                event EventHandler CodeGenerator;
                event EventHandler Sample;
                event EventHandler NamingCollision;
            }

            public interface IEventCaseSensitivityCollisionTarget
            {
                event EventHandler raise;
                event EventHandler Raise;
            }
        }
        """;

    private const string BaseSourceFileName = "EventNamingCollisionGeneratorInput.cs";
    private const string SnippetFileName = "EventNamingCollisionSnippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(EventNamingCollisionPreventionTestsBase)
        );

    protected static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }

    protected static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
}
