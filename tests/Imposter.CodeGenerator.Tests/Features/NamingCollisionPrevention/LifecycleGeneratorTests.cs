using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

public class LifecycleGeneratorTests
{
    [Fact]
    public async Task GivenLifecycleNamedTypeParameters_WhenGenerating_ShouldReserveOnlyBareMembers()
    {
        var context = await GeneratorTestHelper.CreateContext(
            """
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.IService<>))]
            namespace Sample
            {
                public interface IService<Reset>
                {
                    Reset Read<Clear>(Clear value);
                }
            }
            """,
            "LifecycleNames.cs",
            "Snippet.cs",
            nameof(LifecycleGeneratorTests)
        );
        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                """
                using Imposter.Abstractions;
                public class Usage
                {
                    public void Run()
                    {
                        var imposter = new Sample.IServiceImposter<int>();
                        imposter.Read<string>(Arg<string>.Any()).Returns(1);
                        imposter.ClearInvocations();
                        imposter.Reset();
                    }
                }
                """
            )
        );
        var root = context
            .RunGenerator()
            .GeneratedSources.ShouldHaveSingleItem()
            .SyntaxTree.GetRoot();
        var imposterType = root.DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Single(type => type.Identifier.ValueText == "IServiceImposter");
        imposterType
            .TypeParameterList.ShouldNotBeNull()
            .Parameters.ShouldHaveSingleItem()
            .Identifier.ValueText.ShouldBe("Reset");
    }

    [Theory]
    [InlineData("interface")]
    [InlineData("class")]
    public async Task GivenEmptyTarget_WhenGeneratingReset_ShouldEmitAnEmptyBody(string targetKind)
    {
        var context = await GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.Empty))]
            namespace Sample { public {{targetKind}} Empty { } }
            """,
            "EmptyLifecycle.cs",
            "Snippet.cs",
            nameof(LifecycleGeneratorTests)
        );
        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
        var root = context
            .RunGenerator()
            .GeneratedSources.ShouldHaveSingleItem()
            .SyntaxTree.GetRoot();
        root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Single(method =>
                method.Identifier.ValueText == "Reset"
                && method.ExplicitInterfaceSpecifier is not null
            )
            .Body.ShouldNotBeNull()
            .Statements.ShouldBeEmpty();
        root.DescendantNodes()
            .OfType<FieldDeclarationSyntax>()
            .SelectMany(field => field.Declaration.Variables)
            .Any(variable => variable.Identifier.ValueText.StartsWith("_imposterMode"))
            .ShouldBeFalse();
    }

    [Fact]
    public async Task GivenModeNamedConstructorParameter_WhenGenerating_ShouldInitializeReadonlyModeBeforeMembers()
    {
        var context = await GeneratorTestHelper.CreateContext(
            """
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.Service))]
            namespace Sample
            {
                public class Service
                {
                    public Service(int _imposterMode) { }
                    public virtual int Read() => 0;
                    public virtual int Value { get; set; }
                    public virtual int this[int key] { get => 0; set { } }
                }
            }
            """,
            "ModeLifecycle.cs",
            "Snippet.cs",
            nameof(LifecycleGeneratorTests)
        );
        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                """
                using Imposter.Abstractions;
                public class Usage
                {
                    public void Run()
                    {
                        var imposter = new Sample.ServiceImposter(1, ImposterMode.Explicit);
                        imposter.Reset();
                    }
                }
                """
            )
        );
        var root = context
            .RunGenerator()
            .GeneratedSources.ShouldHaveSingleItem()
            .SyntaxTree.GetRoot();
        var imposter = root.DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Single(type => type.Identifier.ValueText == "ServiceImposter");
        var mode = imposter
            .Members.OfType<FieldDeclarationSyntax>()
            .Single(field =>
                field.Declaration.Type.ToString() == "global::Imposter.Abstractions.ImposterMode"
            );
        mode.Modifiers.Any(token => token.IsKind(SyntaxKind.ReadOnlyKeyword)).ShouldBeTrue();
        var modeName = mode.Declaration.Variables.ShouldHaveSingleItem().Identifier.ValueText;
        foreach (
            var first in imposter
                .Members.OfType<ConstructorDeclarationSyntax>()
                .Select(constructor =>
                    constructor
                        .Body.ShouldNotBeNull()
                        .Statements[0]
                        .ShouldBeOfType<ExpressionStatementSyntax>()
                        .Expression.ShouldBeOfType<AssignmentExpressionSyntax>()
                )
        )
        {
            first.Left.ToString().ShouldBe("this." + modeName);
        }
        var reset = imposter
            .Members.OfType<MethodDeclarationSyntax>()
            .Single(method => method.Identifier.ValueText == "Reset");
        reset.DescendantNodes().OfType<LocalDeclarationStatementSyntax>().ShouldBeEmpty();
        reset
            .DescendantNodes()
            .OfType<AssignmentExpressionSyntax>()
            .Any(assignment => assignment.Left.ToString() == "this." + modeName)
            .ShouldBeFalse();
    }
}
