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

            Query = spec.Includes.Aggregate(Query,(current,Include) => current.Include(Include));

            return Query;
        }
    }
}