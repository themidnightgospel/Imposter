#if USE_CSHARP14
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IAllowsRefStructSut))]
[assembly: GenerateImposter(typeof(AllowsRefStructClass))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public ref struct Gauge
    {
        public int Level;

        public Gauge(int level) => Level = level;
    }

    public interface IAllowsRefStructSut
    {
        int Measure<T>(T value, int scale)
            where T : allows ref struct;

        T Echo<T>(T value)
            where T : allows ref struct;

        T Create<T>()
            where T : allows ref struct;

        void Reset<T>(ref T value)
            where T : allows ref struct;
    }

    public class AllowsRefStructClass
    {
        public virtual int Measure<T>(T value, int scale)
            where T : allows ref struct => scale * 10;

        public virtual T Echo<T>(T value)
            where T : allows ref struct => value;
    }
}
#endif
