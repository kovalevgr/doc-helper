using DocHelper.Domain.Common.Interfaces;
using DocHelper.Domain.Entities;

namespace DocHelper.Application.Common.Specifications
{
    public class SpecBuilderFactory
    {
        private readonly IApplicationDbContext _dbContext;

        public SpecBuilderFactory(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SpecBuilder<T> Create<T>() where T : BaseEntity
        {
            return new(_dbContext);
        }
    }
}