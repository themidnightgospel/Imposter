using System.Collections.Generic;
using Imposter.CodeGenerator.Helpers;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Helpers;

public class SymbolNameContextTests
{
    [Fact]
    public void Get_NoConflicts_ReturnsOriginalName()
    {
        var context = new SymbolNameContext([]);

        var result = context.Use("name");

        result.ShouldBe("name");
    }

    [Fact]
    public void Get_CalledMultipleTimesForSameKey_ReturnsSameValueFromCache()
    {
        var context = new SymbolNameContext([]);

        var result1 = context.Use("name");
        var result2 = context.Use("name");

        result1.ShouldBe("name");
        result2.ShouldBeSameAs(result1);
    }

    [Fact]
    public void Get_WithInitialConflict_ReturnsSuffixedName()
    {
        var context = new SymbolNameContext(["name"]);

        var result = context.Use("name");

        result.ShouldBe("name_1");
    }

    [Fact]
    public void Get_WithMultipleInitialConflicts_ReturnsCorrectlySuffixedName()
    {
        var context = new SymbolNameContext(["name", "name_1"]);

        var result = context.Use("name");

        result.ShouldBe("name_2");
    }

    [Fact]
    public void Get_GeneratesUniqueNamesSequentially()
    {
        var context = new SymbolNameContext([]);

        var name1 = context.Use("name");
        var name2 = context.Use("name_1");
        var name3 = context.Use("name_2");

        name1.ShouldBe("name");
        name2.ShouldBe("name_1");
        name3.ShouldBe("name_2");
    }

    [Fact]
    public void Get_InChildScope_ParentNamesAreConsidered()
    {
        var parentContext = new SymbolNameContext([]);
        parentContext.Use("name");
        parentContext.Use("name_1");

        var childContext = parentContext.CreateScope(new List<string>());
        var result = childContext.Use("name");

        result.ShouldBe("name_2");
    }

    [Fact]
    public void Get_InChildScope_DoesNotAffectParentScope()
    {
        var parentContext = new SymbolNameContext([]);
        var childContext = parentContext.CreateScope(new List<string>());

        childContext.Use("name");
        var parentResult = parentContext.Use("name");

        parentResult.ShouldBe("name");
    }

    [Fact]
    public void Get_InChildScope_DoesNotAffectSiblingScope()
    {
        var parentContext = new SymbolNameContext([]);
        var child1Context = parentContext.CreateScope(new List<string>());
        var child2Context = parentContext.CreateScope(new List<string>());

        child1Context.Use("name");
        var child2Result = child2Context.Use("name");

        child2Result.ShouldBe("name");
    }

    [Fact]
    public void Get_DeeplyNestedScopes_NamesPropagateDownCorrectly()
    {
        var root = new SymbolNameContext([]);
        root.Use("name");

        var level1 = root.CreateScope(new List<string>());
        level1.Use("name_1");

        var level2 = level1.CreateScope(new List<string>());

        var resultForName = level2.Use("name");
        var resultForName1 = level2.Use("name_1");

        resultForName.ShouldBe("name_2");
        resultForName1.ShouldBe("name_1_1");
    }

    [Fact]
    public void Get_WithKeyThatIsAlreadySuffixed_GeneratesCorrectly()
    {
        var context = new SymbolNameContext(["name", "name_1"]);
        var result = context.Use("name_1");
        result.ShouldBe("name_1_1");
    }

    [Fact]
    public void Get_WithEmptyString_HandlesCorrectly()
    {
        var contextWithEmpty = new SymbolNameContext([""]);
        var contextWithEmptyAndSuffix = new SymbolNameContext(["", "_1"]);

        var result1 = contextWithEmpty.Use("");
        var result2 = contextWithEmpty.Use("");
        var result3 = contextWithEmptyAndSuffix.Use("");

        result1.ShouldBe("_1");
        result2.ShouldBe("_1");
        result3.ShouldBe("_2");
    }
}