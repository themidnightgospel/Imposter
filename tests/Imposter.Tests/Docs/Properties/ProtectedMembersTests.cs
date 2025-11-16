using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Properties.MyService))]

namespace Imposter.Tests.Docs.Properties
{
    public class MyService
    {
        protected virtual int ProtectedAge { get; set; } = 7;

        public virtual int ReadProtected() => ProtectedAge;

        public virtual void WriteProtected(int value) => ProtectedAge = value;
    }
}
