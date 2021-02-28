using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.Entities;

namespace DocHelper.Infrastructure.Persistence.Seeds
{
    public class SpecialtiesSeed : IApplicationDbSeed
    {
        public void Refresh(IApplicationDbContext context)
        {
            context.Specialties.ToList().ForEach(e => context.Specialties.Remove(e));

            context.SaveChanges();
        }

        public async Task SeedAsync(IApplicationDbContext context)
        {
            var specialties = new List<Specialty>
            {
                new Specialty {Title = "Невролог", Alias = "nevrolog"},
                new Specialty {Title = "Отоларинголог", Alias = "otolaringolog-lor"},
                new Specialty {Title = "Психолог", Alias = "psiholog"},
                new Specialty {Title = "Трихолог", Alias = "triholog"},
                new Specialty {Title = "Косметолог", Alias = "kosmetolog"},
                new Specialty {Title = "Ортодонт", Alias = "ortodont"}
            };

            foreach (var specialty in specialties)
            {
                await context.Specialties.AddAsync(specialty);
            }
        }
    }
}