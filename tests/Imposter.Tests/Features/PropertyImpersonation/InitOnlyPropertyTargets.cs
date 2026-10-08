using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;

[assembly: GenerateImposter(typeof(IInitOnlyPropertySut))]
[assembly: GenerateImposter(typeof(AbstractInitOnlyPropertySut))]
[assembly: GenerateImposter(typeof(VirtualInitOnlyPropertySut))]

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public interface IInitOnlyPropertySut
    {
        int Value { get; init; }
    }

    public abstract class AbstractInitOnlyPropertySut
    {
        public abstract int Value { get; init; }
    }

    public class VirtualInitOnlyPropertySut
    {
        private int _value = 10;

        public int BaseValue => _value;

        public int BaseInitCount { get; private set; }

        public virtual int Value
        {
            get => _value;
            init
            {
                _value = value;
                BaseInitCount++;
            }
        }

        public virtual int WriteOnly
        {
            init
            {
                _value = value;
                BaseInitCount++;
            }
        }

        public virtual int Throwing
        {
            init => throw new InvalidOperationException("Base init failed.");
        }
    }
}
