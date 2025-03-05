namespace Ordering.Domain.Models;
public class Order : Aggregate<OrderId>
{
	private readonly List<OrderItem> _orderItems = new();
	private IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
	public CustomerId CustomerId { get; private set; } = default!;
	public OrderName OrderName { get; private set; } = default!;
	public Address ShoppingAddress { get; private set; } = default!;
	public Address BillingAddress { get; private set; } = default!;
	public Payment Payment { get; private set; } = default!;
	public OrderStatus Status { get; private set; } = OrderStatus.Pending;
	public decimal TotalPrice
	{
		get => OrderItems.Sum(o => o.Price * o.Quantity);
		private set { }
	}

	public static Order Create(OrderId id, CustomerId customerId, OrderName orderName,
		Address shoppingAddress, Address billingAddress,  Payment payment)
	{
		var order = new Order
		{
			Id = id,
			CustomerId = customerId,
			OrderName = orderName,
			ShoppingAddress = shoppingAddress,
			BillingAddress = billingAddress,
			Payment = payment,
			Status = OrderStatus.Pending
		};

		order.AddDomainEvent(new OrderCreatedEvent(order));

		return order;

	}
	
	public void UpdateOrder(OrderName orderName, Address shoppingAddress, Address billingAddres
		,Payment payment, OrderStatus status)
	{
		OrderName = orderName;
		ShoppingAddress = shoppingAddress;
		BillingAddress = billingAddres;
		Payment = payment;
		Status = status;

		AddDomainEvent(new OrderUpdatedEvent(this));
	}

	public void Remove(ProductId productId)
	{
		var order = _orderItems.FirstOrDefault(o => o.ProductId == productId);
		if(order is not null)
		{
			_orderItems.Remove(order);
		}
	}
}

