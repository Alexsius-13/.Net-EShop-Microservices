namespace Ordering.Domain.Models;
public class Order : Aggregate<Guid>
{
	private readonly List<OrderItem> _orderItems = new();
	private IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
	public Guid CustomerId { get; private set; } = default!;
	public string OrderName { get; private set; } = default!;
	public Address ShoppingAddress { get; private set; } = default!;
	public Address BillingAddress { get; private set; } = default!;
	public Payment Payment { get; private set; } = default!;
	public OrderStatus Status { get; private set; } = OrderStatus.Pending;
	public decimal TotalPrice
	{
		get => OrderItems.Sum(o => o.Price * o.Quantity);
		private set { }
	}
}

