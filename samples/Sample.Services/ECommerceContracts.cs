namespace Sample.Services;

public interface IPricingService
{
    decimal CalculateOrderTotal(Order order);

    decimal ApplyDiscount(string customerId, decimal subtotal);
}

public interface ICustomerSession
{
    string? CurrentCustomerId { get; set; }

    CustomerStatus Status { get; set; }
}

public interface IInventoryLookup
{
    InventoryRecord? this[string sku] { get; }
}

public interface IOrderNotifier
{
    event EventHandler<OrderStatusChangedEventArgs> OrderStatusChanged;
}
