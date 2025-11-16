using Imposter.Tests.Features.EventImposter;
using Imposter.Tests.Features.IndexerImposter;
using Imposter.Tests.Features.MethodImposter;
using Imposter.Tests.Features.PropertyImposter;

namespace Imposter.Tests.Features.PerformedInvocationExceptionMessage
{
    internal static class PerformedInvocationSutFactory
    {
        internal static IMethodSetupFeatureSutImposter CreateMethodSut()
        {
#if USE_CSHARP14
            return IMethodSetupFeatureSut.Imposter();
#else
            return new IMethodSetupFeatureSutImposter();
#endif
        }

        internal static IPropertySetupSutImposter CreatePropertySut()
        {
#if USE_CSHARP14
            return IPropertySetupSut.Imposter();
#else
            return new IPropertySetupSutImposter();
#endif
        }

        internal static IIndexerSetupSutImposter CreateIndexerSut()
        {
#if USE_CSHARP14
            return IIndexerSetupSut.Imposter();
#else
            return new IIndexerSetupSutImposter();
#endif
        }

        internal static IEventSetupSutImposter CreateEventSut()
        {
#if USE_CSHARP14
            return IEventSetupSut.Imposter();
#else
            return new IEventSetupSutImposter();
#endif
        }
    }
}
