using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Entities.DoctorAggregate;

namespace DocHelper.Domain.Repository
{
    public interface IDoctorRepository : IAggregateRepository, IBaseRepository<Doctor>
    {
        Task<Doctor> CreateDoctorAsync(Doctor doctor,
            CancellationToken cancellationToken = default);

        Task<Stats> CreateStatAsync(Stats stats,
            CancellationToken cancellationToken = default);

        Task<Information> CreateInformationAsync(Information information,
            CancellationToken cancellationToken = default);
    }
}