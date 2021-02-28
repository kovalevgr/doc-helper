using System.Threading;
using System.Threading.Tasks;
using DocHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Entities.City> Cities { get; set; }

        DbSet<Specialty> Specialties { get; set; }

        int SaveChanges();
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}