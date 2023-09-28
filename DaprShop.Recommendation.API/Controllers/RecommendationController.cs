using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DaprShop.Contracts;
using Dapr;
namespace DaprShop.Recommendation.API.Controllers
{
    [ApiController]
    [Route("api/recommendations")]
    public class RecommendationController : ControllerBase
    {
        private const string PubsubName = "pubsub-kafka";
        private const string TopicNameOfShoppingCartItems = "daprshop.shoppingcart.items";

        [Topic(PubsubName, TopicNameOfShoppingCartItems)]
        [Route("products")]
        [HttpPost]
        public ActionResult AddProduct(ProductItemAddedToShoppingCartEvent productItemAddedToShoppingCartEvent)
        {
            Console.WriteLine($"New product has been added into shopping cart. Product Id: {productItemAddedToShoppingCartEvent.ProductId} User Id: {productItemAddedToShoppingCartEvent.UserId}");

            return Ok();
        }
    }
}