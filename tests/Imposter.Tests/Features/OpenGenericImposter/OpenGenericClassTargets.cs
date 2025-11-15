using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Imposter.Tests.Features.OpenGenericImposter;

[assembly: GenerateImposter(typeof(OpenGenericClass<>))]
[assembly: GenerateImposter(typeof(OpenGenericPairClass<,>))]

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public class OpenGenericClass<T>
        where T : class
    {
        private readonly Dictionary<int, T> _items = new Dictionary<int, T>();
        private readonly List<(T Key, int Index, T Value)> _compositeEntries =
            new List<(T Key, int Index, T Value)>();
        private readonly EqualityComparer<T> _compositeComparer = EqualityComparer<T>.Default;
        private T _current = default!;

        public virtual T Current
        {
            get => _current;
            set
            {
                _current = value;
                ValueChanged?.Invoke(this, new GenericEventArgs<T>(value));
            }
        }

        public virtual T this[int index]
        {
            get => _items.TryGetValue(index, out var value) ? value : default!;
            set => _items[index] = value;
        }

        public virtual T this[T key, int index]
        {
            get
            {
                for (var i = 0; i < _compositeEntries.Count; i++)
                {
                    var entry = _compositeEntries[i];
                    if (_compositeComparer.Equals(entry.Key, key) && entry.Index == index)
                    {
                        return entry.Value;
                    }
                }

                return default!;
            }
            set
            {
                for (var i = 0; i < _compositeEntries.Count; i++)
                {
                    var entry = _compositeEntries[i];
                    if (_compositeComparer.Equals(entry.Key, key) && entry.Index == index)
                    {
                        _compositeEntries[i] = (key, index, value);
                        return;
                    }
                }

                _compositeEntries.Add((key, index, value));
            }
        }

        public virtual string Describe(T value) => $"base:{value}";

        public virtual T Echo(T value) => value;

        public virtual event EventHandler<GenericEventArgs<T>>? ValueChanged;
    }

    public class OpenGenericPairClass<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        private readonly Dictionary<TKey, TValue> _store = new Dictionary<TKey, TValue>();
        private KeyValuePair<TKey, TValue> _current = new KeyValuePair<TKey, TValue>(
            default!,
            default!
        );

        public virtual TValue this[TKey key]
        {
            get => _store.TryGetValue(key, out var value) ? value : default!;
            set => _store[key] = value;
        }

        public virtual KeyValuePair<TKey, TValue> CurrentPair
        {
            get => _current;
            set => _current = value;
        }

        public virtual TValue Resolve(TKey key, TValue defaultValue)
        {
            return _store.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public virtual string DescribePair(TKey key, TValue value) => $"{key}:{value}";
    }
}
