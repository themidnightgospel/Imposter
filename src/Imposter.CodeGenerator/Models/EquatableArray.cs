using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// An immutable array that compares by its items, so models holding one still compare by value.
/// </summary>
internal readonly struct EquatableArray<T> : IEquatable<EquatableArray<T>>, IReadOnlyList<T>
    where T : IEquatable<T>
{
    private readonly T[]? _items;

    internal EquatableArray(T[] items) => _items = items;

    private T[] Items => _items ?? [];

    public int Count => Items.Length;

    public T this[int index] => Items[index];

    public bool Equals(EquatableArray<T> other) => Items.SequenceEqual(other.Items);

    public override bool Equals(object? obj) => obj is EquatableArray<T> other && Equals(other);

    public override int GetHashCode() =>
        Items.Aggregate(17, (hash, item) => unchecked(hash * 31 + item.GetHashCode()));

    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)Items).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal static class EquatableArray
{
    internal static EquatableArray<T> ToEquatableArray<T>(this IEnumerable<T> items)
        where T : IEquatable<T> => new(items.ToArray());
}
