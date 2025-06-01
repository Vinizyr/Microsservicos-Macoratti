using Basket.API.Entities;
using Basket.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            if (basket == null)
            {
                return new ShoppingCart(userName);
            }
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            if (basket == null || string.IsNullOrEmpty(basket.UserName))
            {
                return BadRequest("Invalid basket data.");
            }
            var updatedBasket = await _repository.UpdateBasket(basket);
            return Ok(updatedBasket);
        }
        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            var result = await _repository.DeleteBasket(userName);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
