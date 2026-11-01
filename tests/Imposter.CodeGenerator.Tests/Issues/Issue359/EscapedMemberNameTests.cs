using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue359;

public class EscapedMemberNameTests
{
    [Theory]
    [InlineData("class")]
    [InlineData("int")]
    [InlineData("default")]
    [InlineData("object")]
    [InlineData("string")]
    [InlineData("event")]
    [InlineData("delegate")]
    [InlineData("new")]
    [InlineData("this")]
    [InlineData("base")]
    [InlineData("var")]
    [InlineData("async")]
    [InlineData("await")]
    public async Task Given_KeywordMembers_When_SetupsAndInstancesAreUsed_Should_Compile(
        string name
    )
    {
        await AssertCompiles(
            """
            [assembly: GenerateImposter(typeof(Sample.IMethods))]
            [assembly: GenerateImposter(typeof(Sample.IProperties))]
            [assembly: GenerateImposter(typeof(Sample.IEvents))]
            """,
            $$"""
            public interface IMethods
            {
                int @{{name}}();
                int @{{name}}(int @class);
                string @{{name}}(string @string);
                T @{{name}}<T>(T value);
            }
            public interface IProperties { int @{{name}} { get; set; } }
            public interface IEvents { event System.Action @{{name}}; }
            """,
            $$"""
            var methods = new IMethodsImposter();
            methods.@{{name}}().Returns(1);
            methods.@{{name}}(Arg<int>.Any()).Returns(2);
            methods.@{{name}}(Arg<string>.Any()).Returns("value");
            methods.@{{name}}<bool>(Arg<bool>.Any()).Returns(true);
            methods.For(default(IMethods)).@{{name}}().Returns(3);
            methods.For(default(IMethods)).@{{name}}<bool>(Arg<bool>.Any()).Returns(false);
            IMethods instance = methods.Instance();
            _ = instance.@{{name}}();
            _ = instance.@{{name}}(1);
            _ = instance.@{{name}}("value");
            _ = instance.@{{name}}<bool>(true);

            var properties = new IPropertiesImposter();
            properties.@{{name}}.Getter().Returns(4);
            properties.For(default(IProperties)).@{{name}}.Setter(Arg<int>.Any()).Callback(value => { });
            properties.Instance().@{{name}} = 5;
            _ = properties.Instance().@{{name}};

            var events = new IEventsImposter();
            System.Action handler = () => { };
            events.Instance().@{{name}} += handler;
            events.@{{name}}.Raise();
            events.For(default(IEvents)).@{{name}}.Raise();
            events.Instance().@{{name}} -= handler;
            """
        );
    }

    [Fact]
    public async Task Given_ClassKeywordMembers_When_BaseImplementationsAreUsed_Should_Compile()
    {
        await AssertCompiles(
            "[assembly: GenerateImposter(typeof(Sample.Service))]",
            """
            public class Service
            {
                public Service() { @new(); @base<int>(); @this = @this; @object = 1; @event += () => { }; }
                public virtual int @new() => 1;
                public virtual T @base<T>() => default(T);
                public virtual int @this { get; set; }
                public virtual int @object { get; init; }
                public virtual event System.Action @event { add { } remove { } }
            }
            """,
            """
            var imposter = new ServiceImposter();
            imposter.@new().UseBaseImplementation();
            imposter.@base<int>().UseBaseImplementation();
            imposter.@this.Getter().UseBaseImplementation();
            imposter.@this.Setter(Arg<int>.Any()).UseBaseImplementation();
            imposter.@object.Getter().UseBaseImplementation();
            imposter.@object.Setter(Arg<int>.Any()).UseBaseImplementation();
            _ = imposter.Instance().@new();
            _ = imposter.Instance().@base<int>();
            imposter.Instance().@this = 2;
            _ = imposter.Instance().@this;
            System.Action handler = () => { };
            imposter.Instance().@event += handler;
            imposter.@event.Raise();
            imposter.Instance().@event -= handler;
            """
        );
    }

    [Fact]
    public async Task Given_ConflictingInterfaceKeywordMembers_When_SetupViewsAreUsed_Should_Compile()
    {
        await AssertCompiles(
            "[assembly: GenerateImposter(typeof(Sample.IService))]",
            """
            public interface IFirst
            {
                int @class(int x);
                T @default<T>(T value);
                int @object { get; }
                event System.Action @event;
            }
            public interface ISecond
            {
                string @class(int x);
                T @default<T>(T value);
                string @object { get; set; }
                event System.Func<System.Threading.Tasks.Task> @event;
            }
            public interface IService : IFirst, ISecond { }
            """,
            """
            var imposter = new IServiceImposter();
            imposter.For(default(IFirst)).@class(Arg<int>.Any()).Returns(1);
            imposter.For(default(ISecond)).@class(Arg<int>.Any()).Returns("value");
            imposter.For(default(IFirst)).@default<int>(Arg<int>.Any()).Returns(2);
            imposter.For(default(ISecond)).@default<string>(Arg<string>.Any()).Returns("other");
            imposter.For(default(IFirst)).@object.Getter().Returns(3);
            imposter.For(default(ISecond)).@object.Getter().Returns("property");
            imposter.For(default(ISecond)).@object.Setter(Arg<string>.Any()).Callback(value => { });
            IFirst first = imposter.Instance();
            ISecond second = imposter.Instance();
            first.@event += () => { };
            second.@event += () => System.Threading.Tasks.Task.CompletedTask;
            imposter.For(default(IFirst)).@event.Raise();
            _ = imposter.For(default(ISecond)).@event.RaiseAsync();
            _ = first.@class(1);
            _ = second.@class(1);
            """
        );
    }

    [Fact]
    public async Task Given_WriteOnlyKeywordProperty_When_SetupAndInstanceAreUsed_Should_Compile()
    {
        await AssertCompiles(
            "[assembly: GenerateImposter(typeof(Sample.IService))]",
            "public interface IService { int @int { set; } }",
            """
            var imposter = new IServiceImposter();
            imposter.@int.Setter(Arg<int>.Any()).Callback(value => { });
            imposter.For(default(IService)).@int.Setter(Arg<int>.Any()).Called(Count.Never());
            imposter.Instance().@int = 1;
            """
        );
    }

    private static async Task AssertCompiles(
        string registrations,
        string declarations,
        string usage
    )
    {
        var context = await GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;
            {{registrations}}
            namespace Sample
            {
                {{declarations}}
            }
            """,
            baseSourceFileName: "EscapedMembers.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(EscapedMemberNameTests),
            languageVersion: LanguageVersion.CSharp9
        );

        context.RunGenerator().Diagnostics.ShouldBeEmpty();
        context
            .CompileSnippet(
                $$"""
                using Imposter.Abstractions;
                namespace Sample
                {
                    public static class Usage
                    {
                        public static void Execute()
                        {
                            {{usage}}
                        }
                    }
                }
                """,
                DiagnosticSeverity.Warning
            )
            .ShouldBeEmpty();
    }
}
