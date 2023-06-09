using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Specifications
{
    public class ProductWithFiltersForCountSpecifications : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecifications(ProductSpecParams productSpecParams)
         : base(p =>
         (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search)) &&
         (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId) &&
         (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId))
        {
        }
    }
}