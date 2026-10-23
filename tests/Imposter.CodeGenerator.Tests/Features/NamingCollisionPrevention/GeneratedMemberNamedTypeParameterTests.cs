using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

public class GeneratedMemberNamedTypeParameterTests
{
    [Theory]
    [InlineData("Invoke")]
    [InlineData("Matches")]
    [InlineData("As")]
    [InlineData("Arguments")]
    [InlineData("Returns")]
    [InlineData("Throws")]
    [InlineData("Callback")]
    [InlineData("Then")]
    [InlineData("ToString")]
    [InlineData("AddInvocationImposter")]
    [InlineData("Default")]
    [InlineData("Exception")]
    [InlineData("Result")]
    [InlineData("FormatValue")]
    [InlineData("HasMatchingInvocationImposterGroup")]
    [InlineData("Equals")]
    public async Task GivenMethodTypeParameterNamedLikeAGeneratedMember_WhenUsed_ShouldCompile(
        string name
    )
    {
        await AssertInterfaceCompiles(
            $"public interface IService {{ {name} Get<{name}>({name} value, int count); }}",
            "imposter.Get<string>(Arg<string>.Any(), Arg<int>.Any()).Returns(\"result\"); _ = imposter.Instance().Get(\"value\", 1);",
            $"{nameof(GivenMethodTypeParameterNamedLikeAGeneratedMember_WhenUsed_ShouldCompile)}_{name}"
        );
    }

    // A local can't share its name with a method type parameter (CS0412), so the locals of the generic method imposter
    // collection's AddNew and GetImposterWithMatchingInvocationImposterGroup avoid the type parameters' names.
    [Theory]
    [InlineData("imposter")]
    [InlineData("newest")]
    [InlineData("sameTypeImposter")]
    [InlineData("storedImposter")]
    [InlineData("typedImposter")]
    public async Task GivenMethodTypeParameterNamedLikeAnImposterCollectionLocal_WhenSetUpTwice_ShouldCompile(
        string name
    )
    {
        await AssertInterfaceCompiles(
            $"public interface IService {{ {name} Get<{name}>({name} value); }}",
            "imposter.Get<string>(Arg<string>.Any()).Returns(\"first\"); imposter.Get<string>(Arg<string>.Any()).Returns(\"second\"); _ = imposter.Instance().Get(\"value\");",
            $"{nameof(GivenMethodTypeParameterNamedLikeAnImposterCollectionLocal_WhenSetUpTwice_ShouldCompile)}_{name}"
        );
    }

    [Theory]
    [InlineData("ReturnsAsync")]
    [InlineData("ThrowsAsync")]
    [InlineData("UseBaseImplementation")]
    public async Task GivenAsyncClassMethodTypeParameterNamedLikeAGeneratedMember_WhenUsed_ShouldCompile(
        string name
    )
    {
        await AssertCompiles(
            "Sample.Service",
            $"public class Service {{ public virtual System.Threading.Tasks.Task<{name}> Get<{name}>({name} value) => System.Threading.Tasks.Task.FromResult(value); }}",
            "var imposter = new Sample.ServiceImposter(); imposter.Get<string>(Arg<string>.Any()).ReturnsAsync(\"result\").Then().UseBaseImplementation(); _ = imposter.Instance().Get(\"value\");",
            $"{nameof(GivenAsyncClassMethodTypeParameterNamedLikeAGeneratedMember_WhenUsed_ShouldCompile)}_{name}"
        );
    }

