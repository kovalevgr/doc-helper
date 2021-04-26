using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.Entities.DoctorAggregate;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Infrastructure.Persistence.Seeds.Doctors
{
    public class StatsSeed : IApplicationDbSeed
    {
        public int Priority { get; } = 4;

        public void Refresh(IApplicationDbContext context)
        {
            context.Stats.ToList().ForEach(e => context.Stats.Remove(e));

            context.SaveChanges();
        }

        public async Task SeedAsync(IApplicationDbContext context)
        {
            if (!await context.Stats.AnyAsync())
            {
                var doctors = ApplicationDbSeed.GetReferences<List<Doctor>>(DoctorsSeed.GetReferenceKeyName());

                foreach (var doctor in doctors)
                {
                    var stats = new Stats(9.2, 25, 1, 0, doctor);

                    await context.Stats.AddAsync(stats);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}