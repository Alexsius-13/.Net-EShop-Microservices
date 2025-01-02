
namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsHandler(IDocumentSession session, 
	ILogger<GetProductsResult> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
	public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
	{
		logger.LogInformation("Get Product Query Retrieved", query);

		var products = await session.Query<Product>().ToListAsync(cancellationToken);

		return new GetProductsResult(products);
	}
}
