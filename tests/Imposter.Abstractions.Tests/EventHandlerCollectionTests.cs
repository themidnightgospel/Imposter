using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Abstractions.Tests;

public class EventHandlerCollectionTests
{
    [Fact]
    public void GivenMulticastSubscriptions_WhenRemoving_ShouldRemoveTheLastMatchingSubsequence()
    {
        var collection = new EventHandlerCollection<Action>();
        var calls = new List<int>();
        Action first = () => calls.Add(1);
        Action second = () => calls.Add(2);
        collection.Combine(first + second);
        collection.Combine(first + second);
        collection.Combine(first);
        collection.Remove(first + second);

        foreach (var handler in collection)
            handler();

        calls.ShouldBe(new[] { 1, 2, 1 });
    }

    [Fact]
    public void GivenSnapshot_WhenSubscriptionsChange_ShouldKeepTheCapturedHandlers()
    {
        var collection = new EventHandlerCollection<Action>();
        default(EventHandlerCollection<Action>.Enumerator).MoveNext().ShouldBeFalse();
        collection.GetEnumerator().MoveNext().ShouldBeFalse();
        Action first = () => { };
        Action second = () => { };
        collection.Combine(first);
        var snapshot = collection.GetEnumerator();
        collection.Remove(first);
        collection.Combine(second);

        snapshot.MoveNext().ShouldBeTrue();
        snapshot.Current.ShouldBeSameAs(first);
        snapshot.MoveNext().ShouldBeFalse();
        var current = collection.GetEnumerator();
        current.MoveNext().ShouldBeTrue();
        current.Current.ShouldBeSameAs(second);
        current.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void GivenConcurrentSubscriptions_WhenPublished_ShouldKeepEveryHandler()
    {
        var collection = new EventHandlerCollection<Action>();
        var calls = new List<int>();
        var handlers = Enumerable.Range(0, 100).Select(i => (Action)(() => calls.Add(i))).ToArray();
        Parallel.ForEach(handlers, collection.Combine);
        foreach (var handler in collection)
            handler();
        calls.OrderBy(i => i).ShouldBe(Enumerable.Range(0, handlers.Length));

        Parallel.ForEach(handlers, collection.Remove);
        collection.GetEnumerator().MoveNext().ShouldBeFalse();
    }
}
