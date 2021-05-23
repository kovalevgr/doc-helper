using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Entities.DoctorAggregate;

namespace DocHelper.Domain.Repository
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        Task<Stats> AddStatAsync(Stats entity, CancellationToken cancellationToken = default);

        Task<Information> AddInformationAsync(Information entity, CancellationToken cancellationToken = default);
    }
}