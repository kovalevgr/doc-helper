using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Entities.DoctorAggregate;
using DocHelper.Domain.Repository;
using DocHelper.Infrastructure.Persistence;

namespace DocHelper.Infrastructure.Repository
{
    public class DoctorRepository : AggregateRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor,
            CancellationToken cancellationToken = default)
        {
            return await AddAsync(doctor, cancellationToken);
        }

        public async Task<Stats> CreateStatAsync(Stats stats,
            CancellationToken cancellationToken = default)
        {
            return await AddAsync(stats, cancellationToken);
        }

        public async Task<Information> CreateInformationAsync(
            Information information,
            CancellationToken cancellationToken = default)
        {
            return await AddAsync(information, cancellationToken);
        }
    }
}