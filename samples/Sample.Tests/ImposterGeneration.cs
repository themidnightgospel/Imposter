using Imposter.Abstractions;
using Sample.Services;

[assembly: GenerateImposter(typeof(IPricingService))]
[assembly: GenerateImposter(typeof(ICustomerSession))]
[assembly: GenerateImposter(typeof(IInventoryLookup))]
[assembly: GenerateImposter(typeof(IOrderNotifier))]
[assembly: GenerateImposter(typeof(PaymentGateway))]
