namespace Basket.API.Models;

public class ShoppingCart
{
    //Primary key (Unique Identifier)
    public string Username { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(string userName)
    {
        Username = userName;
    }

    //Required for Mapping
    public ShoppingCart()
    {
        
    }
}
