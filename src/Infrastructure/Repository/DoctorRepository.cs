using DocHelper.Domain.Entities.DoctorAggregate;
using DocHelper.Domain.Repository;
using DocHelper.Infrastructure.Persistence;

namespace DocHelper.Infrastructure.Repository
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}