namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories, 
	string Description, string ImageUrl, decimal Price) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);
internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
	public async Task<CreateProductResult> Handle(CreateProductCommand command, 
		CancellationToken cancellationToken)
	{
		//create product entity via command object
		var product = new Product
		{
			Name = command.Name,
			Categories = command.Categories,
			Description = command.Description,
			ImageUrl = command.ImageUrl,
			Price = command.Price
		};

		//save to db
		session.Store(product);
		await session.SaveChangesAsync(cancellationToken);

		//return result
		return new CreateProductResult(Guid.NewGuid());
	}
}
