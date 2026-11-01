using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue354;

public class ImposterTypeParameterNameTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Given_ParameterNamedLikeImposter_When_ImposterIsUsed_Should_KeepPublicTypeName(
        bool sameNamespace
    )
    {
        var name = sameNamespace
            ? "IServiceImposter"
            : "Imposters.Sample.IService_IServiceImposter_.IServiceImposter";
        await AssertCompiles(
            "Sample.IService<>",
            "public interface IService<IServiceImposter> { IServiceImposter Get(int count); }",
            $$"""
            var imposter = new {{name}}<string>();
            imposter.Get(Arg<int>.Any()).Returns("value");
            IService<string> instance = imposter.Instance();
            _ = instance.Get(1);
            """,
            sameNamespace
        );
    }

    [Fact]
    public async Task Given_RenamedParameterUsedInMembersAndConstraints_When_ImposterIsUsed_Should_Compile()
    {
        await AssertCompiles(
            "Sample.IService<,>",
            """
            #nullable enable
            public interface IService<IServiceImposter, U>
                where IServiceImposter : class
                where U : IServiceImposter
            {
                IServiceImposter? Current { get; set; }
                U this[IServiceImposter key] { get; }
                event System.Action<IServiceImposter> Changed;
                V Convert<V>(IServiceImposter value) where V : IServiceImposter;
            }
            """,
            """
            var imposter = new IServiceImposter<object, string>();
            imposter.Current.Getter().Returns(new object());
            imposter[new object()].Getter().Returns("value");
            imposter.Changed.Raise(new object());
            imposter.Convert<string>(Arg<object>.Any()).Returns("result");
            _ = imposter.Instance().Convert<string>(new object());
            """
        );
    }

    [Fact]
    public async Task Given_ReplacementNamesAlreadyUsed_When_ImposterIsGenerated_Should_SkipThem()
    {
        await AssertCompiles(
            "Sample.IService<,>",
            """
            public interface IService<IServiceImposter, IServiceImposter_1>
            {
                IServiceImposter Get(IServiceImposter_1 value);
                int IServiceImposter_2 { get; }
                U Convert<U>(IServiceImposter value, int IServiceImposter_3);
                IServiceImposter_4 Make<IServiceImposter_4>();
            }
            """,
            """
            var imposter = new IServiceImposter<string, int>();
            imposter.Get(Arg<int>.Any()).Returns("value");
            imposter.Convert<bool>(Arg<string>.Any(), Arg<int>.Any()).Returns(true);
            imposter.Make<int>().Returns(1);
            imposter.IServiceImposter_2.Getter().Returns(2);
            _ = imposter.Instance().Get(1);
            """
        );
    }

    [Fact]
    public async Task Given_ClassParameterNamedLikeImposter_When_ConstructorAndBaseMembersAreUsed_Should_Compile()
    {
        await AssertCompiles(
            "Sample.Service<>",
            """
            public class Service<ServiceImposter> where ServiceImposter : class
            {
                public Service(ServiceImposter seed, int ServiceImposter_1) { }
                public virtual ServiceImposter Get(ServiceImposter value) => value;
                public virtual U Convert<U>(ServiceImposter value) where U : ServiceImposter => default!;
            }
            """,
            """
            var imposter = new ServiceImposter<object>(new object(), 1);
            imposter.Get(Arg<object>.Any()).UseBaseImplementation();
            imposter.Convert<string>(Arg<object>.Any()).UseBaseImplementation();
            _ = imposter.Instance().Get(new object());
            """
        );
    }

    [Fact]
    public async Task Given_InheritedMembersUsingRenamedParameter_When_SetupViewsAreUsed_Should_Compile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            """
            public interface IFirst<T> { T Get(T value); }
            public interface ISecond<T> { T Get(T value); }
            public interface IService<IServiceImposter> : IFirst<IServiceImposter>, ISecond<IServiceImposter> { }
            """,
            """
            var imposter = new IServiceImposter<string>();
            imposter.For(default(IFirst<string>)).Get(Arg<string>.Any()).Returns("first");
            imposter.For(default(ISecond<string>)).Get(Arg<string>.Any()).Returns("second");
            _ = ((IFirst<string>)imposter.Instance()).Get("value");
            _ = ((ISecond<string>)imposter.Instance()).Get("value");
            """
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Given_NewSuffixCollision_When_GeneratorRuns_Should_KeepHintName(
        bool sameNamespace
    )
    {
        var original = await Context("", sameNamespace);
        var changed = await Context("int IServiceImposter_1 { get; }", sameNamespace);
        changed
            .RunGenerator()
            .GeneratedSources.ShouldHaveSingleItem()
            .HintName.ShouldBe(
                original.RunGenerator().GeneratedSources.ShouldHaveSingleItem().HintName
            );

        static Task<GeneratorTestContext> Context(string member, bool sameNamespace) =>
            CreateContext(
                "Sample.IService<>",
                $$"""public interface IService<IServiceImposter> { IServiceImposter Get(); {{member}} }""",
                sameNamespace
            );
    }

    [Theory]
    [InlineData("var", "var_1")]
    [InlineData("_", "__1")]
    [InlineData("record", "record_1")]
    [InlineData("partial", "partial_1")]
    public async Task Given_KeywordParameter_When_DedicatedNamespaceIsUsed_Should_KeepExistingName(
        string parameterName,
        string legacyName
    )
    {
        await AssertCompiles(
            "Sample.IService<>",
            $$"""public interface IService<@{{parameterName}}> { @{{parameterName}} Get(); }""",
            $$"""var imposter = new Imposters.Sample.IService_{{legacyName}}_.IServiceImposter<int>(); imposter.Get().Returns(1);""",
            sameNamespace: false
        );
    }

#if ROSLYN4_14_OR_GREATER
    [Fact]
    public async Task Given_RenamedParameter_When_ImposterExtensionIsUsed_Should_Compile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            "public interface IService<IServiceImposter> { IServiceImposter Get(); }",
            "IServiceImposter<string> imposter = IService<string>.Imposter(); imposter.Get().Returns(\"value\");",
            languageVersion: LanguageVersion.Preview
        );
    }
#endif

    private static async Task AssertCompiles(
        string target,
        string declarations,
        string usage,
        bool sameNamespace = true,
        LanguageVersion languageVersion = LanguageVersion.CSharp9
    )
    {
        var context = await CreateContext(target, declarations, sameNamespace, languageVersion);
        context.RunGenerator().Diagnostics.ShouldBeEmpty();
        context
            .CompileSnippet(
                $$"""
                using Imposter.Abstractions;
                namespace Sample
                {
                    public static class Usage
                    {
                        public static void Run() { {{usage}} }
                    }
                }
                """,
                DiagnosticSeverity.Warning
            )
            .ShouldBeEmpty();
    }

    private static Task<GeneratorTestContext> CreateContext(
        string target,
        string declarations,
        bool sameNamespace,
        LanguageVersion languageVersion = LanguageVersion.CSharp9
    ) =>
        GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof({{target}}), {{(sameNamespace ? "true" : "false")}})]
            namespace Sample
            {
                {{declarations}}
            }
            """,
            baseSourceFileName: "ImposterName.cs",
            snippetFileName: "Usage.cs",
            assemblyName: nameof(ImposterTypeParameterNameTests),
            languageVersion: languageVersion
        );
}
