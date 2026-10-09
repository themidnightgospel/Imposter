using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;

[assembly: GenerateImposter(typeof(NullableGenericMethodsClass))]

namespace Imposter.Tests.Features.ClassImpersonation.Suts
{
    public class NullableGenericMethodsClass
    {
        public virtual T EchoNullableReference<T>(T value)
            where T : class?, IDisposable? => value;

        public virtual T? EchoMaybeDefault<T>(T? value) => value;
    }
}
