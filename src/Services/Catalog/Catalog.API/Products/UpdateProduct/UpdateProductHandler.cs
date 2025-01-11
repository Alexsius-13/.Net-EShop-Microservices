
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories,
	string Description, string ImageUrl, decimal Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);
internal class UpdateProductHandler(IDocumentSession session) 
	: ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
	public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
	{
		var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

		if(product is null)
		{
			throw new ProductNotFoundException();
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
