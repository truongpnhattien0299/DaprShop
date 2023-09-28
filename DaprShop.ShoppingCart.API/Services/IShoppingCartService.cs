namespace DaprShop.ShoppingCart.API.Services
{
    public interface IShoppingCartService
    {
        Task<Domain.ShoppingCart> GetShoppingCartAsync(string userId);
        Task AddItemToShoppingCartAsync(string userId, Domain.ShoppingCartItem item);
    }
}