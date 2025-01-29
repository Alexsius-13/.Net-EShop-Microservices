namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
		RuleFor(x => x.UserName).NotEmpty().WithMessage("Username can not be empty");
    }
}
public class DeleteBasketHandler(IBasketRepository repository) 
	: ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
	public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
	{
		await repository.DeleteBasket(command.UserName);

		return new DeleteBasketResult(true);
	}
}
