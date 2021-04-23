using System.Linq;
using System.Threading;
using DocHelper.Domain.Dto;
using DocHelper.Domain.Entities.DoctorAggregate;

namespace DocHelper.Domain.Repository
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
    }
}