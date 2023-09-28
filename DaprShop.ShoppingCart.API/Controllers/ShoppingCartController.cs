using DaprShop.ShoppingCart.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaprShop.ShoppingCart.API.Controllers
{
    [ApiController]
    [Route("api/shopping-cart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<Domain.ShoppingCart>> Get(string userId)
        {
            Domain.ShoppingCart shoppingCart = await _shoppingCartService.GetShoppingCartAsync(userId);

            return Ok(shoppingCart);
        }

        [HttpPost("{userId}/items")]
        public async Task<ActionResult<Domain.ShoppingCart>> Post(string userId, Domain.ShoppingCartItem item)
        {
            try
            {
                await _shoppingCartService.AddItemToShoppingCartAsync(userId, item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}