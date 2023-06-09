using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Entities.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams)
         : base(p =>
         (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search)) &&
         (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId) &&
         (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            Applypaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}