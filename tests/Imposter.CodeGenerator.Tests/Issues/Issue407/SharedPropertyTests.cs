using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue407;

public class SharedPropertyTests
{
    [Fact]
    public async Task GivenOrdinaryProperty_WhenGenerated_ShouldShareRuntimeMachineryAndKeepFluentTypes()
    {
        var context = await GeneratorTestHelper.CreateContext(
            """
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.ITarget))]
            namespace Sample { public interface ITarget { int Value { get; set; } } }
            """,
            "Target.cs",
            "Usage.cs",
            nameof(SharedPropertyTests)
        );
        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                """
                using Imposter.Abstractions;
                using Sample;
                public static class Usage
                {
                    public static void Run()
                    {
                        var imposter = new ITargetImposter();
                        ITargetImposter.IValuePropertyGetterBuilder getter = imposter.Value.Getter();
                        ITargetImposter.IValuePropertyGetterContinuationBuilder result = getter.Returns(1);
                        result.Then().Returns(() => 2).Then().Throws<System.Exception>().Callback(() => { });
                        ITargetImposter.IValuePropertySetterBuilder setter = imposter.Value.Setter(Arg<int>.Any());
                        setter.Callback(_ => { }).Then().Callback(_ => { });
                        setter.Called(Count.Never());
                        getter.Called(Count.Never());
                        imposter.Instance().Value = 1;
                        _ = imposter.Instance().Value;
                    }
                }
                """,
                DiagnosticSeverity.Warning
            )
        );
        var source = context.RunGenerator().GeneratedSources.Single();
        // The entire one-property file was 390 lines before the extraction (including its fluent API).
        source.SourceText.Lines.Count.ShouldBeLessThan(260);
        var classes = source
            .SyntaxTree.GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .ToArray();
        classes.ShouldNotContain(type => type.Identifier.Text == "DefaultPropertyBehaviour");
        var getter = classes.Single(type => type.Identifier.Text == "GetterImposterBuilder");
        getter
            .BaseList!.Types.First()
            .Type.ToString()
            .ShouldBe("global::Imposter.Abstractions.PropertyGetter<int>");
        getter.Members.OfType<FieldDeclarationSyntax>().ShouldBeEmpty();
        var setter = classes.Single(type => type.Identifier.Text == "SetterImposter");
        setter
            .BaseList!.Types.First()
            .Type.ToString()
            .ShouldBe("global::Imposter.Abstractions.PropertySetter<int>");
        setter.Members.OfType<FieldDeclarationSyntax>().ShouldBeEmpty();
    }
}
