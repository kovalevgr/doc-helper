using System.Linq;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.Entities;

namespace DocHelper.Application.Common.Specifications
{
    public class SpecBuilder<T> where T : BaseEntity
    {
        private readonly IApplicationDbContext _dbContext;
        private IQueryable<T> _queryable;
        
        public IQueryable<T> Queryable
        {
            get => _queryable;
            set => _queryable = _queryable is null ? value : _queryable.Concat(value);
        }

        public SpecBuilder(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Queryable = dbContext.Set<T>().AsQueryable();
        }

        public SpecBuilder<T> AddSpecification(ISpecification<T> spec)
        {
            ApplySpecification(spec);
            
            return this;
        }
        
        private void ApplySpecification(ISpecification<T> spec)
        {
            var evaluator = new SpecificationEvaluator<T>();
            var query = evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);

            Queryable = query;
        }
    }
}