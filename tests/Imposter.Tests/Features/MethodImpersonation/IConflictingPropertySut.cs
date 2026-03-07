using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IConflictingPropertySut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface IHasStringValue
    {
        string Value { get; }
    }

    public interface IHasIntValue
    {
        int Value { get; }
    }

    public interface IConflictingPropertySut : IHasStringValue, IHasIntValue
    {
        string Name { get; }
    }
}
