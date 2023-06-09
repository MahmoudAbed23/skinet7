using System.Net;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Entities.Specifications;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
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
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductOutput>>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecifications(productParams);

            var totalItems = await _ProductRepo.CountAsync(countSpec);
            var Products = await _ProductRepo.ListAsync(spec);
            var data = _Mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductOutput>>(Products);

            return Ok(new Pagination<ProductOutput>(productParams.PageSize,productParams.PageIndex,totalItems,data));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductOutput>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            var Product = await _ProductRepo.GetEntityWithSpecification(spec);

            if(Product == null) return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));

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