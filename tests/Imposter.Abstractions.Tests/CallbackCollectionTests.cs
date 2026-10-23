using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class CallbackCollectionTests
{
    [Fact]
    public void GivenSnapshot_WhenAddingRegistrations_ShouldKeepOriginalOrderAndContents()
    {
        var collection = new CallbackCollection<string?>();
        collection.GetEnumerator().MoveNext().ShouldBeFalse();
        collection.Add("first");
        collection.Add(null);
        var snapshot = collection.GetEnumerator();
        collection.Add("last");

        snapshot.MoveNext().ShouldBeTrue();
        snapshot.Current.ShouldBe("first");
        snapshot.MoveNext().ShouldBeTrue();
        snapshot.Current.ShouldBeNull();
        snapshot.MoveNext().ShouldBeFalse();
        Items(collection).ShouldBe(new string?[] { "first", null, "last" });
    }

    [Fact]
    public void GivenConcurrentWriters_WhenRegistering_ShouldKeepEveryCallbackInWriterOrder()
    {
        const int writers = 4;
        const int registrations = 100;
        var collection = new CallbackCollection<(int Writer, int Sequence)>();
        Parallel.For(
            0,
            writers,
            writer =>
            {
                for (var sequence = 0; sequence < registrations; sequence++)
                    collection.Add((writer, sequence));
            }
        );

        var items = Items(collection);
        items.Count.ShouldBe(writers * registrations);
        items.Distinct().Count().ShouldBe(items.Count);
        for (var writer = 0; writer < writers; writer++)
            items
                .Where(item => item.Writer == writer)
                .Select(item => item.Sequence)
                .ShouldBe(Enumerable.Range(0, registrations));
    }

    private static List<T> Items<T>(CallbackCollection<T> collection)
    {
        var result = new List<T>();
        foreach (var item in collection)
            result.Add(item);
        return result;
    }
}
