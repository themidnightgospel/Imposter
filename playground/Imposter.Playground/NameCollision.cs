using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Playground.Sample;

[assembly: GenerateImposter(typeof(IEventSut))]

namespace Imposter.Playground
{

    namespace Sample
    {
        public interface IEventSut
        {
            event EventHandler SomethingHappened;

            event Action ActionOnly;

            event Func<object?, EventArgs, Task>? AsyncSomethingHappened;
        }
    }

}