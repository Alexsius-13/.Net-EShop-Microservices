namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreatedOrderResult>
{
    public async Task<CreatedOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {

        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreatedOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName
            , orderDto.ShippingAddress.Email, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country
            , orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

        var billingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName
            , orderDto.ShippingAddress.Email, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country
            , orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

        var newOrder = Order.Create(
                id: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(orderDto.CustomerId),
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration
                , orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
            );

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
        }

        return newOrder;
    }
}
