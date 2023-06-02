using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data
{
    public class SeedDataContext
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var ProductBrandsDate = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsDate);
                context.ProductBrands.AddRange(ProductBrands);
            }
            if (!context.ProductTypes.Any())
            {
                var ProductTypesDate = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesDate);
                context.ProductTypes.AddRange(ProductTypes);
            }
            if (!context.Products.Any())
            {
                var ProductsDate = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsDate);
                context.Products.AddRange(Products);
            }

            if (context.ChangeTracker.HasChanges())
                await context.SaveChangesAsync();
        }

    }
}