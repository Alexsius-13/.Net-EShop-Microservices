namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories, 
	string Description, string ImageUrl, decimal Price) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
		RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
		RuleFor(x => x.Categories).NotEmpty().WithMessage("Category is required");
		RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Image is required");
		RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greate than 0");
    }
}
internal class CreateProductHandler(IDocumentSession session)
	: ICommandHandler<CreateProductCommand, CreateProductResult>
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
