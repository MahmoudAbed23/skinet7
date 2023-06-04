using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Specifications;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _ProductRepo;
        private readonly IGenericRepository<ProductBrand> _ProductBrandRepo;
        private readonly IGenericRepository<ProductType> _ProductTypeRepo;
        private readonly IMapper _Mapper ;
        
        public ProductsController(IGenericRepository<Product> ProductRepo, IGenericRepository<ProductBrand> ProductBrandRepo
        , IGenericRepository<ProductType> ProductTypeRepo ,IMapper Mapper)
        {
            _Mapper = Mapper;
            _ProductRepo = ProductRepo;
            _ProductBrandRepo = ProductBrandRepo;
            _ProductTypeRepo = ProductTypeRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductOutput>>> GetProducts()
        {
            var spec = new ProductWithTypesAndBrandsSpecification();
            var Products = await _ProductRepo.ListAsync(spec);

            return Ok(_Mapper
            .Map<IReadOnlyList<Product>,IReadOnlyList<ProductOutput>>(Products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOutput>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            var Product = await _ProductRepo.GetEntityWithSpecification(spec);

            return _Mapper.Map<Product,ProductOutput>(Product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductbrands()
        {
            var ProductBrands = await _ProductBrandRepo.ListAllAsync();
            return Ok(ProductBrands);

        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProducttypes()
        {
            var ProductTyps = await _ProductTypeRepo.ListAllAsync();
            return Ok(ProductTyps);

        }

    }
}