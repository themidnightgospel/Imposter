namespace Sample.Services;

public class Order
{
    public Order(string id, params OrderLine[] lines)
    {
        Id = id;
        Lines = lines.ToList();
    }

    public string Id { get; }

    public IReadOnlyList<OrderLine> Lines { get; }

    public decimal Subtotal => Lines.Sum(line => line.UnitPrice * line.Quantity);
}
