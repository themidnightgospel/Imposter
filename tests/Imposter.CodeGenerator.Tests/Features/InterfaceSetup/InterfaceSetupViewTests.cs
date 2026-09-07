using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.InterfaceSetup;

public class InterfaceSetupViewTests
{
    private const string Source = /*lang=csharp*/
        """
        using System;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IChild))]
        [assembly: GenerateImposter(typeof(Sample.IGeneric<>))]
        [assembly: GenerateImposter(typeof(Sample.ICollisions<>))]
        [assembly: GenerateImposter(typeof(Sample.IBoth))]

        namespace Left { public interface IContract { int Value { get; } } }
        namespace Right { public interface IContract { string Value { get; } } }

        namespace Sample
        {
            public interface IBoth : Left.IContract, Right.IContract { }

            public interface IBase
            {
                void Execute(int value);
                int NullableHidden(string value);
                event Action Changed;
                int this[int key] { get; }
            }

            public interface IChild : IBase
            {
                void Execute(string value);
                new int NullableHidden(string? value);
                new event Action Changed;
                string this[string key] { get; }
                T Create<T>() where T : class, IDisposable, new();
                T? Echo<T>(T? value) where T : class;
                T? Value<T>(T? value) where T : struct;
                T Unmanaged<T>(T value) where T : unmanaged;
                T? Nullable<T>(T? value);
                T? NullableReference<T>(T? value) where T : class?;
            }

            public interface IGeneric<T> where T : class
            {
                T Echo(T value);
                TResult Convert<TResult>(T value) where TResult : T;
            }

            public interface ICollisions<For>
            {
                void ICollisionsSetup();
                int Value { get; }
                void Accept(For value);
            }
        }
        """;

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "InterfaceSetup.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(InterfaceSetupViewTests)
        );

    [Fact]
    public async Task Given_HiddenAndOverloadedMembers_When_GeneratingViews_Should_AvoidHidingWarnings()
    {
        var context = await TestContextTask.ConfigureAwait(false);
        var compilation = context.Compilation.AddSyntaxTrees(
            context
                .RunGenerator()
                .GeneratedSources.Select(source =>
                    CSharpSyntaxTree.ParseText(
                        source.SourceText,
                        new CSharpParseOptions(LanguageVersion.CSharp9),
                        path: source.HintName
                    )
                )
        );
        var diagnostics = compilation
            .GetDiagnostics()
            .Where(diagnostic =>
                diagnostic.Severity == DiagnosticSeverity.Error
                || diagnostic.Id is "CS0108" or "CS0109"
            )
            .ToImmutableArray();

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_InheritedOverloads_When_SelectingViews_Should_Compile()
    {
        var context = await TestContextTask.ConfigureAwait(false);
        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IChildImposter();
                        var child = imposter.For(default(IChild));
                        child.Execute("value");
                        child.Execute(42);
                        child["key"].Getter().Returns("value");
                        child[42].Getter().Returns(42);
                        imposter.For(default(IBase)).Changed.Raise();
                        child.Changed.Raise();
                    }
                }
            }
            """
        );
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_GenericConstraints_When_SelectingViews_Should_Compile()
    {
        var context = await TestContextTask.ConfigureAwait(false);
        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var view = new IChildImposter().For(default(IChild));
                        view.Create<System.IO.MemoryStream>().Returns(new System.IO.MemoryStream());
                        view.Echo<string>((string?)null).Returns((string?)null);
                        view.Value<int>((int?)null).Returns((int?)null);
                        view.Unmanaged<int>(42).Returns(42);
                        view.Nullable<string>((string?)null);
                        view.NullableReference<string?>((string?)null);
                        var generic = new IGenericImposter<object>().For(default(IGeneric<object>));
                        generic.Echo(new object());
                        generic.Convert<string>(new object()).Returns("value");
                    }
                }
            }
            """
        );
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_SameInterfaceNames_When_SelectingViews_Should_ResolveByType()
    {
        var context = await TestContextTask.ConfigureAwait(false);
        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IBothImposter();
                        imposter.For(default(Left.IContract)).Value.Getter().Returns(42);
                        imposter.For(default(Right.IContract)).Value.Getter().Returns("value");
                    }
                }
            }
            """
        );
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_ReservedNames_When_SelectingViews_Should_PreserveExistingMembers()
    {
        var context = await TestContextTask.ConfigureAwait(false);
        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new ICollisionsImposter<int>();
                        var view = imposter.For_1(default(ICollisions<int>));
                        view.ICollisionsSetup();
                        view.Value.Getter().Returns(42);
                        view.Accept(42);
                    }
                }
            }
            """
        );
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }
}
