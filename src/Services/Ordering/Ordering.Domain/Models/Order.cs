namespace Ordering.Domain.Models;
public class Order : Aggregate<OrderId>
{
	//ID
	private readonly List<OrderItem> _orderItems = new();
	public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
	public CustomerId CustomerId { get; private set; } = default!;
	public OrderName OrderName { get; private set; } = default!;
	public Address ShippingAddress { get; private set; } = default!;
	public Address BillingAddress { get; private set; } = default!;
	public Payment Payment { get; private set; } = default!;
	public OrderStatus Status { get; private set; } = OrderStatus.Pending;
	public decimal TotalPrice
	{
		get => OrderItems.Sum(o => o.Price * o.Quantity);
		private set { }
	}

	public static Order Create(OrderId id, CustomerId customerId, OrderName orderName,
		Address shippingAddress, Address billingAddress,  Payment payment)
	{
		var order = new Order
		{
			Id = id,
			CustomerId = customerId,
			OrderName = orderName,
			ShippingAddress = shippingAddress,
			BillingAddress = billingAddress,
			Payment = payment,
			Status = OrderStatus.Pending
		};

		order.AddDomainEvent(new OrderCreatedEvent(order));

		return order;

	}

	public void Add(ProductId productId, int quantity, decimal price)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

		var orderItem = new OrderItem(Id, productId, quantity, price);
		_orderItems.Add(orderItem);
	}

	public void UpdateOrder(OrderName orderName, Address shippingAddress, Address billingAddres
		,Payment payment, OrderStatus status)
	{
		OrderName = orderName;
		ShippingAddress = shippingAddress;
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

