namespace Catalog.API.Models
{
	public class Product
	{
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
		public List<string> Categories { get; set; } = new();
		public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
