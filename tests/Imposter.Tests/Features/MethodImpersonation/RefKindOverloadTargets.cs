using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IRefKindOverloadSut))]
[assembly: GenerateImposter(typeof(IDerivedRefKindOverloadSut))]
[assembly: GenerateImposter(typeof(RefKindOverloadClass))]
[assembly: GenerateImposter(typeof(ISharedGetSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface IRefKindOverloadSut
    {
        int Count(int value);

        int Count(in int value);
    }

    public interface IBaseRefKindOverloadSut
    {
        int Count(int value);
    }

    public interface IDerivedRefKindOverloadSut : IBaseRefKindOverloadSut
    {
        int Count(in int value);
    }

    public interface IFirstGetSut
    {
        int Get();
    }

    public interface ISecondGetSut
    {
        int Get();
    }

    public interface IKeyedGetSut
    {
        int Get(int key);
    }

    public interface ISharedGetSut : IFirstGetSut, ISecondGetSut, IKeyedGetSut { }

    public class RefKindOverloadClass
    {
        public virtual int Count(int value) => 0;

        public virtual int Count(in int value) => 0;
    }
}
