using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsservices.Entities;
using Microsservices.Repositories;

namespace Microsservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await productRepository.GetProductsAsync());
        }

        [HttpGet("id:length(24)", Name = "GetProduct")]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await productRepository.GetProduct(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            if (category == null)
            {
                return BadRequest("Categoria inválida!");
            }
            var products = await productRepository.GetProductByCategory(category);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Produto inválido!");
            }
            await productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            if(product == null)
            {
                return BadRequest("Produto inválido");
            }
            return Ok(await productRepository.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), 200)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await productRepository.DeleteProduct(id));
        }
    }
}
