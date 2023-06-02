using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int Id);
        Task<List<Product>> GetProductsAsync();
        Task<List<ProductType>> GetProductTypesAsync();
        Task<List<ProductBrand>> GetProductBrandsAsync();
    }
}