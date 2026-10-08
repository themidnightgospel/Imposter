using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation;

[assembly: GenerateImposter(typeof(ConstructorVirtualMembersTarget))]
[assembly: GenerateImposter(typeof(ConstructorAbstractMembersTarget))]
[assembly: GenerateImposter(typeof(ConstructorThrowingMemberTarget))]

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class ConstructorVirtualMembersTarget
    {
        private int _indexerValue;

        public ConstructorVirtualMembersTarget()
        {
            Value = 7;
            PropertyResult = Value;
            this[2] = 8;
            IndexerResult = this[2];
            Action handler = () => { };
            Changed += handler;
            Changed -= handler;
            Touch();
            var input = 3;
            MethodResult = Transform(ref input, out string output, "base");
            RefResult = input;
            OutResult = output;
            AsyncResult = ReadAsync().GetAwaiter().GetResult();
        }

        public int PropertyResult { get; }
        public int IndexerResult { get; }
        public int RefResult { get; }
        public string OutResult { get; }
        public string MethodResult { get; }
        public int AsyncResult { get; }
        public int TouchCount { get; private set; }
        public int AddCount { get; private set; }
        public int RemoveCount { get; private set; }

        public virtual int Value { get; set; }

        public virtual int this[int key]
        {
            get => _indexerValue + key;
            set => _indexerValue = value;
        }

        public virtual event Action Changed
        {
            add => AddCount++;
            remove => RemoveCount++;
        }

        public virtual void Touch() => TouchCount++;

        public virtual string Transform<T>(ref int input, out T output, T expected)
        {
            input++;
            output = expected;
            return "base method";
        }

        public virtual Task<int> ReadAsync() => Task.FromResult(Value + this[2]);
    }

    public abstract class ConstructorAbstractMembersTarget
    {
        protected ConstructorAbstractMembersTarget()
        {
            Value = "ignored";
            PropertyResult = Value;
            this[2] = "ignored";
            IndexerResult = this[2];
            Action handler = () => { };
            Changed += handler;
            Changed -= handler;
            Touch();
            var input = 3;
            MethodResult = Transform(ref input, out string output, "ignored");
            RefResult = input;
            OutResult = output;
            TaskResult = ReadAsync();
        }

        public string? PropertyResult { get; }
        public string? IndexerResult { get; }
        public int RefResult { get; }
        public string? OutResult { get; }
        public string? MethodResult { get; }
        public Task<int>? TaskResult { get; }

        public abstract string Value { get; set; }
        public abstract string this[int key] { get; set; }
        public abstract event Action Changed;
        public abstract void Touch();
        public abstract string Transform<T>(ref int input, out T output, T expected);
        public abstract Task<int> ReadAsync();
    }

    public class ConstructorThrowingMemberTarget
    {
        public ConstructorThrowingMemberTarget() => Run();

        public virtual void Run() =>
            throw new InvalidOperationException("base constructor dispatch");
    }
}
