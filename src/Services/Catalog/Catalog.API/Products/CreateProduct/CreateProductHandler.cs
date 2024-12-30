using MediatR;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories, 
	string Description, string ImageUrl, decimal Price) : IRequest<CreateProductResult>;

public record CreateProductResult(Guid Id);
internal class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
	public Task<CreateProductResult> Handle(CreateProductCommand request, 
		CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
