using System.Globalization;

namespace Sample.Services;

public class PaymentGateway
{
    public virtual PaymentResult Charge(string orderId, decimal amount)
    {
        if (amount <= 0m)
        {
            return new PaymentResult(false, "Amount must be positive");
        }

        var message = $"Authorized {orderId} for {amount:F2}".ToString(CultureInfo.InvariantCulture);
        return new PaymentResult(true, message);
    }

    public virtual PaymentResult Refund(string orderId, decimal amount)
    {
        var message = $"Refunded {amount:F2} for {orderId}".ToString(CultureInfo.InvariantCulture);
        return new PaymentResult(true, message);
    }
}
