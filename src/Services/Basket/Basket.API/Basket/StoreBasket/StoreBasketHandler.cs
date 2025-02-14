using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;


public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
		RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
		RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username can not be empty");
    }
}
public class StoreBasketHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto)
	: ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
	public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
	{
		//TODO: Get discount if exists from Discount.Grpc
		await DeductDiscount(command.Cart, cancellationToken);

		await repository.StoreBasket(command.Cart, cancellationToken);

		return new StoreBasketResult(command.Cart.Username);
	}

	private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
	{
		foreach (var item in cart.Items)
		{
			var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
			item.Price -= coupon.Amount;
		}
	}
}
