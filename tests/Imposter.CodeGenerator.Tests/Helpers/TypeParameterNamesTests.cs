using System;
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Helpers;

public class TypeParameterNamesTests
{
    [Theory]
    [InlineData("int")]
    [InlineData("dynamic")]
    [InlineData("T?")]
    [InlineData("@var")]
    [InlineData("@var?")]
    [InlineData("@var[,][]")]
    [InlineData("@var*")]
    [InlineData("Box<T[]>")]
    [InlineData("Box<@var[]>")]
    [InlineData("Outer<@var>.Inner<T>")]
    [InlineData("Outer<T>.Inner<@var>")]
    [InlineData("(T? Keep, @var Rename)")]
    [InlineData("delegate*<@var, int>")]
    [InlineData("delegate*<int, @var>")]
    public void GivenTypeShape_WhenFormatted_ShouldMatchDisplayPartSubstitution(string typeName)
    {
        var compilation = Compile(
            $$"""
            #nullable enable
            public class Box<T> { }
            public class Outer<T> { public class Inner<U> { } }
            public unsafe class Types<@var, T> where @var : unmanaged where T : class
            {
                public {{typeName}} Value;
            }
            """
        );
        compilation
            .GetDiagnostics()
            .Where(it => it.Severity == DiagnosticSeverity.Error)
            .ShouldBeEmpty();
        var type = (
            (IFieldSymbol)compilation.GetTypeByMetadataName("Types`2")!.GetMembers("Value").Single()
        ).Type;
        var format = SymbolDisplayFormat.FullyQualifiedFormat;
        foreach (
            var current in new[]
            {
                format,
                format.WithMiscellaneousOptions(
                    format.MiscellaneousOptions
                        | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
                ),
            }
        )
        {
            // The original formatter is the oracle: all display parts, even inside containing types and pointers.
            var expected = string.Concat(
                type.ToDisplayParts(current)
                    .Select(part =>
                        part.Symbol is ITypeParameterSymbol parameter
                        && parameter.NameInGeneratedCode() != parameter.Name
                            ? parameter.NameInGeneratedCode()
                            : part.ToString()
                    )
            );
            type.ToGeneratedCodeString(current).ShouldBe(expected);
        }
    }

    [Fact]
    public void GivenRepeatedParameterReferences_WhenLookingUpName_ShouldNotAllocate()
    {
        var parameter = Compile("public class Target<@var> { }")
            .GetTypeByMetadataName("Target`1")!
            .TypeParameters[0];
        parameter.NameInGeneratedCode().ShouldBe("var_1");
        for (var i = 0; i < 100; i++)
            _ = parameter.NameInGeneratedCode();

        var before = GC.GetAllocatedBytesForCurrentThread();
        for (var i = 0; i < 1000; i++)
            _ = parameter.NameInGeneratedCode();
        var allocated = GC.GetAllocatedBytesForCurrentThread() - before;
        allocated.ShouldBe(0);
    }

    [Fact]
    public void GivenChangedCollisionInAnotherCompilation_WhenNamesAreReadConcurrently_ShouldKeepEachSymbolsName()
    {
        var first = Compile("public class Target<@var> { }")
            .GetTypeByMetadataName("Target`1")!
            .TypeParameters[0];
        var changed = Compile("public class Target<@var> { public int var_1; }")
            .GetTypeByMetadataName("Target`1")!
            .TypeParameters[0];
        Parallel.For(
            0,
            100,
            _ =>
            {
                first.NameInGeneratedCode().ShouldBe("var_1");
                changed.NameInGeneratedCode().ShouldBe("var_2");
            }
        );
    }

    private static CSharpCompilation Compile(string source) =>
        CSharpCompilation.Create(
            "TypeParameterNames",
            [CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Preview))],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(
                    typeof(System.Runtime.CompilerServices.DynamicAttribute).Assembly.Location
                ),
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true)
        );
}
