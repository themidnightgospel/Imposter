using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IDiscountServiceSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public record InvoiceItem(string Title, int Quantity, decimal Fee);

    public record Product(string Title, decimal Fee);

    public record History(DateOnly date, int Quantity);

    public interface IDiscountServiceSut
    {
        decimal Calculate(
            IEnumerable<InvoiceItem>? invoicItems,
            IDictionary<Product, IEnumerable<History>>? purchaceHistory
        );
    }
}
