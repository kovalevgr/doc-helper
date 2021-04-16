using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Entities.DoctorAggregate;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Entities.City> Cities { get; set; }
        DbSet<Domain.Entities.Specialty> Specialties { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Information> Informations { get; set; }
        DbSet<Stats> Stats { get; set; }

        int SaveChanges();
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}