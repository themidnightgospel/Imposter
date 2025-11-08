using System;
using Imposter.Abstractions;

namespace Imposter.Tests.Features.ClassImposter
{
    [GenerateImposter(typeof(ParameterizedCtorOnlyClass))]
    public class ParameterizedCtorOnlyClass
    {
        private string _name;

        protected ParameterizedCtorOnlyClass(int seed, string name)
        {
            Seed = seed;
            _name = name;
        }

        public int Seed { get; }

        public virtual string Name
        {
            get => _name;
            set => _name = value;
        }

        public virtual int Compute(int value) => Seed + value;

        public virtual int this[int index]
        {
            get => Seed + index;
            set => LastAssignedIndex = index;
        }

        public int LastAssignedIndex { get; private set; }

        public virtual event EventHandler? SomethingHappened;
    }
}
