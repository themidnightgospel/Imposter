using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.OpenGenericImposter;

[assembly: GenerateImposter(typeof(IOpenGenericWithMethodGenerics<>))]
[assembly: GenerateImposter(typeof(IHaveGenericMethods))]
[assembly: GenerateImposter(typeof(IGenericConstraintsTarget))]

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public interface IOpenGenericWithMethodGenerics<T>
    {
        void DoSomething<TArg>(TArg arg);

        TArg Transform<TArg>(T input);

        TResult Map<TSource, TResult>(TSource source);
    }

    public interface IHaveGenericMethods
    {
        TResult GetValue<TResult>();

        void AddItem<TItem>(TItem item);

        Task<TItem> ProcessAsync<TItem>(IEnumerable<TItem> items);

        Task<TResult> ProcessComplexAsync<TItem, TResult>(IEnumerable<TItem> items);
    }

    public interface IGenericConstraintsTarget
    {
        void HandleReference<TArg>(TArg value)
            where TArg : class, new();

        TArg CloneStruct<TArg>(TArg value)
            where TArg : struct;

        void CompareValues<TArg>(TArg left, TArg right)
            where TArg : IComparable<TArg>;

        TArg CreateInstance<TArg>()
            where TArg : new();
    }

    public sealed class ReferencePayload
    {
        public string Value { get; set; } = string.Empty;
    }

    public readonly struct SampleStruct
    {
        public SampleStruct(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }

    public sealed class ComparablePayload : IComparable<ComparablePayload>
    {
        public ComparablePayload(int score)
        {
            Score = score;
        }

        public int Score { get; }

        public int CompareTo(ComparablePayload? other)
        {
            if (other is null)
            {
                return 1;
            }

            return Score.CompareTo(other.Score);
        }
    }
}
