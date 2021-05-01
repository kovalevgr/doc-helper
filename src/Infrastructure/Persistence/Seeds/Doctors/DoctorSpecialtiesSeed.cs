using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.Entities;
using DocHelper.Domain.Entities.DoctorAggregate;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Infrastructure.Persistence.Seeds.Doctors
{
    public class DoctorSpecialtiesSeed : IApplicationDbSeed
    {
        public int Priority { get; } = 4;

        public void Refresh(IApplicationDbContext context)
        {
            context.DoctorSpecialties.ToList().ForEach(e => context.DoctorSpecialties.Remove(e));

            context.SaveChanges();
        }

        public async Task SeedAsync(IApplicationDbContext context)
        {
            if (!await context.DoctorSpecialties.AnyAsync())
            {
                var doctors = ApplicationDbSeed.GetReferences<List<Doctor>>(DoctorsSeed.GetReferenceKeyName());
                var specialties = ApplicationDbSeed.GetReferences<List<Specialty>>(SpecialtiesSeed.GetReferenceKeyName());

                foreach (var doctor in doctors)
                {
                    foreach (var specialty in specialties)
                    {
                        await context.DoctorSpecialties.AddAsync(new DoctorSpecialty(doctor, specialty));
                    }
                }
                
                await context.SaveChangesAsync();
            }
        }
    }
}