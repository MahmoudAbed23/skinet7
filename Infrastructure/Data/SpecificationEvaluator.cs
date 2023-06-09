using Core.Entities;
using Core.Entities.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> spec)
        {
            var Query = inputQuery;

            if (spec.Criteria != null)
            {
                Query = Query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                Query = Query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                Query = Query.OrderByDescending(spec.OrderByDescending);
            }

            if(spec.IsPagingEnabled)
            {
                Query = Query.Skip(spec.Skip).Take(spec.Take);
            }

            Query = spec.Includes.Aggregate(Query,(current,Include) => current.Include(Include));

            return Query;
        }
    }
}