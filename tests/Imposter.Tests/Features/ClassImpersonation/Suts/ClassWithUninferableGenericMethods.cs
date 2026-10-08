using System.Collections.Generic;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;

[assembly: GenerateImposter(typeof(ClassWithUninferableGenericMethods))]

namespace Imposter.Tests.Features.ClassImpersonation.Suts
{
    // The type parameters can't be inferred from the parameters, so the base methods need explicit type arguments.
    public class ClassWithUninferableGenericMethods
    {
        public List<string> ExecutedTypes { get; } = new List<string>();

        public virtual string Describe<T>() => typeof(T).Name;

        public virtual string Convert<TInput, TResult>(TInput input) =>
            $"{input}->{typeof(TResult).Name}";

        public virtual void Execute<T>() => ExecutedTypes.Add(typeof(T).Name);
    }
}
