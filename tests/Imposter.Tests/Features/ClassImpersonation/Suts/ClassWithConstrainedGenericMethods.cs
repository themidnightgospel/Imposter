using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;

[assembly: GenerateImposter(typeof(ClassWithConstrainedGenericMethods))]

namespace Imposter.Tests.Features.ClassImpersonation.Suts
{
    public class ClassWithConstrainedGenericMethods
    {
        public virtual T Echo<T>(T value)
            where T : IDisposable => value;

        public virtual string Describe<T>(T value)
            where T : new() => $"base:{typeof(T).Name}";
    }
}
