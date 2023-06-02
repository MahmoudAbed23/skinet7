using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _Repo;

        public ProductsController(IProductRepository Repo)
        {
            _Repo = Repo;

        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var Products = await _Repo.GetProductsAsync();

            return Products;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Product = await _Repo.GetProductByIdAsync(id);
            
            return  Product; 
        }
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductbrands()
        {
            var ProductBrands = await _Repo.GetProductBrandsAsync();
            return ProductBrands;

        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProducttypes()
        {
            var ProductTyps = await _Repo.GetProductTypesAsync();
            return ProductTyps;

        }

    }
}