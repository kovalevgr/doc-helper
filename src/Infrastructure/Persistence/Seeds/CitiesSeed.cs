using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.Entities;

namespace DocHelper.Infrastructure.Persistence.Seeds
{
    public class CitiesSeed : IApplicationDbSeed
    {
        public void Refresh(IApplicationDbContext context)
        {
            context.Cities.ToList().ForEach(e => context.Cities.Remove(e));
            
            context.SaveChanges();
        }

        public async Task SeedAsync(IApplicationDbContext context)
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
            
            ApplicationDbSeed.SetReferences(GetReferenceKeyName(), references);
        }

        private string GetReferenceKeyName() => nameof(CitiesSeed);
    }
}