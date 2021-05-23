using System.Threading;
using System.Threading.Tasks;
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

        public async Task<Stats> AddStatAsync(Stats entity, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<Stats>().AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<Information> AddInformationAsync(
            Information entity,
            CancellationToken cancellationToken = default)
        {
            await DbContext.Set<Information>().AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}