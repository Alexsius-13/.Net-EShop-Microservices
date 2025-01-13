
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories,
	string Description, string ImageUrl, decimal Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
		RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
		RuleFor(x => x.Name).NotEmpty().Length(2, 150).WithMessage("Name is required" +
			"and should have minimum 2 and maximum 150 characters");
		RuleFor(x => x.Categories).NotEmpty().WithMessage("Category is required");
		RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Image is required");
		RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
	}
}
internal class UpdateProductHandler(IDocumentSession session) 
	: ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
	public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
	{
		var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

		if(product is null)
		{
			throw new ProductNotFoundException(command.Id);
		}

		product.Categories = command.Categories;
		product.Name = command.Name;
		product.Description = command.Description;
		product.ImageUrl = command.ImageUrl;
		product.Price = command.Price;

		session.Update(product);
		await session.SaveChangesAsync(cancellationToken);

		return new UpdateProductResult(true);
	}
}
