using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(ITypeParameterOutsideSignatureSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface ITypeParameterOutsideSignatureSut
    {
        void Tag<T>(int value);

        string Name<T>();

        void Register<T>();

        void Put<T, TKey>(T value);

        void Relate<T, TDerived>(TDerived value)
            where TDerived : T;
    }
}
