using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

public class GeneratedLocalNamedTypeParameterTests
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task GivenTypeParametersNamedAfterActualGeneratedLocals_WhenGenerating_ShouldCompile(
        bool methodGeneric
    )
    {
        var control = await GeneratorTestHelper.CreateContext(
            Source([], methodGeneric),
            "Control.cs",
            "Snippet.cs",
            nameof(GeneratedLocalNamedTypeParameterTests)
        );
        var names = control
            .RunGenerator()
            .GeneratedSources.SelectMany(source => source.SyntaxTree.GetRoot().DescendantNodes())
            .SelectMany(LocalNames)
            .Where(name => name.Length != 0)
            // C# forbids method type parameters sharing a name with a parameter of that source method.
            .Except(methodGeneric ? ["sourceInput", "sourceOther", "sourceOut", "sourceItems"] : [])
            .Distinct()
            .OrderBy(name => name)
            .ToArray();
        names.ShouldContain("snapshot");
        names.ShouldContain("cursor");
        names.ShouldContain("invocationException");
        var adversarial = await GeneratorTestHelper.CreateContext(
            Source(names, methodGeneric),
            "Adversarial.cs",
            "Snippet.cs",
            nameof(GeneratedLocalNamedTypeParameterTests)
        );
        GeneratorTestHelper.AssertNoDiagnostics(adversarial.CompileSnippet(string.Empty));
    }

    private static string Source(string[] names, bool methodGeneric)
    {
        var targetParameters = methodGeneric
            ? ""
            : "<" + string.Join(", ", names.Prepend("TTarget").Select(name => "@" + name)) + ">";
        var openTarget = methodGeneric ? "" : "<" + new string(',', names.Length) + ">";
        var methodParameters = string.Join(
            ", ",
            (methodGeneric ? names : []).Prepend("TMethod").Select(name => "@" + name)
        );
        return $$"""
            using Imposter.Abstractions;
            [assembly: GenerateImposter(typeof(Sample.Service{{openTarget}}))]
            namespace Sample
            {
                public class Service{{targetParameters}}
                {
                    public virtual System.Threading.Tasks.Task<TMethod> Read<{{methodParameters}}>(
                        TMethod sourceInput, ref TMethod sourceOther, out TMethod sourceOut,
                        System.ReadOnlySpan<TMethod> sourceItems)
                    {
                        sourceOut = sourceInput;
                        return System.Threading.Tasks.Task.FromResult(sourceInput);
                    }
                    public virtual int Number { get; set; }
                    public virtual int this[int key] { get => key; set { } }
                    public virtual event System.Action Changed { add { } remove { } }
                }
            }
            """;
    }

    private static IEnumerable<string> LocalNames(SyntaxNode node) =>
        node switch
        {
            LocalDeclarationStatementSyntax local => local.Declaration.Variables.Select(variable =>
                variable.Identifier.ValueText
            ),
            ForStatementSyntax { Declaration: { } declaration } => declaration.Variables.Select(
                variable => variable.Identifier.ValueText
            ),
            ForEachStatementSyntax loop => [loop.Identifier.ValueText],
            SingleVariableDesignationSyntax designation => [designation.Identifier.ValueText],
            CatchDeclarationSyntax caught => [caught.Identifier.ValueText],
            ParameterSyntax parameter => [parameter.Identifier.ValueText],
            _ => [],
        };
}
