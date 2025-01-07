namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product product);

public class GetProductByIdEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/{id}", async(Guid id, ISender sender) =>
		{
			var result = await sender.Send(new GetProductByIdQuery(id));

			var response = new Product
			{
				Id = result.product.Id,
				Name = result.product.Name,
				Categories = result.product.Categories,
				Description = result.product.Description,
				ImageUrl = result.product.ImageUrl,
				Price = result.product.Price,
			};

			return Results.Ok(response);
		})
		.WithName("Get Product By Id")
		.Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithDescription("Get product by id via mapster and mediatr")
		.WithSummary("Get Product By Id");
	}
}
