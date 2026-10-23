using System.Linq;
using Imposter.CodeGenerator.Helpers;
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
        var model = GetMethodSetup("int");

        model.HidesInheritedMember.ShouldBeTrue();
    }

    [Fact]
    public void GivenOverloadWithOtherArgumentType_WhenMemberModelIsCreated_ShouldNotHideInheritedMember()
    {
        var model = GetMethodSetup("string");

        model.HidesInheritedMember.ShouldBeFalse();
    }

    // A view finds the events its interface declares among the target's by equality.
    [Fact]
    public void GivenEvent_WhenModelledFromItsEventModelAndFromItsInterface_ShouldBeEqual()
    {
        var @event = GetType(Source, "IBase").GetMembers("Changed").OfType<IEventSymbol>().Single();
        var eventModel = EventModel.From(@event, new MemberAccess(@event.ContainingAssembly));

        InterfaceSetupMemberModel
            .From(@event, eventModel)
            .ShouldBe(InterfaceSetupMemberModel.From(@event));
    }

    private static InterfaceSetupMemberModel GetMethodSetup(string keyType)
    {
        var method = GetType(Source, "IService")
            .GetMembers("Get")
            .OfType<IMethodSymbol>()
            .Single(it => it.Parameters[0].Type.ToDisplayString() == keyType);
        var methodModel = MethodModel.From(
            method,
            new MemberAccess(method.ContainingAssembly),
            hasOverloadWithTheSameSetup: false
        );

        return InterfaceSetupMemberModel.From(method, methodModel);
    }

    private static INamedTypeSymbol GetType(string source, string name) =>
        CSharpCompilation
            .Create(
                "InterfaceSetupModels",
                [CSharpSyntaxTree.ParseText(source)],
                [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
            )
            .GetTypeByMetadataName(name)!;
}
