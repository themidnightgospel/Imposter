using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// Generated code declares its locals with var and its discards with _, and a member declaration that starts with record
// or partial reads as a record or, from C# 14, a partial member, so a type parameter of one of these names would change
// its meaning. From C# 11 a declaration that starts with file or required reads as that modifier and scoped names no
// type, from C# 14 one that starts with extension reads as an extension block, and an async lambda can't name await.
// closed and union are the C# 15 preview's, which these tests' compiler doesn't know yet.
public class ContextualKeywordTypeParameterTests
{
    [Theory]
    [InlineData("var")]
    [InlineData("_")]
    [InlineData("record")]
    [InlineData("partial")]
    [InlineData("file")]
    [InlineData("required")]
    [InlineData("scoped")]
    [InlineData("extension")]
    [InlineData("await")]
    [InlineData("closed")]
    [InlineData("union")]
    public async Task GivenTargetTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile(
        string name
    )
    {
        await AssertCompiles(
            "Sample.IRepo<>",
            $"public interface IRepo<@{name}> {{ @{name} Current {{ get; set; }} @{name} this[int index] {{ get; }} @{name} Get(@{name} value); System.Threading.Tasks.Task<@{name}> GetAsync(@{name} value); event System.Action<@{name}> Changed; }}",
            "var imposter = new Sample.IRepoImposter<string>(); imposter.Get(Arg<string>.Any()).Returns(\"a\"); imposter.Current.Getter().Returns(\"b\"); var repo = imposter.Instance(); _ = repo.Get(\"c\") + repo.Current;",
            $"{nameof(GivenTargetTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile)}_{name}",
            LanguageVersion.Preview
        );
    }

    [Theory]
    [InlineData("var")]
    [InlineData("_")]
    [InlineData("record")]
    [InlineData("partial")]
    [InlineData("file")]
    [InlineData("required")]
    [InlineData("scoped")]
    [InlineData("extension")]
    [InlineData("await")]
    [InlineData("closed")]
    [InlineData("union")]
    public async Task GivenMethodTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile(
        string name
    )
    {
        await AssertInterfaceCompiles(
            $"public interface IService {{ @{name} Get<@{name}>(@{name} value, int count); void Put<@{name}>(ref @{name} item, out @{name} result); System.Threading.Tasks.Task<@{name}> GetAsync<@{name}>(@{name} value); }}",
            "imposter.Get<string>(Arg<string>.Any(), Arg<int>.Any()).Returns(\"a\"); _ = imposter.Instance().Get(\"b\", 0);",
            $"{nameof(GivenMethodTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile)}_{name}",
            LanguageVersion.Preview
        );
    }

    [Theory]
    [InlineData("var")]
    [InlineData("_")]
    [InlineData("record")]
    [InlineData("partial")]
    [InlineData("file")]
    [InlineData("required")]
    [InlineData("scoped")]
    [InlineData("extension")]
    [InlineData("await")]
    [InlineData("closed")]
    [InlineData("union")]
    public async Task GivenClassTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile(
        string name
    )
    {
        await AssertCompiles(
            "Sample.RepoBase<>",
            $"public class RepoBase<@{name}> {{ public virtual @{name} Current {{ get; set; }} = default!; public virtual @{name} Get(@{name} value) => value; public virtual T Convert<T>(@{name} value) => default!; public virtual System.Threading.Tasks.Task<@{name}> GetAsync(@{name} value) => System.Threading.Tasks.Task.FromResult(value); }}",
            "var imposter = new Sample.RepoBaseImposter<string>(); imposter.Get(Arg<string>.Any()).Returns(\"a\"); _ = imposter.Instance().Get(\"b\");",
            $"{nameof(GivenClassTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile)}_{name}",
            LanguageVersion.Preview
        );
    }

    [Theory]
    [InlineData("var")]
    [InlineData("_")]
    [InlineData("record")]
    [InlineData("partial")]
    [InlineData("file")]
    [InlineData("required")]
    [InlineData("scoped")]
    [InlineData("extension")]
    [InlineData("await")]
    [InlineData("closed")]
    [InlineData("union")]
    public async Task GivenClassMethodTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile(
        string name
    )
    {
        await AssertCompiles(
            "Sample.ServiceBase",
            $"public class ServiceBase {{ public virtual @{name} Echo<@{name}>(@{name} value, int count) => value; public virtual System.Threading.Tasks.Task<@{name}> EchoAsync<@{name}>(@{name} value) => System.Threading.Tasks.Task.FromResult(value); }}",
            "var imposter = new Sample.ServiceBaseImposter(); imposter.Echo<string>(Arg<string>.Any(), Arg<int>.Any()).Returns(\"a\"); _ = imposter.Instance().Echo(\"b\", 0);",
            $"{nameof(GivenClassMethodTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile)}_{name}",
            LanguageVersion.Preview
        );
    }

