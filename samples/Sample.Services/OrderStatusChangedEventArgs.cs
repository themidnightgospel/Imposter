namespace Sample.Services;

public class OrderStatusChangedEventArgs : EventArgs
{
    public OrderStatusChangedEventArgs(string orderId, string status)
    {
        OrderId = orderId;
        Status = status;
    }

    public string OrderId { get; }

    public string Status { get; }
}
