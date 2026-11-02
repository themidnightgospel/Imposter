using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue337;

public class NestedGenericTargetTests
{
    [Fact]
    public async Task GivenTargetNestedInOpenGenericType_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner",
            "public class Outer<T> { public interface Inner { T Get(); } }",
            """
            public static Outer<int>.Inner Create()
            {
                var imposter = new InnerImposter<int>();
                imposter.Get().Returns(42);
                return imposter.Instance();
            }
            """
        );
    }

    [Fact]
    public async Task GivenSeveralGenericContainers_WhenImposterIsUsed_ShouldPreserveParameterOrderAndConstraints()
    {
        await AssertCompiles(
            "Sample.Outer<>.Middle<>.Inner<>",
            """
            public class Outer<T> where T : class, new()
            {
                public class Middle<U> where U : T, new()
                {
                    public interface Inner<V> where V : struct
                    {
                        T Get(U value, V key);
                        U Current { get; set; }
                        V this[T key] { get; set; }
                        event System.Action<T, U, V> Changed;
                    }
                }
            }
            """,
            """
            public static Outer<object>.Middle<System.Text.StringBuilder>.Inner<int> Create()
            {
                var imposter = new InnerImposter<object, System.Text.StringBuilder, int>();
                imposter.Get(new System.Text.StringBuilder(), 1).Returns(new object());
                imposter.Current.Getter().Returns(new System.Text.StringBuilder());
                imposter[new object()].Getter().Returns(2);
                imposter.Changed.Raise(new object(), new System.Text.StringBuilder(), 3);
                return imposter.Instance();
            }
            """
        );
    }

    [Fact]
    public async Task GivenClassNestedInGenericType_WhenConstructorAndBaseMethodAreUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner<>",
            """
            public class Outer<T> where T : class
            {
                public class Inner<U> where U : T
                {
                    public Inner(T seed) { }
                    public virtual T Get(U value) => value;
                }
            }
            """,
            """
            public static Outer<object>.Inner<string> Create()
            {
                var imposter = new InnerImposter<object, string>(new object());
                imposter.Get("value").UseBaseImplementation();
                return imposter.Instance();
            }
            """
        );
    }

    [Fact]
    public async Task GivenShadowedContainerTypeParameters_WhenImposterIsUsed_ShouldKeepTheirTypesDistinct()
    {
        await AssertCompiles(
            "Sample.Outer<>.Middle<>.Inner<>",
            """
            #pragma warning disable CS0693
            public class Outer<T> where T : struct
            {
                public interface IOuter { T GetOuter(); }
                public class Middle<T> where T : class
                {
                    public interface IMiddle { T GetMiddle(); }
                    public interface Inner<T> : IOuter, IMiddle where T : struct, System.IComparable<T>
                    {
                        T GetInner();
                    }
                }
            }
            """,
            """
            public static Outer<int>.Middle<string>.Inner<bool> Create()
            {
                var imposter = new InnerImposter<int, string, bool>();
                imposter.GetOuter().Returns(1);
                imposter.GetMiddle().Returns("middle");
                imposter.GetInner().Returns(true);
                return imposter.Instance();
            }
            """
        );
    }

    [Theory]
    [InlineData("var", "var_1")]
    [InlineData("_", "__1")]
    [InlineData("record", "record_1")]
    [InlineData("partial", "partial_1")]
    public async Task GivenRenamedContainerTypeParameter_WhenInnerParameterUsesItsGeneratedName_ShouldCompile(
        string outerName,
        string innerName
    )
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner<>",
            $$"""
            #pragma warning disable CS8860
            public class Outer<@{{outerName}}>
            {
                public interface Inner<{{innerName}}>
                {
                    @{{outerName}} Get({{innerName}} value);
                }
            }
            """,
            """
            public static Outer<int>.Inner<string> Create()
            {
                var imposter = new InnerImposter<int, string>();
                imposter.Get("value").Returns(1);
                return imposter.Instance();
            }
            """
        );
    }

    [Fact]
    public async Task GivenContainerParameterNamedLikeSetupView_WhenImposterIsGenerated_ShouldNameThemApart()
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner",
            """
            public class Outer<InnerSetup>
            {
                public interface Inner { InnerSetup Get(); }
            }
            """,
            "public static Outer<int>.Inner Create() => new InnerImposter<int>().Instance();"
        );
    }

    [Fact]
    public async Task GivenContainerParameterNamedLikeTargetMember_WhenImposterIsUsed_ShouldNameThemApart()
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner",
            """
            public class Outer<T>
            {
                public interface Inner
                {
                    T Get();
                    int T { get; }
                    int T_1 { get; }
                }
            }
            """,
            """
            public static Outer<string>.Inner Create()
            {
                var imposter = new InnerImposter<string>();
                imposter.Get().Returns("value");
                imposter.T.Getter().Returns(1);
                imposter.T_1.Getter().Returns(2);
                return imposter.Instance();
            }
            """
        );
    }

    [Fact]
    public async Task GivenContainerAndMethodParametersWithSameName_WhenGenericMethodIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner",
            """
            #pragma warning disable CS0693
            public class Outer<T>
            {
                public interface Inner { T Get<T>(T value); }
            }
            """,
            """
            public static Outer<int>.Inner Create()
            {
                var imposter = new InnerImposter<int>();
                imposter.Get<string>("value").Returns("result");
                return imposter.Instance();
            }
            """
        );
    }

    [Fact]
    public async Task GivenOpenAndClosedContainerRegistrations_WhenImpostersAreUsed_ShouldUseTheirRespectiveArities()
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner",
            "public class Outer<T> { public interface Inner { T Get(); } }",
            """
            public static Outer<int>.Inner CreateClosed() => new InnerImposter().Instance();
            public static Outer<string>.Inner CreateOpen() => new InnerImposter<string>().Instance();
            """,
            additionalRegistrations: "[assembly: GenerateImposter(typeof(Sample.Outer<int>.Inner))]"
        );
    }

    [Fact]
    public async Task GivenNestedTargetInDedicatedNamespace_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner",
            "public class Outer<T> { public interface Inner { T Get(); } }",
            """
            public static Outer<int>.Inner Create() =>
                new Imposters.Sample.Outer_T_.Inner.InnerImposter<int>().Instance();
            """,
            sameNamespace: false
        );
    }

    [Fact]
    public async Task GivenNonGenericIntermediateContainer_WhenImposterIsUsed_ShouldIncludeOuterParameter()
    {
        await AssertCompiles(
            "Sample.Outer<>.Middle.Inner",
            "public class Outer<T> { public class Middle { public interface Inner { T Get(); } } }",
            "public static Outer<int>.Middle.Inner Create() => new InnerImposter<int>().Instance();"
        );
    }

    [Fact]
    public async Task GivenNestedAndTopLevelTargetsWithSameTotalArity_WhenGeneratorRuns_ShouldReportIMP007()
    {
        var context = await GeneratorTestHelper.CreateContext(
            """
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.Outer<>.Inner))]
            [assembly: GenerateImposter(typeof(Sample.Inner<>))]
            namespace Sample
            {
                public class Outer<T> { public interface Inner { T Get(); } }
                public interface Inner<U> { U Get(); }
            }
            """,
            baseSourceFileName: "NestedGenericCollision.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(NestedGenericTargetTests)
        );

        var result = CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(context.Compilation)
            .GetRunResult();

        result
            .Diagnostics.Select(diagnostic => diagnostic.Id)
            .ShouldBe([
                DiagnosticDescriptors.ImposterTypeNameCollision.Id,
                DiagnosticDescriptors.ImposterTypeNameCollision.Id,
            ]);
        result.Diagnostics[0].GetMessage().ShouldContain("Sample.InnerImposter<T>");
        result.GeneratedTrees.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenUnregisteredNestedMemberMatchingTypeParameter_WhenDedicatedImposterIsUsed_ShouldKeepExistingNamespace()
    {
        await AssertCompiles(
            "Sample.IService<>",
            """
            public interface IService<T>
            {
                T Get();
                public interface Unregistered { int T { get; } }
            }
            """,
            """
            public static IService<int> Create() =>
                new Imposters.Sample.IService_T_.IServiceImposter<int>().Instance();
            """,
            sameNamespace: false
        );
    }

    [Theory]
    [InlineData("InnerImposter")]
    [InlineData("InnerImposter_1")]
    public async Task GivenContainingParameterMatchingImposterName_WhenImposterIsUsed_ShouldReserveGeneratedName(
        string otherParameter
    )
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner<>",
            $$"""
            #pragma warning disable CS0693
            public class Outer<InnerImposter>
            {
                public interface Inner<{{otherParameter}}>
                {
                    {{otherParameter}} Get();
                }
            }
            """,
            """
            public static Outer<int>.Inner<string> Create()
            {
                var imposter = new InnerImposter<int, string>();
                imposter.Get().Returns("value");
                return imposter.Instance();
            }
            """
        );
    }

    [Fact]
    public async Task GivenContainingParameterMatchingNonGenericTargetsImposterName_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner",
            "public class Outer<InnerImposter> { public interface Inner { InnerImposter Get(); } }",
            "public static Outer<int>.Inner Create() => new InnerImposter<int>().Instance();"
        );
    }

    [Theory]
    [InlineData("var", "var_1")]
    [InlineData("_", "__1")]
    [InlineData("record", "record_1")]
    [InlineData("partial", "partial_1")]
    public async Task GivenContextualKeywordParameterRenamedAgain_WhenDedicatedImposterIsUsed_ShouldKeepLegacyNamespace(
        string parameterName,
        string legacyName
    )
    {
        await AssertCompiles(
            "Sample.IService<>",
            $$"""
            #pragma warning disable CS8860
            public interface IService<@{{parameterName}}>
            {
                @{{parameterName}} Get();
                public interface Unregistered { int {{legacyName}} { get; } }
            }
            """,
            $$"""
            public static IService<int> Create() =>
                new Imposters.Sample.IService_{{legacyName}}_.IServiceImposter<int>().Instance();
            """,
            sameNamespace: false
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GivenUnregisteredNestedTypeAdded_WhenGeneratorRuns_ShouldKeepExistingHintName(
        bool sameNamespace
    )
    {
        var original = await GenerateWithNestedType("");
        var changed = await GenerateWithNestedType(
            "public interface Unregistered { int T { get; } }"
        );

        changed
            .GeneratedSources.ShouldHaveSingleItem()
            .HintName.ShouldBe(original.GeneratedSources.ShouldHaveSingleItem().HintName);

        async Task<GeneratorRunResult> GenerateWithNestedType(string nestedType)
        {
            var context = await GeneratorTestHelper.CreateContext(
                $$"""
                using Imposter.Abstractions;
                [assembly: GenerateImposter(typeof(Sample.IService<>), {{(
                    sameNamespace ? "true" : "false"
                )}})]
                namespace Sample
                {
                    public interface IService<T>
                    {
                        T Get();
                        {{nestedType}}
                    }
                }
                """,
                baseSourceFileName: "StableIdentity.cs",
                snippetFileName: "Snippet.cs",
                assemblyName: nameof(NestedGenericTargetTests)
            );

            return context.RunGenerator();
        }
    }

#if ROSLYN4_14_OR_GREATER
    [Fact]
    public async Task GivenNestedGenericTarget_WhenImposterExtensionIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Outer<>.Inner<>",
            "public class Outer<T> where T : class { public interface Inner<U> where U : T { U Get(); } }",
            "public static InnerImposter<object, string> Create() => Outer<object>.Inner<string>.Imposter();",
            LanguageVersion.Preview
        );
    }
#endif

    private static async Task AssertCompiles(
        string target,
        string declarations,
        string usage,
        LanguageVersion languageVersion = LanguageVersion.CSharp9,
        bool sameNamespace = true,
        string additionalRegistrations = ""
    )
    {
        var context = await GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof({{target}}), {{(sameNamespace ? "true" : "false")}})]
            {{additionalRegistrations}}
            namespace Sample
            {
                {{declarations}}
            }
            """,
            baseSourceFileName: "NestedGenericTarget.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(NestedGenericTargetTests),
            languageVersion: languageVersion
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
                        {{usage}}
                    }
                }
                """,
                DiagnosticSeverity.Warning
            )
            .ShouldBeEmpty();
    }
}
