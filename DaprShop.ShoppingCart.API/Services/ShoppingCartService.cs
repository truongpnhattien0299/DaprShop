using Dapr.Client;
using DaprShop.ShoppingCart.API.Domain;
using DaprShop.Contracts;
namespace DaprShop.ShoppingCart.API.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly DaprClient _daprClient;
        private static readonly string storeName = "statestore";
        public ShoppingCartService(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task AddItemToShoppingCartAsync(string userId, ShoppingCartItem item)
        {
            Domain.ShoppingCart shoppingCart = await GetShoppingCartAsync(userId);

            ShoppingCartItem? existingItem = shoppingCart.Items.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                shoppingCart.Items.Add(item);
            }

            await _daprClient.SaveStateAsync(storeName, userId, shoppingCart);
            var productItemAddedToShoppingCartEvent = new ProductItemAddedToShoppingCartEvent()
            {
                UserId = userId,
                ProductId = item.ProductId
            };

            const string pubsubName = "pubsub";
            const string topicNameOfShoppingCartItems = "daprshop.shoppingcart.items";

            await _daprClient.PublishEventAsync(pubsubName, topicNameOfShoppingCartItems, productItemAddedToShoppingCartEvent);
        }

        public async Task<Domain.ShoppingCart> GetShoppingCartAsync(string userId)
        {
            var shoppingCart = await _daprClient.GetStateAsync<Domain.ShoppingCart>(storeName, userId.ToString());
            if (shoppingCart == null)
            {
                shoppingCart = new Domain.ShoppingCart()
                {
                    UserId = userId
                };
            }

            return shoppingCart;
        }
    }
}