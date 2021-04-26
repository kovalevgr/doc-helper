using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Infrastructure.Persistence.Seeds
{
    public class CitiesSeed : IApplicationDbSeed
    {
        public int Priority { get; } = 1;

        public void Refresh(IApplicationDbContext context)
        {
            context.Cities.ToList().ForEach(e => context.Cities.Remove(e));
            
            context.SaveChanges();
        }

        public async Task SeedAsync(IApplicationDbContext context)
        {
            if (!await context.Cities.AnyAsync())
            {
                List<City> references = new List<City>();
            
                var cities = new List<City>
                {
                    new City {Name = "Kiev", Alias = "kiev"},
                    new City {Name = "Dnipro", Alias = "dnipro"},
                    new City {Name = "Lviv", Alias = "lviv"},
                };

                foreach (var city in cities)
                {
                    await context.Cities.AddAsync(city);
                
                    references.Add(city);
                }
            
                await context.SaveChangesAsync();

                ApplicationDbSeed.SetReferences(GetReferenceKeyName(), references);
            }
        }

        private string GetReferenceKeyName() => nameof(CitiesSeed);
    }
}