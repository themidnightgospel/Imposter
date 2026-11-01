using System;
using Imposter.CodeGenerator.Features.Imposter;
using Shouldly;
using Xunit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Tests.Issues.Issue331;

public class MemberTypeNamingTests
{
    private readonly MemberTypes _sut = new();

    [Fact]
    public void GivenFixedTypeName_WhenRegisteredForAnotherMember_ShouldRejectIt()
    {
        var exception = Should.Throw<InvalidOperationException>(() =>
            _sut.Add("Read", ClassDeclaration("FixedBuilder"))
        );

        exception.Message.ShouldContain("FixedBuilder");
        exception.Message.ShouldContain("Read");
        _sut.UniqueNamesOfClashingTypes(ClassDeclaration("Imposter")).ShouldBeEmpty();
    }

    [Fact]
    public void GivenClashingDelegate_WhenMemberIsRenamedWithoutRenamingItsType_ShouldRejectIt()
    {
        var delegateType = DelegateDeclaration(IdentifierName("int"), "ReadDelegate");
        _sut.Add("Read", delegateType);
        var imposter = ClassDeclaration("Imposter")
            .AddMembers(delegateType, PropertyDeclaration(IdentifierName("int"), "ReadDelegate"));

        _sut.UniqueNamesOfClashingTypes(imposter).ShouldBe(["Read"]);

        var nextBuild = new MemberTypes();
        var exception = Should.Throw<InvalidOperationException>(() =>
            nextBuild.Add("Read_1", delegateType)
        );

        exception.Message.ShouldContain("ReadDelegate");
        exception.Message.ShouldContain("Read_1");
    }

    [Fact]
    public void GivenClashingInterface_WhenMemberAndTypeAreRenamed_ShouldResolveTheClash()
    {
        var originalType = InterfaceDeclaration("IReadBuilder");
        var conflictingProperty = PropertyDeclaration(IdentifierName("int"), "IReadBuilder");
        _sut.Add("Read", originalType);
        _sut.UniqueNamesOfClashingTypes(
                ClassDeclaration("Imposter").AddMembers(originalType, conflictingProperty)
            )
            .ShouldBe(["Read"]);

        var nextBuild = new MemberTypes();
        var renamedType = InterfaceDeclaration("IRead_1Builder");
        nextBuild.Add("Read_1", renamedType);

        nextBuild
            .UniqueNamesOfClashingTypes(
                ClassDeclaration("Imposter").AddMembers(renamedType, conflictingProperty)
            )
            .ShouldBeEmpty();
    }
}
