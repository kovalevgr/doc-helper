using DocHelper.Domain.Entities;
using DocHelper.Domain.Repository;
using DocHelper.Infrastructure.Persistence;

namespace DocHelper.Infrastructure.Repository
{
    public class SpecialtyRepository : BaseRepository<Specialty>, ISpecialtyRepository
    {
        public SpecialtyRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}