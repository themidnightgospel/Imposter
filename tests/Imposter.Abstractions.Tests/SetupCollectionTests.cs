using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class SetupCollectionTests
{
    [Fact]
    public void GivenEmptyCollection_WhenRead_ShouldHaveNoItems()
    {
        var collection = new SetupCollection<string>();
        collection.TryPeek(out var item).ShouldBeFalse();
        item.ShouldBeNull();
        collection.GetEnumerator().MoveNext().ShouldBeFalse();
        default(SetupCollection<string>.Enumerator).MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void GivenItemsIncludingNull_WhenRead_ShouldKeepNewestFirstOrder()
    {
        var collection = new SetupCollection<string?>();
        collection.Push("first");
        collection.Push(null);
        collection.TryPeek(out var item).ShouldBeTrue();
        item.ShouldBeNull();
        collection.Push("last");
        collection.TryPeek(out item).ShouldBeTrue();
        item.ShouldBe("last");
        Items(collection).ShouldBe(new string?[] { "last", null, "first" });
        Items(collection).ShouldBe(new string?[] { "last", null, "first" });
    }

    [Fact]
    public async Task GivenEnumerator_WhenAnotherThreadAddsSetups_ShouldKeepItsOriginalSnapshot()
    {
        var collection = new SetupCollection<int>();
        collection.Push(1);
        var snapshot = collection.GetEnumerator();
        await Task.Run(() => collection.Push(2));
        snapshot.MoveNext().ShouldBeTrue();
        snapshot.Current.ShouldBe(1);
        snapshot.MoveNext().ShouldBeFalse();
        Items(collection).ShouldBe(new[] { 2, 1 });
    }

    [Fact]
    public void GivenConcurrentWriters_WhenAddingSetups_ShouldPublishEveryItemOnceInWriterOrder()
    {
        const int writers = 4;
        const int itemsPerWriter = 500;
        var collection = new SetupCollection<(int Writer, int Sequence)>();
        Parallel.For(
            0,
            writers,
            writer =>
            {
                for (var sequence = 0; sequence < itemsPerWriter; sequence++)
                {
                    collection.Push((writer, sequence));
                }
            }
        );

        var items = Items(collection);
        items.Count.ShouldBe(writers * itemsPerWriter);
        items.Distinct().Count().ShouldBe(items.Count);
        for (var writer = 0; writer < writers; writer++)
        {
            items
                .Where(item => item.Writer == writer)
                .Select(item => item.Sequence)
                .ShouldBe(Enumerable.Range(0, itemsPerWriter).Reverse());
        }
    }

    [Fact]
    public void GivenSetups_WhenRepeatedlyEnumeratingAndPeeking_ShouldNotAllocate()
    {
        var collection = new SetupCollection<int>();
        collection.Push(1);
        collection.Push(2);
        int Read()
        {
            var sum = 0;
            foreach (var item in collection)
            {
                sum += item;
            }

            collection.TryPeek(out var newest);
            return sum + newest;
        }

        for (var i = 0; i < 100; i++)
            _ = Read();
        var before = GC.GetAllocatedBytesForCurrentThread();
        var result = 0;
        for (var i = 0; i < 100; i++)
            result += Read();
        var allocated = GC.GetAllocatedBytesForCurrentThread() - before;
        result.ShouldBe(500);
        allocated.ShouldBe(0);
    }

    private static List<T> Items<T>(SetupCollection<T> collection)
    {
        var result = new List<T>();
        foreach (var item in collection)
            result.Add(item);
        return result;
    }
}
