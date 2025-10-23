using Imposter.CodeGenerator.Helpers;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Helpers;

public class SymbolNameNamespaceTests
{
    [Fact]
    public void Use_NoConflicts_ReturnsOriginalName()
    {
        var context = new SymbolNameNamespace([]);

        var result = context.Use("name");

        result.ShouldBe("name");
    }

    [Fact]
    public void Use_CalledMultipleTimesForSameKey_ReturnsDifferentValues()
    {
        var context = new SymbolNameNamespace([]);

        var result1 = context.Use("name");
        var result2 = context.Use("name");

        result1.ShouldBe("name");
        result2.ShouldBe("name_1");
    }

    [Fact]
    public void Use_WithInitialConflict_ReturnsSuffixedName()
    {
        var context = new SymbolNameNamespace(["name"]);

        var result = context.Use("name");

        result.ShouldBe("name_1");
    }

    [Fact]
    public void Use_WithMultipleInitialConflicts_ReturnsCorrectlySuffixedName()
    {
        var context = new SymbolNameNamespace(["name", "name_1"]);

        var result = context.Use("name");

        result.ShouldBe("name_2");
    }

    [Fact]
    public void Use_GeneratesUniqueNamesSequentially()
    {
        var context = new SymbolNameNamespace([]);

        var name1 = context.Use("name");
        var name2 = context.Use("name_1");
        var name3 = context.Use("name_2");

        name1.ShouldBe("name");
        name2.ShouldBe("name_1");
        name3.ShouldBe("name_2");
    }

    [Fact]
    public void Use_WithKeyThatIsAlreadySuffixed_GeneratesCorrectly()
    {
        var context = new SymbolNameNamespace(["name", "name_1"]);
        var result = context.Use("name_1");
        result.ShouldBe("name_1_1");
    }
}