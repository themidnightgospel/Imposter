using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;

[assembly: GenerateImposter(typeof(ClassWithOverloadsAndGenerics))]

namespace Imposter.Tests.Features.ClassImposter.Suts
{
    public class ClassWithOverloadsAndGenerics
    {
        public virtual string Format(int value) => $"int:{value}";

        public virtual string Format(string value, int padding) => value.PadLeft(padding, '0');

        public virtual T Echo<T>(T item)
            where T : class => item;

        public virtual TFirst SelectFirst<TFirst, TSecond>(TFirst first, TSecond second) => first;

        public string FormatInt(int value) => Format(value);

        public string FormatString(string value, int padding) => Format(value, padding);

        public T EchoValue<T>(T item)
            where T : class => Echo(item);

        public TFirst SelectFirstValue<TFirst, TSecond>(TFirst first, TSecond second) =>
            SelectFirst(first, second);
    }
}
