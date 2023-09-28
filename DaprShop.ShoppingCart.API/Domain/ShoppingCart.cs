namespace DaprShop.ShoppingCart.API.Domain
{
    public class ShoppingCart
    {
        public ShoppingCart() => Items = new List<ShoppingCartItem>();

        public string UserId { get; set; } = String.Empty;
        public List<ShoppingCartItem> Items { get; set; }
    }
}