    [Fact]
    public async Task GivenNumberedHelperNamesUsedAsTypeParameters_WhenParametersAlsoCollide_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { As_1 Get<As_1, Matches_1, _invocationImposters_1>(As_1 As, Matches_1 Matches, _invocationImposters_1 _invocationImposters); }",
            "imposter.Get<string, int, bool>(Arg<string>.Any(), Arg<int>.Any(), Arg<bool>.Any()).Returns(\"result\"); _ = imposter.Instance().Get(\"value\", 1, true);",
            nameof(
                GivenNumberedHelperNamesUsedAsTypeParameters_WhenParametersAlsoCollide_ShouldCompile
            )
        );
    }

    [Fact]
    public async Task GivenReplacementNamesAlreadyUsed_WhenConstraintsAndInheritedSetupsAreUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            "public interface IBase<Invoke_1> { Invoke Get<Invoke, Invoke_2>(Invoke value, int Invoke_3) where Invoke : class, Invoke_1 where Invoke_2 : Invoke; } public interface IService<Invoke_1> : IBase<Invoke_1> { }",
            "var imposter = new Sample.IServiceImposter<object>(); imposter.For(default(IBase<object>)).Get<string, string>(Arg<string>.Any(), Arg<int>.Any()).Returns(\"result\"); _ = imposter.Instance().Get<string, string>(\"value\", 1);",
            nameof(
                GivenReplacementNamesAlreadyUsed_WhenConstraintsAndInheritedSetupsAreUsed_ShouldCompile
            )
        );
    }

    // Derive adversarial names from the output so a new private helper or fluent member cannot silently escape
    // the collision policy. Names allocated by MemberNames are covered too, including nested types and fields.
    [Theory]
    [InlineData("T", "return value;")]
    [InlineData(
        "System.Threading.Tasks.Task<T>",
        "return System.Threading.Tasks.Task.FromResult(value);"
    )]
    [InlineData("void", "")]
    public async Task GivenEveryGeneratedMemberNameAsAMethodTypeParameter_WhenGenerating_ShouldCompile(
        string returnType,
        string body
    )
    {
        string Source(string typeParameters) =>
            $$"""
                using Imposter.Abstractions;
                [assembly: GenerateImposter(typeof(Sample.Service))]
                namespace Sample
                {
                    public class Service
                    {
                        public virtual {{returnType}} Get<{{typeParameters}}>(T value, ref T other, out T result)
                        {
                            result = value;
                            {{body}}
                        }
                    }
                }
                """;

        var control = await GeneratorTestHelper.CreateContext(
            Source("T"),
            "Control.cs",
            "Snippet.cs",
            nameof(GeneratedMemberNamedTypeParameterTests)
        );
        var generated = control.RunGenerator().GeneratedSources.Single().SourceText;
        var names = CSharpSyntaxTree
            .ParseText(generated)
            .GetRoot()
            .DescendantNodes()
            .OfType<TypeDeclarationSyntax>()
            .Where(type =>
                type.AncestorsAndSelf()
                    .OfType<TypeDeclarationSyntax>()
                    .Any(parent => parent.TypeParameterList is not null)
            )
            .SelectMany(type => type.Members)
            .SelectMany(MemberNames)
            // These fields use the input parameter names, which C# already forbids as method type parameters.
            .Except(["value", "other", "result"])
            .Distinct()
            .OrderBy(name => name)
            .ToArray();
        names.ShouldContain("Invoke");
        names.ShouldContain("Then");
        var adversarial = await GeneratorTestHelper.CreateContext(
            Source(string.Join(", ", names.Prepend("T").Select(name => "@" + name))),
            "Adversarial.cs",
            "Snippet.cs",
            nameof(GeneratedMemberNamedTypeParameterTests)
        );
        GeneratorTestHelper.AssertNoDiagnostics(adversarial.CompileSnippet(string.Empty));
    }

    private static IEnumerable<string> MemberNames(MemberDeclarationSyntax member) =>
        member switch
        {
            MethodDeclarationSyntax { ExplicitInterfaceSpecifier: null } method =>
            [
                method.Identifier.ValueText,
            ],
            PropertyDeclarationSyntax { ExplicitInterfaceSpecifier: null } property =>
            [
                property.Identifier.ValueText,
            ],
            EventDeclarationSyntax { ExplicitInterfaceSpecifier: null } @event =>
            [
                @event.Identifier.ValueText,
            ],
            BaseFieldDeclarationSyntax field => field.Declaration.Variables.Select(variable =>
                variable.Identifier.ValueText
            ),
            BaseTypeDeclarationSyntax type => [type.Identifier.ValueText],
            DelegateDeclarationSyntax @delegate => [@delegate.Identifier.ValueText],
            _ => [],
        };
}
