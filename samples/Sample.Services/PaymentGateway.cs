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

        string message = string.Create( CultureInfo.InvariantCulture, $"Authorized {orderId} for {amount:F2}");
        return new PaymentResult(true, message);
    }

    public virtual PaymentResult Refund(string orderId, decimal amount)
    {
        string message = string.Create( CultureInfo.InvariantCulture, $"Refunded {amount:F2} for {orderId}");
        return new PaymentResult(true, message);
    }
}
