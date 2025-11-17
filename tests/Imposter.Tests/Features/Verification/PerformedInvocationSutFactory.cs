using Imposter.Tests.Features.EventImpersonation;
using Imposter.Tests.Features.IndexerImpersonation;
using Imposter.Tests.Features.MethodImpersonation;
using Imposter.Tests.Features.PropertyImpersonation;

namespace Imposter.Tests.Features.Verification
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
