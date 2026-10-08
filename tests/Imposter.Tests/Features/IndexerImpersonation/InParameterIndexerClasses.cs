using Imposter.Abstractions;
using Imposter.Tests.Features.IndexerImpersonation;

[assembly: GenerateImposter(typeof(InParameterIndexerClass))]
[assembly: GenerateImposter(typeof(InParameterGetterOnlyIndexerClass))]
[assembly: GenerateImposter(typeof(InParameterCopyNameCollisionIndexerClass))]

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class InParameterIndexerClass
    {
        public (int Key, int Value)? LastSet { get; private set; }

        public virtual int this[in int key]
        {
            get => key * 10;
            set => LastSet = (key, value);
        }
    }

    public class InParameterGetterOnlyIndexerClass
    {
        public virtual int this[in int key] => key + 1;
    }

    public class InParameterCopyNameCollisionIndexerClass
    {
        public (int Key, int KeyCopy, string Name, string Value)? LastSet { get; private set; }

        public virtual string this[in int key, in int keyCopy, string name]
        {
            get => $"{key}:{keyCopy}:{name}";
            set => LastSet = (key, keyCopy, name, value);
        }
    }
}
