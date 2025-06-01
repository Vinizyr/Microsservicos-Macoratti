using Discount.API.Entities;
using Discount.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _discountRepository.GetDiscount(productName);
            if (coupon == null)
            {
                return NotFound();
            }
            return Ok(coupon);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateDiscount([FromBody] Coupon coupon)
        {
            if (coupon == null)
            {
                return BadRequest();
            }
            var created = await _discountRepository.CreateDiscount(coupon);
            if (!created)
            {
                return BadRequest();
            }
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            if (coupon == null)
            {
                return BadRequest();
            }
            var updated = await _discountRepository.UpdateDiscount(coupon);
            if (!updated)
            {
                return BadRequest();
            }
            return NoContent();
        }
        [HttpDelete("{productName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteDiscount(string productName)
        {
            var deleted = await _discountRepository.DeleteDiscount(productName);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
