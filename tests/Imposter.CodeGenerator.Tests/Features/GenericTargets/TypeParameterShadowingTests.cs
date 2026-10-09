using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.GenericTargets;

// A generic method the imposter declares, such as Throws<TException>(), names its type parameters apart from the
// type parameters of the types that contain it, so none shadows a target's type parameter (CS0693). The shipped
// generator suppresses warnings in generated files, so this checks the declarations rather than the diagnostics.
public class TypeParameterShadowingTests
{
    [Fact]
    public async Task GivenTargetTypeParameterNamedTException_WhenGeneratorRuns_ShouldNotShadowIt()
    {
        var shadowing = await ShadowingTypeParameters(
            "Sample.IStore<>",
            "public interface IStore<TException> { TException Current { get; set; } TException this[int index] { get; set; } TException Get(); }"
        );

        shadowing.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenInheritedMembersOfTargetTypeParameterNamedTException_WhenGeneratorRuns_ShouldNotShadowIt()
    {
        var shadowing = await ShadowingTypeParameters(
            "Sample.IDerived<>",
            "public interface IBase<T> { T Current { get; } T this[int index] { get; } T Get(); } public interface IDerived<TException> : IBase<TException> { }"
        );

        shadowing.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenGenericMethodTypeParameterNamedTException_WhenGeneratorRuns_ShouldNotShadowIt()
    {
        var shadowing = await ShadowingTypeParameters(
            "Sample.IService",
            "public interface IService { TException Get<TException>(); }"
        );

        shadowing.ShouldBeEmpty();
    }

    // Each generic method in the generated code whose type parameter has the name of a containing type's.
    private static async Task<List<string>> ShadowingTypeParameters(
        string targetType,
        string targetDeclaration
    )
    {
        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof({{targetType}}))]

            namespace Sample
            {
                {{targetDeclaration}}
            }
            """,
            baseSourceFileName: "TypeParameterShadowing.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(TypeParameterShadowingTests)
        );

        return context
            .RunGenerator()
            .GeneratedSources.SelectMany(source =>
                CSharpSyntaxTree
                    .ParseText(source.SourceText)
                    .GetRoot()
                    .DescendantNodes()
                    .OfType<MethodDeclarationSyntax>()
            )
            .SelectMany(ShadowingTypeParameters)
            .ToList();
    }

    private static IEnumerable<string> ShadowingTypeParameters(MethodDeclarationSyntax method)
    {
        var containingTypeParameters = method
            .Ancestors()
            .OfType<TypeDeclarationSyntax>()
            .SelectMany(type => TypeParameterNames(type.TypeParameterList))
            .ToHashSet();

        return TypeParameterNames(method.TypeParameterList)
            .Where(containingTypeParameters.Contains)
            .Select(name => $"{method.Identifier.Text}<{name}>");
    }

    private static IEnumerable<string> TypeParameterNames(
        TypeParameterListSyntax? typeParameters
    ) => typeParameters?.Parameters.Select(parameter => parameter.Identifier.Text) ?? [];
}
