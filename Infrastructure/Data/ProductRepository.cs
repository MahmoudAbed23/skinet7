using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _Context;
        public ProductRepository(StoreContext Context)
        {
            _Context = Context;       
        }

        public async Task<List<ProductBrand>> GetProductBrandsAsync()
        {
           return await _Context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            return await _Context.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _Context.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .ToListAsync();
        }

        public async Task<List<ProductType>> GetProductTypesAsync()
        {
            return await _Context.ProductTypes.ToListAsync();
        }
    }
}