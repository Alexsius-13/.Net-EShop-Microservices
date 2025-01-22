﻿namespace Basket.API.Basket.StoreBasket;


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
public class StoreBasketHandler	: ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
	public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
	{
		ShoppingCart cart = command.Cart;

		return new StoreBasketResult("smn");
	}
}
