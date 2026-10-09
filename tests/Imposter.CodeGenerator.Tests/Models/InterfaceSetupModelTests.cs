using System.Linq;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Models;

public class InterfaceSetupModelTests
{
    private const string Source = /*lang=csharp*/
        """
        public interface IBase
        {
            int Get(int key);
            event System.EventHandler Changed;
        }

        public interface IService : IBase
        {
            new int Get(int key);
            int Get(string key);
        }
        """;

    [Fact]
    public void GivenSameSourceInTwoCompilations_WhenTargetModelsAreCreated_ShouldBeEqual()
    {
        var first = InterfaceSetupTargetModel.From(GetType(Source, "IService"));

        var second = InterfaceSetupTargetModel.From(GetType(Source, "IService"));

        second.ShouldBe(first);
    }

    [Fact]
    public void GivenMethodWithInheritedSignature_WhenMemberModelIsCreated_ShouldHideInheritedMember()
    {
        var model = InterfaceSetupMemberModel.From(GetMethod("int"));

        model.HidesInheritedMember.ShouldBeTrue();
    }

    [Fact]
    public void GivenOverloadWithOtherArgumentType_WhenMemberModelIsCreated_ShouldNotHideInheritedMember()
    {
        var model = InterfaceSetupMemberModel.From(GetMethod("string"));

        model.HidesInheritedMember.ShouldBeFalse();
    }

    private static IMethodSymbol GetMethod(string keyType) =>
        GetType(Source, "IService")
            .GetMembers("Get")
            .OfType<IMethodSymbol>()
            .Single(method => method.Parameters[0].Type.ToDisplayString() == keyType);

    private static INamedTypeSymbol GetType(string source, string name) =>
        CSharpCompilation
            .Create(
                "InterfaceSetupModels",
                [CSharpSyntaxTree.ParseText(source)],
                [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
            )
            .GetTypeByMetadataName(name)!;
}
