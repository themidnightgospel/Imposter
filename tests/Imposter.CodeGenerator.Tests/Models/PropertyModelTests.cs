using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Models;

public class PropertyModelTests
{
    private const string Source = /*lang=csharp*/
        """
        #nullable enable
        public abstract class Service
        {
            public abstract string? Name { get; protected set; }

            public virtual int this[string key, in int index] { get => index; set { } }
        }
        """;

    [Fact]
    public void GivenSameSourceInTwoCompilations_WhenPropertyModelsAreCreated_ShouldBeEqual()
    {
        var first = CreatePropertyModel(Source, "Name");

        var second = CreatePropertyModel(Source, "Name");

        second.ShouldBe(first);
    }

    [Fact]
    public void GivenSameSourceInTwoCompilations_WhenIndexerModelsAreCreated_ShouldBeEqual()
    {
        var first = CreatePropertyModel(Source, "this[]");

        var second = CreatePropertyModel(Source, "this[]");

        second.ShouldBe(first);
    }

    [Fact]
    public void GivenChangedAccessorAccessibility_WhenPropertyModelsAreCreated_ShouldNotBeEqual()
    {
        var original = CreatePropertyModel(Source, "Name");

        var changed = CreatePropertyModel(Source.Replace("protected set;", "set;"), "Name");

        changed.ShouldNotBe(original);
    }

    private static PropertyModel CreatePropertyModel(string source, string memberName)
    {
        var compilation = CSharpCompilation.Create(
            "PropertyModels",
            [CSharpSyntaxTree.ParseText(source)],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
        );
        var property = compilation
            .GetTypeByMetadataName("Service")!
            .GetMembers(memberName)
            .OfType<IPropertySymbol>()
            .Single();

        return PropertyModel.From(property, new MemberAccess(compilation.Assembly));
    }
}
