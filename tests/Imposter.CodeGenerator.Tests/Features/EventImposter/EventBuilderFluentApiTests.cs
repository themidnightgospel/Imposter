using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.EventImposter;

public class EventBuilderFluentApiTests
{
    private const string Source = /*lang=csharp*/"""
                                                 using System;
                                                 using System.Threading.Tasks;
                                                 using Imposter.Abstractions;

                                                 [assembly: GenerateImposter(typeof(Sample.IEventSut))]

                                                 namespace Sample
                                                 {
                                                     public interface IEventSut
                                                     {
                                                         event EventHandler SomethingHappened;

                                                         event Func<object?, EventArgs, Task>? AsyncSomethingHappened;
                                                     }
                                                 }
                                                 """;

    [Fact]
    public async Task GivenInitialBuilder_WhenCallingRaise_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened.Raise(new object(), System.EventArgs.Empty);
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenInitialBuilder_WhenCallingSubscribed_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened.Subscribed(global::Imposter.Abstractions.Arg<System.EventHandler>.Any(), global::Imposter.Abstractions.Count.Once());
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenInitialAsyncBuilder_WhenCallingRaiseAsync_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static async System.Threading.Tasks.Task ExecuteAsync()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       await imposter.AsyncSomethingHappened.RaiseAsync(new object(), System.EventArgs.Empty);
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenInitialAsyncBuilder_WhenCallingSubscribed_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.AsyncSomethingHappened.Subscribed(global::Imposter.Abstractions.Arg<System.Func<object?, System.EventArgs, System.Threading.Tasks.Task>>.Any(), global::Imposter.Abstractions.Count.Once());
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenSetupLane_WhenCallingVerification_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened.Raise(new object(), System.EventArgs.Empty).Subscribed(default!, default);
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenVerificationLane_WhenCallingSetup_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened.Subscribed(default!, default).Raise(new object(), System.EventArgs.Empty);
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenSetupLane_WhenStartingWithCallbackAndSwitchingToVerification_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened.Callback((sender, args) => { }).Subscribed(default!, default);
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenVerificationLane_WhenStartingWithRaisedAndSwitchingToSetup_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened.Raised(default!, default!, default).Callback((sender, args) => { });
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenAsyncSetupLane_WhenCallingVerification_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.AsyncSomethingHappened.Callback((sender, args) => System.Threading.Tasks.Task.CompletedTask).Subscribed(default!, default);
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenEventBuilder_WhenAccessingThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened.Then();
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenEventBuilder_WhenAccessingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened.Called(global::Imposter.Abstractions.Count.Once());
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenAsyncVerificationLane_WhenCallingSetup_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.AsyncSomethingHappened.Subscribed(default!, default).Callback((sender, args) => System.Threading.Tasks.Task.CompletedTask);
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenSetupLane_WhenChainingSetupMethods_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened
                                                           .Callback((sender, args) => { })
                                                           .OnSubscribe(handler => { })
                                                           .OnUnsubscribe(handler => { })
                                                           .Raise(new object(), System.EventArgs.Empty);
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenSetupLane_WhenContinuingAfterRaise_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened
                                                           .Raise(new object(), System.EventArgs.Empty)
                                                           .Callback((sender, args) => { })
                                                           .OnSubscribe(handler => { })
                                                           .Raise(new object(), System.EventArgs.Empty)
                                                           .OnUnsubscribe(handler => { })
                                                           .Raise(new object(), System.EventArgs.Empty);
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenSetupLane_WhenChainingMultipleCallbacks_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened
                                                           .Callback((sender, args) => { })
                                                           .Callback((sender, args) => { });
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenVerificationLane_WhenChainingVerificationMethods_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.SomethingHappened
                                                           .Subscribed(global::Imposter.Abstractions.Arg<System.EventHandler>.Any(), global::Imposter.Abstractions.Count.Exactly(1))
                                                           .Unsubscribed(global::Imposter.Abstractions.Arg<System.EventHandler>.Any(), global::Imposter.Abstractions.Count.AtLeast(0))
                                                           .Raised(global::Imposter.Abstractions.Arg<object?>.Any(), global::Imposter.Abstractions.Arg<System.EventArgs>.Any(), global::Imposter.Abstractions.Count.AtMost(2))
                                                           .HandlerInvoked(global::Imposter.Abstractions.Arg<System.EventHandler>.Any(), global::Imposter.Abstractions.Count.Exactly(1));
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenAsyncSetupLane_WhenAwaitingRaiseAsync_ShouldAllowFurtherSetup()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static async System.Threading.Tasks.Task ExecuteAsync()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       var builder = await imposter.AsyncSomethingHappened.RaiseAsync(new object(), System.EventArgs.Empty);
                                                       builder = await builder
                                                           .OnUnsubscribe(handler => { })
                                                           .Callback((sender, args) => System.Threading.Tasks.Task.CompletedTask)
                                                           .RaiseAsync(new object(), System.EventArgs.Empty);

