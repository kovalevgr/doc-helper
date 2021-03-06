using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            if (!await context.Specialties.AnyAsync())
            {
                var cities = ApplicationDbSeed.GetReferences<List<City>>(nameof(CitiesSeed));

                foreach (City city in cities)
                {
                    var specialties = new List<Specialty>
                    {
                        new Specialty {Title = "Невролог", Alias = "nevrolog", City = city},
                        new Specialty {Title = "Отоларинголог", Alias = "otolaringolog-lor", City = city},
                        new Specialty {Title = "Психолог", Alias = "psiholog", City = city},
                        new Specialty {Title = "Трихолог", Alias = "triholog", City = city},
                        new Specialty {Title = "Косметолог", Alias = "kosmetolog", City = city},
                        new Specialty {Title = "Ортодонт", Alias = "ortodont", City = city}
                    };

                    foreach (var specialty in specialties)
                    {
                        await context.Specialties.AddAsync(specialty);
                    }
                }
                
                await context.SaveChangesAsync();
            }
        }
    }
}