    // The new name also stays clear of the names declared beside the type parameter: a method's parameters, and the
    // target's members and their parameters, such as its constructor's, which the imposter's constructor repeats.
    [Fact]
    public async Task GivenMethodTypeParameterNamedVarBesideAParameterNamedVar_1_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { @var Get<@var>(@var value, int var_1); }",
            "imposter.Get<string>(Arg<string>.Any(), Arg<int>.Any()).Returns(\"a\"); _ = imposter.Instance().Get(\"b\", 0);",
            nameof(
                GivenMethodTypeParameterNamedVarBesideAParameterNamedVar_1_WhenImposterIsUsed_ShouldCompile
            ),
            LanguageVersion.Preview
        );
    }

    [Fact]
    public async Task GivenTargetTypeParameterNamedVarBesideAMemberNamedVar_1_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IRepo<>",
            "public interface IRepo<@var> { @var Get(); int var_1(); }",
            "var imposter = new Sample.IRepoImposter<string>(); imposter.var_1().Returns(1); _ = imposter.Instance().var_1();",
            nameof(
                GivenTargetTypeParameterNamedVarBesideAMemberNamedVar_1_WhenImposterIsUsed_ShouldCompile
            ),
            LanguageVersion.Preview
        );
    }

    [Fact]
    public async Task GivenClassTypeParameterNamedVarBesideAConstructorParameterNamedVar_1_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.RepoBase<>",
            "public class RepoBase<@var> { public RepoBase(@var var_1) { } public virtual @var Get(@var value) => value; }",
            "var imposter = new Sample.RepoBaseImposter<string>(\"a\"); imposter.Get(Arg<string>.Any()).Returns(\"b\"); _ = imposter.Instance().Get(\"c\");",
            nameof(
                GivenClassTypeParameterNamedVarBesideAConstructorParameterNamedVar_1_WhenImposterIsUsed_ShouldCompile
            ),
            LanguageVersion.Preview
        );
    }

    // Generated code renames a type parameter named like any contextual keyword its compiler knows, so a keyword that a
    // later compiler adds is covered as soon as these tests run on that compiler.
    [Fact]
    public async Task GivenTypeParametersNamedLikeEveryContextualKeyword_WhenImpostersAreGenerated_ShouldCompile()
    {
        var keywords = SyntaxFacts
            .GetContextualKeywordKinds()
            .Select(SyntaxFacts.GetText)
            .ToArray();
        var registrations = keywords.Select(
            (_, index) =>
                $"[assembly: GenerateImposter(typeof(Sample.IService{index}<>))] [assembly: GenerateImposter(typeof(Sample.IOther{index}))]"
        );
        var declarations = keywords.Select(
            (name, index) =>
                $"public interface IService{index}<@{name}> {{ @{name} Get(@{name} value); System.Threading.Tasks.Task<@{name}> GetAsync(@{name} value); @{name} Current {{ get; set; }} @{name} this[@{name} key] {{ get; }} event System.Action<@{name}> Changed; }} "
                + $"public interface IOther{index} {{ @{name} Get<@{name}>(@{name} value, ref @{name} item); System.Threading.Tasks.Task<@{name}> GetAsync<@{name}>(@{name} value); }}"
        );
        var context = await GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;

            {{string.Join("\n", registrations)}}

            namespace Sample
            {
                {{string.Join("\n", declarations)}}
            }
            """,
            baseSourceFileName: "EveryContextualKeyword.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(
                GivenTypeParametersNamedLikeEveryContextualKeyword_WhenImpostersAreGenerated_ShouldCompile
            ),
            LanguageVersion.Preview
        );

        keywords.ShouldContain("record");
        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet("public static class Usage { }")
        );
    }

    // Before these names became keywords their imposters compiled, in a dedicated namespace named after them.
    [Theory]
    [InlineData("file")]
    [InlineData("required")]
    [InlineData("scoped")]
    [InlineData("extension")]
    [InlineData("closed")]
    [InlineData("union")]
    public async Task GivenTypeParameterNamedLikeALaterContextualKeyword_WhenDedicatedImposterIsUsed_ShouldKeepItsNamespace(
        string name
    )
    {
        var assemblyName =
            $"{nameof(GivenTypeParameterNamedLikeALaterContextualKeyword_WhenDedicatedImposterIsUsed_ShouldKeepItsNamespace)}_{name}";
        var context = await GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IService<>), false)]

            namespace Sample
            {
                public interface IService<@{{name}}> { @{{name}} Get(); }
            }
            """,
            baseSourceFileName: $"{assemblyName}.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: assemblyName,
            LanguageVersion.Preview
        );

        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                $$"""
                using Imposter.Abstractions;

                public static class Usage
                {
                    public static Sample.IService<int> Create() =>
                        new Imposters.Sample.IService_{{name}}_.IServiceImposter<int>().Instance();
                }
                """
            )
        );
    }
}
