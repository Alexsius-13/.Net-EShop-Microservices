namespace Ordering.Domain.Models;

public class Product : Entity<Guid>
{
	public string Name { get; set; } = default!;
	public decimal Price { get; set; } = default!;
}
