using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Models;

public class ImposterTargetModelTests
{
    private const string Source = /*lang=csharp*/
        """
        #nullable enable
        namespace Sample.Models
        {
            public interface IClock
            {
                int Now(int offset);
                string? Zone { get; set; }
                int this[string key] { get; }
                event System.EventHandler? Ticked;
            }

            public abstract class Service
            {
                protected Service(int seed) { }
                public virtual string Describe(int value) => value.ToString();
                public abstract int Count { get; }
            }
        }
        """;

    [Fact]
    public void GivenSameSourceInTwoCompilations_WhenInterfaceTargetModelsAreCreated_ShouldBeEqual()
    {
        var first = CreateTargetModel(Source, "Sample.Models.IClock");

        var second = CreateTargetModel(Source, "Sample.Models.IClock");

        second.ShouldBe(first);
    }

    [Fact]
    public void GivenSameSourceInTwoCompilations_WhenClassTargetModelsAreCreated_ShouldBeEqual()
    {
        var first = CreateTargetModel(Source, "Sample.Models.Service");

        var second = CreateTargetModel(Source, "Sample.Models.Service");

        second.ShouldBe(first);
    }

    [Fact]
    public void GivenUnrelatedTypeAdded_WhenTargetModelsAreCreated_ShouldBeEqual()
    {
        var original = CreateTargetModel(Source, "Sample.Models.IClock");

        var withUnrelatedType = CreateTargetModel(
            Source + "namespace Sample.Models { public class Unrelated { } }",
            "Sample.Models.IClock"
        );

        withUnrelatedType.ShouldBe(original);
    }

    [Fact]
    public void GivenChangedMember_WhenTargetModelsAreCreated_ShouldNotBeEqual()
    {
        var original = CreateTargetModel(Source, "Sample.Models.IClock");

        var changed = CreateTargetModel(
            Source.Replace("int Now(int offset);", "long Now(int offset);"),
            "Sample.Models.IClock"
        );

        changed.ShouldNotBe(original);
    }

    private static ImposterTargetModel CreateTargetModel(string source, string targetName)
    {
        var compilation = CSharpCompilation.Create(
            "TargetModels",
            [CSharpSyntaxTree.ParseText(source)],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
        );

        return ImposterTargetModel.From(
            compilation.GetTypeByMetadataName(targetName)!,
            new MemberAccess(compilation.Assembly)
        );
    }
}
