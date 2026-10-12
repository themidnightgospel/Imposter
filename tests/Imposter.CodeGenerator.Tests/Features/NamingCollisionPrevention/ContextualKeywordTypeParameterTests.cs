using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// Generated code declares its locals with var and its discards with _, and a member declaration that starts with record
// or partial reads as a record or, from C# 14, a partial member, so a type parameter of one of these names would change
// its meaning.
public class ContextualKeywordTypeParameterTests
{
    [Theory]
    [InlineData("var")]
    [InlineData("_")]
    [InlineData("record")]
    [InlineData("partial")]
    public async Task GivenTargetTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile(
        string name
    )
    {
        await AssertCompiles(
            "Sample.IRepo<>",
            $"public interface IRepo<@{name}> {{ @{name} Current {{ get; set; }} @{name} this[int index] {{ get; }} @{name} Get(@{name} value); System.Threading.Tasks.Task<@{name}> GetAsync(); event System.Action<@{name}> Changed; }}",
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
    public async Task GivenMethodTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile(
        string name
    )
    {
        await AssertInterfaceCompiles(
            $"public interface IService {{ @{name} Get<@{name}>(@{name} value, int count); void Put<@{name}>(ref @{name} item, out @{name} result); }}",
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
    public async Task GivenClassTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile(
        string name
    )
    {
        await AssertCompiles(
            "Sample.RepoBase<>",
            $"public class RepoBase<@{name}> {{ public virtual @{name} Current {{ get; set; }} = default!; public virtual @{name} Get(@{name} value) => value; public virtual T Convert<T>(@{name} value) => default!; }}",
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
    public async Task GivenClassMethodTypeParameterNamedLikeAContextualKeyword_WhenImposterIsUsed_ShouldCompile(
        string name
    )
    {
        await AssertCompiles(
            "Sample.ServiceBase",
            $"public class ServiceBase {{ public virtual @{name} Echo<@{name}>(@{name} value, int count) => value; }}",
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
}
