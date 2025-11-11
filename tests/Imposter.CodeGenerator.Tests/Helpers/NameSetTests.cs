using Imposter.CodeGenerator.Helpers;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Helpers;

public class NameSetTests
{
    [Fact]
    public void Given_NameSetHasNoConflicts_WhenUseIsCalled_ShouldReturnOriginalName()
    {
        var context = new NameSet([]);

        var result = context.Use("name");

        result.ShouldBe("name");
    }

    [Fact]
    public void Given_NameSetKeyUsedTwice_WhenUseIsCalled_ShouldReturnUniqueValues()
    {
        var context = new NameSet([]);

        var result1 = context.Use("name");
        var result2 = context.Use("name");

        result1.ShouldBe("name");
        result2.ShouldBe("name_1");
    }

    [Fact]
    public void Given_NameSetInitialConflict_WhenUseIsCalled_ShouldReturnSuffixedName()
    {
        var context = new NameSet(["name"]);

        var result = context.Use("name");

        result.ShouldBe("name_1");
    }

    [Fact]
    public void Given_NameSetHasMultipleConflicts_WhenUseIsCalled_ShouldIncreaseSuffix()
    {
        var context = new NameSet(["name", "name_1"]);

        var result = context.Use("name");

        result.ShouldBe("name_2");
    }

    [Fact]
    public void Given_NameSet_WhenGeneratingSequentialNames_ShouldProduceUniqueValues()
    {
        var context = new NameSet([]);

        var name1 = context.Use("name");
        var name2 = context.Use("name_1");
        var name3 = context.Use("name_2");

        name1.ShouldBe("name");
        name2.ShouldBe("name_1");
        name3.ShouldBe("name_2");
    }

    [Fact]
    public void Given_NameSetKeyAlreadySuffixed_WhenUseIsCalled_ShouldIncrementFromExistingSuffix()
    {
        var context = new NameSet(["name", "name_1"]);
        var result = context.Use("name_1");
        result.ShouldBe("name_1_1");
    }
}
