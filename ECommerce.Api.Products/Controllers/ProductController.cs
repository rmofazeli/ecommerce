using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var product = await productsProvider.GetProductAsync();
            if (product.IsSuccess)
            {
                return Ok(product.Products);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        { 
            var product = await productsProvider.GetProductAsync(id);
            if (product.IsSuccess)
            {
                return Ok(product.Product);
            }
            return NotFound();
        }
    }
}