                                                       builder.OnSubscribe(handler => { });
                                                       await builder.RaiseAsync(new object(), System.EventArgs.Empty);
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenAsyncSetupLane_WhenSwitchingToVerificationAfterAwait_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static async System.Threading.Tasks.Task ExecuteAsync()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       var builder = await imposter.AsyncSomethingHappened.RaiseAsync(new object(), System.EventArgs.Empty);
                                                       builder.Subscribed(default!, default);
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenAsyncSetupLane_WhenStartingWithCallbackAndContinuing_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.AsyncSomethingHappened
                                                           .Callback((sender, args) => System.Threading.Tasks.Task.CompletedTask)
                                                           .OnSubscribe(handler => { })
                                                           .OnUnsubscribe(handler => { });
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenAsyncSetupLane_WhenChainingMultipleCallbacks_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.AsyncSomethingHappened
                                                           .Callback((sender, args) => System.Threading.Tasks.Task.CompletedTask)
                                                           .Callback((sender, args) => System.Threading.Tasks.Task.CompletedTask);
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenAsyncVerificationLane_WhenStartingWithRaisedAndSwitchingToSetup_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.AsyncSomethingHappened.Raised(default!, default!, default).Callback((sender, args) => System.Threading.Tasks.Task.CompletedTask);
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenAsyncEventBuilder_WhenAccessingThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.AsyncSomethingHappened.Then();
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenAsyncEventBuilder_WhenAccessingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.AsyncSomethingHappened.Called(global::Imposter.Abstractions.Count.Once());
                                                   }
                                               }
                                           }
                                           """);

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }

    [Fact]
    public async Task GivenAsyncVerificationLane_WhenChainingVerificationMethods_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
                                           namespace Sample
                                           {
                                               public static class Scenario
                                               {
                                                   public static void Execute()
                                                   {
                                                       var imposter = new IEventSutImposter();
                                                       imposter.AsyncSomethingHappened
                                                           .Subscribed(global::Imposter.Abstractions.Arg<System.Func<object?, System.EventArgs, System.Threading.Tasks.Task>>.Any(), global::Imposter.Abstractions.Count.Exactly(1))
                                                           .Unsubscribed(global::Imposter.Abstractions.Arg<System.Func<object?, System.EventArgs, System.Threading.Tasks.Task>>.Any(), global::Imposter.Abstractions.Count.AtLeast(0))
                                                           .Raised(global::Imposter.Abstractions.Arg<object?>.Any(), global::Imposter.Abstractions.Arg<System.EventArgs>.Any(), global::Imposter.Abstractions.Count.AtMost(2))
                                                           .HandlerInvoked(global::Imposter.Abstractions.Arg<System.Func<object?, System.EventArgs, System.Threading.Tasks.Task>>.Any(), global::Imposter.Abstractions.Count.Exactly(1));
                                                   }
                                               }
                                           }
                                           """);

        AssertNoDiagnostics(diagnostics);
    }

    private const string BaseSourceFileName = "GeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(EventBuilderFluentApiTests));

    private static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }

    private static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);

    private static void AssertSingleDiagnostic(ImmutableArray<Diagnostic> diagnostics, string expectedId)
    {
        var diagnostic = Assert.Single(diagnostics);
        Assert.Equal(expectedId, diagnostic.Id);
    }
}


