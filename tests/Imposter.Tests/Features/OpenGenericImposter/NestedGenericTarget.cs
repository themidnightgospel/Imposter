using Imposter.Abstractions;
using Imposter.Tests.Features.OpenGenericImposter.NestedTargets;

[assembly: GenerateImposter(typeof(Outer<>.Inner<>))]

namespace Imposter.Tests.Features.OpenGenericImposter.NestedTargets
{
    public class Outer<T>
        where T : class
    {
        public interface Inner<U>
            where U : T
        {
            U Get();
        }
    }

    public static class Usage
    {
        public static Outer<object>.Inner<string> Create()
        {
            var imposter = new InnerImposter<object, string>();
            imposter.Get().Returns("value");
            return imposter.Instance();
        }
    }
}
