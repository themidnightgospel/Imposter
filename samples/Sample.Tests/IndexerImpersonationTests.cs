using Imposter.Abstractions;
using Sample.Services;
using Shouldly;
using Xunit;

namespace Sample.Tests;

public class IndexerImpersonationTests
{
    [Fact]
    public void IndexerImpersonation_WhenConfigured_ShouldReturnInventoryRecord()
    {
        var imposter = new IInventoryLookupImposter();
        imposter["SKU-BOARD-GAME"].Getter().Returns(new InventoryRecord("SKU-BOARD-GAME", 5));

        var record = imposter.Instance()["SKU-BOARD-GAME"];

        record.ShouldNotBeNull();
        record!.AvailableQuantity.ShouldBe(5);
    }

    [Fact]
    public void IndexerImpersonation_WhenNoMatch_ShouldReturnDefault()
    {
        var imposter = new IInventoryLookupImposter();
        var lookup = imposter.Instance();

        lookup["UNKNOWN"].ShouldBeNull();
    }
}
